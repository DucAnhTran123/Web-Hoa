using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Hoa.Models
{
    public class Item
    {
        public Hoa Hoa
        {
            get;
            set;
        }
        public int? SoLuongHoa
        {
            get;
            set;
        }
        public int? ThanhTien { get; set; }
    }
}