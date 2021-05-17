using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Umbraco.Core;
using Umbraco.Core.Composing;

using Structure.Core.ModelsBuilder;
using Structure.Model.DocumentTypes;
using Structure.Services.Interfaces;


namespace Structure.Core.Composing
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    class WebComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // builders
            composition.Register<IBuilder<Master>, MasterBuilder>(Lifetime.Request);
            composition.Register<IBuilder<HomePage>, HomePageBuilder>(Lifetime.Request);

            // services
        }
    }
}
