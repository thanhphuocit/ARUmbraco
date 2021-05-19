using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Model.DocumentTypes;

namespace Structure.Model.ViewModel.MasterPage
{
    public class FooterVM
    {
        public HomePage RootPage { get; set; }
        public string HomepageUrl { get; set; }
        public Image LogoBottom { get; set; }
        public string TitleAboutUs { get; set; }
        public IHtmlString ShortDescriptionAbout { get; set; }
        public string Copyright { get; set; }
        public ContactVM Contact { get; set; }
        public List<SocialLinkVM> SocialLinks { get; set; } = new List<SocialLinkVM>();
    }
}
