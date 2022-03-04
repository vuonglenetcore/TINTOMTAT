using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Data.Entites
{
    [Table("BaiViet")]
    public class BaiViet
    {
        public long Id { get; set; }
        public string TenBaiViet { get; set; }
        public string Alias { get; set; }
        public string NoiDungNgan { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayTao { get; set; }
        public int? LuotXem { get; set; }
        public DateTime? NgayUpdate { get; set; }
        public int? ThuTuHienThiTrangChu { get; set; }
        public int? ThuTuHienThi { get; set; }
        public bool? DaXoa { get; set; }
        public long DanhMucId { get; set; }

        [ForeignKey("DanhMucId")]
        public virtual DanhMucBaiViet DanhMucBaiViet { get; set; }
    }
}