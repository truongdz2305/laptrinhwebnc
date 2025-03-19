using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanlisanpham.Models;
using QuanLySanPham.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace QuanLySanPham.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public SanPhamController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SanPham sanPham)
        {
            if (sanPham.FileAnh != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + sanPham.FileAnh.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sanPham.FileAnh.CopyTo(fileStream);
                }

                sanPham.HinhAnh = uniqueFileName; // Lưu đường dẫn ảnh vào DB
            }

            _context.SanPhams.Add(sanPham);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
