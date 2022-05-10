using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Xray.Services
{
    public class PersistService<TConfig> : IDisposable where TConfig : class
    {
        public TConfig Instance { get; private set; }

        public PersistService()
        {
            if (File.Exists("persist.json"))
            {
                var readAllText = File.ReadAllText("persist.json");
                var config = JsonConvert.DeserializeObject<TConfig>(readAllText);
                if (config != null)
                    Instance = config;
            }
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Instance);
            File.WriteAllText("persist.json", json);
        }

        ~PersistService()
        {
            Dispose();
        }

        public void Dispose()
        {
            Save();
        }
    }
}
