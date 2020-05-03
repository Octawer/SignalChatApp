using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SignalChatApp.Common
{
    public static class Constants
    {
        // NOTE: If testing locally, use http://localhost:7071
        // otherwise enter your Azure Function App url
        // For example: http://YOUR_FUNCTION_APP_NAME.azurewebsites.net
        //public static string HostName { get; set; } = "https://signalchatfunctions.azurewebsites.net";

        public static string HostName { get; set; } = "https://ubump-demo-functions.azurewebsites.net";

        // NOTE: for clients to receive messages, this value must match
        // the value in the Server (Azure Functions App) Constants.cs file.
        public static string MessageName { get; set; } = "newMessage";

        public static string Username
        {
            get
            {
                return $"{Device.RuntimePlatform} User";
            }
        }
    }
}
