using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Data
{
    public partial class Vending
    {
        public string AddressAndPlace { 
            get
            {
                return Address + "\n" + Place;
            }

            set { }
        }
    }
}
