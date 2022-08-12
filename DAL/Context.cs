using System;
using System.IO;
using Newtonsoft.Json;

namespace DAL
{
    public class Context
    {
        public ContextData ContextData { get; set; }
        
        private string Path = AppDomain.CurrentDomain.BaseDirectory + "data.json";

        
        public Context()
        {
            if (!File.Exists(Path))
                File.Create(Path).Close();
        }
        
        
        public void JSON_Serialize()
        {
            using (StreamWriter saver = new StreamWriter(Path))
            {
                saver.WriteLine(JsonConvert.SerializeObject(ContextData, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
        }
        

        public void JSON_DeSerialize()
        {
            using (StreamReader loader = new StreamReader(Path))
            {
                ContextData = JsonConvert.DeserializeObject<ContextData>(loader.ReadToEnd(), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }

            if (ContextData == null)
            {
                ContextData = new ContextData();
            }
        }
    }
}