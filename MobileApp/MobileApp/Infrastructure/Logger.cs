using System;

namespace MobileApp.Infrastructure
{

    sealed class Logger
    {
        /// <summary>
        /// Module which save last message about important events. 
        /// (Singleton Pattern)
        /// </summary>
        public Logger()
        {
            _message = MessageLog.Create();
        }

        #region <Fields>

        /// <summary>
        /// Message with color (red - error, gray - empty message, green - good news)
        /// </summary>
        private MessageLog _message;
        static Lazy<Logger> _lazy = new Lazy<Logger>(() => new Logger());

        #endregion

        #region <Properties>

        public static Logger Instance { get; } = _lazy.Value;

        #endregion

        #region <Enumerations & inner classes>

        internal enum Color
        {
            Red,
            Green,
            Gray
        }

        internal class MessageLog
        {
            public string Text { get; set; }
            public Color Color { get; set; }
            public static MessageLog Create() => new MessageLog() {Text = "", Color = Color.Gray};
        }

        #endregion

        #region <Methods>

        /// <summary>
        /// Saves last message
        /// </summary>
        /// <param name="message">text</param>
        /// <param name="color">color</param>
        private void LogMessage(string message, Color color = Color.Gray)
        {
            _message = new  MessageLog(){Text = message, Color=color};
        }
        /// <summary>
        /// Gets last message.
        /// </summary>
        /// <returns></returns>
        private MessageLog GetLastLog()
        {
            // ?? - if left part is null, than take right part, else left
            return _message ??  MessageLog.Create();
        }

        #endregion

        #region <Static Methods>

        /// <summary>
        /// Saves last message
        /// </summary>
        /// <param name="message">text</param>
        /// <param name="color">color</param>
        public static void SetLogMessage(string message, Color color = Color.Gray)
        {
            Instance.LogMessage(message, color);
        }

        /// <summary>
        /// Gets last message.
        /// </summary>
        /// <returns></returns>
        public static MessageLog GetLogMessage()
        {
            return Instance.GetLastLog();
        }

        #endregion
    }
}