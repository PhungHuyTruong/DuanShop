﻿using DAL.Models;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepo
{
    public interface IQuanLyGiaoDich
    {
          GioHang AddGioHang();
          Task UpdateGioHang(int giohang,bool trangthai);
          Task<List<GioHangChiTiet>> AllChiTietTrongXe(int idgiohang);
          Task<CheckResult> AddChiTietGioHang(int idgiohang, int idsanpham);
          Task<CheckResult>  UpdateChiTiet(int chitiet, int idsp, int soluong);
          Task<bool> DeleteChiTiet(int chitiet);


          Task TruSoLuong(HoaDon hoaDon);
          Task<(CheckResult, HoaDon)> TaoHoaDon(GioHang gioHang, int userid, bool phuongthuc, decimal tongtien, int idkhachhang);
          Task<CheckResult> ThanhToan(int idgiohang, int userid, decimal tien, bool phuongthuc, int idkhachhang);

          Task XoaGioHang(int idgiohang);
          




        //PHu
          Task<IEnumerable<SanPham>> AllSanPham();
          Task<IEnumerable<GioHang>> AllGioHangCho();



        // tab2
        //Task<IEnumerable<HoaDon>> AllHoaDonVanChuyen();
        //Task<IEnumerable<HoaDonChiTiet>> AllChiTietHoaDon(); 
        //Task XacThucHoaDon(int hoadon);
        //Task DeleteHoaDon(int hoadon);




    }
}
