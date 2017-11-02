using System;
using MobileApp.Abstractions;
using MobileApp.Infrastructure;

namespace MobileApp.ViewModels.Tabs_SettingViewModels
{
    class SettingViewModel : SavingViewModel, IModelChecker
    {
        public SettingViewModel()
        {
            ResetState();
            Action = Save;
        }

        #region <Fields>

        private string _apiKey;
        private int _time;
        private Logger.MessageLog _message;

        #endregion

        #region <Properties>

        public string ApiKey
        {
            get { return _apiKey; }
            set
            {
                _apiKey = value;
                ResetState();
                OnPropertyChanged();
            }
        }

        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                ResetState();
                OnPropertyChanged();
            }
        }

        public Logger.MessageLog Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region <Methods>

        private void Save()
        {
            if (!IsValid()) return;
            var setting = Settings.Instance;
            setting.ApiKey = ApiKey;
            setting.TimeBetweenCalls = Time;
            setting.Save();
        }

        public bool IsValid()
        {
            var isNull = string.IsNullOrEmpty(ApiKey);
            if (!isNull)
            {
                ResetState();
                IsFinished = true;
            }
            else
            {
                IsFinished = false;
                Message = new Logger.MessageLog() {Text = "ApiKey is empty. Fill, please.", Color = Logger.Color.Red};
            }
            return !isNull;
        }

        public void ResetState()
        {
            Message = Logger.MessageLog.Create();
        }

        #endregion
    }
}