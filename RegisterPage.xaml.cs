using ClinicaVeterinaraMobile.Models;

namespace ClinicaVeterinaraMobile
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Eroare", "Completeaza toate campurile", "OK");
                return;
            }

            var user = new User
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text
            };

            await App.Database.SaveUserAsync(user);
            await DisplayAlert("Succes", "Cont creat!", "OK");
            await Navigation.PopModalAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}