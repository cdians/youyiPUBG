using System;
using System.IO;

namespace YouYiApp.common
{
    internal class DataContractJsonSerializer
    {
        private Type type;

        public DataContractJsonSerializer(Type type)
        {
            this.type = type;
        }

        internal void WriteObject<T>(MemoryStream stream, T model)
        {
            throw new NotImplementedException();
        }
    }
}
