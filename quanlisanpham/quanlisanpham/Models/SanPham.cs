using Microsoft.AspNetCore.Http; // Thêm dòng này để hỗ trợ upload file
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class SanPham
{
    public int Id { get; set; }

    public string TenSanPham { get; set; }

    public decimal Gia { get; set; }

    public string HinhAnh { get; set; }

    public string MoTa { get; set; }

    public DateTime NgayTao { get; set; } = DateTime.Now;

    [NotMapped] // Không lưu vào database
    public IFormFile FileAnh { get; set; } // Thêm dòng này
}
