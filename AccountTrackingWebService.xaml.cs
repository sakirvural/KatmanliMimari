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

namespace BOA.UI.Banking.Customer
{
    /// <summary>
    /// Interaction logic for AccountTrackingWebService.xaml
    /// </summary>
    /// 


    public partial class AccountTrackingWebService : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public int hesapNo;
        public int HesapNo
        {
            get { return hesapNo; }
            set
            {
                hesapNo = value;
                OnPropertyChanged("HesapNo");
            }
        }


        public short hesapEkno;

        public short HesapEkNo
        {
            get { return hesapEkno; }
            set
            {
                hesapEkno = value;
                OnPropertyChanged("HesapEkNo");
            }
        }

        //public string islemTarihi;
        //public string IslemTarihi
        //{
        //    get { return islemTarihi; }
        //    set
        //    {

        //        islemTarihi = value;
        //        OnPropertyChanged("IslemTarihi");
        //    }
        //}



        public AccountTrackingWebService()
        {

            InitializeComponent();

        }

        private void btnGetir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var webServisContcract = new Types.Banking.WebServisContcract();

                var request = new Types.Banking.CustomerRequest();

                var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();



                webServisContcract.AccountNumber = HesapNo;

                webServisContcract.AccountSuffix = HesapEkNo;

                request.MethodName = "WebServis_Control";
                request.webServisContcract = webServisContcract;

                var response = connect.Execute(request);

                if (response.IsSuccess)
                {

                    ServiceReference1.CustomerTransactionServiceClient client = new ServiceReference1.CustomerTransactionServiceClient();

                    var clientRequest = new ServiceReference1.AccountBalanceRequest();

                    clientRequest.ExtUName = response.webServisContcractResponse.ExtUName;

                    clientRequest.ExtUPassword = response.webServisContcractResponse.ExtUPassword;

                    clientRequest.AccountNumber = HesapNo;
                    clientRequest.AccountSuffix = HesapEkNo;

                    clientRequest.BalanceDate = Convert.ToDateTime(dateTarih.Text);



                    var value = client.GetAccountBalance(clientRequest).Value;

                    var accountBalanceResponseModel = new List<ServiceReference1.AccountBalanceResponseModel>();

                    accountBalanceResponseModel.Add(value);

                    dataGrid.ItemsSource = accountBalanceResponseModel;

                    //MessageBox.Show("BOŞ alan bırakmayınız", "Hata", MessageBoxButton.OK,MessageBoxImage.Error);    

                    client.Close();

                }
                else
                {
                    MessageBox.Show(response.ErrorMessage);
                }


            }
            catch (Exception hata)
            {

                MessageBox.Show("BAŞARISIZ\n" + hata);

            }
        }


    }
}
