using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.BaiVietPortalViewModel
{
    public class BaiVietViewModel
    {
        public long Id { get; set; }
        public string TenBaiViet { get; set; }
        public string Alias { get; set; }
        public int? LuotXem { get; set; }
        public DateTime NgayTao { get; set; }
        public string NoiDungNgan { get; set; }
        public string HinhAnh { get; set; }
    }
}