using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Model;
using Umbraco.Core.Models.PublishedContent;

namespace Structure.Services.Interfaces
{
    public interface IBuilder<T> where T : IPublishedContent
    {
        T Build(T model);
    }
}


