using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.ViewModel.CustomerOrderModel
{
    public class CustomerOrderListResponseModel
    {
        public long orderId { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string customerPhone { get; set; }
        public string orderDate { get; set; } 

    } 
}
