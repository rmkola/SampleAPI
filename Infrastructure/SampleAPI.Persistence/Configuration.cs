using Microsoft.Extensions.Configuration;

namespace SampleAPI.Persistence
{
    static class Configuration
    {
        /// <summary>
        /// Veritabanı bağlantısını appsettings.json dosyasından okur.
        /// DefaultConnection adında bir connectionstrings olması gereklidir.
        /// </summary>
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/SampleAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
