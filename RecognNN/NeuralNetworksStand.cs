using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Accord.Neuro;

namespace NeuralNetwork1
{

    public delegate void FormUpdater(double progress, double error, TimeSpan time);

    public delegate void UpdateTLGMessages(string msg);

    delegate void FormUpdateDelegate();
    public partial class NeuralNetworksStand : Form
    {
        public static NeuralNetworksStand instance;
        AIMLBotik botik = new AIMLBotik();
        TLGBotik tlgBot;

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
        /// <summary>
        /// Генератор изображений (образов)
        /// </summary>
        //GenerateImage generator = new GenerateImage();
        Loader loader = null;
        Controller controllerForLoader = null;
        //AugmentationImage augmentation = new AugmentationImage();
        /// <summary>
        /// Текущая выбранная через селектор нейросеть
        /// </summary>
        public BaseNetwork Net
        {
            get
            {
                if (netTypeBox.InvokeRequired)
                {
                    // Если вызов происходит из другого потока, используем Invoke
                    return (BaseNetwork)netTypeBox.Invoke(new Func<BaseNetwork>(() => GetSelectedNetwork()));
                }
                else
                {
                    // Иначе просто получаем выбранный элемент
                    return GetSelectedNetwork();
                }
            }
        }

        private BaseNetwork GetSelectedNetwork()
        {
            var selectedItem = (string)netTypeBox.SelectedItem;
            if (!networksCache.ContainsKey(selectedItem))
                networksCache.Add(selectedItem, CreateNetwork(selectedItem));

            return networksCache[selectedItem];
        }

        private readonly Dictionary<string, Func<int[], BaseNetwork>> networksFabric;
        private Dictionary<string, BaseNetwork> networksCache = new Dictionary<string, BaseNetwork>();

        /// <summary>
        /// Конструктор формы стенда для работы с сетями
        /// </summary>
        /// <param name="networksFabric">Словарь функций, создающих сети с заданной структурой</param>
        public NeuralNetworksStand(Dictionary<string, Func<int[], BaseNetwork>> networksFabric)
        {
            instance = this;
            InitializeComponent();
            this.networksFabric = networksFabric;
            netTypeBox.Items.AddRange(this.networksFabric.Keys.Select(s => (object)s).ToArray());
            netTypeBox.SelectedIndex = 0;
            button3_Click(this, null);
            pictureBox1.Image = Properties.Resources.Title;
            cb_methods.SelectedIndex = 0;

            controller = new Controller(new FormUpdateDelegate(UpdateFormFields));
            controllerForLoader = new Controller(new FormUpdateDelegate(UpdateFormFields));
            loader = new Loader(controllerForLoader, this);
            loader.FigureCount = (int)classCounter.Value;

            List<string> split = netStructureBox.Text.Split(';').ToList();
            netStructureBox.Text = (Program.size * 2).ToString();
            for (int i = 1; i < split.Count(); i++)
            {
                netStructureBox.Text += ";" + split[i];
            }
            
            tlgBot = new TLGBotik(Net, new UpdateTLGMessages(UpdateTLGInfo), botik);
        }

        public void UpdateTLGInfo(string message)
        {
            if (TLGUsersMessages.InvokeRequired)
            {
                TLGUsersMessages.Invoke(new UpdateTLGMessages(UpdateTLGInfo), new Object[] { message });
                return;

            }
            TLGUsersMessages.Text += message + Environment.NewLine;
        }

        public void UpdateLearningInfo(double progress, double error, TimeSpan elapsedTime)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new TrainProgressHandler(UpdateLearningInfo), progress, error, elapsedTime);
                return;
            }
            st_lable.Text = "Ошибка: " + error;
            int progressPercent = (int)Math.Round(progress * 100);
            progressPercent = Math.Min(100, Math.Max(0, progressPercent));
            elapsedTimeLabel.Text = "Затраченное время : " + elapsedTime.Duration().ToString(@"hh\:mm\:ss\:ff");
            progressBar1.Value = progressPercent;
        }


        private void set_result(Sample figure)
        {
            if (InvokeRequired)
            {
                // If it is, invoke the method on the UI thread
                Invoke(new Action(() => set_result(figure)));
                return;
            }

            label1.ForeColor = figure.Correct() ? Color.Green : Color.Red;

            label1.Text = "Распознано : " + Program.titles[figure.recognizedClass];

            label8.Text = string.Join("\n", figure.Output.Select(d => $"{d:f2}"));
            //pictureBox1.Image = generator.GenBitmap();
            pictureBox1.Image = loader.GenImage();
            
            //pictureBox1.Image = augmentation.GenBitmap();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Sample fig = generator.GenerateFigure();
            Sample fig = null;
            fig = loader.LoadImage(Net.MethodIndex);

            if (!cb_multy_recogn.Checked)
            {
                Net.Predict(fig);
            }
            else
            {
                var output = new double[fig.Output.Length];
                foreach (var network in networksCache)
                {
                    fig = loader.LoadImage(network.Value.MethodIndex, false, true);
                    network.Value.Predict(fig);
                    for (int i = 0; i < output.Length; i++)
                    {
                        output[i] += fig.Output[i] / networksCache.Count;
                    }
                    //Debug.WriteLine("net " + string.Join("\n", fig.Output));
                    Debug.WriteLine(network.Key +": " + fig.Correct().ToString());
                }
                //Debug.WriteLine("out "+string.Join("\n", output));
                fig.ProcessPrediction(output);
            }
            //Sample fig = augmentation.Get30x30();


            set_result(fig);
        }

        private void PredictUserImage()
        {
            Sample fig = null;
            fig = loader.LoadImage(Net.MethodIndex);

            if (!cb_multy_recogn.Checked)
            {
                Net.Predict(fig);
            }
            else
            {
                var output = new double[fig.Output.Length];
                foreach (var network in networksCache)
                {
                    fig = loader.LoadImage(network.Value.MethodIndex, false, true);
                    network.Value.Predict(fig);
                    for (int i = 0; i < output.Length; i++)
                    {
                        output[i] += fig.Output[i] / networksCache.Count;
                    }
                    //Debug.WriteLine("net " + string.Join("\n", fig.Output));
                    Debug.WriteLine(network.Key + ": " + fig.Correct().ToString());
                }
                //Debug.WriteLine("out "+string.Join("\n", output));
                fig.ProcessPrediction(output);
            }

            set_result(fig);
        }

        private async Task<double> train_networkAsync(int training_size, int epoches, double acceptable_error,
            bool parallel = true)
        {
            //  Выключаем все ненужное
            label1.Text = "Выполняется обучение...";
            label1.ForeColor = Color.Red;
            groupBox1.Enabled = false;
            pictureBox1.Enabled = false;
            //trainOneButton.Enabled = false;

            //  Создаем новую обучающую выборку
            SamplesSet samples = null;
            samples = loader.LoadSampleSet();
            Debug.WriteLine(samples.Count);
            //SamplesSet samples = augmentation.LoadSampleSet();
            /*SamplesSet samples = new SamplesSet();

            for (int i = 0; i < training_size; i++)
                samples.AddSample(generator.GenerateFigure());*/
            try
            {
                //  Обучение запускаем асинхронно, чтобы не блокировать форму
                var curNet = Net;
                double f = await Task.Run(() => curNet.TrainOnDataSet(samples, epoches, acceptable_error, parallel));

                label1.Text = "Щелкните на картинку для теста нового образа";
                label1.ForeColor = Color.Green;
                groupBox1.Enabled = true;
                pictureBox1.Enabled = true;
                //trainOneButton.Enabled = true;
                st_lable.Text = "Ошибка: " + f;
                st_lable.ForeColor = Color.Green;
                return f;
            }
            catch (Exception e)
            {
                label1.Text = $"Исключение: {e.Message}";
            }

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            train_networkAsync((int)TrainingSizeCounter.Value, (int)EpochesCounter.Value,
                (100 - AccuracyCounter.Value) / 100.0, parallelCheckBox.Checked);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enabled = false;
            //  Тут просто тестирование новой выборки
            //  Создаем новую обучающую выборку
            //SamplesSet samples = loader.LoadSampleSetTest();
            SamplesSet samples = null;
            samples = loader.LoadSampleSetTest();
            Debug.WriteLine(samples.Count);
            //SamplesSet samples = augmentation.LoadSampleSetTest();
            //SamplesSet samples = new SamplesSet();

            /*
            for (int i = 0; i < (int)TrainingSizeCounter.Value; i++)
                samples.AddSample(generator.GenerateFigure());*/

            double accuracy = samples.TestNeuralNetwork(Net);

            st_lable.Text = $"Точность на тестовой выборке : {accuracy * 100,5:F2}%";
            st_lable.ForeColor = accuracy * 100 >= AccuracyCounter.Value ? Color.Green : Color.Red;

            Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] structure = CurrentNetworkStructure();
            var selectedItem = (string)netTypeBox.SelectedItem;
            if (!networksCache.ContainsKey(selectedItem))
            {
                return;
            }
            networksCache[selectedItem].TrainProgress -= UpdateLearningInfo;

            // Пересоздаем выбранную сеть с новой структурой
            networksCache[selectedItem] = CreateNetwork(selectedItem);

            tlgBot.SetNet(Net);
        }

        private int[] CurrentNetworkStructure()
        {
            return netStructureBox.Text.Split(';').Select(int.Parse).ToArray();
        }



        private void classCounter_ValueChanged(object sender, EventArgs e)
        {
            //generator.FigureCount = (int)classCounter.Value;
            loader.FigureCount = (int)classCounter.Value;
            //augmentation.FigureCount = (int)classCounter.Value;
            var vals = netStructureBox.Text.Split(';');
            if (!int.TryParse(vals.Last(), out _)) return;
            vals[vals.Length - 1] = classCounter.Value.ToString();
            netStructureBox.Text = vals.Aggregate((partialPhrase, word) => $"{partialPhrase};{word}");
        }

        private void btnTrainOne_Click(object sender, EventArgs e)
        {
            if (Net == null) return;
            //Sample fig = generator.GenerateFigure();
            Sample fig = null;
            //Sample fig = augmentation.Get30x30();
            //pictureBox1.Image = generator.GenBitmap();
            fig = loader.LoadImage(Net.MethodIndex);
            pictureBox1.Image = loader.GenImage();
            //pictureBox1.Image = augmentation.GenBitmap();
            pictureBox1.Invalidate();
            Net.Train(fig, 0.00005, parallelCheckBox.Checked);
            set_result(fig);
        }

        private BaseNetwork CreateNetwork(string networkName)
        {
            var network = networksFabric[networkName](CurrentNetworkStructure());
            network.TrainProgress += UpdateLearningInfo;
            return network;
        }

        private void recreateNetButton_MouseEnter(object sender, EventArgs e)
        {
            //infoStatusLabel.Text = "Заново пересоздает сеть с указанными параметрами";
        }

        private void netTrainButton_MouseEnter(object sender, EventArgs e)
        {
            //infoStatusLabel.Text = "Обучить нейросеть с указанными параметрами";
        }

        private void testNetButton_MouseEnter(object sender, EventArgs e)
        {
            //infoStatusLabel.Text = "Тестировать нейросеть на тестовой выборке такого же размера";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Net.Print();
        }

        /// <summary>
        /// Класс, реализующий всю логику работы
        /// </summary>
        private Controller controller = null;
        /// <summary>
        /// Событие для синхронизации таймера
        /// </summary>
        private AutoResetEvent evnt = new AutoResetEvent(false);

        /// <summary>
        /// Список устройств для снятия видео (веб-камер)
        /// </summary>
        private FilterInfoCollection videoDevicesList;

        /// <summary>
        /// Выбранное устройство для видео
        /// </summary>
        private IVideoSource videoSource;

        /// <summary>
        /// Таймер для измерения производительности (времени на обработку кадра)
        /// </summary>
        private Stopwatch sw = new Stopwatch();

        /// <summary>
        /// Таймер для обновления объектов интерфейса
        /// </summary>
        System.Threading.Timer updateTmr;

        public void UpdateFormFields()
        {

        }

        /// <summary>
        /// Обертка для обновления формы - перерисовки картинок, изменения состояния и прочего
        /// </summary>
        /// <param name="StateInfo"></param>
        public void Tick(object StateInfo)
        {
            UpdateFormFields();
            Console.WriteLine("2");
            return;
        }

        private void tresholdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            controller.settings.threshold = (byte)tresholdTrackBar.Value;
            controller.settings.differenceLim = (float)tresholdTrackBar.Value / tresholdTrackBar.Maximum;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updateTmr != null)
                updateTmr.Dispose();

            //  Как-то надо еще робота подождать, если он работает

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: controller.settings.decTop(); Debug.WriteLine("Up!"); break;
                case Keys.S: controller.settings.incTop(); Debug.WriteLine("Down!"); break;
                case Keys.A: controller.settings.decLeft(); Debug.WriteLine("Left!"); break;
                case Keys.D: controller.settings.incLeft(); Debug.WriteLine("Right!"); break;
                case Keys.Q: controller.settings.border++; Debug.WriteLine("Plus!"); break;
                case Keys.E: controller.settings.border--; Debug.WriteLine("Minus!"); break;
                case Keys.F:
                    SaveFile();
                    break;
                case Keys.G:
                    MakePhoto();
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            controller.settings.processImg = checkBox1.Checked;
        }

        private void SaveFile()
        {
            if (controlPanel.Enabled)
            {
                int fclass = 0;

                if (Directory.Exists(fclass.ToString()))
                {
                    int cfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + fclass).Length + 30;
                    controller.processor.processed.Save(fclass + "/" + fclass + "_" + cfiles + ".jpg");
                }
                else
                {
                    Directory.CreateDirectory(fclass.ToString());
                    int cfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + fclass).Length + 30;
                    controller.processor.processed.Save(fclass + "/" + fclass + "_" + cfiles + ".jpg");
                }
                Debug.WriteLine("Photo!");
            }

        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void LoadDataset_Click(object sender, EventArgs e)
        {
            loader.LoadDataset(Program.MethodIndex);
        }

        private void createDataset_Click(object sender, EventArgs e)
        {
            loader.CreateDataset();
        }

        private void button_F_Click(object sender, EventArgs e)
        {
            MakePhoto();
            Sample photo = null;

            // отрисовка
            if (Net == null) return;
            photo = loader.LoadImage(Program.MethodIndex, true);
            pictureBox1.Image = loader.GenImage();
            pictureBox1.Invalidate();

            //
            Net.Predict(photo);
            set_result(photo);
        }

        private void MakePhoto()
        {
            //recognizedBox.Image = controller.processor.processed;
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset";
            controller.processor.processed.Save(path + "\\input.jpg");
        }

        private void button_dop_Click(object sender, EventArgs e)
        {
            //if (Net == null) return;
            ////Sample fig = loader.CheckImage(comboBoxMethod.SelectedIndex);
            //Sample fig = null;
            //fig = loader.LoadImage(0, true);

            //fig.actualClass = (FigureType)Int32.Parse(textBox_dop.Text);

            //Console.WriteLine(fig.actualClass);

            //Net.Train(fig, 0.00005, parallelCheckBox.Checked);
            //set_result(fig);
        }

        private void bt_save_network_Click(object sender, EventArgs e)
        {
            string filename = "";
            switch (Net.MethodIndex)
            {
                case 0: filename = "sum.txt"; break;
                case 1: filename = "pixel.txt"; break;
                case 2: filename = "alt.txt"; break;
                case 3: filename = "combo.txt"; break;
            }
            Net.Save(path + "\\Networks\\" + filename);
        }

        private void bt_load_network_Click(object sender, EventArgs e)
        {
            string filename = "";
            switch (Program.MethodIndex)
            {
                case 0: filename = "sum.txt"; break;
                case 1: filename = "pixel.txt"; break;
                case 2: filename = "alt.txt"; break;
                case 3: filename = "combo.txt"; break;
            }
            Net.Load(path + "\\Networks\\" + filename);
        }

        private void cb_methods_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.MethodIndex = cb_methods.SelectedIndex;
            switch (Program.MethodIndex)
            {
                case 0: netStructureBox.Text = $"{Program.size * 2};{600};{classCounter.Value}"; break;
                case 1: netStructureBox.Text = $"{Program.size * Program.size};{600};{classCounter.Value}"; break;
                case 2: netStructureBox.Text = $"{Program.size * 2};{600};{classCounter.Value}"; break;
                case 3: netStructureBox.Text = $"{Program.size * 2};{600};{classCounter.Value}"; break;
            }
        }

        private void TLGBotOnButton_Click(object sender, EventArgs e)
        {
            tlgBot.Act();
            TLGBotOnButton.Enabled = false;
        }

        private void bt_aiml_process_Click(object sender, EventArgs e)
        {
            var phrase = AIMLInput.Text;
            if (phrase.Length > 0)
                AIMLOutput.Text += botik.Talk(phrase) + Environment.NewLine;
        }

        internal FigureType RecognTGImage(Bitmap bm)
        {
            var fig = loader.LoadImage(Program.MethodIndex, bm);
            var res = Net.Predict(fig);
            set_result(fig);
            return res;
        }
    }
}