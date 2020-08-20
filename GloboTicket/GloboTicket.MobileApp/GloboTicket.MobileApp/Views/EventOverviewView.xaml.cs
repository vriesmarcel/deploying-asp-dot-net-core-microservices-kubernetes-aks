using GloboTicket.MobileApp.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GloboTicket.MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventOverviewView : ContentPage
	{
		public EventOverviewView()
		{
			InitializeComponent ();
		    
            this.BindingContext = ViewModelLocator.EventOverviewViewModel;
		}
	}
}