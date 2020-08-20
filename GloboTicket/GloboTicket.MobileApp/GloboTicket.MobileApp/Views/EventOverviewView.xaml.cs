using GloboTicket.MobileApp.Utility;
using GloboTicket.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GloboTicket.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventOverviewView : ContentPage
    {
        private readonly EventOverviewViewModel eventOverviewViewModel;

		public EventOverviewView()
		{
			InitializeComponent ();
            
			eventOverviewViewModel = ViewModelLocator.EventOverviewViewModel;

            this.BindingContext = eventOverviewViewModel;
        }

        protected override async void OnAppearing()
        {
            await eventOverviewViewModel.OnAppearing();
        }
	}
}