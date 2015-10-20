using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.UserActivityStore
{
    [Serializable]
    public class SingleEntryHistory<T> : ISerializable
    {
        public T LastValue { get; set; }

        public SingleEntryHistory()
        {
            LastValue = default(T);
        }

        public SingleEntryHistory(T value)
        {
            LastValue = value;
        }

        public SingleEntryHistory(SerializationInfo info, StreamingContext context)
        {
            LastValue = (T)info.GetValue("HistoricValue", typeof(T));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("HistoricValue", LastValue);
        }
    }
}
