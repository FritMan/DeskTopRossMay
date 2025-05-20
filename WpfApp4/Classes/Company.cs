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

                if (user == null)
                {
                    return "1";
                }
                else
                {
                    return user.Login;
                }
            }

            set { }
        }

        public string CompanyEmail
        {
            get
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                if(user == null)
                {
                    return "test@mail.ru";
                }
                else
                {
                    return user.Email;
                }
            }

            set { }
        }

        public string CompanyPhone
        {
            get
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                if (user == null)
                {
                    return "+ 7 (xxx) (xxx) xx xx";
                }
                else
                {
                    return user.Phone;
                }
            }

            set { }
        }

        public string CompanyFIO
        {
            get 
            {
                var user = Db.User.FirstOrDefault(el => el.Id == UserId);

                if (user == null)
                {
                    return "Иванов Иван Иванович";
                }
                else
                {
                    return user.FIO;
                }
            }

            set { }
        }
    }
}
