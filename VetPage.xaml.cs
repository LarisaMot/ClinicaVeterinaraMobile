using ClinicaVeterinaraMobile.Data;
using ClinicaVeterinaraMobile.Models;

namespace ClinicaVeterinaraMobile
{
    public partial class VetPage : ContentPage
    {
        public VetPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            var vets = await App.Database.GetVetsAsync();
            listView.ItemsSource = vets;
        }

        async void OnAddVetClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VetEntryPage
            {
                BindingContext = new Vet()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new VetEntryPage
                {
                    BindingContext = e.SelectedItem as Vet
                });
            }
        }
    }
}