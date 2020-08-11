using MyChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MindLink.Recruitment.MyChat.Conversation;
using static MindLink.Recruitment.MyChat.ConversationExporterConfiguration;
using static MindLink.Recruitment.MyChat.Message;

namespace MindLink.Recruitment.MyChat
{
    /// <summary>
    /// The output of this filter is all messages send by a specific user which is defined by the command-line argument
    /// </summary>
    public class FindUserFilter : IFilter<IEnumerable<Message>>
    {
        string uzer;
        ConversationExporterConfiguration uz = new ConversationExporterConfiguration("cht.txt", "export.txt", "bob", "bob", "", "");




        public IEnumerable<Message> Execute(IEnumerable<Message> input)
        {
            if (input == null || input.Count() < 1)
            {



                return input;

            }


            return input.Where(y => y.senderId == uz.user);
        }
    }
}
