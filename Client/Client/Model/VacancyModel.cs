using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class VacancyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Salary { get; set; }
    }
}
