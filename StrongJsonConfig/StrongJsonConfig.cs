using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConfig
{
    public static class ConfigMgr<T> where T : new()
    {
        private static string _filename = "";

        public static T Config { get; private set; }

        /// <summary>
        /// Load config file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns>Returns true if default config was used</returns>
        public static bool Load(string filename)
        {
            bool defconf = false;

            _filename = filename;

            Config = new T();
            if (File.Exists(filename))
            {
                var oldtext = File.ReadAllText(filename);

                try
                {
                    if (oldtext != "")
                        Config = JsonConvert.DeserializeObject<T>(oldtext);
                }
                catch (JsonSerializationException)
                {
                    var bakname = string.Format("{0}.bak", filename);
                    for (int i = 0; File.Exists(bakname); ++i)
                        bakname = string.Format("{0}.bak{1}", filename, i);

                    Console.WriteLine("{0} contains invalid JSON, copying to {1} and overwriting.", filename, bakname);
                    File.Move(filename, bakname);
                    defconf = true;
                }

                var newtext = JsonConvert.SerializeObject(Config, Formatting.Indented);

                if (oldtext != newtext)
                    File.WriteAllText(filename, newtext);

                Config = JsonConvert.DeserializeObject<T>(newtext);
            }
            else
                File.WriteAllText(filename, JsonConvert.SerializeObject(Config, Formatting.Indented));

            return defconf;
        }

        /// <summary>
        /// Save config file
        /// </summary>
        public static void Save()
        {
            string filejson = File.ReadAllText(_filename);
            string configjson = JsonConvert.SerializeObject(Config, Formatting.Indented);

            if (filejson != configjson)
                File.WriteAllText(_filename, configjson);
        }
    }
}
