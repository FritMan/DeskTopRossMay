using LiveCharts.SeriesAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Data
{
    public partial class User
    {
        public string FIO { get
            {
                return Surname + " " + Name[0] + ". " + LastName[0] + ".";
            } 
        }

        public byte[] ImgSource { get
            {
                if(Photo == null)
                {
                    return File.ReadAllBytes("C:\\Users\\Ансар\\Downloads\\png-transparent-traffic-police-police-officer-traffic-commander-people-cartoon-material-thumbnail.png");
                }
                else
                {
                    return Photo;
                }
            } 
        }
    }
}
