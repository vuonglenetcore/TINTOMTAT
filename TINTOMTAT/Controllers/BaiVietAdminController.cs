using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Infrastructure;
using TINTOMTAT.Models;

namespace TINTOMTAT.Controllers
{
    [CustomAuthenticationFilter]
    public class BaiVietAdminController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();

        public ActionResult Index()
        {
            var result = _connect.BaiViets.Where(x => x.DaXoa != true).Select(p => new BaiVietViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                Alias = p.Alias,
                HinhAnh = p.HinhAnh,
                ThuTuHienThi = p.ThuTuHienThi,
                ThuTuHienThiTrangChu = p.ThuTuHienThiTrangChu,
                LuotXem = p.LuotXem,
                NgayTao = p.NgayTao,
                NoiDungNgan = p.NoiDungNgan,
                NoiDung = p.NoiDung,
                NgayUpdate = p.NgayUpdate,
                DaXoa = p.DaXoa,
                DanhMucId = p.DanhMucId,
                TenDanhMuc = p.DanhMucBaiViet.TenDanhMuc
            });

            return View(result);
        }

        [HttpGet]
        public ActionResult ThemBaiViet()
        {
            var danhMuc = _connect.DanhMucBaiViets.Where(x=>x.DaXoa != true).ToList();
            ViewBag.DanhMuc = danhMuc;

            return View();
        }

        [HttpPost]
        public ActionResult ThemBaiViet(BaiVietViewModel model)
        {
            var baiViet = new BaiViet()
            {
                TenBaiViet = model.TenBaiViet,
                Alias = LoaiDau(model.TenBaiViet),
                ThuTuHienThiTrangChu = model.ThuTuHienThiTrangChu,
                ThuTuHienThi = model.ThuTuHienThi,
                LuotXem = 2000 + model.LuotXem,
                NoiDungNgan = model.NoiDungNgan,
                NoiDung = model.NoiDung,
                DanhMucId = model.DanhMucId,
                NgayTao = DateTime.Now,
                DaXoa = false
            };

            //To Get File Extension  
            string FileExtension = Path.GetExtension(model.HinhAnhFile.FileName);

            string FileName = baiViet.Alias + DateTime.Now.ToString("yyyyMMdd") + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            //Its Create complete path to store in server.  
            model.HinhAnh = UploadPath + FileName;

            //To copy and save file into server.  
            model.HinhAnhFile.SaveAs(Server.MapPath(model.HinhAnh));

            baiViet.HinhAnh = model.HinhAnh;
            _connect.BaiViets.Add(baiViet);
            _connect.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SuaBaiViet(long id)
        {
            var baiViet = _connect.BaiViets.Where(x => x.Id == id).Select(p => new BaiVietViewModel
            {
                Id = p.Id,
                TenBaiViet = p.TenBaiViet,
                ThuTuHienThiTrangChu = p.ThuTuHienThiTrangChu,
                ThuTuHienThi = p.ThuTuHienThi,
                LuotXem = p.LuotXem,
                NoiDungNgan = p.NoiDungNgan,
                NoiDung = p.NoiDung,
                DanhMucId = p.DanhMucId,
                HinhAnh = p.HinhAnh
            }).FirstOrDefault();
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            var danhMuc = _connect.DanhMucBaiViets.Where(x => x.DaXoa != true).ToList();
            ViewBag.DanhMuc = danhMuc;
            return View(baiViet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SuaBaiViet(BaiVietViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            BaiViet baiViet = _connect.BaiViets.FirstOrDefault(x => x.Id == model.Id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            baiViet.TenBaiViet = model.TenBaiViet;
            baiViet.Alias = LoaiDau(model.TenBaiViet);
            baiViet.ThuTuHienThiTrangChu = model.ThuTuHienThiTrangChu;
            baiViet.ThuTuHienThi = model.ThuTuHienThi;
            baiViet.NoiDungNgan = model.NoiDungNgan;
            baiViet.NoiDung = model.NoiDung;
            baiViet.LuotXem = model.LuotXem;
            baiViet.NgayUpdate = DateTime.Now;
            baiViet.DanhMucId = model.DanhMucId;

            if (model.HinhAnhFile != null)
            {
                //dell file olf
                System.IO.File.Delete(Server.MapPath(model.HinhAnh));

                //save file new
                //To Get File Extension  
                string FileExtension = Path.GetExtension(model.HinhAnhFile.FileName);

                string FileName = baiViet.Alias + DateTime.Now.ToString("yyyyMMdd") + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                //Its Create complete path to store in server.  
                model.HinhAnh = UploadPath + FileName;
                baiViet.HinhAnh = model.HinhAnh;

                //To copy and save file into server.  
                model.HinhAnhFile.SaveAs(Server.MapPath(model.HinhAnh));
            }
            _connect.Entry(baiViet).State = EntityState.Modified;
            _connect.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaBaiViet(long id)
        {
            var baiViet = _connect.BaiViets.Where(x => x.Id == id).FirstOrDefault();
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            baiViet.DaXoa = true;

            _connect.Entry(baiViet).State = EntityState.Modified;
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
       
    }
}