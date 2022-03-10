using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Models.BaiVietPortalViewModel;

namespace TINTOMTAT.Controllers
{
    public class CategoryController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();
        // GET: Category
        public ActionResult Index(string alias)
        {
            var result = _connect.BaiViets.Where(x => x.DanhMucBaiViet.Alias == alias && x.DaXoa != true).Select(p => new BaiVietViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                Alias = p.Alias,
                LuotXem = p.LuotXem,
                NoiDungNgan = p.NoiDungNgan,
                NgayTao = p.NgayTao,
                HinhAnh = p.HinhAnh
            }).ToList();

            var postHot = _connect.BaiViets.Where(x => x.DaXoa != true).Take(4).Select(p => new BaiVietViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                Alias = p.Alias,
                LuotXem = p.LuotXem,
                HinhAnh = p.HinhAnh
            }).ToList();

            ViewBag.Title = alias;
            ViewBag.PostHot = postHot;

            return View(result);
        }
    }
}