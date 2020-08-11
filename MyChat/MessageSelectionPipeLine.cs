using System;
using System.Collections.Generic;
using System.Text;

namespace MindLink.Recruitment.MyChat
{
    public class MessageSelectionPipeine : Pipeline<IEnumerable<Message>>
    {
        /// <summary>
        /// Pipeline which to select final list of message result
        /// </summary>
        public override IEnumerable<Message> Process(IEnumerable<Message> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }

    }

}
