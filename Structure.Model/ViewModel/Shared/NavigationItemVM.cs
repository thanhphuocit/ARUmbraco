using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model.ViewModel.Shared
{
    public class NavigationItemVM
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public int? TemplateId { get; set; }
        public bool HideChildrenFromNavigation { get; set; }
        public IEnumerable<NavigationItemVM> Children { get; set; }
        public bool IsAncestor { get; set; }
        public bool HasChildren { get; set; }
        public bool IsCurrent { get; set; }
    }
}
