using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для Vacancy.xaml
    /// </summary>
    public partial class VacancyDialogCreate : Window
    {
        public VacancyDialogCreate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var vacancyNew = new VacancyCreate { Name = textBoxName.Text, Quantity = int.Parse(textBoxQuantity.Text), Salary = int.Parse(textBoxSalary.Text) };
            var response = Manager.client.PostAsJsonAsync("api/vacancy", vacancyNew);
            response.Result.EnsureSuccessStatusCode();
            if (!response.Result.IsSuccessStatusCode)
                MessageBox.Show(response.Result.IsSuccessStatusCode.ToString());
            else
            {
                Vacancy vacancy = new Vacancy();
                vacancy.Show();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Vacancy vacancy = new Vacancy();
            vacancy.Show();
            Close();
        }
    }
}
