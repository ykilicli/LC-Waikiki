using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.ViewModel.ProductModel
{
    public class ProductResponseModel
    {
        public long id { get; set; }
        public string productName { get; set; }
        public string barcode { get; set; }
        public double price { get; set; }
    }
}
