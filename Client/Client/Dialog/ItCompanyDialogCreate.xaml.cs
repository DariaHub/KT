using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Dialog
{
    /// <summary>
    /// Логика взаимодействия для ItCompanyDialogCreate.xaml
    /// </summary>
    public partial class ItCompanyDialogCreate : Window
    {
        private string _idVacancy;
        public ItCompanyDialogCreate(string idVacancy)
        {
            InitializeComponent();
            _idVacancy = idVacancy;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var itCompanyNew = new ItCompanyCreate { Name = textBoxName.Text, Address = textBoxAdress.Text, Phone = textBoxPhone.Text };
            var response = Manager.client.PostAsJsonAsync($"api/vacancy/{_idVacancy}/itcompanies", itCompanyNew);
            response.Result.EnsureSuccessStatusCode();
            if (!response.Result.IsSuccessStatusCode)
                MessageBox.Show(response.Result.IsSuccessStatusCode.ToString());
            else
            {
                ItCompany itCompany = new ItCompany(_idVacancy);
                itCompany.Show();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ItCompany itCompany = new ItCompany(_idVacancy);
            itCompany.Show();
            Close();
        }
    }
}
