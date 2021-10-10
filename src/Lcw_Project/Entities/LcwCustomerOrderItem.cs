using System;
using System.Collections.Generic;

#nullable disable

namespace Lcw_Project.Entities
{
    public partial class LcwCustomerOrderItem
    {
        public long Id { get; set; }
        public long CustomerOrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } 

        public virtual LcwCustomerOrder CustomerOrder { get; set; }
        public virtual LcwProduct Product { get; set; }
    }
}
