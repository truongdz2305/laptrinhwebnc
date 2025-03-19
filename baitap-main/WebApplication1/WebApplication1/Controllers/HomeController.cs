using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

public class HomeController : Controller
{
    private th1Entities2 db = new th1Entities2();

    public ActionResult Index()
    {
        // Lấy danh sách danh mục
        var danhMucs = db.DanhMucs.ToList();

        // Lấy sản phẩm theo từng danh mục
        var sanPhams = db.SanPhams
            .OrderByDescending(sp => sp.NgayCapNhat)
            .ToList();

        // Lưu danh sách sản phẩm vào ViewBag
        ViewBag.SanPhams = sanPhams;

        // Trả danh sách danh mục cho View
        return View(danhMucs);
    }
}
