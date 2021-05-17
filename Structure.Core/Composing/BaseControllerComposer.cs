using Structure.Core.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Structure.Core.Composing
{
   public class BaseControllerComposer: IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.SetDefaultRenderMvcController(typeof(MasterController));
        }
    }
}
