using Client.Dialog;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для ItCompany.xaml
    /// </summary>
    public partial class ItCompany : Window
    {
        private string _idVacancy;
        public ItCompany(string idVacancy)
        {
            InitializeComponent();
            _idVacancy = idVacancy;
            getItCompanies();
        }
        private async Task getItCompanies()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = Manager.client.GetFromJsonAsync<List<ItCompanyModel>>($"api/vacancy/{_idVacancy}/itcompanies");
            datagrid.ItemsSource = response.Result;
        }

        private void buttonCreateItCompany_Click(object sender, RoutedEventArgs e)
        {
            var itCompany = (sender as Button).DataContext as ItCompanyModel;
            ItCompanyDialogCreate itCompanyDialogCreate = new ItCompanyDialogCreate(_idVacancy);
            itCompanyDialogCreate.Show();
            Close();
        }

        private void buttonViewItCompany_Click(object sender, RoutedEventArgs e)
        {
            var itCompany = (sender as Button).DataContext as ItCompanyModel;
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = Manager.client.GetFromJsonAsync<ItCompanyModel>($"api/vacancy/{_idVacancy}/itcompanies/{itCompany.Id}");
            MessageBox.Show($"Компания: {response.Result.Name}\nАдрес: {response.Result.Address}\nТелефон: {response.Result.Phone}");
        }

        private void buttonEditItCompany_Click(object sender, RoutedEventArgs e)
        {
            var itCompany = (sender as Button).DataContext as ItCompanyModel;
            ItCompanyDialogEdit itCompanyDialogEdit = new ItCompanyDialogEdit(itCompany.Id, _idVacancy);
            itCompanyDialogEdit.Show();
            Close();
        }

        private void buttonDeleteItCompany_Click(object sender, RoutedEventArgs e)
        {
            var itCompany = (sender as Button).DataContext as ItCompanyModel;
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = Manager.client.DeleteAsync($"api/vacancy/{_idVacancy}/itcompanies/{itCompany.Id}");
            response.Result.EnsureSuccessStatusCode();
            if (response.Result.IsSuccessStatusCode)
                getItCompanies();
            else
                MessageBox.Show(response.Result.IsSuccessStatusCode.ToString());
        }
    }
}
