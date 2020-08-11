using System;
using System.Collections.Generic;
using System.Text;
using MyChat;
using System;
using System.Linq;
using static MindLink.Recruitment.MyChat.Conversation;
using static MindLink.Recruitment.MyChat.ConversationExporterConfiguration;
using static MindLink.Recruitment.MyChat.Message;

namespace MindLink.Recruitment.MyChat
{
    /// <summary>
    /// The output of this filter is all messages send by a specific user which is defined by the command-line argument
    /// </summary>
    class SearchWordFilter : IFilter<IEnumerable<Message>>
    {
        string word;
        ConversationExporterConfiguration wordFilter = new ConversationExporterConfiguration("chat.txt", "", "bob", "", "", "");



        public IEnumerable<Message> Execute(IEnumerable<Message> input)
        {
            if (input == null || input.Count() < 1)
            {
                return input;
            }

            return input.Where(message => message.content == wordFilter.word);
        }
    }
}

