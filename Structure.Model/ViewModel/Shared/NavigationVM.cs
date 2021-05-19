using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace Structure.Model.ViewModel.Shared
{
    public class NavigationVM
    {
        public IPublishedContent CurrentPage { get; set; }
        public IEnumerable<NavigationItemVM> Items { get; set; }
    }
}
