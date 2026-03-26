using Cigral.Services;
using Microsoft.Extensions.Configuration;

namespace Cigral
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Login>(); 

            IConfiguration config = builder.Build();

            // 2. Inyectar el secreto en la clase estática
            ApiServices.AuthHeaderKey = config["AuthHeader:Key"];

            Application.Run(new Login());
        }
    }
}