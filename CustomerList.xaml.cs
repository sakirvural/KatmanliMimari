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

namespace BOA.UI.Banking.CustomerList
{
    /// <summary>
    /// CustomerList.xaml etkileşim mantığı
    /// </summary>
    public partial class CustomerList : UserControl
    {


        //TabControl tabControl;

        //public CustomerList(TabControl control)
        //{

        //    this.tabControl = control;
        //    InitializeComponent();

        //}

        public CustomerList()
        {

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<CustomerContract> contracts;

        public List<CustomerContract> CustomerContList
        {
            get { return contracts; }
            set
            {
                contracts = value;
                OnPropertyChanged("CustomerContList");
            }
        }



        private void btnBilgi_Click(object sender, RoutedEventArgs e)
        {
            var connect = new BOA.Connector.Banking.Connect<CustomerResponse, CustomerRequest>();
            var request = new BOA.Types.Banking.CustomerRequest();
            var contract = new BOA.Types.Banking.CustomerContract();


            contract.customerNameSurname = tbxAdSoyad.Text;
            contract.customerNo = tbxNo.Text;
            contract.TC = tbxTC.Text;





            request.customerContract = contract;
            request.MethodName = "CustomerFilterRead";


            var response = connect.Execute(request);

            //contracts = new List<CustomerContract>();

            this.contracts = response.customerContractsList;

            dataGrid.ItemsSource = response.customerContractsList;



        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {

            foreach (Control c in gridGiris.Children)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }

            }

            dataGrid.ItemsSource = "";
        }

        public string id;

        //dataDel();
        //private void dataDel()
        //{
        //    dataGrid.Columns.RemoveAt(3);
        //    dataGrid.Columns.RemoveAt(8);
        //    dataGrid.Columns.RemoveAt(8);
        //}


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                id = ((TextBlock)dataGrid.Columns[0].GetCellContent(dataGrid.SelectedItem)).Text;
            }
            catch
            {
                id = null;
            }
        }





        //public static TabControl getParentTab(DependencyObject child)
        //{
        //    DependencyObject dependency = VisualTreeHelper.GetParent(child);
        //    TabControl tabControl=dependency as TabControl;

        //    return tabControl;
        //}

        //Button btn = (Button)sender;

        //var a = getParentTab(btn);

        //a.Items.Add(tabItem);


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

        private void btnAc_Click(object sender, RoutedEventArgs e)
        {



            if (id != null)
            {

                TabControl tabControl = FindParents<TabControl>(btnAc);

               var definition = new BOA.UI.Banking.CustomerDefinition.CustomerDefinition(id);

                TabItem tabItem = new TabItem();
                tabItem.Header = "Müşteri Tanımlama";
                tabItem.Content = definition;

                tabControl.Items.Add(tabItem);

                tabControl.SelectedIndex = tabControl.Items.Count - 1; // kaç tane tab item var onu döndürür
            }
            else
            {
                MessageBox.Show("Müşteri Seçiniz");
            }

        }

    }
}
