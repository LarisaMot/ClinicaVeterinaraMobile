namespace ClinicaVeterinaraMobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = await App.Database.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);
            if (user != null)
            {
                Preferences.Set("CurrentUserId", user.ID);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Eroare", "Date incorecte", "OK");
            }
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
    }
}