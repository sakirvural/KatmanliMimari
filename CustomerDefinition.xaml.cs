using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BOA.UI.Banking.CustomerDefinition
{
    /// <summary>
    /// CustomerDefinition.xaml etkileşim mantığı
    /// </summary>
    public partial class CustomerDefinition : UserControl, INotifyPropertyChanged
    {


        private void control_Loaded(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<ParameterTypeResponse, ParameterTypeRequest>();
            var contract = new ParameterTypeContract();
            var request = new ParameterTypeRequest();

            request.MethodName = "ParameterRead";

            contract.parameterName = "JOB" + "," + "EDUCATION" + "," + "PHONE" + "," + "EMAIL" + "," + "ADDRESS";

            request.parameterTypeContract = contract;

            var response = connect.Execute(request);

            cbxEducation.ItemsSource = from x in response.parameterTypesList where x.parameterName == "EDUCATION" select x.explanation;

            cbxJob.ItemsSource = from x in response.parameterTypesList where x.parameterName == "JOB" select x.explanation;

            cbxTel.ItemsSource = from x in response.parameterTypesList where x.parameterName == "PHONE" select x.explanation;

            cbxEmail.ItemsSource = from x in response.parameterTypesList where x.parameterName == "EMAIL" select x.explanation;

            cbxAdres.ItemsSource = from x in response.parameterTypesList where x.parameterName == "ADDRESS" select x.explanation;


            //cbxEducation.ItemsSource = from x in response.parameterTypesList where response.parameterTypesList.Contains(x.parameterName== "EDUCATION")
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        CustomerContract _contract = new CustomerContract();

        public int ID
        {
            get { return _contract.ID; }
            set
            {
                _contract.ID = value;
                OnPropertyChanged("ID");
            }
        }

        public string CustomerNameSurname
        {
            get { return _contract.customerNameSurname; }
            set
            {
                _contract.customerNameSurname = value;
                OnPropertyChanged("CustomerNameSurname");
            }
        }
        public string CustomerSurname
        {
            get { return _contract.customerSurname; }
            set
            {
                _contract.customerSurname = value;
                OnPropertyChanged("CustomerSurname");
            }
        }
        public string TC
        {
            get { return _contract.TC; }
            set
            {
                _contract.TC = value;
                OnPropertyChanged("TC");
            }
        }
        public string BirthPlace
        {
            get { return _contract.birthPlace; }
            set
            {
                _contract.birthPlace = value;
                OnPropertyChanged("BirthPlace");
            }
        }
        public DateTime BirthDate
        {
            get { return _contract.birthDate; }
            set
            {
                _contract.birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        public string MotherName
        {
            get { return _contract.motherName; }
            set
            {
                _contract.motherName = value;
                OnPropertyChanged("MotherName");
            }
        }
        public string FatherName
        {
            get { return _contract.fatherName; }
            set
            {
                _contract.fatherName = value;
                OnPropertyChanged("FatherName");
            }
        }
        public string Education
        {
            get { return _contract.education; }
            set
            {
                _contract.education = value;
                OnPropertyChanged("Education");
            }
        }
        public string Job
        {
            get { return _contract.job; }
            set
            {
                _contract.job = value;
                OnPropertyChanged("Job");
            }
        }

        public T FindParents<T>(DependencyObject child) where T : DependencyObject
        {

            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;

            if (parent != null)
                return parent;
            else
                return FindParents<T>(parentObject);
        }


        public void veri()//tarih mail eposta
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerContract = new BOA.Types.Banking.CustomerContract();

            customerContract.ID = this.ID;

            request.customerContract = customerContract;
            request.MethodName = "CustomerContactRead";

            var response = connect.Execute(request);

            gridTelefon.ItemsSource = response.customerPhoneNumbers;

            gridEposta.ItemsSource = response.customerEmails;

            gridAdres.ItemsSource = response.customerAddressS;

        }

        public void kapat()
        {
            tbxBirthPlace.IsEnabled = false;
            tbxCustomerName.IsEnabled = false;
            tbxCustomerSurname.IsEnabled = false;
            tbxFatherName.IsEnabled = false;
            tbxMotherName.IsEnabled = false;
            tbxTC.IsEnabled = false;
            birthDate.IsEnabled = false;


        }





        public CustomerDefinition(string id)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerContract = new BOA.Types.Banking.CustomerContract();



            customerContract.ID = Convert.ToInt32(id);

            request.customerContract = customerContract;

            request.MethodName = "CustomerIdRead";

            var response = connect.Execute(request);


            this._contract = response.customerContractResponse;
            ID = Convert.ToInt32(id);
            InitializeComponent();


            veri();
            kapat();


        }

        public CustomerDefinition()
        {

            InitializeComponent();

        }




        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {


            if (CustomerNameSurname != "" && CustomerSurname != "" && TC != "" &&
                    BirthPlace != "" && BirthDate.ToString() != "" && FatherName != "" && MotherName != "" && Education != "" && Job != "")
            {

                if (MessageBox.Show("Müşteri kaydetme işlemi yapılsın mı ?", "KONTROL", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    var customerContract = _contract;


                    var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();

                    var request = new BOA.Types.Banking.CustomerRequest();


                    request.customerContract = customerContract;

                    if (ID.ToString() == "0")
                    {

                        request.MethodName = "CustomerSave";

                        var response = connect.Execute(request);

                        if (response.IsSuccess == true)
                        {
                            MessageBox.Show("kaydedildi");

                            ID = response.id;




                            veri();
                            kapat();

                        }
                        else
                        {
                            MessageBox.Show("Başarısız");
                        }

                    }
                    else
                    {

                        request.MethodName = "CustomerUpdate";
                        var response = connect.Execute(request);
                        if (response.IsSuccess == true)
                        {
                            MessageBox.Show("Güncelleme yapıldı");

                            kapat();
                        }
                        else
                        {
                            MessageBox.Show("TC-telefon-eposta tekrar kontrol edip güncelleme yapın");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("İşlem İptal Edildi");

                }


            }
            else
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }


        }

        public void textboxTemizle()
        {
            gridAdres.ItemsSource = "";
            gridTelefon.ItemsSource = "";
            gridEposta.ItemsSource = "";
            cbxJob.Text = "";
            cbxEducation.Text = "";

            foreach (Control c in gridGiris.Children)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }

            }


        }



        //müşterisil
        private void btnSil_Click(object sender, RoutedEventArgs e)
        {

            if (ID.ToString() != "0")
            {
                if (MessageBox.Show("Müşteri Silme işlemi yapılsın mı ?", "KONTROL", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();

                    var request = new BOA.Types.Banking.CustomerRequest();



                    request.customerContract = _contract;

                    request.MethodName = "CustomerDelete";
                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {

                        MessageBox.Show("Başarılı Müşteri silme işlemi");
                        textboxTemizle();

                    }
                    else
                    {

                        MessageBox.Show("Başarısız Müşteri silme işlemi");
                    }

                }
                else
                {
                    MessageBox.Show("İşlem İptal Edildi");

                }
            }
            else
            {
                MessageBox.Show("Önceliklle Müşteri Listeleme Ekranından Müşteri seçiniz");

                if (MessageBox.Show("Müşteri Listeleme Ekranına Dönmek ister misiniz?", "KONTROL", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    //TabControl tabControl = FindParents<TabControl>(btnSil);



                    //var customerList = new BOA.UI.Banking.CustomerList.CustomerList();

                    //TabItem tabItem = new TabItem();

                    //tabItem.Header = "Hesap Açma";

                    //tabItem.Content = customerList;

                    //tabControl.Items.Add(tabItem);

                    //tabControl.SelectedIndex = tabControl.Items.Count - 1;
                }

            }

        }


        //popupları kapat

        private void closePOPUP(object sender, RoutedEventArgs e)
        {
            //tbxAdres_Add.Text = "";
            //tbxMail_Add.Text = "";
            //tbxTel_Add.Text= "";

            lblAdres.Content = "";
            popAdres.IsOpen = false;
            lblMail.Content = "";
            popEmail.IsOpen = false;
            lblPhone.Content = "";
            popTel.IsOpen = false;
        }

        //popup aç
        private void openPOPUP(object sender, RoutedEventArgs e)
        {
            var buttonName = ((Button)sender).Name.ToString();


            if (buttonName == "btnNumaraEklePOPUP")
                popTel.IsOpen = true;
            if (buttonName == "btnEpostaEklePOPUP")
                popEmail.IsOpen = true;
            if (buttonName == "btnAdresEklePOPUP")
                popAdres.IsOpen = true;
        }




        //EKSTRA GİRİŞLER İÇİN

        private void btnTel_Click(object sender, RoutedEventArgs e)
        {

            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();

            var request = new BOA.Types.Banking.CustomerRequest();

            var customerPhoneNumber = new CustomerPhoneNumber();

            customerPhoneNumber.phoneNumber = tbxTel_Add.Text;
            customerPhoneNumber.cusID = ID;
            customerPhoneNumber.phoneType = cbxTel.Text;



            request.customerPhoneNumber = customerPhoneNumber;
            request.MethodName = "NumberAdd";
            var response = connect.Execute(request);

            if (response.IsSuccess)
            {

                lblPhone.Foreground = new SolidColorBrush(Colors.Green);
                lblPhone.Content = response.Message;
                veri();
            }
            else
            {
                lblPhone.Foreground = new SolidColorBrush(Colors.Red);
                lblPhone.Content = response.ErrorMessage;

            }

        }



        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerMail = new CustomerEmail();

            customerMail.emailType = cbxEmail.Text;
            customerMail.email = tbxMail_Add.Text;
            customerMail.cusID = ID;

            request.customerEmail = customerMail;

            request.MethodName = "EmailAdd";
            var response = connect.Execute(request);
            if (response.IsSuccess)
            {
                lblMail.Foreground = new SolidColorBrush(Colors.Green);
                lblMail.Content = response.Message;
                veri();
            }
            else
            {
                lblMail.Foreground = new SolidColorBrush(Colors.Red);
                lblMail.Content = response.ErrorMessage;
            }



        }

        private void btnAdres_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerAddress = new CustomerAddress();

            customerAddress.address = tbxAdres_Add.Text;
            customerAddress.addressType = cbxAdres.Text;
            customerAddress.cusID = ID;

            request.customerAddress = customerAddress;
            request.MethodName = "AdressAdd";
            var response = connect.Execute(request);
            if (response.IsSuccess)
            {
                lblAdres.Foreground = new SolidColorBrush(Colors.Green);
                lblAdres.Content = response.Message;
                veri();
            }
            else
            {
                lblAdres.Foreground = new SolidColorBrush(Colors.Red);
                lblAdres.Content = response.ErrorMessage;
            }


        }
        //EKSTRA GİRİŞLER İÇİN



        //telefon-mail- adres silme 
        private void btnTelSil_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerPhoneNumber = new CustomerPhoneNumber();

            customerPhoneNumber.phoneNumber = tbxTel_Add.Text;
            request.customerPhoneNumber = customerPhoneNumber;

            request.MethodName = "PhoneNumberDelete";
            var response = connect.Execute(request);
            if (response.IsSuccess)
            {
                lblPhone.Foreground = new SolidColorBrush(Colors.Green);
                lblPhone.Content = "Başarılı silme işlemi";
                veri();
            }
            else
            {
                lblPhone.Foreground = new SolidColorBrush(Colors.Red);
                lblPhone.Content = "Başarısız silme işlemi";
            }
        }

        private void btnMailSil_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerMail = new CustomerEmail();

            customerMail.email = tbxMail_Add.Text;
            request.customerEmail = customerMail;
            request.MethodName = "MailDelete";
            var response = connect.Execute(request);

            if (response.IsSuccess)
            {
                lblMail.Foreground = new SolidColorBrush(Colors.Green);
                lblMail.Content = "Başarılı silme işlemi";
                veri();
            }
            else
            {
                lblMail.Foreground = new SolidColorBrush(Colors.Red);
                lblMail.Content = "Başarısız silme işlemi";
            }
        }

        private void btnAdresSil_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerAddress = new CustomerAddress();

            customerAddress.address = tbxAdres_Add.Text;
            request.customerAddress = customerAddress;
            request.MethodName = "AddressDelete";
            var response = connect.Execute(request);
            if (response.IsSuccess)
            {
                lblAdres.Foreground = new SolidColorBrush(Colors.Green);
                lblAdres.Content = "Başarılı silme işlemi";
                veri();
            }
            else
            {
                lblAdres.Foreground = new SolidColorBrush(Colors.Red);
                lblAdres.Content = "Başarısız silme işlemi";
            }

        }



        private void btnHesapAc_Click(object sender, RoutedEventArgs e)
        {

            TabControl tabControl = FindParents<TabControl>(btnHesapAc);


            var parameter = new BOA.UI.Banking.CustomerAccount.CustomerAccount(ID.ToString());

            TabItem tabItem = new TabItem();

            tabItem.Header = "Hesap Açma";

            tabItem.Content = parameter;

            tabControl.Items.Add(tabItem);

            tabControl.SelectedIndex = tabControl.Items.Count - 1;

        }



        //telefon-mail- adres silme 











        //BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest> connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>(); // bağlanılacak yer

        //BOA.Types.Banking.CustomerRequest request = new BOA.Types.Banking.CustomerRequest();//dönüs


        //BOA.Types.Banking.CustomerContract customerContract = new BOA.Types.Banking.CustomerContract();

        //BOA.Types.Banking.CustomerPhoneNumber customerPhoneNumber = new CustomerPhoneNumber();
        //BOA.Types.Banking.CustomerMail customerMail = new CustomerMail();
        //BOA.Types.Banking.CustomerAddress customerAddress = new CustomerAddress();
    }
}
