using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOA.Business.Banking;
using BOA.Process.Banking.Abstract;

namespace BOA.Process.Banking
{
   
    public class Customer:ICustomer
    {

        //müşteri kaydetme
        public CustomerResponse CustomerSave(CustomerRequest request)
        {
            var customer = new BOA.Business.Banking.Customer();

            var response = new CustomerResponse();

            string tcNo = request.customerContract.TC;
            int toplam = 0; int toplam2 = 0; int toplam3 = 0;

            if (tcNo.Length == 11)
            {
                if (Convert.ToInt32(tcNo[0].ToString()) != 0) //tc kimlik numaranın ilk hanesi 0 değilse
                {
                    for (int i = 0; i < 10; i++)
                    {
                        toplam = toplam + Convert.ToInt32(tcNo[i].ToString());
                        if (i % 2 == 0)
                        {
                            if (i != 10)
                            {
                                toplam2 = toplam2 + Convert.ToInt32(tcNo[i].ToString()); // 7 ile çarpılacak sayıları topluyoruz
                            }

                        }
                        else
                        {
                            if (i != 9)
                            {
                                toplam3 = toplam3 + Convert.ToInt32(tcNo[i].ToString());
                            }
                        }
                    }
                }
                else
                {
                    response.ErrorMessage = "Tc Kimlik Numaranızın ilk hanesi 0 olamaz.";
                    response.IsSuccess = false;
                    return response;
                }
            }
            else
            {
                response.ErrorMessage = "Tc Kimlik Numarınız 11 haneli olmak zorunda.Eksik ya da fazla değer girdiniz.";
                response.IsSuccess = false;
                return response;
            }
            //if (((toplam2 * 7) - toplam3) % 10 == Convert.ToInt32(tcNo[9].ToString()) && toplam % 10 == Convert.ToInt32(tcNo[10].ToString()))
            //{

            response = customer.CustomerSave(request.customerContract);

            return response;
            //}
            //else
            //{
            //    response.ErrorMessage = "Tc Kimlik Numarası Yanlış!";
            //    response.IsSuccess = false;
            //    return response;
            //}



        }

        //müşteri güncelleme
        public CustomerResponse CustomerUpdate(CustomerRequest request)
        {
            var response = new CustomerResponse();

            var customer = new BOA.Business.Banking.Customer();

            response = customer.CustomerUpdate(request.customerContract);

            return response;
        }

        //ID göre müşteri okuma
        public CustomerResponse CustomerIdRead(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response = customer.CustomerIdRead(request.customerContract.ID);

            return response;
        }

        //filtreye göre müşteri okuma
        public CustomerResponse CustomerFilterRead(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response = customer.CustomerFilterRead(request.customerContract);

            return response;
        }

        //müşteri silme
        public CustomerResponse CustomerDelete(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response = customer.CustomerDelete(request.customerContract);

            return response;
        }


        //ekstra adres eposta telefon
        public CustomerResponse NumberAdd(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            if (request.customerPhoneNumber.cusID != 0)
            {
                response = customer.NumberAdd(request.customerPhoneNumber);
                if (response.IsSuccess)
                {


                    response.Message = "Başarılı Numara girişi";

                }
                else
                {

                    response.ErrorMessage = "Aynı numarada veri var";

                }
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "öncelikle müşteri bilgilerini giriniz";
            }


            return response;
        }
        public CustomerResponse EmailAdd(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            if (request.customerEmail.cusID != 0)
            {

                response = customer.EmailAdd(request.customerEmail);
                if (response.IsSuccess)
                {

                    response.Message = "Başarılı Mail girişi";

                }
                else
                {

                    response.ErrorMessage = "Aynı mailde veri var";
                }
            }
            else
            {

                response.ErrorMessage = "öncelikle müşteri bilgilerini giriniz";

            }

            return response;
        }
        public CustomerResponse AdressAdd(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            if (request.customerAddress.cusID != 0)
            {
                if (request.customerAddress.address == "")
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "adres girilmedi";
                    return response;
                }
                response = customer.AdressAdd(request.customerAddress);
                if (response.IsSuccess)
                {

                    response.Message = "Başarılı Adres girişi";

                }
                else
                {

                    response.ErrorMessage = "Aynı adreste veri var";
                }
            }
            else
            {

                response.ErrorMessage = "öncelikle müşteri bilgilerini giriniz";
            }


            return response;
        }
        //ekstra adres eposta telefon

        //telefon email adres silme
        public CustomerResponse PhoneNumberDelete(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            response = customer.PhoneNumberDelete(request.customerPhoneNumber.phoneNumber);
            return response;
        }
        public CustomerResponse MailDelete(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            response = customer.MailDelete(request.customerEmail.email);
            return response;
        }

        public CustomerResponse AddressDelete(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            response = customer.AddressDelete(request.customerAddress.address);
            return response;
        }
        //telefon email adres silme


        //toplu adres bilgi okuma
        public CustomerResponse CustomerContactRead(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response.customerPhoneNumbers = customer.CustomerPhoneNumberRead(request.customerContract.ID).customerPhoneNumbers;
            response.customerEmails = customer.CustomerMailRead(request.customerContract.ID).customerEmails;
            response.customerAddressS = customer.CustomerAddressRead(request.customerContract.ID).customerAddressS;

            return response;
        }


       //hesap okuma
        public CustomerResponse Customer_Account_Read(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();
            response = customer.Customer_Account_Count_Read(request.customerAccount.accountNumber);

            if (response.customerAccountCount.ToString() != "0")
            {
                int customerAccountCount = response.customerAccountCount;
                
                response.customerAccountList = customer.Customer_AccountNumber_Read(request.customerAccount.accountNumber).customerAccountList;
                response.customerAccountCount = customerAccountCount;

                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }

            return response;
        }

        //hesap ekleme
        public CustomerResponse Customer_Account_Add(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response = customer.Customer_Account_Add(request.customerAccount.accountNumber, request.customerAccount.currencyType);

  

            return response;
        }


        //hesap kapatma işlemi
        public CustomerResponse Customer_Account_Delete(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            if (request.customerAccount.active)
            {

                response = customer.Customer_Account_Delete(request.customerAccount.accountNumberExtra, request.customerAccount.accountNumber);

                response.ErrorMessage = "Başarılı Hesap kapama";
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Hesap zaten kapalı";
            }


            return response;
        }



        //bakiye güncelleme sorgulama işlemi
        public CustomerResponse Customer_AccountBalance_Update(CustomerRequest request)
        {
            var response = new CustomerResponse();
            var customer = new BOA.Business.Banking.Customer();

            response = customer.Customer_Account_Count_Read(request.customerAccount.accountNumber);

            string customerAccountCount = response.customerAccountCount.ToString();
            
            if (customerAccountCount == "0")//HESAP KONTROL
            {
                response.ErrorMessage = "Müşteri Bulunamadı";
                response.IsSuccess = false;

            }
            else if (customerAccountCount == "1")//HESAP EK NO KONTROL
            {
                response.ErrorMessage = "Müşteri mevcut fakat hesabı yok";
                response.IsSuccess = false;
            }
            else
            {
                response = new CustomerResponse();
                
                response = customer.Customer_AccountNumber_Read(request.customerAccount.accountNumber);

                foreach (var res in response.customerAccountList)//İLGİLİ MÜŞTEERİ AİT HESAPLARI OKUMA
                {
                    if (res.accountNumberExtra == request.customerAccount.accountNumberExtra && res.currencyType == request.customerAccount.currencyType && res.active==true)
                    {

                        //this.hesapBakiyesi = res.accountBalance;
                        decimal accountBalance = res.accountBalance;

                        if (request.customerMoneyTransferHistory.explanation =="yatırma")
                        {
                            
                            if (request.customerMoneyTransferHistory.WhoPerson == "TC = ")
                            {
                                response.IsSuccess = false;
                                response.ErrorMessage = "Lütfen hesap sizin değilse TC giriniz";
                                return response;
                            }
                                
                            if (request.customerMoneyTransferHistory.WhoPerson == null)
                            {
                                response.IsSuccess = false;
                                response.ErrorMessage = "Lütfen para yatıracak kişiyi giriniz";
                                return response;
                            }

                            request.customerMoneyTransferHistory.explanation = request.customerAccount.accountBalance.ToString() + " " + request.customerAccount.currencyType + " yatırma işlemi yapıldı";

                            response.accountBalance = customer.Customer_AccountBalance_Update(request.customerAccount.accountNumber,
                                                                         request.customerAccount.accountNumberExtra,
                                                                         request.customerAccount.accountBalance,
                                                                         request.customerMoneyTransferHistory.explanation, 
                                                                         request.customerMoneyTransferHistory.WhoPerson).accountBalance;

                            response.customerMoneyTransferHistoryList = customer.MoneyTransferHistory_Read(request.customerAccount.accountNumber, request.customerAccount.accountNumberExtra).customerMoneyTransferHistoryList;

                            response.IsSuccess = true;
                            return response;
                        }
                        if(request.customerMoneyTransferHistory.explanation == "cekme")
                        {
                            
                            if ((accountBalance >= request.customerAccount.accountBalance))
                            {


                               request.customerMoneyTransferHistory.explanation = request.customerAccount.accountBalance.ToString() + " " + request.customerAccount.currencyType + " çekme işlemi yapıldı";
                               request.customerAccount.accountBalance *= -1;

                               response.accountBalance = customer.Customer_AccountBalance_Update(request.customerAccount.accountNumber, 
                                                                             request.customerAccount.accountNumberExtra,
                                                                             request.customerAccount.accountBalance,
                                                                             request.customerMoneyTransferHistory.explanation, 
                                                                             request.customerMoneyTransferHistory.WhoPerson).accountBalance;


                                response.customerMoneyTransferHistoryList = customer.MoneyTransferHistory_Read(request.customerAccount.accountNumber, 
                                                                                                                  request.customerAccount.accountNumberExtra).customerMoneyTransferHistoryList;
                                
                                response.IsSuccess = true;
                                return response;
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.ErrorMessage = "Bakiye Yetersiz";
                                return response;
                            }
                            
                        }

                        response.customerMoneyTransferHistoryList= customer.MoneyTransferHistory_Read(request.customerAccount.accountNumber, request.customerAccount.accountNumberExtra).customerMoneyTransferHistoryList;
                        response.accountBalance = accountBalance;
                            
                            response.IsSuccess = true;

                            break;


                    }
                    else if (res.accountNumberExtra == request.customerAccount.accountNumberExtra && res.active == true)
                    {
                        response.ErrorMessage = "döviz türünü  " + res.currencyType + " yapınız";
                        response.IsSuccess = false;
                        break;
                    }
                    else if (res.accountNumberExtra == request.customerAccount.accountNumberExtra && res.active == false)
                    {
                        response.ErrorMessage = "Hesap Kapalı";
                        response.IsSuccess = false;
                        break;
                    }

                    //else if (res.currencyType == request.customerAccount.currencyType)
                    //{
                    //    response.ErrorMessage = "hesap Ek no kontrol ediniz";
                    //    response.IsSuccess = false;
                    //    break;
                    //}

                    else
                    {

                        if (request.customerAccount.accountNumberExtra.ToString() == "" && request.customerAccount.currencyType == "")
                        {
                            response.ErrorMessage = "Müşteri ek No ve Döviz girilmemiş";
                            response.IsSuccess = false;
                            break;
                        }
                        else if (request.customerAccount.accountNumberExtra.ToString() == "" )
                        {
                            response.ErrorMessage = "Müşteri ek No girilmemiş";
                            response.IsSuccess = false;
                            break;
                        }
                      
                        else
                        {
                            response.ErrorMessage = "Müşteri var hesap Ek-No hatalı";
                            response.IsSuccess = false;

                        }


                    }
                }
             
            }

            return response;
        }


        public CustomerResponse WebServis_Control(CustomerRequest request)
        {
            var response = new CustomerResponse();


            if (request.webServisContcract.AccountNumber == 444 && request.webServisContcract.AccountSuffix == 2)
            {
                response.IsSuccess = true;
                WebServisContcract webServisContcract = new WebServisContcract();
                webServisContcract.ExtUName = "sakirstaj";
                webServisContcract.ExtUPassword = "SIFRETEST";
                response.webServisContcractResponse = webServisContcract;


            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Girilen Hesap No ve Ek No hatalı";
            }


            return response;

        }




    }
}
