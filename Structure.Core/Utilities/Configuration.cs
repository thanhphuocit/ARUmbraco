using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Structure.Core.Utilities
{
    public class Configuration : Singleton<Configuration>
    {
        private string _errorFrom;
        private string _errorFromName;
        private string _errorTo;

        public Configuration()
        {
            _errorFrom = ConfigurationManager.AppSettings["errorFrom"];
            _errorFromName = ConfigurationManager.AppSettings["errorFromName"];
            _errorTo = ConfigurationManager.AppSettings["errorTo"];
        }

        public string ErrorFrom
        {
            get
            {
                return _errorFrom;
            }
        }

        public string ErrorFromName
        {
            get
            {
                return _errorFromName;
            }
        }

        public string ErrorTo
        {
            get
            {
                return _errorTo;
            }
        }
    }
}
