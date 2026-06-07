using System.IO;
using UnityEngine;

namespace HunterAllen.SaveSystem
{
    public class FileDataHandler : IDataHandler
    {
        string _dataPath = "";
        
        public void Save<T>(T t, string nameAndExtension)
        {
            string fullPath = Path.Combine(_dataPath, nameAndExtension);
            Debug.Log($"Attempting to save {typeof(T).Name} to {fullPath}...");

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string data = JsonUtility.ToJson(t, true);

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

        public T Load<T>(string nameAndExtension)
        {
            string fullPath = Path.Combine(_dataPath, nameAndExtension);

            T data = default;

            if (File.Exists(fullPath))
            {
                try
                {
                    string dataAsJson = "";

                    Debug.Log($"Found {typeof(T).Name} at {fullPath}, loading...");
                    using (FileStream stream = new(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new(stream))
                        {
                            dataAsJson = reader.ReadToEnd();
                        }
                    }

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

        public FileDataHandler(string path)
        {
            _dataPath = path;
        }
    }
}