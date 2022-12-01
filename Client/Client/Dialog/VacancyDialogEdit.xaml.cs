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
    /// Логика взаимодействия для VacancyEdit.xaml
    /// </summary>
    public partial class VacancyDialogEdit : Window
    {
        private string _id;
        public VacancyDialogEdit(string id)
        {
            InitializeComponent();
            _id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            var vacancyEdit = new VacancyEdit { Name = textBoxName.Text, Quantity = int.Parse(textBoxQuantity.Text), Salary = int.Parse(textBoxSalary.Text) };
            var response = Manager.client.PutAsJsonAsync($"api/vacancy/{_id}", vacancyEdit);
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
