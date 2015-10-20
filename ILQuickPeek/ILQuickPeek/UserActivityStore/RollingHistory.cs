using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.UserActivityStore
{
    [Serializable]
    public class RollingHistory<T> : ISerializable
    {
        private int _falloffSize;
        private Queue<T> _historicItems;

        public RollingHistory(int falloffSize)
        {
            _historicItems = new Queue<T>();
            _falloffSize = falloffSize;
        }

        public RollingHistory(SerializationInfo info, StreamingContext context)
        {
            T[] historicalValues = (T[])info.GetValue("HistoricalItems", typeof(T[]));
        }

        public void AddHistoricItem(T itemToAdd)
        {
            _historicItems.Enqueue(itemToAdd);

            if(_historicItems.Count > _falloffSize)
            {
                _historicItems.Dequeue();
            }
        }

        public T[] GetHistoricItems()
        {
            return _historicItems.ToArray();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("HistoricalItems", GetHistoricItems());
        }
    }
}
