using Structure.Model.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Structure.Core.Extensions
{
    public static class  PublishedContentExtensions
    {
        public static HomePage GetHomepage(this IPublishedContent content)
            => content.AncestorOrSelf<HomePage>();
    }
}
