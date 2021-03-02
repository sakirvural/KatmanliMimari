using BOA.UI.Banking.Customer;
using System.Windows;
using System.Windows.Controls;
using BOA.UI.Banking.CustomerList;

namespace BOA.UI.Banking.Menu
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        string isim;
      


        public MainWindow(string isim)
        {
            this.isim = isim;

            InitializeComponent();
            kullanici.Content = this.isim;
        }
        public MainWindow()
        {
       
            InitializeComponent();

        }
        private void button_sekmekapat(object sender, RoutedEventArgs e)
            
        {
            
            if (menuTabcontrol.SelectedIndex!=0)
            {
                menuTabcontrol.Items.Remove(menuTabcontrol.SelectedItem);
            }
           
        }
        private void button_Menu(object sender, RoutedEventArgs e)
        {

            var content = ((ContentControl)sender).Content.ToString();

            object userControl = new object();
           
            

            if (content == "Müşteri Tanımlama")
            {
                
                userControl = new BOA.UI.Banking.CustomerDefinition.CustomerDefinition();

            }

            if (content == "Müşteri Listeleme")
            {
                userControl = new BOA.UI.Banking.CustomerList.CustomerList();

            }

            if (content == "Hesap Açma")
            {
                userControl = new BOA.UI.Banking.CustomerAccount.CustomerAccount();

            }

            if (content == "Para Yatırma - Çekme")
            {
                userControl = new BOA.UI.Banking.CustomerMoneyCheck.CustomerMoneyCheck();

            }
            if (content == "Web Servis")
            {
                userControl = new BOA.UI.Banking.Customer.AccountTrackingWebService();

            }
            if (content == "Parametre")
            {
                userControl = new BOA.UI.Banking.ParameterAdd.ParameterAdd();

            }



            TabItem tabItem = new TabItem
            {
                Header = content,

                Content = userControl,
               

            };
            
            menuTabcontrol.Items.Add(tabItem);
            
            menuTabcontrol.SelectedIndex = menuTabcontrol.Items.Count - 1;

        }

  


        //CustomerList customerList = new CustomerList();
        //    //gridim.Children.Add(customerList); --grid içinde açma

        //    //customerList.Show();--yeni sekmede açma



        //private void button_Click(object sender, RoutedEventArgs e)
        //{


        //}



        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    tabItem = new TabItem();



        //    tabItem.Header = "Müşteri Listeleme";
        //    tabItem.Content = customerList;


        //    menuTabcontrol.Items.Add(tabItem);

        //    menuTabcontrol.SelectedIndex = menuTabcontrol.Items.Count - 1; // kaç tane tab item var onu döndürür



        //    

        //}

        //private void button2_Click_1(object sender, RoutedEventArgs e)
        //{
        //    tabItem = new TabItem();



        //    tabItem.Header = "Hesap Açma";
        //    tabItem.Content = parameter;


        //    menuTabcontrol.Items.Add(tabItem);

        //    menuTabcontrol.SelectedIndex = menuTabcontrol.Items.Count - 1; // kaç tane tab item var onu döndürür
        //}




        //private void button3_Click(object sender, RoutedEventArgs e)
        //{

        //    tabItem = new TabItem();



        //    tabItem.Header = ((System.Windows.Controls.ContentControl)sender).Content;
        //    tabItem.Content = moneyCheck;


        //    menuTabcontrol.Items.Add(tabItem);

        //    menuTabcontrol.SelectedIndex = menuTabcontrol.Items.Count - 1; // kaç tane tab item var onu döndürür

        //}


        //private void button3_Copy_Click(object sender, RoutedEventArgs e)
        //{
        //    //tabItem = new TabItem();
        //    //Parameter parameter = new Parameter();


        //    //tabItem.Header = "Para Çekme";
        //    //tabItem.Content = parameter;


        //    //menuTabcontrol.Items.Add(tabItem);

        //    //menuTabcontrol.SelectedIndex = menuTabcontrol.Items.Count - 1; // kaç tane tab item var onu döndürür

        //}
    }
}
