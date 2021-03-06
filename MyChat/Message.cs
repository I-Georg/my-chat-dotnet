namespace MindLink.Recruitment.MyChat
{
    using System;

    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public sealed class Message
    {
        /// <summary>
        /// The message content.
        /// </summary>
        public string content; 
        //public string Content
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// The message timestamp.
        /// </summary>
        public DateTimeOffset timestamp;
        //public DateTimeOffset Timestamp
        //{
        //    get; set;
        //}

        /// <summary>
        /// The message sender.
        /// </summary>
        public string senderId;
        //public string SenderId 
        //{ 
        //    get; 
        //    set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="timestamp">
        /// The message timestamp.
        /// </param>
        /// <param name="senderId">
        /// The ID of the sender.
        /// </param>
        /// <param name="content">
        /// The message content.
        /// </param>
        public Message(DateTimeOffset timestamp, string senderId, string content)
        {
            this.content = content;
            this.timestamp = timestamp;
            this.senderId = senderId;
        }
        public interface IFilter<T>
        {
            /// <summary>
            /// Filter implementing this method would perform processing on the input type T
            /// </summary>
            /// <param name="input">The input to be executed by the filter</param>
            /// <returns></returns>
            T Execute(T input);
        }
    }
}
