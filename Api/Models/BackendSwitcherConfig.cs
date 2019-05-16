using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class BackendSwitcherConfig
    {
        public List<Application> Applications { get; set; }
        public List<Environment> Environments { get; set; }
    }

    public class Application
    {
        public string Name { get; set; }
        public string TemplatePath { get; set; }
        public string OriginalPath { get; set; }
    }

    public class Environment
    {
        private string _FilePath;

        public string Name { get; set; }
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (value != null && value != _FilePath)
                {
                    _FilePath = value;
                    Placeholders = LoadPlaceholders(value);
                }
            }
        }
        public Dictionary<string, string> Placeholders { get; set; }

        private Dictionary<string, string> LoadPlaceholders(string FilePath)
        {
            using (var reader = new StreamReader(FilePath))
            {
                string content = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            }
        }
    }
}
