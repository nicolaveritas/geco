using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Geco.Models;
using Geco.ViewModels;

namespace Geco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage(string name)
        {
            InitializeComponent();

            var item = new Item
            {
                Text = name,
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Anagrafica.IsVisible = true;
            AnagraficaLabel.FontAttributes = FontAttributes.Bold;
            AnagraficaLabel.TextColor = Color.Black;
            Settings.IsVisible = false;
            SettingsLabel.FontAttributes = FontAttributes.None;
            SettingsLabel.TextColor = Color.DarkGray;
        }
        
        private void TapGestureRecognizerSettings_OnTapped(object sender, EventArgs e)
        {
            Anagrafica.IsVisible = false;
            AnagraficaLabel.FontAttributes = FontAttributes.None;
            AnagraficaLabel.TextColor = Color.DarkGray;
            Settings.IsVisible = true;
            SettingsLabel.FontAttributes = FontAttributes.Bold;
            SettingsLabel.TextColor = Color.Black;
        }
    }
}