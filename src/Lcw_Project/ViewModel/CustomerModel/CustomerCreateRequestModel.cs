using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.ViewModel.CustomerModel
{
    public class CustomerCreateRequestModel
    {
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
