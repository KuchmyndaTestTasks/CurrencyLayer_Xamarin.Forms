using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MobileApp.Infrastructure
{
    [Serializable]
    sealed class Settings
    {
        private Settings()
        {
            Read();
        }

        #region <Fields>

        [NonSerialized] private static readonly Lazy<Settings> Lazy = new Lazy<Settings>(() => new Settings());

        [NonSerialized] private readonly string _settingsFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), CommonData.SettingsFile);

        #endregion

        #region <Properties>

        public static Settings Instance => Lazy.Value;
        public string ApiKey { get; set; }
        public int TimeBetweenCalls { get; set; } = 10;

        #endregion

        #region <Methods>

        /// <summary>
        /// Saves API key, time to settings.txt (bin/Debug or Release) using serialization
        /// </summary>
        public void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(_settingsFile, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(stream, this);
            }
        }

        /// <summary> 
        /// Reads API key and time from settings.txt  (bin/Debug or Release) using deserialization
        /// </summary>
        public void Read()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(_settingsFile, FileMode.OpenOrCreate))
            {
                if (stream.Length <= 0) return;
                var obj = binaryFormatter.Deserialize(stream);
                var converted = (Settings) obj;
                ApiKey = converted.ApiKey;
                TimeBetweenCalls = converted.TimeBetweenCalls;
            }
        }

        #endregion
    }
}
