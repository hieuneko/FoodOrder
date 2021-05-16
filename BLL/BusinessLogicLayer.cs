﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;
using System.Drawing;

namespace BLL
{
    public class BusinessLogicLayer
    {
        private static BusinessLogicLayer _Instance;
        public static BusinessLogicLayer Instance
        {
            get
            {
                if (_Instance == null) return _Instance = new BusinessLogicLayer();
                return _Instance;
            }
        }
        private BusinessLogicLayer() { }

        public void setColumnHeaderDataGridView(DataGridView dgv)
        {
            dgv.Columns["IdMon"].HeaderText = "Id Món";
            dgv.Columns["TenMon"].HeaderText = "Tên Món";
            dgv.Columns["GiaTien"].HeaderText = "Giá Tiền";
            dgv.Columns["SoLanGoimon"].HeaderText = "Số Lần Gọi Món";
            dgv.Columns["DanhMuc"].HeaderText = "Danh Mục";
        }

        /// <summary>
        /// Phan nay danh cho combobox
        /// </summary>
        /// <returns></returns>
        private List<CBBItem> GetCBBItems()
        {
            List<CBBItem> data = new List<CBBItem>();
            data.Add(new CBBItem
            {
                Value = 0,
                Text = "All"
            });
            foreach (DanhMuc i in GetAllDanhMuc())
            {
                data.Add(new CBBItem
                {
                    Value = i.IdDanhMuc,
                    Text = i.TenDanhMuc
                });
            }
            return data;
        }

        public void setCbbDanhMuc(ComboBox cb)
        {
            cb.Items.AddRange(GetCBBItems().ToArray());
            cb.SelectedIndex = 0;
        }
       

        public void SetCbbDetailForm(ComboBox cb)
        {
            List<CBBItem> data = new List<CBBItem>();
            foreach (DanhMuc i in GetAllDanhMuc())
            {
                data.Add(new CBBItem
                {
                    Value = i.IdDanhMuc,
                    Text = i.TenDanhMuc
                });
            }
            cb.Items.AddRange(data.ToArray());
            cb.SelectedIndex = 0;
        }
        public void SetColumnHeader(DataGridView dgv)
        {
            dgv.Columns["IdMon"].HeaderText = "Id Món";
            dgv.Columns["TenMon"].HeaderText = "Tên Món";
            dgv.Columns["GiaTien"].HeaderText = "Giá Tiền";
            dgv.Columns["SoLanGoiMon"].HeaderText = "Số Lần Gọi Món";
            dgv.Columns["DanhMuc"].HeaderText = "Danh Mục";
        }
        public void SetColumnHeaderDM(DataGridView dgv)
        {
            dgv.Columns["IdDanhMuc"].HeaderText = "ID Danh Mục";
            dgv.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
        }
        public MemoryStream GetByteValuesOfAnh(int idAnh)
        {
            byte[] bAnh = null;
            bAnh = DataAccessLayer.Instance.GetAnhMinhHoaByIdAnh(idAnh).Anh;
            MemoryStream ms = new MemoryStream(bAnh, 0, bAnh.Length);
            ms.Write(bAnh, 0, bAnh.Length);
            return ms;
        }
        public AnhMinhHoa GetIdAnhByIdAnh(int idAnh)
        {
            return DataAccessLayer.Instance.GetAnhMinhHoaByIdAnh(idAnh);
        }
        public int GetMaxIdAnh()
        {
            return DataAccessLayer.Instance.GetMaxIdAnh();
        }
        public bool ThemAnhVaoDb(string tenAnh, byte[] Anh)
        {
            return DataAccessLayer.Instance.ThemAnhVaoDB(tenAnh, Anh);
        }

        /// <summary>
        /// //////////////////// all Funcs of Mon
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MonView> GetMonByIdDanhMucAndTenMon(int idMon,string st)
        {
            List<MonView> data = new List<MonView>();
            data = ConvertToListMonview(DataAccessLayer.Instance.GetMonByIdDanhMucAndTenMon(idMon, st));
            return data;
        }
        public Mon GetMonByIdMon(int idMon)
        {
            Mon m = new Mon();
            foreach(Mon i in GetAllMon())
            {
                if(idMon == i.IdMon)
                {
                    m = i;
                }
            }
            return m;
        }

        public List<Mon> GetAllMon()
        {
            return DataAccessLayer.Instance.GetAllMon_DAL();
        }
        public List<DanhMuc> GetAllDanhMuc()
        {
            return DataAccessLayer.Instance.GetAllDanhMuc_DAL();
        }
        public MonView ConvertToMonView(Mon m)
        {
            MonView mv = new MonView();
            foreach (DanhMuc i in GetAllDanhMuc())
            {
                if (m.IdDanhMuc == i.IdDanhMuc)
                {
                    mv.IdMon = m.IdMon;
                    mv.TenMon = m.TenMon;
                    mv.GiaTien = m.GiaTien;
                    mv.SoLanGoiMon = m.SoLanGoiMon;
                    mv.DanhMuc = i.TenDanhMuc;
                    break;
                }
            }
            return mv;
        }
        public List<MonView> ConvertToListMonview(List<Mon> m)
        {
            List<MonView> mv = new List<MonView>();
            foreach (Mon i in m)
            {
                mv.Add(ConvertToMonView(i));
            }
            return mv;
        }
        public int GetMaxIdMon()
        {
            return DataAccessLayer.Instance.GetMaxIdMon();
        }
        public int GetMaxIdDanhMuc()
        {
            return DataAccessLayer.Instance.GetMaxIdMon();
        }
        /// <summary>
        /// ////////// del, add, update
        /// </summary>
        /// <param name="mssv"></param>
        /// <returns></returns>
        public bool XoaMon(int idmon)
        {
            return DataAccessLayer.Instance.XoaMonTheoIdMon(idmon);

        }
        public bool ThemDanhMuc(DanhMuc dm)
        {
            return DataAccessLayer.Instance.ThemDanhMuc(dm);
        }
        public bool SuaDanhMuc(DanhMuc dm)
        {
            return DataAccessLayer.Instance.SuaDanhMuc(dm);
        }
        public bool XoaDanhMuc(int iddanhmuc)
        {
            return DataAccessLayer.Instance.XoaDanhMucTheoIdDanhMuc(iddanhmuc);
        }
        public bool ExcuteDB_BLL(Mon mon, int idMonDetailForm)
        {
            if (idMonDetailForm != 0)
            {
                return DataAccessLayer.Instance.EditMon(mon);
            }
            else
            {
                return DataAccessLayer.Instance.ThemMon(mon);
            }
        }

    }
}
