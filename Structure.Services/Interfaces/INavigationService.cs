using Structure.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace Structure.Services.Interfaces
{
    public interface INavigationService
    {
        NavigationVM GetPrimaryNavigation(IPublishedContent model);
        NavigationVM GetSiteMap(IPublishedContent model);
        NavigationVM GetBreadcrumbs(IPublishedContent model);
    }
}
