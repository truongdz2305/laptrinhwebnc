using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        private th1Entities2 db = new th1Entities2();

        // GET: SanPham/Add
        public ActionResult Add()
        {
            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc"); // Load danh mục
            return View();
        }

        // POST: SanPham/Add
        [HttpPost]
        public ActionResult Add(SanPham sp, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    fileUpload.SaveAs(path);
                    sp.HinhAnh = "/Images/" + fileName;
                }
                else
                {
                    sp.HinhAnh = "/Images/default.png"; // Ảnh mặc định nếu không có ảnh
                }

                sp.NgayCapNhat = DateTime.Now;

                db.SanPhams.Add(sp);
                db.SaveChanges();

                return RedirectToAction("Info", new { id = sp.ID });
            }

            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.DanhMucID);
            return View(sp);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null) return HttpNotFound();

            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.DanhMucID);
            return View(sp);
        }

        // POST: SanPham/Edit
        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var spOld = db.SanPhams.Find(sp.ID);
                if (spOld != null)
                {
                    spOld.TenSanPham = sp.TenSanPham;
                    spOld.MoTa = sp.MoTa;
                    spOld.Gia = sp.Gia;
                    spOld.DanhMucID = sp.DanhMucID;
                    spOld.NgayCapNhat = DateTime.Now;

                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        fileUpload.SaveAs(path);
                        spOld.HinhAnh = "/Images/" + fileName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Info", new { id = sp.ID });
                }
            }

            ViewBag.DanhMucID = new SelectList(db.DanhMucs, "ID", "TenDanhMuc", sp.DanhMucID);
            return View(sp);
        }

        // GET: SanPham/Info
        public ActionResult Info()
        {
            var danhSach = db.SanPhams.Include("DanhMuc").AsNoTracking().ToList();
            return View(danhSach);
        }

        // GET: SanPham/Index
        public ActionResult Index()
        {
            var danhSach = db.SanPhams.Include("DanhMuc").AsNoTracking().ToList();
            return View(danhSach);
        }

        // DELETE: SanPham/Delete
        public ActionResult Delete(int id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null) return HttpNotFound();

            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
