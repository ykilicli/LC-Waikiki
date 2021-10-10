using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.ViewModel.CustomerOrderModel
{
    public class CustomerOrderUpdateRequestModel
    {
        public string address { get; set; }
        public List<CustomerOrderItemUpdateRequestModel> orderItems { get; set; }
    }
    public class CustomerOrderItemUpdateRequestModel
    {
        public long id { get; set; }
        public long productId { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
}
