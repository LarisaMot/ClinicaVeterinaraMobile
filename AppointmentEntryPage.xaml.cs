using ClinicaVeterinaraMobile.Models;

namespace ClinicaVeterinaraMobile
{
    public partial class AppointmentEntryPage : ContentPage
    {
        public AppointmentEntryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vets = await App.Database.GetVetsAsync();
            vetPicker.ItemsSource = vets;
            var appointment = (Appointment)BindingContext;
            if (appointment != null && appointment.VetID != 0)
            {
                vetPicker.SelectedItem = vets.FirstOrDefault(v => v.ID == appointment.VetID);
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var appointment = (Appointment)BindingContext;

            if (string.IsNullOrWhiteSpace(appointment.NumePacient) || string.IsNullOrWhiteSpace(appointment.Motiv))
            {
                await DisplayAlert("Eroare", "Scrie numele pacientului si motivul!", "OK");
                return;
            }

            var selectedVet = vetPicker.SelectedItem as Vet;
            if (selectedVet == null)
            {
                await DisplayAlert("Atentie", "Selecteaza un medic!", "OK");
                return;
            }

            appointment.VetID = selectedVet.ID;
            appointment.UserId = Preferences.Get("CurrentUserId", 0);
            await App.Database.SaveAppointmentAsync(appointment);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var appointment = (Appointment)BindingContext;
            if (appointment.ID != 0)
            {
                await App.Database.DeleteAppointmentAsync(appointment);
            }
            await Navigation.PopAsync();
        }
    }
}