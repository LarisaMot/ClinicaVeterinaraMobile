using ClinicaVeterinaraMobile.Models;
using Plugin.LocalNotification;

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
            var selectedVet = vetPicker.SelectedItem as Vet;

            if (selectedVet == null)
            {
                await DisplayAlert("Eroare", "Trebuie sa aloci un medic!", "OK");
                return;
            }

            appointment.VetID = selectedVet.ID;


            await App.Database.SaveAppointmentAsync(appointment);


            if (appointment.DataProgramare > DateTime.Now)
            {
                var request = new NotificationRequest
                {
                    NotificationId = appointment.ID,
                    Title = $"🔔 Urmeaza: {appointment.NumePacient}",
                    Description = $"Stapan: {appointment.NumeStapan} ({appointment.TelefonStapan})\nLa Dr. {selectedVet.VetName}",
                    Schedule = new NotificationRequestSchedule
                    {

                        NotifyTime = appointment.DataProgramare.AddMinutes(-15)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }

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