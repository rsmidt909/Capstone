using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ChatBotMessage
    {

        //0 -> UserMessage 
        //1-> BotMessage
        public int ID { get; set; }
        public MessageType MsgType { get; set; }
        public string ChatMessage { get; set; }
    }

    public enum MessageType
    {
        UserMessage,
        LexMessage
    }


}
