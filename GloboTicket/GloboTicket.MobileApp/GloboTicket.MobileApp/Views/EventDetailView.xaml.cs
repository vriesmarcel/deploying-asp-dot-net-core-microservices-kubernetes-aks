using GloboTicket.MobileApp.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GloboTicket.MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailView : ContentPage
	{
		public EventDetailView ()
		{
			InitializeComponent ();
			
			BindingContext = ViewModelLocator.EventDetailViewModel;
		}
	}
}