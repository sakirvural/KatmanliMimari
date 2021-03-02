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
    public class CustomerDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=BOA;integrated security=true ");

        

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        
      
        //müşteri kaydet
        public CustomerResponse CustomerSave(CustomerContract customer)
        {

            
            CustomerResponse response = new CustomerResponse();

            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.ins_Customer", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@customerName", customer.customerNameSurname);
                command.Parameters.AddWithValue("@customerSurname", customer.customerSurname);
                command.Parameters.AddWithValue("@TC", customer.TC);
                command.Parameters.AddWithValue("@birthPlace", customer.birthPlace);
                command.Parameters.AddWithValue("@birthDate", customer.birthDate);
                command.Parameters.AddWithValue("@motherName", customer.motherName);
                command.Parameters.AddWithValue("@fatherName", customer.fatherName);
                command.Parameters.AddWithValue("@education", customer.education);
                command.Parameters.AddWithValue("@knowledge", customer.knowledge);
                //command.Parameters.AddWithValue("@phoneNumber", customerphoneNumber.phoneNumber);
                //command.Parameters.AddWithValue("@mail", customerMail.mail);
                //command.Parameters.AddWithValue("@address", customerAddress.address);
                var read =Convert.ToInt32(command.ExecuteScalar());
                _connection.Close();



                response.id = read;

                response.IsSuccess = true ;

                return response;
                 }
            catch
            { 
                response.IsSuccess = false;
                return response;
            }

        }

        //müşteri güncelle
        public CustomerResponse CustomerUpdate(CustomerContract customer)
        {


            CustomerResponse response = new CustomerResponse();

            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.upd_Customer", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", customer.ID);
                command.Parameters.AddWithValue("@customerName", customer.customerNameSurname);
                command.Parameters.AddWithValue("@customerSurname", customer.customerSurname);
                command.Parameters.AddWithValue("@TC", customer.TC);
                command.Parameters.AddWithValue("@birthPlace", customer.birthPlace);
                command.Parameters.AddWithValue("@birthDate", customer.birthDate);
                command.Parameters.AddWithValue("@motherName", customer.motherName);
                command.Parameters.AddWithValue("@fatherName", customer.fatherName);
                command.Parameters.AddWithValue("@education", customer.education);
                command.Parameters.AddWithValue("@knowledge", customer.knowledge);

                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;

                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }
        }

        //müşteri sil
            public CustomerResponse CusDelete(CustomerContract customer)
             {
           
            CustomerResponse response = new CustomerResponse();
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.del_Customer", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ID", customer.ID);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }

            }

        //filtreye göre müşteri oku
        public List<CustomerContract> CustomerFilterRead(CustomerContract request)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Customer_filtre", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@customerNo", request.customerNo);
            command.Parameters.AddWithValue("@TC", request.TC);
            command.Parameters.AddWithValue("@customerNameSurname", request.customerNameSurname);

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerContract> customers = new List<CustomerContract>();

            while (reader.Read())
            {
                CustomerContract customer = new CustomerContract
                {

                    ID = Convert.ToInt32(reader["ID"]),
                    customerNo = reader["customerNo"].ToString(),
                    customerNameSurname = reader["Adsoyad"].ToString(),
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
            _connection.Close();

          

            return customers;
        }

        //ekstra adres eposta telefon girişi
        CustomerResponse response = new CustomerResponse();
        //numara ekle
        public CustomerResponse NumberAdd(CustomerPhoneNumber request)
        {
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.ins_Customer_Phone", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@cusID", request.cusID);
                command.Parameters.AddWithValue("@phoneNumber", request.phoneNumber);
                command.Parameters.AddWithValue("@tur", request.phoneType);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }
          
        }
        //mail ekle
        public CustomerResponse EpostaAdd(CustomerMail request)
        {
            try
            {
             ConnectionControl();
            SqlCommand command = new SqlCommand("CUS.ins_Customer_Eposta", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@cusID", request.cusID);
            command.Parameters.AddWithValue("@mail", request.mail);
            command.Parameters.AddWithValue("@tur", request.mailType);
                command.ExecuteNonQuery();
            _connection.Close();
            response.IsSuccess = true;
            return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }
           
        }
        //telefon ekle
        public CustomerResponse AdressAdd(CustomerAddress request)
        {
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.ins_Customer_Address", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@cusID", request.cusID);
                command.Parameters.AddWithValue("@address", request.address);
                command.Parameters.AddWithValue("@tur", request.addressType);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
              
                response.IsSuccess = false;
                return response;
            }
           
            
        }
        //ekstra adres eposta telefon girişi


        //adres telefon e posta okuma
        public List<CustomerPhoneNumber> CustomerPhoneNumberRead(int cusID)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Customer_Phone", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("cusID", cusID);

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerPhoneNumber> customerPhoneNumbers = new List<CustomerPhoneNumber>();

            while (reader.Read())
            {
                CustomerPhoneNumber customerPhoneNumber = new CustomerPhoneNumber
                {
                    phoneNumber = reader["phoneNumber"].ToString(),
                    phoneType = reader["tur"].ToString(),
                    cusID =Convert.ToInt32( reader["cusID"])
                };
                customerPhoneNumbers.Add(customerPhoneNumber);

            }
            reader.Close();
            _connection.Close();



            return customerPhoneNumbers;

        }
        //mail oku
        public List<CustomerMail> CustomerMailRead(int cusID)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Customer_Eposta", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("cusID", cusID);

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerMail> customerMails = new List<CustomerMail>();

            while (reader.Read())
            {
                CustomerMail customerMail = new CustomerMail
                {
                    mail = reader["mail"].ToString(),
                    mailType = reader["tur"].ToString(),
                    cusID = Convert.ToInt32(reader["cusID"])
                };
                customerMails.Add(customerMail);

            }
            reader.Close();
            _connection.Close();



            return customerMails;

        }
        //adres oku
        public List<CustomerAddress> CustomerAddressRead(int cusID)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Customer_Address", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("cusID", cusID);

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerAddress> customerAddressS = new List<CustomerAddress>();

            while (reader.Read())
            {
                CustomerAddress customerAddress = new CustomerAddress
                {
                  
                    address = reader["address"].ToString(),
                    addressType = reader["tur"].ToString(),
                    cusID = Convert.ToInt32(reader["cusID"])
                };
                customerAddressS.Add(customerAddress);

            }
            reader.Close();
            _connection.Close();



            return customerAddressS;

        }
        //adres telefon e posta okuma




        //adres telefon e posta sil
        public CustomerResponse phoneNumberDelete(string phoneNumber)
        {

            CustomerResponse response = new CustomerResponse();
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.del_Customer_Phone", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }

        }

        //mail sil
        public CustomerResponse mailDelete(string mail)
        {

            CustomerResponse response = new CustomerResponse();
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.del_Customer_Eposta", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@mail", mail);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }

        }

        //adres sil
        public CustomerResponse adresDelete(string address)
        {

            CustomerResponse response = new CustomerResponse();
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.del_Customer_Address", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@address", address);
                command.ExecuteNonQuery();
                _connection.Close();
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.IsSuccess = false;
                return response;
            }

        }

        //adres telefon e posta sil


        //ID göre müşteri bilgilerini getir
        public List<CustomerContract> CustomerRead(int ID)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Customer", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID", ID);
            

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerContract> customers = new List<CustomerContract>();
            while (reader.Read())
            {
                CustomerContract customer  = new CustomerContract
                {
                    customerNameSurname = reader["customerName"].ToString(),
                    customerSurname = reader["customerSurname"].ToString(),
                    TC = reader["TC"].ToString(),
                    birthPlace = reader["birthPlace"].ToString(),
                    birthDate = (DateTime)reader["birthDate"],
                    motherName = reader["motherName"].ToString(),
                    fatherName = reader["fatherName"].ToString(),
                    education = reader["education"].ToString(),
                    knowledge = reader["knowledge"].ToString()

                };
                customers.Add(customer);
            }

            
               
            
            reader.Close();
            _connection.Close();



            return customers;
        }


        //böyle bir  hesap numarası var mı kontrolu varsa +1 olacak şekilde döndür
        public CustomerResponse CustomerParameter_AccountCount(int accountNumber)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Parameter_Account_Count", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@accountNumber", accountNumber);


            var reader = command.ExecuteScalar().ToString();
            response.parameterCount = reader;

            _connection.Close();

            return response;
        }

        //yeni müşteri ekle
        public CustomerResponse CustomerParameter_Add(int accountNumber,string currencyType)
        {
            ConnectionControl();

            //try
            //{
                SqlCommand command = new SqlCommand("CUS.ins_Parameter", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@accountNumber", accountNumber);
                command.Parameters.AddWithValue("@currencyType", currencyType);

                command.ExecuteNonQuery();
                response.IsSuccess = true;
                _connection.Close();
            //}
            //catch
            //{
            //    response.IsSuccess = false;
            //}
            

            return response;
        }

        //müşterinin tüm hesapları getir
        public List<CustomerParameter> CustomerParameter_AccountNumber_Read(int accountNumber)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_Parameter", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@accountNumber", accountNumber);
        

            SqlDataReader reader = command.ExecuteReader();


            List<CustomerParameter> parameters = new List<CustomerParameter>();

            while (reader.Read())
            {
                CustomerParameter parameter = new CustomerParameter
                {

                    //parameterID = Convert.ToInt32(reader["parameterID"]),
                    active = (bool)reader["active"],
                    accountNumber = Convert.ToInt32(reader["accountNumber"]),
                    accountNumberExtra = reader["accountNumberExtra"].ToString(),
                    accountBalance = (decimal)reader["accountBalance"],
                    currencyType = reader["currencyType"].ToString(),
                    openDate =  reader["openDate"].ToString(),
                    closeDate = reader["closeDate"].ToString()

                };
                
                parameters.Add(parameter);

            }
            reader.Close();
            _connection.Close();



            return parameters;
        }

        //müşteri pasifleştir
        public CustomerResponse CustomerParameter_Delete(string accountNumberExtra,int accountNumber)
        {
            ConnectionControl();

           
                SqlCommand command = new SqlCommand("CUS.del_Parameter", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@accountNumberExtra", accountNumberExtra);
                command.Parameters.AddWithValue("@accountNumber", accountNumber);

            command.ExecuteNonQuery();

            response.IsSuccess = true;
                _connection.Close();
            
            


            return response;
        }

        //para çek yatır
        public CustomerResponse AccountBalance_Update(int accountNumber,string accountNumberExtra,Decimal accountBalance,string explanation,string WhoPerson)
        {


            CustomerResponse response = new CustomerResponse();

          
                ConnectionControl();
                SqlCommand command = new SqlCommand("CUS.upd_Parameter_AccountBalance", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@accountNumber", accountNumber);
                command.Parameters.AddWithValue("@accountNumberExtra", accountNumberExtra);
                command.Parameters.AddWithValue("@accountBalance", accountBalance);
                command.Parameters.AddWithValue("@explanation", explanation);
                command.Parameters.AddWithValue("@WhoPerson", WhoPerson);

                var balance = command.ExecuteScalar();
                _connection.Close();
                response.accountBalance =(decimal)balance;

                return response;
 
        }


        //para transfer geçmişi
        public List<CustomerMoneyTransferHistory> MoneyTransferHistory_Read(int accountNumber,string accountNumberExtra)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("CUS.sel_MoneyTransferHistory_Read", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@accountNumber", accountNumber);
            command.Parameters.AddWithValue("@accountNumberExtra", accountNumberExtra);

            SqlDataReader reader = command.ExecuteReader();


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
            _connection.Close();

            return  historys;
        }

    }
}
