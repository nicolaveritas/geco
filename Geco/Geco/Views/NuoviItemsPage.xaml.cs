using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Geco.Models;
using Geco.Views;
using Geco.ViewModels;
using XLabs;
using XLabs.Forms.Controls;

namespace Geco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuoviItemsPage : ContentPage
    {
        NuoviItemsViewModel viewModel;

        public NuoviItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NuoviItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            // Manually deselect item.
            ItemsListView.SelectedItem = null;
            
            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            ItemsListView.IsVisible = false;
            Indicator.IsVisible = true;
            await Task.Delay(1700);
            ItemsListView.IsVisible = true;
            Indicator.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            foreach (var item in viewModel.Items)
            {
                if (item.Checked)
                {
                    Selected.Add(item);
                }
            }
            foreach (var item in Selected)
            {
                viewModel.Items.Remove(item);
            }
        }

        private List<Item> Selected = new List<Item>();

        private void CheckBox_OnCheckedChanged(object sender, EventArgs<bool> e)
        {
            var item = viewModel.Items.FirstOrDefault(i => i.Text == (sender as CheckBox)?.Text);
            if (item != null)
                item.Checked = e.Value;
            
        }

        private async void ClickGestureRecognizer_OnClicked(object sender, EventArgs e)
        {
            ItemsListView.SelectedItem = null;
            var item = viewModel.Items.FirstOrDefault(i => i.Text == (sender as Image)?.ClassId);
            var delete = await DisplayAlert($"Rimuovere {item?.Text}?", "L'asset verrà rimosso dalla configurazione", "Rimuovi", "Annulla");
            if (delete)
            {
                viewModel.Items.Remove(item);
            }
        }

        private async void ClickGestureRecognizerDetail_OnClicked(object sender, EventArgs e)
        {
            ItemsListView.SelectedItem = null;
            var item = viewModel.Items.FirstOrDefault(i => i.Text == (sender as Image)?.ClassId);
            await Navigation.PushAsync(new ItemDetailPage(item?.Text));
        }
    }
}