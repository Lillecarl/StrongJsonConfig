namespace JsonConfig
{
    public static class ConfigMgr<T> where T : new()
    {
#pragma warning disable RECS0108 // Warns about static fields in generic types
        private static string _filename = ""; // There will be one instance of this per type
#pragma warning restore RECS0108 // Warns about static fields in generic types

        public static T Config { get; private set; }

        /// <summary>
        /// Load config file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns>Returns true if default config was used</returns>
        public static bool Load(string filename)
        {
            _filename = filename;

            var result = FileInteractor.Load<T>(filename);
            Config = result.Value;

            return result.Key;
        }

        /// <summary>
        /// Save config file
        /// </summary>
        public static void Save()
        {
            FileInteractor.Save(_filename, Config);
        }
    }
}
