﻿using BUS.IService;
using DAL.Models;
using System.Globalization;
using System.Windows.Forms;

namespace GUI
{
    public partial class KhoHang : Form
    {
        private string _currentpic;
        private int seletedid;
        private int selectedKhuyenmai;

        private readonly IChucNangKhoHang _services;
        public KhoHang(IChucNangKhoHang services)
        {
            _services = services;
            InitializeComponent();
            LoadComboBox();
            cbb_boolkhuyemai.SelectedIndexChanged += cbb_boolkhuyemai_SelectedIndexChanged;
            LoadDataGrid(_services.GetAllSanPham());
            LoadDataKhuyenMai(_services.GetAllGiamGia());
            if (cbb_boolkhuyemai.SelectedValue != null)
            {
                string filter = cbb_boolkhuyemai.SelectedValue.ToString();
                LoadDataSPKhuyenMai(_services.GetAllSanPham(), filter);
            }
        }
        public void LoadComboBox()
        {

            var allhang = _services.GetAllHangSP();
            cbb_hangsanpham.DataSource = allhang;
            cbb_hangsanpham.DisplayMember = "TenHang";
            cbb_hangsanpham.ValueMember = "IdHang";

            var allkichco = _services.GetAllKichCo();
            cbb_kichco.DataSource = allkichco;
            cbb_kichco.DisplayMember = "KichCo1";
            cbb_kichco.ValueMember = "IdKichCo";

            var allmausac = _services.GetAllMauSac();
            cbb_mausac.DataSource = allmausac;
            cbb_mausac.DisplayMember = "MauSac1";
            cbb_mausac.ValueMember = "IdMau";

            var allgioitinh = _services.GetAllGioiTinh();
            cbb_gioitinh.DataSource = allgioitinh;
            cbb_gioitinh.DisplayMember = "TenGioiTinh";
            cbb_gioitinh.ValueMember = "IdGioiTinh";

            var filterOptions = new List<ComboItem>
            {
            new ComboItem { Id = "KoGiamGia", Name = "Ko Giảm Giá" },
            new ComboItem { Id = "GiamGia", Name = "Giảm Giá" }
             };
            cbb_boolkhuyemai.DataSource = filterOptions;
            cbb_boolkhuyemai.DisplayMember = "Name";
            cbb_boolkhuyemai.ValueMember = "Id";
            cbb_boolkhuyemai.SelectedIndex = 0;

        }
        private void cbb_boolkhuyemai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_boolkhuyemai.SelectedValue != null)
            {
                string selectedFilter = cbb_boolkhuyemai.SelectedValue.ToString();
                LoadDataSPKhuyenMai(_services.GetAllSanPham(), selectedFilter);
            }
        }

        public void LoadDataGrid(IEnumerable<SanPham> data)
        {
            dtg_sanpham.Rows.Clear();

            dtg_sanpham.ColumnCount = 21;

            dtg_sanpham.Columns[0].Name = "IdSanPham"; dtg_sanpham.Columns[0].HeaderText = "Mã Sản Phẩm"; dtg_sanpham.Columns[0].Visible = false;
            dtg_sanpham.Columns[1].Name = "TenSanPham"; dtg_sanpham.Columns[1].HeaderText = "Tên Sản Phẩm";
            dtg_sanpham.Columns[2].Name = "Hang"; dtg_sanpham.Columns[2].HeaderText = "Hãng";
            dtg_sanpham.Columns[3].Name = "SoLuong"; dtg_sanpham.Columns[3].HeaderText = "Số Lượng";
            dtg_sanpham.Columns[4].Name = "MoTa"; dtg_sanpham.Columns[4].HeaderText = "Mô Tả";
            dtg_sanpham.Columns[5].Name = "ChatLieu"; dtg_sanpham.Columns[5].HeaderText = "Chất Liệu";
            dtg_sanpham.Columns[6].Name = "KieuGiay"; dtg_sanpham.Columns[6].HeaderText = "Kiểu Giày";
            dtg_sanpham.Columns[7].Name = "GioiTinh"; dtg_sanpham.Columns[7].HeaderText = "Giới Tính";
            dtg_sanpham.Columns[8].Name = "KichCo"; dtg_sanpham.Columns[8].HeaderText = "Kích Cỡ";
            dtg_sanpham.Columns[9].Name = "MauSac"; dtg_sanpham.Columns[9].HeaderText = "Màu Sắc";
            dtg_sanpham.Columns[10].Name = "Barcode"; dtg_sanpham.Columns[10].HeaderText = "Mã Vạch";
            dtg_sanpham.Columns[11].Name = "Gia"; dtg_sanpham.Columns[11].HeaderText = "Giá";
            dtg_sanpham.Columns[12].Name = "GiamGia"; dtg_sanpham.Columns[12].HeaderText = "Giảm Giá";
            dtg_sanpham.Columns[13].Name = "ChuongTrinh"; dtg_sanpham.Columns[13].HeaderText = "Chương Trình";
            dtg_sanpham.Columns[14].Name = "TrangThai"; dtg_sanpham.Columns[14].HeaderText = "Trạng Thái"; dtg_sanpham.Columns[14].Visible = false;
            dtg_sanpham.Columns[15].Name = "TenTrangThai"; dtg_sanpham.Columns[15].HeaderText = "Tên Trạng Thái";
            dtg_sanpham.Columns[16].Name = "HinhAnh"; dtg_sanpham.Columns[16].HeaderText = "Hình Ảnh"; dtg_sanpham.Columns[16].Visible = false;
            dtg_sanpham.Columns[17].Name = "IdHang"; dtg_sanpham.Columns[17].HeaderText = "Mã Hãng";
            dtg_sanpham.Columns[18].Name = "IdGioiTInh"; dtg_sanpham.Columns[18].HeaderText = "Mã Giới Tính";
            dtg_sanpham.Columns[19].Name = "IdKichCo"; dtg_sanpham.Columns[19].HeaderText = "Mã Kích Cỡ";
            dtg_sanpham.Columns[20].Name = "IdMauSac"; dtg_sanpham.Columns[20].HeaderText = "Mã Màu Sắc";



            foreach (SanPham sp in data)
            {
                string tenHang = sp.IdHangNavigation?.TenHang ?? "Null";
                string tenGioiTinh = sp.IdGioiTinhNavigation?.TenGioiTinh ?? "Null";
                int kichCo = sp.KichCoNavigation?.KichCo1 ?? 0;
                string mauSac = sp.MauSacNavigation?.MauSac1 ?? "Null";
                decimal? mucGiamGia = sp.IdGiamGiaNavigation?.MucGiamGia ?? 0;
                string chuongtrinh = sp.IdGiamGiaNavigation?.LoaiGiamGia ?? "";
                string trangthai = sp.TrangThai ? "Kinh Doanh" : "Ngừng Kinh Doanh";

                dtg_sanpham.Rows.Add(sp.IdSanPham, sp.TenSanPham, tenHang, sp.SoLuong, sp.MoTa, sp.ChatLieu,
                                     sp.KieuGiay, tenGioiTinh, kichCo, mauSac, sp.Barcode, sp.Gia, mucGiamGia,
                                     chuongtrinh, sp.TrangThai, trangthai, sp.HinhAnh, sp.IdHang, sp.IdGioiTinh, sp.KichCo, sp.MauSac
                                     );
            }
        }
        public void LoadDataSPKhuyenMai(IEnumerable<SanPham> data, string filter)
        {
            dtg_sanphamkhuyenmai.Rows.Clear();
            dtg_sanphamkhuyenmai.ColumnCount = 14;
            dtg_sanphamkhuyenmai.Columns[0].Name = "IdSanPham";
            dtg_sanphamkhuyenmai.Columns[1].Name = "IdGiamGia";
            dtg_sanphamkhuyenmai.Columns[2].Name = "TenSanPham";
            dtg_sanphamkhuyenmai.Columns[3].Name = "HangSP";
            dtg_sanphamkhuyenmai.Columns[4].Name = "Barcode";
            dtg_sanphamkhuyenmai.Columns[5].Name = "MauSac";
            dtg_sanphamkhuyenmai.Columns[6].Name = "Phai";
            dtg_sanphamkhuyenmai.Columns[7].Name = "KichCo";
            dtg_sanphamkhuyenmai.Columns[8].Name = "ChuongTrinh";
            dtg_sanphamkhuyenmai.Columns[9].Name = "Giam";
            dtg_sanphamkhuyenmai.Columns[10].Name = "TuNgay";
            dtg_sanphamkhuyenmai.Columns[11].Name = "DenNgay";
            dtg_sanphamkhuyenmai.Columns[12].Name = "GiaGoc";
            dtg_sanphamkhuyenmai.Columns[13].Name = "GiaGiam";

            dtg_sanphamkhuyenmai.Columns[0].Visible = false;
            dtg_sanphamkhuyenmai.Columns[1].Visible = false;
            dtg_sanphamkhuyenmai.Columns[3].Visible = false;
            dtg_sanphamkhuyenmai.Columns[5].Visible = false;
            dtg_sanphamkhuyenmai.Columns[6].Visible = false;
            dtg_sanphamkhuyenmai.Columns[7].Visible = false;
            dtg_sanphamkhuyenmai.Columns[10].Visible = false;
            dtg_sanphamkhuyenmai.Columns[11].Visible = false;
            dtg_sanphamkhuyenmai.Columns[12].Visible = false;
            dtg_sanphamkhuyenmai.Columns[13].Visible = false;
            IEnumerable<SanPham> filteredData;

            if (filter == "KoGiamGia")
            {
                filteredData = data.Where(sp => sp.IdGiamGia == null);
            }
            else
            {
                filteredData = data.Where(sp => sp.IdGiamGia != null);
            }

            foreach (var sp in filteredData)
            {
                var phantram = sp.IdGiamGiaNavigation?.MucGiamGia / 100 ?? 0;
                string giagiam = Convert.ToString(sp.Gia * (1 - phantram));
                dtg_sanphamkhuyenmai.Rows.Add(
                    sp.IdSanPham, sp.IdGiamGia, sp.TenSanPham, sp.IdHangNavigation.TenHang,
                    sp.Barcode, sp.MauSacNavigation.MauSac1, sp.IdGioiTinhNavigation.TenGioiTinh,
                    sp.KichCoNavigation.KichCo1, sp.IdGiamGiaNavigation?.LoaiGiamGia,
                    sp.IdGiamGiaNavigation?.MucGiamGia, sp.IdGiamGiaNavigation?.NgayBatDau,
                    sp.IdGiamGiaNavigation?.NgayKetThuc, sp.Gia, giagiam);
            }
        }
        public void LoadDataKhuyenMai(IEnumerable<GiamGium> data)
        {
            dtg_khuyenmai.Rows.Clear();
            dtg_khuyenmai.ColumnCount = 5;
            dtg_khuyenmai.Columns[0].Name = "ID";
            dtg_khuyenmai.Columns[1].Name = "Ten";
            dtg_khuyenmai.Columns[2].Name = "Giam";
            dtg_khuyenmai.Columns[3].Name = "TuNgay";
            dtg_khuyenmai.Columns[4].Name = "DenNgay";
            foreach (GiamGium g in data)
            {
                dtg_khuyenmai.Rows.Add(g.IdGiamGia, g.LoaiGiamGia, g.MucGiamGia, g.NgayBatDau, g.NgayKetThuc);
            }

        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            string timkiem = txt_timkiem.Text;
            LoadDataGrid(_services.SearchSanPham(timkiem));
        }

        private void dtg_sanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            var sanphamhientai = dtg_sanpham.Rows[index];
            var cellvalue = Enumerable.Range(0, sanphamhientai.Cells.Count)
            .ToDictionary(i => $"{i}", i => sanphamhientai.Cells[i].Value ?? "null");
            if (index >= 0)
            {

                string imgpath = cellvalue["16"] != null ? cellvalue["16"].ToString() : string.Empty;
                pt_hinhanh.Image = FileManager.SPPicManager.LoadPic(imgpath);
                _currentpic = imgpath;
                if (Convert.ToBoolean(cellvalue["14"]) == true) { rb_kinhdoanh.Checked = true; }
                else { rb_ngungknihdoanh.Checked = true; }
                seletedid = Convert.ToInt32(cellvalue["0"].ToString());
                txt_id.Text = cellvalue["0"].ToString();
                txt_ten.Text = cellvalue["1"].ToString();
                cbb_hangsanpham.SelectedValue = cellvalue["17"];
                txt_soluong.Text = cellvalue["3"].ToString();
                txt_chatlieu.Text = cellvalue["5"].ToString();
                txt_kieugiay.Text = cellvalue["6"].ToString();
                txt_barcode.Text = cellvalue["10"].ToString();
                cbb_gioitinh.SelectedValue = cellvalue["18"];
                cbb_kichco.SelectedValue = cellvalue["19"];
                cbb_mausac.SelectedValue = cellvalue["20"];
                txt_mucgiam.Text = cellvalue["12"].ToString();
                txt_tengiamgia.Text = cellvalue["13"].ToString();
                txt_mota.Text = cellvalue["4"].ToString();
                txt_gia.Text = cellvalue["11"].ToString();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            var ten = txt_ten.Text;
            var hang = Convert.ToInt32(cbb_hangsanpham.SelectedValue);
            var soluong = Convert.ToInt32(txt_soluong.Text);
            var mota = txt_mota.Text;
            var chatlieu = txt_chatlieu.Text;
            var kieugiay = txt_kieugiay.Text;
            var gioitinh = Convert.ToInt32(cbb_gioitinh.SelectedValue);
            var kichco = Convert.ToInt32(cbb_kichco.SelectedValue);
            var mausac = Convert.ToInt32(cbb_mausac.SelectedValue);
            var barcode = txt_barcode.Text;
            var gia = Convert.ToDecimal(txt_gia.Text);
            var hinhanh = _currentpic;

            if (!_services.CreateSanPham(ten, hang, soluong, mota, chatlieu, kieugiay, gioitinh, kichco, mausac, barcode, gia, hinhanh))
            {
                MessageBox.Show("Bị Trùng Thông Tin");
                return;
            }

            LoadDataGrid(_services.GetAllSanPham());

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            var id = seletedid;
            var ten = txt_ten.Text;
            var hang = Convert.ToInt32(cbb_hangsanpham.SelectedValue);
            var soluong = Convert.ToInt32(txt_soluong.Text);
            var mota = txt_mota.Text;
            var chatlieu = txt_chatlieu.Text;
            var kieugiay = txt_kieugiay.Text;
            var gioitinh = Convert.ToInt32(cbb_gioitinh.SelectedValue);
            var kichco = Convert.ToInt32(cbb_kichco.SelectedValue);
            var mausac = Convert.ToInt32(cbb_mausac.SelectedValue);
            var barcode = txt_barcode.Text;
            var gia = Convert.ToDecimal(txt_gia.Text);


            var sanPham = _services.GetSanPhamByID(id);
            var oldHinhAnh = sanPham.HinhAnh;
            var hinhanh = oldHinhAnh;
            if (_currentpic != null && _currentpic != oldHinhAnh)
            {
                FileManager.SPPicManager.DeletePic(oldHinhAnh);
                hinhanh = _currentpic;
            }
            else if (_currentpic == null && oldHinhAnh != null)
            {
                FileManager.SPPicManager.DeletePic(oldHinhAnh);
                hinhanh = null;
            }

            var trangthai = false;
            if (rb_kinhdoanh.Checked == true)
            {
                trangthai = true;
            }
            else if (rb_ngungknihdoanh.Checked == true)
            {
                trangthai = false;
            }

            if (!_services.UpdateSanPham(id, ten, hang, soluong, mota, chatlieu, kieugiay, gioitinh, kichco, mausac, barcode, gia, trangthai, hinhanh))
            {
                MessageBox.Show("Bị Trùng Thông Tin");
                return;
            }
            LoadDataGrid(_services.GetAllSanPham());
        }

        private void btn_uploadanh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select a Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _currentpic = FileManager.SPPicManager.SavePic(openFileDialog.FileName);
                    pt_hinhanh.Image = FileManager.SPPicManager.LoadPic(_currentpic);
                }
            }
        }

        private void btn_kopic_Click(object sender, EventArgs e)
        {
            pt_hinhanh.Image = null;
            _currentpic = null;
        }

        private void dtg_khuyenmai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            var khuyenmaichon = dtg_khuyenmai.Rows[rowindex];
            var cellvalue = Enumerable.Range(0, khuyenmaichon.Cells.Count).ToDictionary(i => i, i => khuyenmaichon.Cells[i].Value ?? "");
            if (rowindex >= 0)
            {
                string text = txt2_giagoc.Text ?? "0";
                decimal mucgiam = Convert.ToDecimal(cellvalue[2]);
                decimal giagoc = decimal.TryParse(text, out giagoc) ? giagoc : 0;
                var giagiam = giagoc * (1 - mucgiam / 100);

                selectedKhuyenmai = Convert.ToInt32(cellvalue[0]);
                txt2_chuongtrinh.Text = cellvalue[1].ToString();
                txt2_mucgiam.Text = cellvalue[2].ToString();
                txt2_tungay.Text = cellvalue[3].ToString();
                txt2_denngay.Text = cellvalue[4].ToString();
                txt2_giagiam.Text = Convert.ToString(giagiam);
            }
        }

        private void dtg_sanphamkhuyenmai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            var sanphamchon = dtg_sanphamkhuyenmai.Rows[rowindex];
            var cellvalue = Enumerable.Range(0, sanphamchon.Cells.Count).ToDictionary(i => i, i => sanphamchon.Cells[i].Value ?? "null");
            if (rowindex >= 0)
            {
                seletedid = Convert.ToInt32(cellvalue[0]);
                txt2_barcode.Text = cellvalue[4].ToString();
                txt2_ten.Text = cellvalue[2].ToString();
                txt2_hang.Text = cellvalue[3].ToString();
                txt2_mausac.Text = cellvalue[5].ToString();
                txt2_phai.Text = cellvalue[6].ToString();
                txt2_kichco.Text = cellvalue[7].ToString();
                txt2_chuongtrinh.Text = cellvalue[8].ToString();
                txt2_mucgiam.Text = cellvalue[9].ToString();
                txt2_tungay.Text = cellvalue[10].ToString();
                txt2_denngay.Text = cellvalue[11].ToString();
                txt2_giagoc.Text = cellvalue[12].ToString();
                txt2_giagiam.Text = cellvalue[13].ToString();
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            int idsp = seletedid;
            int idgg = selectedKhuyenmai;
            if (!_services.UpdateKhuyenMai(idsp, idgg))
            {
                MessageBox.Show("Chua Chon Sp Hoac Khuyen Mai");
                return;
            }
            if (cbb_boolkhuyemai.SelectedValue != null)
            {
                string filter = cbb_boolkhuyemai.SelectedValue.ToString();
                LoadDataSPKhuyenMai(_services.GetAllSanPham(), filter);
            }
        }

        private void btn_huykhuyenmai_Click(object sender, EventArgs e)
        {
            int idsp = seletedid;
            if (!_services.DeleteKhuyenMai(idsp))
            {
                MessageBox.Show("Chua Chon Sp");
                return;
            }
            if (cbb_boolkhuyemai.SelectedValue != null)
            {
                string filter = cbb_boolkhuyemai.SelectedValue.ToString();
                LoadDataSPKhuyenMai(_services.GetAllSanPham(), filter);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentpic = null;
            seletedid = -1;
            selectedKhuyenmai = -1;
        }

        private void KhoHang_Load(object sender, EventArgs e)
        {
            LoadDataGrid(_services.GetAllSanPham());
        }
    }
}