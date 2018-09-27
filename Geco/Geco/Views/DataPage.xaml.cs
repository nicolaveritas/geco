using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Geco.Models;
using Geco.Views;
using Geco.ViewModels;

namespace Geco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataPage : ContentPage
    {
        DataViewModel viewModel;

        public DataPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DataViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            ItemsListView.IsVisible = false;
            Indicator.IsVisible = true;
            UploadBtn.IsEnabled = false;
            await Task.Delay(1700);
            ItemsListView.IsVisible = true;
            Indicator.IsVisible = false;
            UploadBtn.IsEnabled = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            UploadBtn.IsEnabled = false;
            await ProgressBar.ProgressTo (1, 1000, Easing.Linear);
            ProgressBar.Progress = 0;
            UploadBtn.IsEnabled = true;
            viewModel.Items.Clear();
        }
    }
}