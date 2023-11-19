using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SerializeData
{
    public Hashtable Data = new Hashtable();
}

namespace GameJamLibrary
{

    public class IOData
    {

        public static SerializeData _Data = new SerializeData();

        public static void SaveData<T>(string FileName, string Key, T Value)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs =
                File.Open(
                Application.persistentDataPath + "/" + FileName + ".dat",
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.ReadWrite);

            _Data.Data[Key] = Value;

            bf.Serialize(fs, _Data);
            fs.Close();

        }

        public static T LoadData<T>(string FileName, string Key)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs =
                File.Open(Application.persistentDataPath + "/" + FileName + ".dat",
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.ReadWrite);

            T result = default(T);

            if (fs != null)
            {

                try
                {

                    SerializeData pd = (SerializeData)bf.Deserialize(fs);

                    result = (pd.Data[Key] == null) ? default(T) : (T)pd.Data[Key];

                }
                catch(Exception e)
                {

                }

            }

            fs.Close();
            return result;

        }

        public static bool isFile(string path) { return File.Exists(Application.persistentDataPath + "/" + path); }

    }

}
