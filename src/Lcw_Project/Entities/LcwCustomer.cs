using System;
using System.Collections.Generic;

#nullable disable

namespace Lcw_Project.Entities
{
    public partial class LcwCustomer
    {
        public LcwCustomer()
        {
            LcwCustomerOrders = new HashSet<LcwCustomerOrder>();
        }

        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<LcwCustomerOrder> LcwCustomerOrders { get; set; }
    }
}
