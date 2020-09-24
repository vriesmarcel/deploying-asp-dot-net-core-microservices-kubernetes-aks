using GloboTicket.MobileApp.ViewModels;

namespace GloboTicket.MobileApp.Utility
{
    public static class ViewModelLocator
    {
        public static EventOverviewViewModel EventOverviewViewModel { get; set; } = new EventOverviewViewModel(App.EventDataServie, App.NavigationService);
        public static EventDetailViewModel EventDetailViewModel { get; set; } = new EventDetailViewModel();
    }
}
