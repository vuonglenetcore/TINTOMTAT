using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Models.BaiVietPortalViewModel;

namespace TINTOMTAT.Controllers
{
    public class HomeController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();
        public ActionResult Index()
        {
            //get danh sách trang chủ
            var result = _connect.BaiViets.Where(x => x.ThuTuHienThiTrangChu.HasValue && x.DaXoa != true).Select(p => new BaiVietViewModel {
            Id = p.Id,
            TenBaiViet = p.TenBaiViet,
            Alias = p.Alias,
            LuotXem = p.LuotXem,
            NoiDungNgan = p.NoiDungNgan,
            NgayTao = p.NgayTao,
            HinhAnh = p.HinhAnh
            }).ToList();
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuPartial()
        {
            ViewBag.cc = "con cac ";
            return PartialView();
        }
    }
}