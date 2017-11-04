using System;
using MobileApp.Shared.Global;

namespace MobileApp.Shared.Infrastructure.MainOperations
{
    [Serializable]
    sealed class Settings
    {
        private Settings()
        {
        }

        #region <Fields>

       
        [NonSerialized] public bool IsConfigured = false;
        [NonSerialized] private static Settings _instance;

        #endregion

        #region <Properties>

        public static Settings Instance => _instance ?? new Settings();

        public string ApiKey { get; set; }
        public int TimeBetweenCalls { get; set; } = 10;

        #endregion

        #region <Methods>

        /// <summary>
        /// Saves API key, time to settings.txt (bin/Debug or Release) using serialization
        /// </summary>
        public void Save()
        {
            var serializator = new Serializator(CommonData.SettingsFile);
            serializator.Serialize(this);
        }

        /// <summary> 
        /// Reads API key and time from settings.txt  (bin/Debug or Release) using deserialization
        /// </summary>
        public static void Read()
        {
            var serializator = new Serializator(CommonData.SettingsFile);
            var deserialized = serializator.Deserialize<Settings>();
            _instance = deserialized ?? new Settings();
        }

        #endregion
    }
}