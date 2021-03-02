using BOA.Business.Banking.Abstract;
using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
   
    public class Customer: ICustomerDal
    {

        Common common = new Common();

        SqlParameter[] parameters { get; set; }

        string spName { get; set; }
     
        
      
        //müşteri kaydet
        public CustomerResponse CustomerSave(CustomerContract customer)
        {

               var response = new CustomerResponse();

            
                spName = "CUS.ins_Customer";

                parameters = new SqlParameter[]
                {
                new SqlParameter("@customerName", customer.customerNameSurname),
                new SqlParameter("@customerSurname", customer.customerSurname),
                new SqlParameter("@TC", customer.TC),
                new SqlParameter("@birthPlace", customer.birthPlace),
                new SqlParameter("@birthDate", customer.birthDate),
                new SqlParameter("@motherName", customer.motherName),
                new SqlParameter("@fatherName", customer.fatherName),
                new SqlParameter("@education", customer.education),
                new SqlParameter("@job", customer.job)
               };
               
                var read =common.spExecuteScalar(spName, parameters);

            if ((read is Boolean)!= true)
            {
                response.id = (int)read;
                response.IsSuccess = true;
            }

            else
            {
                response.IsSuccess = false;
            }
                
                return response;

        }

        //müşteri güncelle
        public CustomerResponse CustomerUpdate(CustomerContract customer)
        {

            CustomerResponse response = new CustomerResponse();


             spName = "CUS.upd_Customer";

              parameters = new SqlParameter[]
                {
               new SqlParameter("@ID", customer.ID),
               new SqlParameter("@customerName", customer.customerNameSurname),
               new SqlParameter("@customerSurname", customer.customerSurname),
               new SqlParameter("@TC", customer.TC),
               new SqlParameter("@birthPlace", customer.birthPlace),
               new SqlParameter("@birthDate", customer.birthDate),
               new SqlParameter("@motherName", customer.motherName),
               new SqlParameter("@fatherName", customer.fatherName),
               new SqlParameter("@education", customer.education),
               new SqlParameter("@job", customer.job)
                };

              response.IsSuccess = common.SpExecute(spName, parameters);

              return response;
           
        }

        //ID göre müşteri bilgilerini getir
        public CustomerResponse CustomerIdRead(int ID)
        {


            CustomerResponse response = new CustomerResponse();


            spName = "CUS.sel_Customer_ID";

            parameters = new SqlParameter[] { new SqlParameter("@ID", ID) };


                SqlDataReader reader = common.GetData(spName, parameters);

                CustomerContract customer = new CustomerContract();


                while (reader.Read())
                {
                    customer = new CustomerContract
                    {
                        customerNameSurname = reader["customerName"].ToString(),
                        customerSurname = reader["customerSurname"].ToString(),
                        TC = reader["TC"].ToString(),
                        birthPlace = reader["birthPlace"].ToString(),
                        birthDate = (DateTime)reader["birthDate"],
                        motherName = reader["motherName"].ToString(),
                        fatherName = reader["fatherName"].ToString(),
                        education = reader["education"].ToString(),
                        job = reader["job"].ToString()

                    };

                }

                response.customerContractResponse = customer;

             reader.Close();
            common.CloseConnection();

            return response;
            
        }
        //müşteri sil
        public CustomerResponse CustomerDelete(CustomerContract customer)
        {

              CustomerResponse response = new CustomerResponse();
           
                spName = "CUS.del_Customer";
                parameters = new SqlParameter[] { new SqlParameter("@ID", customer.ID) };
                
                response.IsSuccess = common.SpExecute(spName,parameters);
              
                return response;  

        }

        //filtreye göre müşteri oku
        public CustomerResponse CustomerFilterRead(CustomerContract request)
        {
            var response = new CustomerResponse();

            spName = "CUS.sel_Customer_filtre";


           parameters = new SqlParameter[]
           {
          new SqlParameter("@customerNo",request.customerNo),
          new SqlParameter("@TC",request.TC),
          new SqlParameter("@customerNameSurname",request.customerNameSurname)
           };


          SqlDataReader reader = common.GetData(spName, parameters);


            List<CustomerContract> customers = new List<CustomerContract>();

            while (reader.Read())
            {
                CustomerContract customer = new CustomerContract
                {

                    ID = Convert.ToInt32(reader["ID"]),
                    customerNo = reader["customerNo"].ToString(),
                    customerNameSurname = reader["NameSurname"].ToString(),
                    TC = reader["TC"].ToString(),
                    birthPlace = reader["birthPlace"].ToString(),
                    birthDate = (DateTime)reader["birthDate"],
                    motherName = reader["motherName"].ToString(),
                    fatherName = reader["fatherName"].ToString()
                    //education = reader["education"].ToString(),
                    //knowledge = reader["knowledge"].ToString()

                };
                customers.Add(customer);

            }
            reader.Close();
            common.CloseConnection();
            response.customerContractsList = customers;

            return response;
        }

        //ekstra adres eposta telefon girişi

        //numara ekle
        public CustomerResponse NumberAdd(CustomerPhoneNumber request)
        {
            var response = new CustomerResponse();

            spName = "CUS.ins_Customer_Phone";

            parameters = new SqlParameter[]
            {
                 new SqlParameter("@cusID", request.cusID),
                 new SqlParameter("@phoneNumber", request.phoneNumber),
                 new SqlParameter("@phoneType", request.phoneType)
            };

           
            response.IsSuccess = common.SpExecute(spName, parameters);

            return response;
           

        }
        //mail ekle
        public CustomerResponse EmailAdd(CustomerEmail request)
        {
            var response = new CustomerResponse();

            spName = "CUS.ins_Customer_Email";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@cusID", request.cusID),
                new SqlParameter("@email", request.email),
                new SqlParameter("@emailType", request.emailType)
              };
           
             response.IsSuccess = common.SpExecute(spName,parameters);
              
                return response;
            

        }
        //telefon ekle
        public CustomerResponse AdressAdd(CustomerAddress request)
        {
            var response = new CustomerResponse();

            spName = "CUS.ins_Customer_Address";

            parameters = new SqlParameter[]
            {
            new SqlParameter("@cusID", request.cusID),
            new SqlParameter("@address", request.address),
            new SqlParameter("@addressType", request.addressType)
             };
                    
              
                
                response.IsSuccess = common.SpExecute(spName,parameters);
               
                return response;
            


        }
        //ekstra adres eposta telefon girişi


        //adres telefon e posta okuma
        public CustomerResponse CustomerPhoneNumberRead(int cusID)
        {
            var response = new CustomerResponse();

            spName = "CUS.sel_Customer_Phone";

         


            parameters = new SqlParameter[] { new SqlParameter("@cusID", cusID) };

            SqlDataReader reader = common.GetData(spName, parameters);


            List<CustomerPhoneNumber> customerPhoneNumbers = new List<CustomerPhoneNumber>();

            while (reader.Read())
            {
                CustomerPhoneNumber customerPhoneNumber = new CustomerPhoneNumber
                {
                    phoneType = reader["phoneType"].ToString(),
                    phoneNumber = reader["phoneNumber"].ToString()

                    //cusID =Convert.ToInt32( reader["cusID"])
                };
                customerPhoneNumbers.Add(customerPhoneNumber);

            }
            reader.Close();


            response.customerPhoneNumbers = customerPhoneNumbers;
            common.CloseConnection();
            return response;

        }
        //mail oku
        public CustomerResponse CustomerMailRead(int cusID)
        {
            var response = new CustomerResponse();

            spName = "CUS.sel_Customer_Email";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@cusID", cusID)
            };

            SqlDataReader reader = common.GetData(spName, parameters);


            List<CustomerEmail> customerEmails = new List<CustomerEmail>();

            while (reader.Read())
            {
                CustomerEmail customerEmail = new CustomerEmail
                {
                    emailType = reader["emailType"].ToString(),
                    email = reader["email"].ToString()

                    //cusID = Convert.ToInt32(reader["cusID"])
                };
                customerEmails.Add(customerEmail);

            }
            reader.Close();
            common.CloseConnection();
            response.customerEmails = customerEmails;

            return response;

        }
        //adres oku
        public CustomerResponse CustomerAddressRead(int cusID)
        {
            var response = new CustomerResponse();

            spName = "CUS.sel_Customer_Address";

            parameters = new SqlParameter[]
            {
             new SqlParameter("@cusID", cusID)
            };


            SqlDataReader reader = common.GetData(spName, parameters);


            List<CustomerAddress> customerAddressS = new List<CustomerAddress>();

            while (reader.Read())
            {
                CustomerAddress customerAddress = new CustomerAddress
                {
                    addressType = reader["addressType"].ToString(),
                    address = reader["address"].ToString()

                    //cusID = Convert.ToInt32(reader["cusID"])
                };
                customerAddressS.Add(customerAddress);

            }
            reader.Close();
            common.CloseConnection();
            response.customerAddressS = customerAddressS;

            return response;

        }
        //adres telefon e posta okuma




        //adres telefon e posta sil
        public CustomerResponse PhoneNumberDelete(string phoneNumber)
        {

            CustomerResponse response = new CustomerResponse();
            spName = "CUS.del_Customer_Phone";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@phoneNumber", phoneNumber)
            };
                
              
                response.IsSuccess = common.SpExecute(spName,parameters);
                return response;
            
            
                
            

        }

        //mail sil
        public CustomerResponse MailDelete(string mail)
        {

            CustomerResponse response = new CustomerResponse();
            spName = "CUS.del_Customer_Email";
            parameters = new SqlParameter[]
            {
                new SqlParameter("@email", mail)
            };
           
          
          
            response.IsSuccess = common.SpExecute(spName, parameters);
            return response;


        }

        //adres sil
        public CustomerResponse AddressDelete(string address)
        {

            CustomerResponse response = new CustomerResponse();
            spName = "CUS.del_Customer_Address";
            parameters = new SqlParameter[]
            {
                new SqlParameter("@address", address)
            };
     
                response.IsSuccess = common.SpExecute(spName, parameters);
                return response;
          

        }

        //adres telefon e posta sil






        //böyle bir  hesap numarası var mı kontrolu varsa +1 olacak şekilde döndür
        public CustomerResponse Customer_Account_Count_Read(int accountNumber)
        {
            var response = new CustomerResponse();
            spName = "CUS.sel_Customer_Account_Count";

            parameters = new SqlParameter[]
            {
             new SqlParameter("@accountNumber", accountNumber)
            };
            

            var read = common.spExecuteScalar(spName,parameters);

            if (read != null)
            {
                response.customerAccountCount =Convert.ToInt32(read);
                response.IsSuccess = true;
            }

            else
            {
                response.IsSuccess = false;
            }
            

           

            return response;
        }

        //müşterinin tüm hesapları getir
        public CustomerResponse Customer_AccountNumber_Read(int accountNumber)
        {
            var response = new CustomerResponse();
            spName = "CUS.sel_Customer_Account";


            parameters = new SqlParameter[]
            {
               new SqlParameter("@accountNumber", accountNumber)
            };
           


            SqlDataReader reader = common.GetData(spName, parameters) ;
           
            
            List<CustomerAccount> customerAccounts = new List<CustomerAccount>();

            while (reader.Read())
            {
                CustomerAccount customerAccount = new CustomerAccount
                {

                    //parameterID = Convert.ToInt32(reader["parameterID"]),
                    active = (bool)reader["active"],
                    accountNumber = Convert.ToInt32(reader["accountNumber"]),
                    accountNumberExtra = Convert.ToInt32(reader["accountNumberExtra"]),
                    accountBalance = (decimal)reader["accountBalance"],
                    //accountBalance = (double)reader["accountBalance"],
                    currencyType = reader["currencyType"].ToString(),
                    openDate = reader["openDate"].ToString(),
                    closeDate = reader["closeDate"].ToString()

                };

                customerAccounts.Add(customerAccount);

            }
            reader.Close();
            common.CloseConnection();
            response.customerAccountList = customerAccounts;


            return response;
        }

        //yeni müşteri ekle
        public CustomerResponse Customer_Account_Add(int accountNumber, string currencyType)
        {
            var response = new CustomerResponse();
            spName = "CUS.ins_Customer_Account";




            parameters = new SqlParameter[]
            {
                new SqlParameter("@accountNumber", accountNumber),
                new SqlParameter("@currencyType", currencyType)
            };
          

          
            response.IsSuccess = common.SpExecute(spName,parameters);
           
         


            return response;
        }




        //müşteri pasifleştir
        public CustomerResponse Customer_Account_Delete(int accountNumberExtra, int accountNumber)
        {
            var response = new CustomerResponse();
            spName = "CUS.del_Customer_Account";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@accountNumberExtra", accountNumberExtra), 
                new SqlParameter("@accountNumber", accountNumber)
            };


            response.IsSuccess =common.SpExecute(spName,parameters);

            return response;
        }

        //para çek yatır
        public CustomerResponse Customer_AccountBalance_Update(int accountNumber, int accountNumberExtra, decimal accountBalance, string explanation, string WhoPerson)
        {


            CustomerResponse response = new CustomerResponse();
            spName = "CUS.upd_Customer_AccountBalance";

            parameters = new SqlParameter[]
            {
             new SqlParameter("@accountNumber", accountNumber),
             new SqlParameter("@accountNumberExtra", accountNumberExtra),
             new SqlParameter("@accountBalance", accountBalance),
             new SqlParameter("@explanation", explanation),
             new SqlParameter("@WhoPerson", WhoPerson)
            };
            
        

            var read = common.spExecuteScalar(spName,parameters);

            if (read != null)
            {
                response.accountBalance = (decimal)read;
                response.IsSuccess = true;
            }

            else
            {
                response.IsSuccess = false;
            }

     

            return response;

        }


        //para transfer geçmişi
        public CustomerResponse MoneyTransferHistory_Read(int accountNumber, int accountNumberExtra)
        {
            var response = new CustomerResponse();
            spName = "CUS.sel_MoneyTransferHistory_Read";

            parameters = new SqlParameter[]
            {
             new SqlParameter("@accountNumber", accountNumber),
             new SqlParameter("@accountNumberExtra", accountNumberExtra)
            };


            SqlDataReader reader = common.GetData(spName,parameters);


            List<CustomerMoneyTransferHistory> historys = new List<CustomerMoneyTransferHistory>();

            while (reader.Read())
            {
                CustomerMoneyTransferHistory history = new CustomerMoneyTransferHistory
                {

                    //parameterID = Convert.ToInt32(reader["parameterID"]),
                    historyID = Convert.ToInt32(reader["historyID"]),
                    accountNumber = Convert.ToInt32(reader["accountNumber"]),
                    accountNumberExtra = reader["accountNumberExtra"].ToString(),
                    explanation = reader["explanation"].ToString(),
                    WhoPerson = reader["WhoPerson"].ToString(),
                    transferHistory = (DateTime)reader["transferHistory"]


                };

                historys.Add(history);

            }
            reader.Close();
            common.CloseConnection();


            response.customerMoneyTransferHistoryList = historys;

            return response;
        }

    }
}
