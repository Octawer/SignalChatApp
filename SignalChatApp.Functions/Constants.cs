using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalChatApp.Functions
{
    public static class Constants
    {
        // NOTE: for clients to receive messages, this value must match
        // the value in the ChatClient Constants.cs file.
        public static string MessageName { get; set; } = "newMessage";
    }
}
