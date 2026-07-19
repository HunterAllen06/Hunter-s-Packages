using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace HunterAllen.SaveSystem
{
    public class FileDataHandler : IDataSaveHandler
    {
        string _dataPath = "";
        
        public void Save<T>(T t, string fileName)
        {
            string fullPath = Path.Combine(_dataPath, fileName + ".dat");
            Debug.Log($"Attempting to save {typeof(T).Name} to {fullPath}...");

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataAsJson = JsonUtility.ToJson(t, true);
                string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataAsJson));

                using (FileStream stream = new(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new(stream))
                    {
                        writer.Write(data);
                    }
                }
            }
            catch (IOException e)
            {
                Debug.LogError($"Error while creating file at {fullPath}\n{e}");
            }
        }

        public T Load<T>(string fileName)
        {
            string fullPath = Path.Combine(_dataPath, fileName + ".dat");

            T data = default;

            if (File.Exists(fullPath))
            {
                try
                {
                    string encryptedData = "";

                    Debug.Log($"Found {typeof(T).Name} at {fullPath}, loading...");
                    using (FileStream stream = new(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new(stream))
                        {
                            encryptedData = reader.ReadToEnd();
                        }
                    }

                    string dataAsJson = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
                    Debug.Log($"Converting {typeof(T).Name} from Json...");
                    data = JsonUtility.FromJson<T>(dataAsJson);
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error while reading file at {fullPath}\n{e}");
                }
            }

            return data;
        }

        public FileDataHandler(string filePath)
        {
            _dataPath = filePath;
        }
    }
}