using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web_Hoa.Models
{
    public class ThanhToanNguoiDung
    {
        public string Email { get; set; }
        [Display(Name = "UserName")]
        public string TenNguoiDung { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        [Display(Name = "SoLuong")]
        public int? SoLuongHoa { get; set; }
        public int? SoTien { get; set; }
    }
}