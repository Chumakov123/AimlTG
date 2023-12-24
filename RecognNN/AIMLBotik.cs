using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace NeuralNetwork1
{
    class AIMLBotik
    {
        Bot myBot;
        Dictionary<long, User> users = new Dictionary<long, User>();
        User myUser;  ///   map[TLGUserID] -> AIML User ID

        public AIMLBotik()
        {
            myBot = new Bot();
            myBot.loadSettings();
            myUser = new User("TLGUser", myBot);
            myBot.isAcceptingUserInput = false;
            myBot.loadAIMLFromFiles();
            myBot.isAcceptingUserInput = true;
        }

        public string Talk(string phrase)
        {
            Request r = new Request(phrase, myUser, myBot);
            Result res = myBot.Chat(r);
            return res.Output;
        }

        public string Talk(long userId, string userName, string phrase)
        {
            var result = "";
            User user;
            if (!users.ContainsKey(userId))
            {
                user = new User(userId.ToString(), myBot);
                users.Add(userId, user);
                Request r = new Request($"Меня зовут {userName}", user, myBot);
                result += myBot.Chat(r).Output + System.Environment.NewLine;
            }
            else
            {
                user = users[userId];
            }
            result = myBot.Chat(new Request(phrase, user, myBot)).Output;
            return result;
        }
    }
}
