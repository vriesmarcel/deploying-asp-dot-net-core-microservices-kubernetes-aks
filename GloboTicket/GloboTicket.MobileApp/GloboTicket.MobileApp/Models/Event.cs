using System;
using System.ComponentModel;

namespace GloboTicket.MobileApp.Models
{
    public class Event : INotifyPropertyChanged
    {
        private Guid _id;
        private string _eventName;
        private string _description;
        private double _price;
        private string _imageUrl;
        private string _artist;

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _eventName;
            set
            {
                _eventName = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                RaisePropertyChanged(nameof(ImageUrl));
            }
        }

        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
