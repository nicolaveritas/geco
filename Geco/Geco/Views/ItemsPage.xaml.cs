﻿using System;
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
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
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

        private void CheckBoxAll_OnCheckedChanged(object sender, EventArgs<bool> e)
        {
            var sel = viewModel.Items.Any(i => i.Checked);
            if (!sel)
            {
                CheckBoxAll.DefaultText = "Seleziona tutti";
                foreach (var item in viewModel.Items)
                {
                    item.Checked = false;
                }
            }
            else
            {
                CheckBoxAll.DefaultText = "Deseleziona tutti";
                foreach (var item in viewModel.Items)
                {
                    item.Checked = true;
                }
            }
        }
    }
}