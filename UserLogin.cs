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
using BOA.UI.Banking;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BOA.UI.Banking.USERLOGIN
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        UserContract _userContract = new UserContract();

        public string UserName
        {
            get { return _userContract.userName; }
            set
            {
                _userContract.userName = value;
                OnPropertyChanged("UserName");
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
            {


                var connect = new BOA.Connector.Banking.Connect<UserResponse,UserRequest>();
                var request = new BOA.Types.Banking.UserRequest();


                 _userContract.password = passwordBox.Password.ToString();

                request.userContract = _userContract;
                request.MethodName = "User_Login";

                var response = connect.Execute(request);

                if (response.IsSuccess)
                {
                MessageBox.Show("Başarılı Giriş");
                BOA.UI.Banking.Menu.MainWindow menu = new Menu.MainWindow(tbxUser.Text.ToString());
                
                menu.Show();
                this.Close();
            }
                else
                {
                    MessageBox.Show("Tekrar Deneyin.");
                }

            }
        
    }
}
