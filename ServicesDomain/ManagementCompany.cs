using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDomain
{
    public class ManagementCompany : BaseEntity
    {
        public ICollection<Service> ServiceList { get; set; }
    }
}
