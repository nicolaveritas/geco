using System;
using System.Collections.Generic;
using System.Text;

namespace Geco.Models
{
    public enum MenuItemType
    {
        Assets,
        NuoviAssets,
        Data,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
