using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.DanhMucBaiViet
{
    public class DanhMucBaiVietViewModel
    {
        public long Id { get; set; }

        [DisplayName("Tên danh mục")]
        public string TenDanhMuc { get; set; }

        public string Alias { get; set; }

        [DisplayName("Thứ tự hiển thị")]
        public int? ThuTuHienThi { get; set; }
    }
}