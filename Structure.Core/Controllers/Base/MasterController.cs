
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

using Structure.Model.DocumentTypes;
using Structure.Services.Interfaces;


namespace Structure.Core.Controllers.Base
{
   public class MasterController: RenderMvcController
   {
        private readonly IBuilder<Master> _builder;
        public MasterController(IBuilder<Master> builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
        public override ActionResult Index(ContentModel model)
        {
            var viewModel = _builder.Build(model.Content as Master);
            return base.Index(new ContentModel(viewModel));
        }

    }
}
