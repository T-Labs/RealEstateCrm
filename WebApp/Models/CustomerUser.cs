using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public class CustomerUser
    {
        private CustomerUser()
        {
        }

        public int CustomerId { get; set; }
        public DateTime LoginTime { get; set; }
        public string Phone { get; set; }

        public static CustomerUser CreateInstance()
        {
            return new CustomerUser();
        }
        public static CustomerUser FromSession(ISession session)
        {
            var json = session.GetString(typeof(CustomerUser).FullName);

            if (json == null)
            {
                return null;
            }

            var data = JsonConvert.DeserializeObject<CustomerUser>(json);
            return data;
        }

        public void ToSession(ISession session)
        {
            session.SetString(typeof(CustomerUser).FullName, JsonConvert.SerializeObject(this));
        }
    }
}
