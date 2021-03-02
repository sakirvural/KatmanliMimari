using BOA.Connector.Banking;
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

namespace BOA.UI.Banking.CustomerMoneyCheck
{
    /// <summary>
    /// CustomerMoneyCheck.xaml etkileşim mantığı
    /// </summary>
    public partial class CustomerMoneyCheck : UserControl
    {
        Connect<CustomerResponse, CustomerRequest> connect = new Connect<CustomerResponse, CustomerRequest>();
        BOA.Types.Banking.CustomerRequest request = new BOA.Types.Banking.CustomerRequest();
        BOA.Types.Banking.CustomerMoneyTransferHistory customerMoneyTransferHistory = new BOA.Types.Banking.CustomerMoneyTransferHistory();
        BOA.Types.Banking.CustomerAccount customerAccount = new BOA.Types.Banking.CustomerAccount();


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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



        public CustomerMoneyCheck()
        {

            InitializeComponent();

        }


        private void btnOnay_Click(object sender, RoutedEventArgs e)
        {


            customerAccount.accountBalance = Convert.ToDecimal(tbxTutar.Text);


            if (btnYatır.IsChecked == true)
            {
                if (btnYetkinKisi.IsChecked == true)
                {
                    customerMoneyTransferHistory.WhoPerson = btnYetkinKisi.Content.ToString();
                    customerMoneyTransferHistory.explanation = "yatırma";
                }
                if (btnBaskaKisi.IsChecked == true)
                {
                    customerMoneyTransferHistory.WhoPerson = "TC = " + tbxTC.Text;
                    customerMoneyTransferHistory.explanation = "yatırma";
                }



            }
            if (btnCek.IsChecked == true)
            {
                customerMoneyTransferHistory.WhoPerson = btnYetkinKisi.Content.ToString();
                customerMoneyTransferHistory.explanation = "cekme";
            }
            string message = "Para " + customerMoneyTransferHistory.explanation + " işlemi yapılsın mı ?";

            if (MessageBox.Show(message, "Kontrol", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();

                //customerAccount.accountBalance = Convert.ToDecimal(tbxTutar.Text);

                request.MethodName = "Customer_AccountBalance_Update";

                request.customerMoneyTransferHistory = customerMoneyTransferHistory;

                request.customerAccount = customerAccount;

                var response = connect.Execute(request);



                if (response.IsSuccess)
                {
                    MessageBox.Show("Başarılı " + customerMoneyTransferHistory.explanation);
                    tbxBakiye.Text = response.accountBalance.ToString();

                    dataGridHistory.ItemsSource = response.customerMoneyTransferHistoryList;
                }
                else
                {
                    MessageBox.Show(response.ErrorMessage);
                }

            }
            else
            {
                MessageBox.Show("İşlem İptal Edildi");
            }






        }

        private void btnKontrol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                customerAccount.accountNumber = Convert.ToInt32(tbxHesapNo.Text);

                customerAccount.accountNumberExtra = Convert.ToInt32(tbxHesapNoEk.Text);

                customerAccount.currencyType = cbxDoviz.Text;
            }
      
            catch
            {
                MessageBox.Show("Lütfen parametleri düzgün giriniz veya boş bırakmayınız");
            }
            

            request.customerMoneyTransferHistory = customerMoneyTransferHistory;
            request.customerAccount = customerAccount;
            request.MethodName = "Customer_AccountBalance_Update";

            var response = connect.Execute(request);
            if (response.IsSuccess)
            {

                btnOnay.IsEnabled = true;

                gridKontrol.IsEnabled = true;


                btnKontrol.IsEnabled = false;



                dataGridHistory.Visibility = Visibility.Visible;

                tbxHesapNo.IsEnabled = false;
                tbxHesapNoEk.IsEnabled = false;
                cbxDoviz.IsEnabled = false;


                dataGridHistory.ItemsSource = response.customerMoneyTransferHistoryList;

                tbxBakiye.Text = response.accountBalance.ToString();
            }

            else
            {
                MessageBox.Show(response.ErrorMessage);
            }



        }


        private void btnBaska_Checked(object sender, RoutedEventArgs e)
        {
            gridTC.IsEnabled = true;
        }

        private void btnYetkin_Checked(object sender, RoutedEventArgs e)
        {
            gridTC.IsEnabled = false;
            tbxTC.Text = "";
        }



        private void btnYatır_Checked(object sender, RoutedEventArgs e)
        {
            gridParaYatır.IsEnabled = true;
        }

        private void btnCek_Checked(object sender, RoutedEventArgs e)
        {
            gridParaYatır.IsEnabled = false;
            btnYetkinKisi.IsChecked = false;
            btnBaskaKisi.IsChecked = false;
            tbxTC.Text = "";
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Control c in gridGiris.Children)
            //{
            //    if (c is TextBox)
            //    {
            //        (c as TextBox).Clear();
            //    }

            //}
            tbxBakiye.Text = "";
            tbxHesapNo.Text = "";
            tbxHesapNoEk.Text = "";
            tbxTutar.Text = "";
            cbxDoviz.Text = "";
            tbxTC.Text = "";

            btnKontrol.IsEnabled = true;
            tbxHesapNo.IsEnabled = true;
            tbxHesapNoEk.IsEnabled = true;
            cbxDoviz.IsEnabled = true;

            btnOnay.IsEnabled = false;
            gridKontrol.IsEnabled = false;
            btnBaskaKisi.IsChecked = false;
            btnCek.IsChecked = false;
            btnYetkinKisi.IsChecked = false;
            btnYatır.IsChecked = false;


            dataGridHistory.Visibility = Visibility.Collapsed;



        }



        //Connect<CustomerResponse, CustomerRequest> connect = new Connect<CustomerResponse, CustomerRequest>();// bağlanılacak yer

        //CustomerRequest request = new BOA.Types.Banking.CustomerRequest();//dönüs

        //customerAccount customerAccount = new BOA.Types.Banking.customerAccount();//dönüs

        //CustomerMoneyTransferHistory customerMoneyTransferHistory = new BOA.Types.Banking.CustomerMoneyTransferHistory();
    }
}
