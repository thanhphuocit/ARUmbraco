using Umbraco.Core;
using Umbraco.Core.Composing;

using Structure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Structure.Core.Extensions;
using Structure.Model.DocumentTypes;
using Structure.Model.ViewModel.MasterPage;


namespace Structure.Core.ModelsBuilder
{
    public class MasterBuilder : IBuilder<Master>
    {
        private readonly INavigationService _navigationService;
        public MasterBuilder(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }
        public Master Build(Master model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var homePage = model.GetHomepage();
            if (homePage == null) return model;

            model.Homepage = homePage;
            return model;
        }
        private MetaDataVM MapMetaData(Master model)
        {
            var viewModel = new MetaDataVM
            {
                FacebookPixelCode = model.Homepage.FacebookPixelCode,
                GoogleSiteVerificationCode = model.Homepage.GoogleSiteVerificationCode,
                GoogleTagManagerCode = model.Homepage.GoogleTagManagerCode
            };

            if (model is IMetaDataControls)
            {
                viewModel.PageTitle = MetaDataControls.GetPageTitle(model);
                viewModel.MetaDescription = MetaDataControls.GetMetaDescription(model);
                viewModel.MetaKeywords = string.Join(",", MetaDataControls.GetMetaKeywords(model));
                // viewModel.MetaImage = MetaDataControls.GetMetaImage(model);
            }

            return viewModel;
        }

        private HeaderVM MapHeader(Master model)
        {
            Dictionary<string, string> mutilLange = new Dictionary<string, string>();
            foreach (var culture in model.Cultures)
            {
                mutilLange.Add(culture.Key, model.Url(culture.Key, UrlMode.Absolute));
            }

            var viewModel = new HeaderVM
            {
                HomepageUrl = model.Homepage.Url,
                SiteName = model.Homepage.Name,
                SmallIconSite = model.Homepage.SmallIconSite as Image,
                LogoTop = model.Homepage.LogoTop as Image,
                PrimaryNavigation = _navigationService.GetPrimaryNavigation(model),
                MultiLanguage = mutilLange
            };

            return viewModel;
        }

        private FooterVM MapFooter(Master model)
        {
            var urlNode = FooterContentControls.GetContactPageLink(model.Homepage).FirstOrDefault();
            List<IconLink> socialItems = FooterContentControls.GetLinksSocial(model.Homepage).ToList();
            List<SocialLinkVM> socialLinks = (from socialItem in socialItems
                                              select new SocialLinkVM
                                              {
                                                  TitleLink = socialItem.TitleLink,
                                                  Tooltip = socialItem.Tooltip,
                                                  ClassIcon = socialItem.ClassIcon,
                                                  LinkUrl = socialItem?.LinkUrl?.Url
                                              }).ToList();

            return new FooterVM
            {
                RootPage = model.Homepage,
                HomepageUrl = model.Homepage.Url,
                Copyright = model.Homepage.Copyright,
                TitleAboutUs = model.Homepage.TitleAboutUsfooter,
                ShortDescriptionAbout = model.Homepage.ShortDescription,
                Contact = new ContactVM
                {
                    TitleContact = model.Homepage.TitleContactSection,
                    Address = FooterContentControls.GetAddress(model.Homepage),
                    Email = FooterContentControls.GetEmail(model.Homepage),
                    Phone = FooterContentControls.GetPhone(model.Homepage),
                    ContactPageURL = urlNode?.Url,
                    ContactPageName = urlNode?.Name
                },
                SocialLinks = socialLinks
            };
        }

        private BreadcrumbsVM MapBreadcrumbs(Master model)
        {
            return new BreadcrumbsVM
            {
                Breadcrumbs = _navigationService.GetBreadcrumbs(model)
            };
        }
    }
}
