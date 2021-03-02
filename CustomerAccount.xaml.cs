using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BOA.UI.Banking.CustomerAccount
{
    /// <summary>
    /// CustomerAccount.xaml etkileşim mantığı
    /// </summary>
    public partial class CustomerAccount : UserControl
    {
        private void CustomerAccountControl_Loaded(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<ParameterTypeResponse, ParameterTypeRequest>();
            var contract = new ParameterTypeContract();
            var request = new ParameterTypeRequest();
            request.MethodName = "ParameterRead";

            contract.parameterName = "CURRENCY";

            request.parameterTypeContract = contract;

            var response = connect.Execute(request);


            cbxDoviz.ItemsSource = response.parameterTypesList.Select(p => p.explanation);

            //foreach (var res in response.parameterTypesList)
            //{
            //    if (res.parameterName == "CURRENCY")
            //        cbxDoviz.Items.Add(res.explanation);
            //}
        }

        public string id { get; set; }


        public CustomerAccount(string id)
        {

            this.id = id;

            InitializeComponent();



            denetim();
        }
        public CustomerAccount()
        {

            InitializeComponent();


            textboxTemizle();

        }

        public void textboxTemizle()
        {
            foreach (Control c in gridGiris.Children)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }

            }

            tbxHesapNo.IsEnabled = true;
            btnTemizle.IsEnabled = false;
            btnSil.IsEnabled = false;
            btnKayıt.IsEnabled = false;
            cbxDoviz.IsEnabled = false;
            dataGrid.Visibility = Visibility.Collapsed;

            //tbxHesapNo.Text = "";
            //tbxEkNo.Text = "";
            //cbxDoviz.Text = "";
            //tbxBakiye.Text = "";
            //lblSorgu.Content = "";

        }

        public void aktif()
        {
            btnSil.IsEnabled = true;
            btnTemizle.IsEnabled = true;
            btnKayıt.IsEnabled = true;
            cbxDoviz.IsEnabled = true;

            dataGrid.Visibility = Visibility.Visible;
        }


        public void denetim()
        {
            var request = new BOA.Types.Banking.CustomerRequest();
            var customerAccount = new BOA.Types.Banking.CustomerAccount();

            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();// bağlanılacak yer

            customerAccount.accountNumber = Convert.ToInt32(id);

            request.customerAccount = customerAccount;

            request.MethodName = "Customer_Account_Read";

            var response = connect.Execute(request);


            if (response.IsSuccess)
            {
                aktif();
                tbxHesapNo.IsEnabled = false;
                //tbxAcilisTarihi.Text = DateTime.Now.ToString();
                tbxBakiye.Text = "0";
                lblSorgu.Foreground = new SolidColorBrush(Colors.Green);
                lblSorgu.Content = "( Müşteri Mevcut )";

                tbxEkNo.Text = response.customerAccountCount.ToString();

                dataGrid.ItemsSource = response.customerAccountList;
            }
            else
            {

                lblSorgu.Foreground = new SolidColorBrush(Colors.Red);
                lblSorgu.Content = "( Müşteri Mevcut Değil )";
            }

        }



        private void btnKayıt_Click(object sender, RoutedEventArgs e)
        {
            var request = new BOA.Types.Banking.CustomerRequest();
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();// bağlanılacak yer
            var customerAccount = new BOA.Types.Banking.CustomerAccount();
            if (cbxDoviz.Text != "")
            {
                if (MessageBox.Show("Hesap açma işlemi Yapılsın mı?", "Kontrol", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    customerAccount.accountNumber = Convert.ToInt32(id);
                    customerAccount.currencyType = cbxDoviz.Text;
                    request.customerAccount = customerAccount;

                    request.MethodName = "Customer_Account_Add";


                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Hesap Açıldı");
                        cbxDoviz.Text = "";
                        denetim();

                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage);
                    }
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi");
                }

            }
            else
            {
                MessageBox.Show("Döviz cinsini giriniz");
            }




        }

        private void btnKontrol_Click(object sender, RoutedEventArgs e)
        {
            if (tbxHesapNo.Text != "")
            {
                denetim();
            }
            else
            {
                lblSorgu.Foreground = new SolidColorBrush(Colors.Red);
                lblSorgu.Content = "( Müşteri Numarası Girin )";
            }

        }

        public int accountNumber;
        public int accountNumberExtra;
        public bool active;

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            var customerAccount = new BOA.Types.Banking.CustomerAccount();//dönüs
            var request = new BOA.Types.Banking.CustomerRequest();//dönüs
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();// bağlanılacak yer

            if (MessageBox.Show("Hesap silme işlemi yapılsın mı ?", "Kontrol", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                customerAccount.accountNumberExtra = this.accountNumberExtra;
                customerAccount.active = this.active;
                customerAccount.accountNumber = this.accountNumber;
                request.customerAccount = customerAccount;

                request.MethodName = "Customer_Account_Delete";
                var response = connect.Execute(request);
                if (response.IsSuccess)
                {
                    MessageBox.Show(response.ErrorMessage);
                    denetim();
                }
                else
                {
                    MessageBox.Show(response.ErrorMessage);
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi");
            }



        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.active = Convert.ToBoolean(((TextBlock)dataGrid.Columns[0].GetCellContent(dataGrid.SelectedItem)).Text);
                this.accountNumber = Convert.ToInt32(((TextBlock)dataGrid.Columns[1].GetCellContent(dataGrid.SelectedItem)).Text);
                this.accountNumberExtra = Convert.ToInt32(((TextBlock)dataGrid.Columns[2].GetCellContent(dataGrid.SelectedItem)).Text);
            }
            catch
            {

            }

        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            textboxTemizle();
        }

       
    }
}
