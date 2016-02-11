using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum Gender
    {
        Male,
        Female
    }

    public class Client 
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MidleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Resource { get; set; }

        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MinSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateMeeting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContractSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ReheshSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateClosed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateContract { get; set; }

        public bool IsSite { get; set; }

        public int CityId { get; set; }

        public virtual City Cities { get; set; }

        public List<DistrictToСlient> DistrictToСlients { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
