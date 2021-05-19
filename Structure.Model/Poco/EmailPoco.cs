using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model.Poco
{
    public class EmailPoco
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public string CCEmail { get; set; }

        public string BCCEmail { get; set; }

        public DateTime Date { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
