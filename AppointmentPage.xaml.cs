using ClinicaVeterinaraMobile.Models;

namespace ClinicaVeterinaraMobile
{
    public partial class AppointmentPage : ContentPage
    {
        private List<Appointment> _allAppointments;

        public AppointmentPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _allAppointments = await App.Database.GetAppointmentsAsync();

            listView.ItemsSource = _allAppointments;
        }

        async void OnAddAppointmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentEntryPage
            {
                BindingContext = new Appointment()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AppointmentEntryPage
                {
                    BindingContext = e.SelectedItem as Appointment
                });
            }
        }

        void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string textCautat = e.NewTextValue;

            if (_allAppointments == null) return;

            if (string.IsNullOrWhiteSpace(textCautat))
            {

                listView.ItemsSource = _allAppointments;
            }
            else
            {
                listView.ItemsSource = _allAppointments
                    .Where(x => x.NumePacient != null && x.NumePacient.ToLower().Contains(textCautat.ToLower()))
                    .ToList();
            }
        }
    }
}