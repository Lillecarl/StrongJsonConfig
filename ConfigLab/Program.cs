using System;

using JsonConfig;

using Conf1 = JsonConfig.ConfigMgr<ConfigLab.StaticConfig1>;
using Conf2 = JsonConfig.ConfigMgr<ConfigLab.StaticConfig2>;

namespace ConfigLab
{
    public class Subconfig
	{
		public string sape = "";
		public int iape = 0;
		public double dape = 0;
	}

    public class StaticConfig1
    {
        public string string1 = "";
        public int int1 = 0;
        public double double1 = 0;
        public Subconfig subconfig = new Subconfig();
    }

	public class StaticConfig2
	{
		public string string2 = "";
		public int int2 = 0;
		public double double2 = 0;
		public Subconfig subconfig = new Subconfig();
	}

    public class Conf3 : SingletonJsonConfig<Conf3>
    {
		public string string3 = "";
		public int int3 = 0;
		public double double3 = 0;
		public Subconfig subconfig = new Subconfig();
    }

    public class Conf4 : SingletonJsonConfig<Conf4>
	{
		public string string4 = "";
		public int int4 = 0;
		public double double4 = 0;
		public Subconfig subconfig = new Subconfig();
	}

    public class Program
    {
        public static void Main(string[] args)
        {
            Conf1.Load("Conf1.json");
            Conf1.Config.string1 = "string1";
            Conf1.Save();

			Conf2.Load("Conf2.json");
			Conf2.Config.string2 = "string2";
			Conf2.Save();

            Conf3.Config.Load("Conf3.json");
            Conf3.Config.string3 = "string3";
            Conf3.Config.Save();

			Conf4.Config.Load("Conf4.json");
			Conf4.Config.string4 = "string4";
			Conf4.Config.Save();

            Console.ReadKey();
        }
    }
}
