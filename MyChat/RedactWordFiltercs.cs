using System;
using System.Collections.Generic;
using System.Text;
using MyChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MindLink.Recruitment.MyChat.Conversation;
using static MindLink.Recruitment.MyChat.ConversationExporterConfiguration;
using System.IO;

namespace MindLink.Recruitment.MyChat
{
    /// <summary>
    /// The output of this filter is all messages send by a specific user which is defined by the command-line argument
    /// </summary>
    public class RedactWordFiltercs : IFilter<IEnumerable<Message>>
    {
        string wordlist;
        ConversationExporterConfiguration wordl = new ConversationExporterConfiguration("chat.txt", "export.txt", "", "", "blacklist.txt", "");


        public IEnumerable<Message> Execute(IEnumerable<Message> input)
        {
            //create a list that holds the blacklist words
            List<string> blacklist = new List<string>();
            // read the blacklist file
            string[] lines = File.ReadAllLines(wordl.blacklistPath);
            //add words to the list
            foreach (string line in lines)
            {
                blacklist.Add(line);


                if (input == null || input.Count() < 1)
                {

                }
                return input;
            }


            //if (message.content.Contains(line))
            //{
            //    message.content.Replace(message.content, "redacted");
            //}
            return input; ;

        }
    }
}


