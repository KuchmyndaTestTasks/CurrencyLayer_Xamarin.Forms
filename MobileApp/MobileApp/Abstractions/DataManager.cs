using MobileApp.Infrastructure.MainOperations;

namespace MobileApp.Abstractions
{
    /// <summary>
    /// Interface which provides methods for uploading data from server/local memory and saving.
    /// (Strategy Pattern)
    /// </summary>
    /// <typeparam name="T">type of data</typeparam>
    abstract class DataManager<T> where T : class
    {
        protected Serializator Serializator;
        protected DataManager(string path)
        {
            Serializator=new Serializator(path);
        }
        /// <summary>
        /// Saves into local memory
        /// </summary>
        /// <param name="data"></param>
        public virtual void Save(T data) { }

        /// <summary>
        /// Uploads data from local DB/api
        /// </summary>
        /// <returns></returns>
        public abstract T Upload();
    }
}
