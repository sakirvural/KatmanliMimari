
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerRequest : RequestBase
    {
        public CustomerContract customerContract { get; set; }

        public CustomerPhoneNumber customerPhoneNumber { get; set; }

        public CustomerEmail customerEmail { get; set; }

        public CustomerAddress customerAddress { get; set; }

        public CustomerAccount customerAccount { get; set; }

        public CustomerMoneyTransferHistory customerMoneyTransferHistory { get; set; }
        public WebServisContcract webServisContcract { get; set; }
    }
}
