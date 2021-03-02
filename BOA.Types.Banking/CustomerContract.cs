using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    
    public class CustomerContract
    {
        public int ID { get; set; }
        public string customerNo { get; set; }
        public string customerNameSurname { get; set; }
        public string customerSurname { get; set; }
        public string TC { get; set; }
        public string birthPlace { get; set; }
        public DateTime birthDate { get; set; }
        public string motherName { get; set; }
        public string fatherName { get; set; }
        public string education { get; set; }
        public string job { get; set; }

    }

    //interface ICustomerPhoneNumber
    //{
    //     string phoneNumber { get; set; }
    //}
    //interface ICustomerMail
    //{
    //     string mail { get; set; }
    //}
    //interface ICustomerAddress
    //{
    //     string address { get; set; }
    //}


    public class CustomerPhoneNumber    /*:ICustomerPhoneNumber*/
    {
        public int cusID { get; set; }
        public string phoneType { get; set; }
        public string phoneNumber { get; set; }
    }

    public class CustomerEmail   /*:ICustomerMail*/
    {
        public int cusID { get; set; }
        public string emailType { get; set; }
        public string email { get; set; }
    }

    public class CustomerAddress    /*:ICustomerAddress*/
    {
        public int cusID { get; set; }
        public string addressType { get; set; }
        public string address { get; set; }
    }

    public class CustomerAccount    /*:ICustomerAddress*/
    {
        //public int parameterID { get; set; }
        public bool active { get; set; }
        public int accountNumber { get; set; }
        public int accountNumberExtra { get; set; }
        //public decimal accountBalance { get; set; }
        public decimal accountBalance { get; set; }
        public string currencyType { get; set; }
        public string openDate { get; set; }
        public string closeDate { get; set; }  
    }

    public class CustomerMoneyTransferHistory
    {
        public int historyID { get; set; }  
        public int accountNumber { get; set; }
        public string accountNumberExtra { get; set; }
        public string explanation { get; set; }
        public string WhoPerson{ get; set; }
        public DateTime transferHistory { get; set; }
    }


    public class WebServisContcract
    {
        public string ExtUName { get; set; }
        public string ExtUPassword { get; set; }
        public int AccountNumber { get; set; }
        public short AccountSuffix { get; set; }
    }



}
