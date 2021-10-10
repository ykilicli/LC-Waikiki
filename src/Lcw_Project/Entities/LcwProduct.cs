using System;
using System.Collections.Generic;

#nullable disable

namespace Lcw_Project.Entities
{
    public partial class LcwProduct
    {
        public LcwProduct()
        {
            LcwCustomerOrderItems = new HashSet<LcwCustomerOrderItem>();
        }

        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }

        public virtual ICollection<LcwCustomerOrderItem> LcwCustomerOrderItems { get; set; }
    }
}
