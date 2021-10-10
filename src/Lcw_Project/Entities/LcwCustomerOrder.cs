using System;
using System.Collections.Generic;

#nullable disable

namespace Lcw_Project.Entities
{
    public partial class LcwCustomerOrder
    {
        public LcwCustomerOrder()
        {
            LcwCustomerOrderItems = new HashSet<LcwCustomerOrderItem>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }

        public virtual LcwCustomer Customer { get; set; }
        public virtual ICollection<LcwCustomerOrderItem> LcwCustomerOrderItems { get; set; }
    }
}
