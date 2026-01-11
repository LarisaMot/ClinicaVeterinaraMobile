using ClinicaVeterinaraMobile.Data;
using System.IO;

namespace ClinicaVeterinaraMobile
{
    public partial class App : Application
    {
      
        static ClinicaDatabase database;

        
        public static ClinicaDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Clinica.db3");
                    database = new ClinicaDatabase(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}