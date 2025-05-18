using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfApp4.Classes.Helper;

namespace WpfApp4.Data
{
    public partial class Company
    {
        public string CompanyLogin
        {
            get
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                return user.Login;
            }
        }

        public string CompanyEmail
        {
            get
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                return user.Email;
            }
        }

        public string CompanyPhone
        {
            get
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                return user.Phone;
            }
        }

        public string CompanyFIO
        {
            get 
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                return user.FIO;
            }
        }
    }
}
