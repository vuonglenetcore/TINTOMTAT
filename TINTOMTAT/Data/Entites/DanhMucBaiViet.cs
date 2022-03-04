using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TINTOMTAT.Data.Entites
{
    [Table("DanhMucBaiViet")]
    public class DanhMucBaiViet
    {
        public long Id { get; set; }
        public string TenDanhMuc { get; set; }
        public string Alias { get; set; }
        public string Logo { get; set; }
        public int? ThuTuHienThi { get; set; }
        public bool? DaXoa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayUpdate { get; set; }

        public virtual List<BaiViet> BaiViets { get; set; }
    }
}