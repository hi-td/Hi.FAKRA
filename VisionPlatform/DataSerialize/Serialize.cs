using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATM_Online
{
    class Serialize
    {
        public static void SerializeData(string strFileName, CSerialData serializableObject)
        {
            using (FileStream fs = new FileStream(strFileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, serializableObject);
                fs.Flush();
                fs.Close();
            }
        }

        public static Object UnSerialize(string strFileName)
        {
            CSerialData serializableObject;
            using (FileStream fs = new FileStream(strFileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                serializableObject = (CSerialData)formatter.Deserialize(fs);
                fs.Close();
            }
            return serializableObject;
        }
    }
}
