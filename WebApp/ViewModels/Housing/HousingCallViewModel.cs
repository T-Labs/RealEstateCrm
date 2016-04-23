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

        public string EmployeeId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public static HousingCallViewModel Create(HousingCall call)
        {
            var item = new HousingCallViewModel
            {
                Id = call.Id,
                EmployeeId = call.ApplicationUserId,
                Status = call.StatusType.ToLocalizedString(),
                Date = call.Date
            };

            return item;
        }

    }
}
