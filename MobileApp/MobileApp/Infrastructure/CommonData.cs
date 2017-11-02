﻿using System;

namespace MobileApp.Infrastructure
{
    public static partial class CommonData
    {

        #region Local Paths

        public static string MainDirectory { get; } = Environment.CurrentDirectory.Replace(@"bin\Debug", "");
        public static string SettingsFile { get; } = "settings.txt";
        public static readonly string IconFolder = $@"{MainDirectory}\Resources\Pictures\Flags\";
        public static readonly string CurrenciesAssetFile = @"Currencies.txt";
        public static readonly string SelectedModelsFile = $@"{MainDirectory}\App_Data\Models.txt";

        #endregion

        #region Api Adresses

        public static readonly string CurrentLayerApi = "http://apilayer.net/api/";
        public static readonly string CurrentLayerApiLiveData = "live";
        public static readonly string CurrentLayerApiHistoricalData = "historical";

        #endregion

    }
}
