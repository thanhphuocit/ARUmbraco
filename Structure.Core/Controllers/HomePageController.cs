using Structure.Core.Controllers.Base;
using Structure.Model.DocumentTypes;
using Structure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Structure.Core.Controllers
{
   public class HomePageController: MasterController
    {
        public HomePageController(IBuilder<Master> masterBuilder)
            : base(masterBuilder)
        {

        }
        public override ActionResult Index(ContentModel model)
        {
            return base.Index(model);
        }
    }
}
