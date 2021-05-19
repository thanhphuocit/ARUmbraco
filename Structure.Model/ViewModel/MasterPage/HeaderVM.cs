using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Model.DocumentTypes;
using Structure.Model.ViewModel.Shared;

namespace Structure.Model.ViewModel.MasterPage
{
    public class HeaderVM
    {
        public string SiteName { get; set; }
        public Image SmallIconSite { get; set; }
        public Image LogoTop { get; set; }
        public string HomepageUrl { get; set; }
        public NavigationVM PrimaryNavigation { get; set; }
        public Dictionary<string, string> MultiLanguage { get; set; }
    }
}
