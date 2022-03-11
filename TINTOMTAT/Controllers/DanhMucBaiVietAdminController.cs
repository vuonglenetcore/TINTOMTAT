using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Infrastructure;
using TINTOMTAT.Models.DanhMucBaiViet;

namespace TINTOMTAT.Controllers
{
    [CustomAuthenticationFilter]
    public class DanhMucBaiVietAdminController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();

        //[Authorize]
        public ActionResult Index()
        {
            var result = _connect.DanhMucBaiViets.Where(x => x.DaXoa != true).ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult ThemDanhMuc()
        {
            ViewBagDanhMucBaiViet();

            return View();
        }

        [HttpPost]
        public ActionResult ThemDanhMuc(DanhMucBaiVietViewModel model)
        {
            var danhMuc = new DanhMucBaiViet()
            {
                TenDanhMuc = model.TenDanhMuc,
                Alias = LoaiDau(model.TenDanhMuc),
                ThuTuHienThi = model.ThuTuHienThi,
                NgayTao = DateTime.Now,
                DaXoa = false
            };

            _connect.DanhMucBaiViets.Add(danhMuc);
            _connect.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SuaDanhMuc(long id)
        {
            var danhMuc = _connect.DanhMucBaiViets.Where(x => x.Id == id).Select(p => new DanhMucBaiVietViewModel
            {
                Id = p.Id,
                TenDanhMuc = p.TenDanhMuc,
                ThuTuHienThi = p.ThuTuHienThi
            }).FirstOrDefault();
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            ViewBagDanhMucBaiViet();

            return View(danhMuc);
        }

        [HttpPost]
        public ActionResult SuaDanhMuc(DanhMucBaiVietViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            DanhMucBaiViet danhMuc = _connect.DanhMucBaiViets.FirstOrDefault(x => x.Id == model.Id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            danhMuc.TenDanhMuc = model.TenDanhMuc;
            danhMuc.Alias = LoaiDau(model.TenDanhMuc);
            danhMuc.ThuTuHienThi = model.ThuTuHienThi;
            danhMuc.NgayUpdate = DateTime.Now;

            var danhMucUpdate = danhMuc;

            _connect.Entry(danhMuc).State = EntityState.Modified;
            _connect.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaDanhMuc(long id)
        {
            var danhMuc = _connect.DanhMucBaiViets.Where(x => x.Id == id).FirstOrDefault();
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            //_connect.DanhMucBaiViets.Remove(danhMuc);
            danhMuc.DaXoa = true;
            _connect.Entry(danhMuc).State = EntityState.Modified;
            _connect.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public string LoaiDau(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('đ', 'd').Replace('Đ', 'D').Replace(' ', '-');
        }



        public void ViewBagDanhMucBaiViet()
        {
            var thuTuHienThi = _connect.DanhMucBaiViets.Where(x => x.ThuTuHienThi.HasValue && x.DaXoa != true)
                .Select(x => x.ThuTuHienThi).OrderBy(x => x.Value).ToList();

            ViewBag.ThuTuHienThi = thuTuHienThi;
        }
    }
}