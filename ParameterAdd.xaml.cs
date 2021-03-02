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
using BOA.Types.Banking;
using BOA.Connector.Banking;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BOA.UI.Banking.ParameterAdd
{
    /// <summary>
    /// ParameterAdd.xaml etkileşim mantığı
    /// </summary>
    public partial class ParameterAdd : UserControl
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        ParameterTypeContract _contract = new ParameterTypeContract();

        public string ParameterName
        {
            get { return _contract.parameterName; }
            set
            {
                _contract.parameterName = value;
                OnPropertyChanged("ParameterName");
            }
        }
        public string Explanation
        {
            get { return _contract.explanation; }
            set
            {
                _contract.explanation = value;
                OnPropertyChanged("Explanation");
            }
        }


        public ParameterAdd()
        {
            
            InitializeComponent();

            ParameterTypes();
        }

        public void ParameterTypes()
        {
         

            var request = new ParameterTypeRequest();
            request.MethodName = "ParameterTypesRead";
            var connect = new Connect<ParameterTypeResponse,ParameterTypeRequest>();

            var response = connect.Execute(request);

            cbxTip.ItemsSource = response.parameterTypesList.Select(p=> p.parameterName);
            

            //foreach (var res in response.parameterTypesList)
            //{
            //    cbxTip.Items.Add(res.parameterName);
            //}

        }

        public void ParameterExplanation()
        {
           
            try
            {
                var connect = new BOA.Connector.Banking.Connect<ParameterTypeResponse, ParameterTypeRequest>();
                var contract = new ParameterTypeContract();
                var request = new ParameterTypeRequest();
                request.MethodName = "ParameterRead";

                contract.parameterName = cbxTip.SelectedItem.ToString();

                request.parameterTypeContract = contract;

                var response = connect.Execute(request);


                cbxParametre.ItemsSource = response.parameterTypesList.Select(p => p.explanation);

                //foreach (var res in response.parameterTypesList)
                //{
                //    if (res.parameterName == cbxTip.SelectedItem.ToString())
                //        cbxParametre.Items.Add(res.explanation);
                //}
            }
            catch
            {

            }



        }

        private void cbxTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ParameterExplanation();
                tbxParametre.IsEnabled = true;
                cbxParametre.IsEnabled = true;
                btnParametre.IsEnabled = true;
            }
            catch
            {

            }
            

        }


        private void btnParametreTip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new ParameterTypeRequest();
                request.MethodName = "ParameterTypeAdd";

                _contract.parameterName = ParameterName.ToUpper();
                request.parameterTypeContract = _contract;

                var connect = new Connect<ParameterTypeResponse, ParameterTypeRequest>();

                var response = connect.Execute(request);

                if (response.IsSuccess)
                {
                    MessageBox.Show("Başarılı Parametre tip Ekleme İşlemi");

                    ParameterTypes();
                    tbxParametre.IsEnabled = false;
                    cbxParametre.IsEnabled = false;
                    btnParametre.IsEnabled = false;


                }
                else
                {
                    MessageBox.Show("Başarısız Parametre tip Ekleme İşlemi Aynı veri var");
                }
            }
            catch(Exception hata)
            {
                MessageBox.Show("HATA\n"+hata);
            }
       
        }

        private void btnParametre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new ParameterTypeRequest();
                request.MethodName = "ParameterAdd";

                _contract.parameterName = cbxTip.SelectedItem.ToString();

                _contract.explanation = Explanation.ToUpper();

                request.parameterTypeContract = _contract;

                var connect = new Connect<ParameterTypeResponse, ParameterTypeRequest>();

                var response = connect.Execute(request);

                if (response.IsSuccess)
                {
                    MessageBox.Show("Başarılı Parametre Ekleme İşlemi");

                    ParameterExplanation();


                }
                else
                {
                    MessageBox.Show("Başarısız Parametre Ekleme İşlemi Aynı veri var");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("HATA\n" + hata);
            }

        }
    }
}
