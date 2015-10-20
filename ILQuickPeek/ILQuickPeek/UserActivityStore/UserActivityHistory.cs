using System;
using System.Runtime.Serialization;

namespace ILQuickPeek.UserActivityStore
{
    [Serializable]
    public class UserActivityHistory : ISerializable
    {
        public SingleEntryHistory<string> OpenFileDialogLastPath { get; set; }

        public UserActivityHistory()
        {
            OpenFileDialogLastPath = new SingleEntryHistory<string>();
        }

        public UserActivityHistory(SerializationInfo info, StreamingContext context)
        {
            OpenFileDialogLastPath = (SingleEntryHistory<string>)info.GetValue("OpenFileDialogLastPath", typeof(SingleEntryHistory<string>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OpenFileDialogLastPath", OpenFileDialogLastPath);
        }
    }
}
