namespace JsonConfig
{
    public class SingletonJsonConfig<T> where T : class, new()
    {
        #region Singleton
        private static T config;

        protected SingletonJsonConfig() { }

        public static T Config
        {
            get
            {
                if (config == null)
                    config = new T();

                return config;
            }
            private set
            {
                config = value;
            }
        }
        #endregion Singleton

#pragma warning disable RECS0108 // Warns about static fields in generic types
        private static string _filename = ""; // There will be one instance of this per type
#pragma warning restore RECS0108 // Warns about static fields in generic types


		/// <summary>
		/// Load config file
		/// </summary>
		/// <param name="filename">Path to file</param>
		/// <returns>Returns true if default config was used</returns>
		public bool Load(string filename)
        {
			_filename = filename;

			var result = FileInteractor.Load<T>(filename);
            config = result.Item2;

			return result.Item1;
        }

		/// <summary>
		/// Save config file
		/// </summary>
		public void Save()
        {
            FileInteractor.Save(_filename, config);
        }
    }
}
