using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models
{
    public class BaiVietViewModel
    {
        public long Id { get; set; }

        [DisplayName("Tên bài viết")]
        public string TenBaiViet { get; set; }
        public string Alias { get; set; }

        [DisplayName("Nội dung ngắn")]
        public string NoiDungNgan { get; set; }

        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }
        public HttpPostedFileBase HinhAnhFile { get; set; }
        public DateTime NgayTao { get; set; }
        [DisplayName("Lượt xem ảo")]
        public int? LuotXemAo { get; set; }
        public int? LuotXem { get; set; }
        public DateTime? NgayUpdate { get; set; }

        [DisplayName("Thứ tự trên trang chủ")]
        public int? ThuTuHienThiTrangChu { get; set; }

        [DisplayName("Thứ tự hiển thị")]
        public int? ThuTuHienThi { get; set; }
        public bool? DaXoa { get; set; }

        [DisplayName("Danh mục")]
        public long DanhMucId { get; set; }

        public string TenDanhMuc { get; set; }
    }
}