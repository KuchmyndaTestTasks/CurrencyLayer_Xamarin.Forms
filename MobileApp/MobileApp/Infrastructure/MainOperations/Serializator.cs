using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MobileApp.Infrastructure.MainOperations
{
    class Serializator
    {
        public Serializator(string filename)
        {
            _binaryFormatter = new BinaryFormatter();
            SetFullPath(filename);
        }

        #region <Fields>

        private readonly BinaryFormatter _binaryFormatter;
        private string _filepath;

        #endregion
        
        #region <Methods>

        public void Serialize<T>(T data)
        {
            using (var stream=new FileStream(_filepath,FileMode.OpenOrCreate))
            {
                _binaryFormatter.Serialize(stream,data);
            }
        }

        public T Deserialize<T>()
        {
            T result = default(T);
            using (var stream = new FileStream(_filepath, FileMode.OpenOrCreate))
            {
                if (stream.Length <= 0) return result;
                result=(T) _binaryFormatter.Deserialize(stream);
            }
            return result;
        }
        private void SetFullPath(string path)
        {
            _filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
        }

        #endregion
    }
}