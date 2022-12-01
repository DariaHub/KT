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
    /// Логика взаимодействия для ItCompanyDialogEdit.xaml
    /// </summary>
    public partial class ItCompanyDialogEdit : Window
    {
        private string _id;
        private string _idVacancy;
        public ItCompanyDialogEdit(string id, string idVacancy)
        {
            InitializeComponent();
            _id = id;
            _idVacancy = idVacancy;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var itCompanyEdit = new ItCompanyEdit { Name = textBoxName.Text, Address = textBoxAdress.Text, Phone = textBoxPhone.Text };
            var response = Manager.client.PutAsJsonAsync($"api/vacancy/{_idVacancy}/itcompanies/{_id}", itCompanyEdit);
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
