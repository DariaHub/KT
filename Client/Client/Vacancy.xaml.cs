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
using System.Windows.Shapes;
using System.Net.Http.Json;
using Client.Model;
using Client.Dialog;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для Vacancy.xaml
    /// </summary>
    public partial class Vacancy : Window
    {
        public Vacancy()
        {
            InitializeComponent();
            getVacancies();
        }
        private async Task getVacancies()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = await Manager.client.GetFromJsonAsync<List<VacancyModel>>("api/vacancy");
            datagrid.ItemsSource = response;
        }

        private async void buttonDeleteVacancy_Click(object sender, RoutedEventArgs e)
        {
            var vacancy = (sender as Button).DataContext as VacancyModel;
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = await Manager.client.DeleteAsync($"api/vacancy/{vacancy.Id}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
                getVacancies();
            else
                MessageBox.Show(response.IsSuccessStatusCode.ToString());
        }

        private void buttonEditVacancy_Click(object sender, RoutedEventArgs e)
        {
            var vacancy = (sender as Button).DataContext as VacancyModel;
            VacancyDialogEdit vacancyDialogEdit = new VacancyDialogEdit(vacancy.Id);
            vacancyDialogEdit.ShowDialog();
            Close();
        }

        private void buttonViewVacancy_Click(object sender, RoutedEventArgs e)
        {
            var vacancy = (sender as Button).DataContext as VacancyModel;
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var response = Manager.client.GetFromJsonAsync<VacancyModel>($"api/vacancy/{vacancy.Id}");
            MessageBox.Show($"Вакансия: {response.Result.Name}\nКоличество мест: {response.Result.Quantity}\nЗарплата: {response.Result.Salary}");
        }

        private void buttonCreateVacancy_Click(object sender, RoutedEventArgs e)
        {
            VacancyDialogCreate vacancyManipulation = new VacancyDialogCreate();
            vacancyManipulation.Show();
            Close();
        }

        private void datagrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vacancy = (sender as DataGrid).SelectedItem as VacancyModel;
            ItCompany itCompany = new ItCompany(vacancy.Id);
            itCompany.Show();
            Close();
        }
    }
}
