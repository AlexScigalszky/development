using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Example.Utilities
{
    public class Serializer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static public string Serialize2JSON(Object o)
        {
            string ses = "ERROR al serializar el objecto";
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                // se serializa todo el hashset
                ses = JsonConvert.SerializeObject(o, settings);
            }
            catch (JsonSerializationException jse)
            {
                logger.Error(jse);
            }
            catch (Exception jse)
            {
                logger.Error(jse);
            }
            return ses;
        }

        static public T DeserializeFromJSON<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return (T)JsonConvert.DeserializeObject(json, settings);
        }

        static public object DeserializeFromJSONToDinamicObject(string json)
        {
            return JObject.Parse(json);
        }

        static public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            FileStream f = File.Create(fileName, 6400, FileOptions.None);
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, serializableObject);
            ms.Flush();
            byte[] bytes = ms.ToArray();
            f.Write(bytes, 0, bytes.Count());
            f.Flush();
            f.Close();
        }

        static public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            BinaryFormatter bf = new BinaryFormatter();
            byte[] objBytes = File.ReadAllBytes(fileName);
            MemoryStream ms = new MemoryStream(objBytes);
            
            objectOut = (T)bf.Deserialize(ms);
            return objectOut;
        }
    }
}
