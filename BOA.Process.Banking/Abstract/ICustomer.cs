using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOA.Process.Banking;
using BOA.Types.Banking;

namespace BOA.Process.Banking.Abstract
{
    public interface ICustomer
    {
        CustomerResponse CustomerSave(CustomerRequest request);
        CustomerResponse CustomerUpdate(CustomerRequest request);
        CustomerResponse CustomerIdRead(CustomerRequest request);
        CustomerResponse CustomerFilterRead(CustomerRequest request);
        CustomerResponse CustomerDelete(CustomerRequest request);
        CustomerResponse NumberAdd(CustomerRequest request);
        CustomerResponse EmailAdd(CustomerRequest request);
        CustomerResponse AdressAdd(CustomerRequest request);
        CustomerResponse PhoneNumberDelete(CustomerRequest request);
        CustomerResponse MailDelete(CustomerRequest request);
        CustomerResponse AddressDelete(CustomerRequest request);
        CustomerResponse CustomerContactRead(CustomerRequest request);
        CustomerResponse Customer_Account_Read(CustomerRequest request);
        CustomerResponse Customer_Account_Add(CustomerRequest request);
        CustomerResponse Customer_Account_Delete(CustomerRequest request);
        CustomerResponse Customer_AccountBalance_Update(CustomerRequest request);
        CustomerResponse WebServis_Control(CustomerRequest request);
    }
}
