using System;
using System.Collections.Generic;
using System.Text;
using static MindLink.Recruitment.MyChat.Conversation;
using static MindLink.Recruitment.MyChat.Message;

namespace MindLink.Recruitment.MyChat
{
    public abstract class Pipeline<T>
    {
        /// <summary>
        /// Filters in the pipeline
        /// </summary>
        protected readonly List<IFilter<T>> filters = new List<IFilter<T>>();
        public Pipeline<T> Register(IFilter<T> filter)
        {
            filters.Add(filter);
            return this;
        }
        //source https://www.codeproject.com/Articles/1094513/Pipeline-and-Filters-Pattern-using-Csharp
        /// <summary>
        /// To start processing on the Pipeline
        /// </summary>
        /// <param name="input">
        /// The input object on which filter processing would execute</param>
        /// <returns></returns>
        public abstract T Process(T input);
    }
}
