using Client.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Claims;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Войти_Click(object sender, RoutedEventArgs e)
        {
            //RunAsync(user_name.Text, password.Text);
            Manager.client.BaseAddress = new Uri("http://localhost:5021/");
            Manager.client.DefaultRequestHeaders.Accept.Clear();
            Manager.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var userLogin = new UserLogin { UserName = user_name.Text, Password = password.Text };
                var response = await Manager.client.PostAsJsonAsync("api/authentication/login", userLogin);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    Token.token = response.Content.ReadAsAsync<TokenModel>().Result.token;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(Token.token);
                    Token.idUser = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    Vacancy vacancy = new Vacancy();
                    vacancy.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
