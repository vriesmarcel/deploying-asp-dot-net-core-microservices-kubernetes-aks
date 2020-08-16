using System.ComponentModel;
using Xamarin.Forms;
using GloboTicket.Mobile.ViewModels;

namespace GloboTicket.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}