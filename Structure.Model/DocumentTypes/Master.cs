using Structure.Model.ViewModel.MasterPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model.DocumentTypes
{
   public partial class Master
    {
        public HomePage Homepage { get; set; }
        public HeaderVM Header { get; set; }
        public FooterVM Footer { get; set; }
        public MetaDataVM MetaData { get; set; }
        public BreadcrumbsVM Breadcrumbs { get; set; }
        public int CachedPartialDuration { get; set; }
        public string CachePrefix { get; set; }
    }
}
