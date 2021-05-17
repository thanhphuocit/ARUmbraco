using Umbraco.Core;
using Umbraco.Core.Composing;

using Structure.Core.Extensions;
using Structure.Model.DocumentTypes;
using Structure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Core.ModelsBuilder
{
    public class MasterBuilder : IBuilder<Master>
    {
        public Master Build(Master model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var homePage = model.GetHomepage();
            if (homePage == null) return model;

            model.Homepage = homePage;
            return model;
        }
    }
}
