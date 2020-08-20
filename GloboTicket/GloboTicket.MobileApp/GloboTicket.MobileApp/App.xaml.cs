using System;
using GloboTicket.MobileApp.Models;
using GloboTicket.MobileApp.Services;
using GloboTicket.MobileApp.Utility;
using GloboTicket.MobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GloboTicket.MobileApp
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();
        public static IEventDataService EventDataServie { get; set; } = new EventDataService(new EventRepository());

        public App()
        {
            InitializeComponent();

            NavigationService.Configure(ViewNames.EventOverviewView, typeof(EventOverviewView));
            NavigationService.Configure(ViewNames.EventDetailView, typeof(EventDetailView));

            MainPage = new NavigationPage(new EventOverviewView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
