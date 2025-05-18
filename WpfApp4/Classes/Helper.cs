using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp4.Data;

namespace WpfApp4.Classes
{
    public static class Helper
    {
        public static VseRoss2DBEntities3 Db = new VseRoss2DBEntities3();

        public static Expander UserExp;
    }
}
