using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Structure.Model.DocumentTypes;
using Structure.Services.Interfaces;

namespace Structure.Core.ModelsBuilder
{
    public class HomePageBuilder : IBuilder<HomePage>
    {
        public HomePage Build(HomePage model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            return model;
        }
    }
}
