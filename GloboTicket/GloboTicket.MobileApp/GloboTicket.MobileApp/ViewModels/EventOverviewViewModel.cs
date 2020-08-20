using GloboTicket.MobileApp.Models;
using GloboTicket.MobileApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GloboTicket.MobileApp.ViewModels
{
    public class EventOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Event> events;

        private readonly IEventDataService eventDataService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged("Events");
            }
        }

        public EventOverviewViewModel(IEventDataService eventDataService, INavigationService navigationService)
        {
            this.eventDataService = eventDataService;
            _navigationService = navigationService;

            LoadCommand = new Command(OnLoadCommand);
            EventSelectedCommand = new Command<Event>(OnEventSelectedCommand);

            Events = new ObservableCollection<Event>();

        }

        public ICommand LoadCommand { get; }
        public ICommand EventSelectedCommand { get; }

        public async void OnLoadCommand()
        {
            Events = new ObservableCollection<Event>(await eventDataService.GetAllEventsAsync(false));
        }

        private void OnEventSelectedCommand(Event ev)
        {
            _navigationService.NavigateTo("EventDetailView", ev);
        }
    }
}
