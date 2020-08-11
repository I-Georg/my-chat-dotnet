namespace MyChat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Text.RegularExpressions;
    using MindLink.Recruitment.MyChat;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a conversation exporter that can read a conversation and write it out in JSON.
    /// </summary>
    public sealed class ConversationExporter
    {
        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>

        static void Main(string[] args)
        {

            var conversationExporter = new ConversationExporter();
            ConversationExporterConfiguration configuration = new CommandLineArgumentParser().ParseCommandLineArguments(args);

            /// <summary>
            /// The conversation is read, input file is specified as chat.txt
            /// </summary>
            /// <param name="c">
            /// the returned object from ReadConversation c is used in WriteConversation
            /// </param>
            /// <summary>
            /// The conversation is exported as json
            /// </summary>
             // Console.WriteLine(agentsStatus_1);
            conversationExporter.ReadConversation(configuration.inputFilePath);
            Message c = conversationExporter.ReadConversation(configuration.inputFilePath);
            //Construct the Pipeline object
            MessageSelectionPipeline messageStatusPipeline = new MessageSelectionPipeline();

            //Register the filters to be executed
            messageStatusPipeline.Register(new FindUserFilter())
                .Register(new SearchWordFilter())
               /* .Register(new RedactWordFiltercs())*/;
            //Start pipeline processing
            var agentsStatus_1 = messageStatusPipeline.Process(Message.Messag);

            conversationExporter.WriteConversation(c, configuration.outputFilePath);
            conversationExporter.ExportConversation(configuration.inputFilePath, configuration.outputFilePath);
            //conversationExporter.FindUser(c, configuration.user);
            //conversationExporter.SearchWord(c, configuration.user);
            //conversationExporter.RedactWord(c, configuration.blacklistPath, configuration.redactedConversationPath);

        }

    

        /// <summary>
        /// Exports the conversation at <paramref name="inputFilePath"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when a path is invalid.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something bad happens.
        /// </exception>
        public void ExportConversation(string inputFilePath, string outputFilePath)
        {

           Message conversation = this.ReadConversation(inputFilePath);

            this.WriteConversation(conversation, outputFilePath);

            Console.WriteLine("Conversation exported from '{0}' to '{1}'", inputFilePath, outputFilePath);
        }

        /// <summary>
        /// Helper method to read the conversation from <paramref name="inputFilePath"/>.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <returns>
        /// A <see cref="Conversation"/> model representing the conversation.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input file could not be found.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else went wrong.
        /// </exception>
        public Message ReadConversation(string inputFilePath)
        {
            //try
            //{
            var messagez = new List<Message>();
            string[] linez = File.ReadAllLines(inputFilePath, Encoding.UTF8);
            var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read),
                   Encoding.ASCII);
            string conversationName = reader.ReadLine();

            foreach (var line in linez)
            {
                //creates three different groups of patterns for each part of the object - timestamp,senderrId,content
                string rx = @"(\d{10})\s(\w{2,10})\s(.*)";

                MatchCollection matches = Regex.Matches(line, rx);
                foreach (Match match in matches)
                {
                    // Console.WriteLine(match.Groups[3].Value);
                    messagez.Add(new Message(DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(match.Groups[1].Value)), match.Groups[2].Value, match.Groups[3].Value)); //doesn't add the messages to the object and as a result - null value and breaks the code
                }


            }

            return new Message(DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(messagez.Select(x => x.timestamp))), messagez.Select(x => x.senderId).ToString(), messagez.Select(x => x.content).ToString()); //not recognised by the method if try/catch is on 

        }


        //catch (FileNotFoundException)
        //{
        //    throw new ArgumentException("The file was not found.");
        //}
        //catch (IOException)
        //{
        //    throw new Exception("Something went wrong in the IO.");
        //}
        //catch (System.IndexOutOfRangeException)
        //{
        //    throw new ArgumentException("Outside the bounds of the array");
        //}
        //catch (RankException)
        //{
        //    Console.WriteLine("Array with wrong number of items is passed to the method");
        //}

        //catch (EndOfStreamException)
        //{
        //    Console.WriteLine("Trying to read past the ecnd of the file");
        //}
        //catch (FileLoadException)
        //{
        //    Console.WriteLine("File cannot load");
        //}

        // }


        /// <summary>
        /// Helper method to write the <paramref name="conversation"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="conversation">
        /// The conversation.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when there is a problem with the <paramref name="outputFilePath"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else bad happens.
        /// </exception>
        public void WriteConversation(Message conversation, string outputFilePath)
    {
        try
        {
            var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite));
            var serialized = JsonConvert.SerializeObject(conversation, Formatting.Indented);
            writer.Write(serialized);
            writer.Flush();
            writer.Close();
        }
        catch (SecurityException)
        {
            throw new ArgumentException("No permission to file.");
        }
        catch (DirectoryNotFoundException)
        {
            throw new ArgumentException("Path invalid.");
        }
        catch (IOException)
        {
            throw new Exception("Something went wrong in the IO.");
        }

    }
        
        

      
        /// <summary> needs more work
        /// Helper method to write the <paramref name=RedactCard "/>  to <paramref name="RedactConversationPath"/>.
        /// </summary>
        /// <param name="RedactCard method">
        /// Find a card/phone number in teh conversationt and redact it
        /// </param>
        /// <param name="path">
        /// The output file path - new file called redactConversation.txt.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when there is a problem with the <paramref name="path"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else bad happens.
        /// </exception>

        //public void RedactCard(Conversation conversation, string blacklistPath, string redactedConversationPath) - not fully working
        //{
        //    try
        //    {
        //        //bool flag = true;

        //        ////source for phone number https://stackoverflow.com/questions/25155970/validating-uk-phone-number-regex-c
        //        //var patterns = new string[] { @"\d{4} \d{4} \d{4} \d{4}", @"\d{16}", @"\d{4}-\d{4}-\d{4}-\d{4}", @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$" };
        //        //var rgPattern = new Regex(string.Join("|", patterns), RegexOptions.IgnoreCase);

        //        var serialized = JsonConvert.SerializeObject(conversation, Formatting.Indented);
        //        //deserialise object
        //        Conversation deserialisedChat = JsonConvert.DeserializeObject<Conversation>(serialized);
        //        //    MatchCollection creditCard = rgPattern.Matches(args[6]);
        //        //    for (int count = 0; count < creditCard.Count; count++)
        //        //    {

        //        //        foreach (var file in deserialisedChat)
        //        //        {
        //        //            var list4 = from y in seserialisedChat

        //        //                        select y.messages;
        //        //            if (rgPattern.IsMatch(list4.ToString()))
        //        //            {

        //        //                Console.WriteLine("loooo");
        //        //            }
        //        //            //var listRedacted = from k in RedactCardList
        //        //            //                   where k.content
        //        //            //                   select k;

        //        //            //                                   select y;
        //        //            // Console.WriteLine(creditCard[count].Value);
        //        //        }
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        throw new ArgumentException("Path invalid.");
        //    }

        //}
    }}
