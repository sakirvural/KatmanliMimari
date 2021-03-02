
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerResponse : ResponseBase
    {
        public CustomerContract customerContractResponse { get; set; }

        public List<CustomerContract> customerContractsList;

        public List<CustomerPhoneNumber> customerPhoneNumbers { get; set; }

        public List<CustomerEmail> customerEmails { get; set; }

        public List<CustomerAddress> customerAddressS { get; set; }

        public List<CustomerAccount> customerAccountList { get; set; }

        public List<CustomerMoneyTransferHistory> customerMoneyTransferHistoryList { get; set; }

        public WebServisContcract webServisContcractResponse { get; set; }

        public int id { get; set; }

        public int customerAccountCount { get; set; }

        public decimal accountBalance { get; set; }
    }
}
