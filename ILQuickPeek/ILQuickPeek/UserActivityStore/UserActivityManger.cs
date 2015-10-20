using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ILQuickPeek.UserActivityStore
{
    public static class UserActivityManger
    {
        public static UserActivityHistory History { get; }

        static UserActivityManger()
        {
            if(File.Exists("history.bin"))
            {
                using (FileStream historyStream = new FileStream("history.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IFormatter historyFormater = new BinaryFormatter();
                    History = (UserActivityHistory)historyFormater.Deserialize(historyStream);
                }
            }
            else
            {
                History = new UserActivityHistory();
            }
        }

        public static void PersistHistory()
        {
            if(File.Exists("history.bin"))
            {
                File.Delete("history.bin");
            }

            using (FileStream historyStream = new FileStream("history.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter historyFormater = new BinaryFormatter();
                historyFormater.Serialize(historyStream, History);
            }
        }
    }
}
