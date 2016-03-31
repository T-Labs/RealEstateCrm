using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class HousingCallViewModel
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public static HousingCallViewModel Create(HousingCall call)
        {
            var item = new HousingCallViewModel
            {
                Id = call.Id,
                EmployeeName = call.User.FIO,
                Status = call.StatusType.ToLocalizedString(),
                Date = call.Date
            };

            return item;
        }

    }
}
