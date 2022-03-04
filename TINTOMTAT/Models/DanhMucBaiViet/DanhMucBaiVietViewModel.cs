using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.DanhMucBaiViet
{
    public class DanhMucBaiVietViewModel
    {
        public long Id { get; set; }
        public string TenDanhMuc { get; set; }
        public int? ThuTuHienThi { get; set; }
    }
}