using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ChatBotMessage
    {
        public int ID { get; set; }
        public BotMessageType MessageType { get; set; }
        public string ChatMessage { get; set; }
    }

    public enum BotMessageType
    {
        UserMessage,
        LexMessage
    }


}
