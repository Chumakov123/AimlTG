using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace NeuralNetwork1
{
    class TLGBotik
    {
        public Telegram.Bot.TelegramBotClient botik = null;

        private UpdateTLGMessages formUpdater;

        private BaseNetwork perseptron = null;
        // CancellationToken - инструмент для отмены задач, запущенных в отдельном потоке
        public string Username { get; private set; }
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private AIMLBotik aiml;
        public TLGBotik(BaseNetwork net,  UpdateTLGMessages updater, AIMLBotik aiml = null)
        { 
            var botKey = System.IO.File.ReadAllText("botkey.txt");
            botik = new Telegram.Bot.TelegramBotClient(botKey);
            formUpdater = updater;
            perseptron = net;
            this.aiml = aiml;
        }

        public void SetNet(BaseNetwork net)
        {
            perseptron = net;
            formUpdater("Net updated!");
        }

        private async Task HandleUpdateMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //  Тут очень простое дело - банально отправляем назад сообщения
            var message = update.Message;
            var chatId = message.Chat.Id;
            var username = message.Chat.FirstName;
            formUpdater("Тип сообщения : " + message.Type.ToString());

            if (message.Type == MessageType.Text)
            {
                var messageText = update.Message.Text;

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: aiml.Talk(chatId, username, messageText),
                    cancellationToken: cancellationToken);
                return;
            }
            //  Получение файла (картинки)
            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                formUpdater("Picture loadining started");
                var photoId = message.Photo.Last().FileId;
                Telegram.Bot.Types.File fl = botik.GetFileAsync(photoId).Result;
                var imageStream = new MemoryStream();
                await botik.DownloadFileAsync(fl.FilePath, imageStream, cancellationToken: cancellationToken);
                var img = System.Drawing.Image.FromStream(imageStream);
                
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(img);

                //Sample sample = Loader.instance.LoadImage(perseptron.MethodIndex, bm);
                FigureType res = await Task.Run(() =>
                {
                    // Замените formUpdater на Invoke, если он взаимодействует с UI
                    //Invoke(new Action(() => formUpdater("Picture recognized!")));

                    return NeuralNetworksStand.instance.RecognTGImage(bm);
                });
                //Random random = new Random();

                //List<string> messages = new List<string>
                //{
                //    $"Это легко, это кнопка \"{Program.titles[res]}\"!",
                //    $"Это легко, кнопка \"{Program.titles[res]}\"!",
                //    $"Это легко, это была кнопка \"{Program.titles[res]}\"!",
                //    $"Это \"{Program.titles[res]}\"!",
                //    $"Полагаю, что это \"{Program.titles[res]}\"."
                //};

                //int randomIndex = random.Next(messages.Count);

                //// Отправляем сообщение
                //await botik.SendTextMessageAsync(message.Chat.Id, messages[randomIndex]);
                string predicted = Program.titles[res];

                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: aiml.Talk(chatId, username, predicted),
                     cancellationToken: cancellationToken);

                formUpdater("Picture recognized!");
                return;

                //return;
            }
            if (message.Type == MessageType.Video)
            {
                await botik.SendTextMessageAsync(message.Chat.Id, aiml.Talk(chatId, username, "Видео"), cancellationToken: cancellationToken);
                return;
            }
            if (message.Type == MessageType.Audio)
            {
                await botik.SendTextMessageAsync(message.Chat.Id, aiml.Talk(chatId, username, "Аудио"), cancellationToken: cancellationToken);
                return;
            }
        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var apiRequestException = exception as ApiRequestException;
            if (apiRequestException != null)
                Console.WriteLine($"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}");
            else
                Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }

        public bool Act()
        {
            try
            {
                botik.StartReceiving(HandleUpdateMessageAsync, HandleErrorAsync, new ReceiverOptions
                {   // Подписываемся только на сообщения
                    AllowedUpdates = new[] { UpdateType.Message }
                },
                cancellationToken: cts.Token);
                // Пробуем получить логин бота - тестируем соединение и токен
                Console.WriteLine($"Connected as {botik.GetMeAsync().Result}");
                Username = botik.GetMeAsync().Result.Username;
            }
            catch(Exception e) { 
                return false;
            }
            return true;
        }

        public void Stop()
        {
            cts.Cancel();
        }

    }
}
