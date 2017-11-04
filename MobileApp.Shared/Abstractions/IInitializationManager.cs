namespace MobileApp.Shared.Abstractions
{/// <summary>
    /// Interface for initialization data/models with blocking resources 
    /// (for example: set IsEnabled in UI)
    /// </summary>
    internal interface IInitializationViewModel
    {
        /// <summary>
        /// Property for blocking UI or etc
        /// </summary>
        bool IsLoading { get; set; }
        void Initialize();
    }
}
