using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var baiViet = _connect.BaiViets.FirstOrDefault(x => x.Alias.Contains(alias));
            var baiVietViewModel = new BaiVietDetailViewModel
            {
                Id = baiViet.Id,
                TenBaiViet = baiViet.TenBaiViet,
                Alias = baiViet.Alias,
                NoiDung = baiViet.NoiDung,
                HinhAnh = baiViet.HinhAnh,
                LuotXem = baiViet.LuotXem,
                NgayTao = baiViet.NgayTao

            };

            var postHot = _connect.BaiViets.Where(x => x.DaXoa != true).Take(4).Select(p => new BaiVietViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                Alias = p.Alias,
                LuotXem = p.LuotXem,
                HinhAnh = p.HinhAnh
            }).ToList();

            ViewBag.PostHot = postHot;
            //update lượt xem

            baiViet.LuotXem += 1;
            _connect.Entry(baiViet).State = EntityState.Modified;
            _connect.SaveChanges();

            return View(baiVietViewModel);
        }
    }
}