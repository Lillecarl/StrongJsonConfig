using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JsonConfig;

using Conf1 = JsonConfig.ConfigMgr<ConfigLab.Config>;

namespace ConfigLab
{
    public class Config
    {
        public string string1 = "";
        public int int1 = 0;
        public double double1 = 0;
        public Config_Ape ape1 = new Config_Ape();
    }

    public class Config_Ape
    {
        public string string2 = "";
        public int int2 = 0;
        public double double2 = 0;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Conf1.Load("config.json");
            Conf1.Save();

            Console.ReadKey();
        }
    }
}
