using Structure.Model.DocumentTypes;
using Structure.Model.ViewModel.Shared;
using Structure.Services.Interfaces;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Structure.Services.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Func<IPublishedContent, bool> _visible = x => x.IsVisible();

        public NavigationVM GetPrimaryNavigation(IPublishedContent model)
        {
            if (model == null) return null;
                
            var homepage = model.AncestorOrSelf<HomePage>();
            if (homepage == null) return null;

            var primaryNavigation = homepage?.Children(x => x.IsVisible());
            if (primaryNavigation == null) return null;

            return new NavigationVM
            {
                CurrentPage = model,
                Items = primaryNavigation.Select(x => Map(x, model))
            };
        }

        public NavigationVM GetSiteMap(IPublishedContent model)
        {
            if (model == null) return null;

            var homepage = model.AncestorOrSelf<HomePage>();
            if (homepage == null) return null;

            var siteMap = homepage?.Children?.Where(_visible);

            return new NavigationVM
            {
                CurrentPage = model,
                Items = siteMap.Select(x => Map(x, model))
            };
        }

        public NavigationVM GetBreadcrumbs(IPublishedContent model)
        {
            if (model == null) return null;

            var breadcrumbs = model
                .AncestorsOrSelf()?
                .Reverse();

            if (breadcrumbs == null) return null;

            return new NavigationVM
            {
                CurrentPage = model,
                Items = breadcrumbs.Select(x => Map(x, model))
            };
        }

        private NavigationItemVM Map(IPublishedContent model, IPublishedContent currentPage)
        {
            var item = new NavigationItemVM
            {
                Url = model.Url,
                TemplateId = model.TemplateId,
                Title = model.Name, //model.HasValue("title") ? model.Value<string>("title") : model.Name,
                IsAncestor = model.IsAncestorOrSelf(currentPage),
                IsCurrent = model.Id == currentPage.Id
            };

            if (model is INavigationControls)
                item.HideChildrenFromNavigation = NavigationControls.GetHideChildrenFromNavigation(model as INavigationControls);

            if (model.Children?.Any() ?? false)
            {
                item.HasChildren = true;
                item.Children = model.Children?
                    .Where(_visible)?
                    .Select(x => Map(x, currentPage));
            }
            return item;
        }
    }
}
