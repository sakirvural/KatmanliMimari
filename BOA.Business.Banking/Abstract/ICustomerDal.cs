using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking.Abstract
{
    public interface ICustomerDal
    {

        CustomerResponse CustomerSave(CustomerContract customer);
        CustomerResponse CustomerUpdate(CustomerContract customer);
        CustomerResponse CustomerIdRead(int ID);
        CustomerResponse CustomerDelete(CustomerContract customer);
        CustomerResponse CustomerFilterRead(CustomerContract request);
        CustomerResponse NumberAdd(CustomerPhoneNumber request);
        CustomerResponse EmailAdd(CustomerEmail request);
        CustomerResponse AdressAdd(CustomerAddress request);
        CustomerResponse CustomerPhoneNumberRead(int cusID);
        CustomerResponse CustomerMailRead(int cusID);
        CustomerResponse CustomerAddressRead(int cusID);
        CustomerResponse PhoneNumberDelete(string phoneNumber);
        CustomerResponse MailDelete(string mail);
        CustomerResponse AddressDelete(string address);
        CustomerResponse Customer_Account_Count_Read(int accountNumber);
        CustomerResponse Customer_AccountNumber_Read(int accountNumber);
        CustomerResponse Customer_Account_Add(int accountNumber, string currencyType);
        CustomerResponse Customer_Account_Delete(int accountNumberExtra, int accountNumber);
        CustomerResponse Customer_AccountBalance_Update(int accountNumber, int accountNumberExtra, decimal accountBalance, string explanation, string WhoPerson);
        CustomerResponse MoneyTransferHistory_Read(int accountNumber, int accountNumberExtra);
    }
}
