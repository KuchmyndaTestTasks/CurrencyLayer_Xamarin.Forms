using System;

namespace MobileApp.Shared.Models
{
    /// <inheritdoc />
    /// <summary>
    /// For converting currencies in Exchange Tab.
    /// </summary>
    [Serializable]
    public class ExchangeModel 
    {
        /// <inheritdoc />
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public double Rating { get; set; }
    }
}
