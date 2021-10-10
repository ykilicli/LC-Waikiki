using Lcw_Project.ViewModel.CustomerModel;
using Lcw_Project.ViewModel.ProductModel;
using System.Collections.Generic;

namespace Lcw_Project.ViewModel.CustomerOrderModel
{
    public class CustomerOrderResponseModel
    {
        public long id { get; set; }
        public CustomerResponseModel customer { get; set; }
        public string address { get; set; }
        public List<CustomerOrderItemResponseModel> orderItems { get; set; }
    }
    public class CustomerOrderItemResponseModel
    {
        public long id { get; set; }
        public ProductResponseModel product { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
  
}
