using Structure.Model.DocumentTypes;

namespace Structure.Model.ViewModel.MasterPage
{
    public class MetaDataVM
    {
        public string GoogleTagManagerCode { get; set; }
        public string FacebookPixelCode { get; set; }
        public string GoogleSiteVerificationCode { get; set; }
        public string PageTitle { get; set; }
        public Image MetaImage { get; set; }
        public string MetaName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
