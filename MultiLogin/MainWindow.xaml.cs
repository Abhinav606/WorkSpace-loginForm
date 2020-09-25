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
using System.Collections.Specialized;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;


namespace MultiLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

      

        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                                                       .ToString();
        }
   

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(con.State==System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if(VerifyUser(txtUsername.Text,txtPassword.Password))
            {
                MessageBox.Show("Login Successfull", "Congrates", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }
        private bool VerifyUser(string username, string password)
        {
            con.Open();
            com.Connection = con;
            
            com.CommandText = "select Status from tblUser where Username='" + username + "' and Password='" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
       
                if (Convert.ToBoolean(dr["Status"])== true)
                {
                   return true;
                }
                else
                {
                  return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
