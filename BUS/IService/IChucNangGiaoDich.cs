﻿using DAL.Models;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IChucNangGiaoDich
    {
        GioHang AddGioHang();
        Task UpdateGioHang(int giohang, bool trangthai);
        Task<List<GioHangChiTiet>> AllChiTietTrongXe(int idgiohang);
        Task<CheckResult> AddChiTietGioHang(int idgiohang, int idsanpham);
        Task<CheckResult> UpdateChiTiet(int chitiet, int idsp, int soluong);
        Task<bool> DeleteChiTiet(int chitiet);
        Task<CheckResult> ThanhToan(int idgiohang, int userid, decimal tien, bool phuongthuc, int idkhachhang);
        Task XoaGioHang(int idgiohang);
        Task<IEnumerable<SanPham>> AllSanPham();
        Task<IEnumerable<GioHang>> AllGioHangCho();
    }
}