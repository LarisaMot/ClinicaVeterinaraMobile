using ClinicaVeterinaraMobile.Models;
using ClinicaVeterinaraMobile.Data;

namespace ClinicaVeterinaraMobile
{
    public partial class VetEntryPage : ContentPage
    {
        public VetEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var vet = (Vet)BindingContext;

            if (string.IsNullOrWhiteSpace(vet.VetName))
            {
                await DisplayAlert("Eroare", "Scrie numele medicului!", "OK");
                return;
            }

            await App.Database.SaveVetAsync(vet);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var vet = (Vet)BindingContext;

            if (vet.ID != 0)
            {
                await App.Database.DeleteVetAsync(vet);
            }

            await Navigation.PopAsync();
        }

        async void OnMapClicked(object sender, EventArgs e)
        {
            var vet = (Vet)BindingContext;

            if (!string.IsNullOrEmpty(vet.AdresaCabinet))
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    var adresaPtUrl = Uri.EscapeDataString(vet.AdresaCabinet);
                    await Launcher.OpenAsync($"https://www.google.com/maps/search/?api=1&query={adresaPtUrl}");
                }
                else
                {
                    await Launcher.OpenAsync($"geo:0,0?q={vet.AdresaCabinet}");
                }
            }
            else
            {
                await DisplayAlert("Eroare", "Acest medic nu are o adresa setata!", "OK");
            }
        }
    }
}