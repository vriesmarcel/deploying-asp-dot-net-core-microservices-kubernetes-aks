using GloboTicket.MobileApp.Models;
using GloboTicket.MobileApp.Services;

namespace GloboTicket.MobileApp.ViewModels
{
    public class EventDetailViewModel : BaseViewModel
    {
        private Event selectedEvent;


        public EventDetailViewModel()
        {
            SelectedEvent = new Event();
        }

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        public override void Initialize(object parameter)
        {
            if (parameter == null)
                SelectedEvent = new Event();
            else
                SelectedEvent = parameter as Event;
        }
    }
}
