using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Models.BaiVietPortalViewModel;

namespace TINTOMTAT.Controllers
{
    public class PostController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();

        public ActionResult Index(string alias)
        {
            var baiViet = _connect.BaiViets.Where(x => x.Alias.Contains(alias)).Select(p => new BaiVietDetailViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                NoiDung = p.NoiDung,
                HinhAnh = p.HinhAnh
            }).FirstOrDefault();

            return View(baiViet);
        }
    }
}