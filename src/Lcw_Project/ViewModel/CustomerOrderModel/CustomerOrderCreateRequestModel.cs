using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.ViewModel.CustomerOrderModel
{
    public class CustomerOrderCreateRequestModel
    {
        public long customerId { get; set; }
        public string address { get; set; }
        public DateTime date { get; set; }
        public List<CustomerOrderItemCreateRequestModel> orderItems { get; set; }
    }

    public class CustomerOrderItemCreateRequestModel
    {
        public long id { get; set; }
        public long productId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}
