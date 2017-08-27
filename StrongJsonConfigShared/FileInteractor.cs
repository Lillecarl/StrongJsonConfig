using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonConfig
{
    internal static class FileInteractor
    {
        public static KeyValuePair<bool, T> Load<T>(string filename) where T : new()
        {
			bool defconf = false;

			T Config = new T();
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

            return new KeyValuePair<bool, T>(defconf, Config);
        }

        public static void Save(string filename, object Config)
        {
            string filejson = "";

            if (File.Exists(filename))
                filejson = File.ReadAllText(filename);
            
			string configjson = JsonConvert.SerializeObject(Config, Formatting.Indented);

			if (filejson != configjson)
				File.WriteAllText(filename, configjson);
        }
    }
}
