using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Structure.Model
{
    public class BaseModel : ContentModel
    {
        public BaseModel(IPublishedContent content) : base(content) { }

        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
