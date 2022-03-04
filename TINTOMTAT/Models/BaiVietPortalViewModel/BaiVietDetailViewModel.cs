using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.BaiVietPortalViewModel
{
    public class BaiVietDetailViewModel
    {
        public long Id { get; set; }
        public string TenBaiViet { get; set; }
        public string HinhAnh { get; set; }
        public string NoiDung { get; set; }
    }
}