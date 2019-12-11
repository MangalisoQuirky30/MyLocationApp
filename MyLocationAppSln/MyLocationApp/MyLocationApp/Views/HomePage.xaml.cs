using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyLocationApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
             InitializeComponent();

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-33.933329, 18.6333308), Distance.FromMiles(10)));

        }

    }
}
