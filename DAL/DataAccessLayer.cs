﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DBProvider;
using System.Data;
using System.Windows.Forms;

namespace DAL
{
    public class DataAccessLayer
    {
        private static DataAccessLayer _Instance;
        public static DataAccessLayer Instance
        {
            get
            {
                if (_Instance == null) return _Instance = new DataAccessLayer();
                return _Instance;
            }
        }
        private DataAccessLayer() { }
        /// <summary>
        /// lay anh minh hoa tu DataTable
        /// </summary>
        /// <param name="i">1 data row of datatable</param>
        /// <returns></returns>
        private AnhMinhHoa GetAnh(DataRow i)
        {
            return new AnhMinhHoa
            {
                IdAnh = int.Parse(i["IdAnh"].ToString()),
                TenAnh = i["TenAnh"].ToString(),
                Anh = (byte[])i["Anh"]
            };
        }
        /// <summary>
        /// Lay toan bo anh minh hoa tu DB => List<Anhminhhoa>
        /// </summary>
        /// <returns>List<AnhMinhHoa></returns>
        public List<AnhMinhHoa> GetListAnhMinhHoa()
        {
            try
            {
                List<AnhMinhHoa> anhMinhHoas = new List<AnhMinhHoa>();
                string query = "select * from AnhMinhHoa";
                foreach (DataRow i in DBHelper.Instance.GetDataTable(query).Rows)
                {
                    anhMinhHoas.Add(GetAnh(i));
                }
                return anhMinhHoas;
            }
            catch(Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
                return null;
            }
        }
        public List<DanhMuc> GetAllDanhMuc_DAL()
        {
            string query = "select * from DanhMuc";
            List<DanhMuc> lisDanhMuc = new List<DanhMuc>();
            foreach (DataRow i in DBHelper.Instance.GetDataTable(query).Rows)
            {
                lisDanhMuc.Add(GetDanhMuc(i));
            }
            return lisDanhMuc;
        }
        private DanhMuc GetDanhMuc(DataRow i)
        {
            return new DanhMuc
            {
                TenDanhMuc = i["TenDanhMuc"].ToString(),
                IdDanhMuc = int.Parse(i["IdDanhMuc"].ToString())
            };
        }
        /////
        public List<Mon> GetAllMon_DAL()
        {
            try
            {
                string query = "select * from Mon";
                List<Mon> listMon = new List<Mon>();
                foreach (DataRow i in DBHelper.Instance.GetDataTable(query).Rows)
                {
                    listMon.Add(getMon(i));
                }
                return listMon;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private Mon getMon(DataRow i)
        {
            return new Mon
            {
                IdMon = int.Parse(i["IdMon"].ToString()),
                TenMon = i["TenMon"].ToString(),
                SoLanGoiMon = int.Parse(i["SoLanGoiMon"].ToString()),
                GiaTien = int.Parse(i["GiaTien"].ToString()),
                IdDanhMuc = Convert.ToInt32(i["IdDanhMuc"].ToString()),
                IdAnh = int.Parse(i["IdAnh"].ToString())

            };
        }
        public List<Mon> GetMonByIdDanhMucAndTenMon(int idDanhMuc, string st)
        {
            List<Mon> data = new List<Mon>();
            if (idDanhMuc == 0)
            {
                if (string.IsNullOrEmpty(st)) data = DataAccessLayer.Instance.GetAllMon_DAL();
                else
                {
                    foreach (Mon i in DataAccessLayer.Instance.GetAllMon_DAL())
                    {
                        if (i.TenMon.ToLower().Contains(st.ToLower()) || ((i.GiaTien).ToString()) == st) data.Add(i);
                    }
                }
            }
            else
            {
                foreach (Mon i in DataAccessLayer.Instance.GetAllMon_DAL())
                {
                    if (i.IdDanhMuc == idDanhMuc && (i.TenMon.ToLower().Contains(st.ToLower()) || ((i.GiaTien).ToString())==st)) data.Add(i);
                }
            }

            return data;
        }
        public int GetMaxIdMon()
        {
            return DBHelper.Instance.GetMaxValueOf("IdMon");
        }
        
        /// <summary>
        /// ///////// CRUD
        /// </summary>
        /// <param name="IdMon"></param>
        /// <returns></returns>
        public bool XoaMonTheoIdMon(int IdMon)
        {
            try
            {
                string query = "delete from Mon where IdMon = @id ";
                object[] prams = { IdMon };
                return DBHelper.Instance.ExecuteNonQuery(query, prams) > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool checkMon(int IdMon)
        {
            try
            {
                foreach (Mon i in GetAllMon_DAL())
                {
                    if (i.IdMon == IdMon)
                    {
                        return true;
                        
                    }
                    break;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ThemMon(Mon Mon)
        {
            try
            {
                string query = "insert into Mon(TenMon, GiaTien, SoLanGoiMon, IdDanhMuc, IdAnh)" +
                                   "values ( @name , @gia , @solangoi , @idDm , @idAnh )";
                object[] prams = {Mon.TenMon, Mon.GiaTien, Mon.SoLanGoiMon, Mon.IdDanhMuc, Mon.IdAnh };
                return DBHelper.Instance.ExecuteNonQuery(query, prams) > 0;
            }
            catch (Exception)
            {
                MessageBox.Show("ID món đã tồn tại!");
                return false;
            }
        }
        public bool EditMon(Mon Mon)
        {
            try
            {
                string query = "update Mon set TenMon = @name , GiaTien = @gia ," +
                        " SoLanGoiMon = @solangoi , IdDanhMuc = @idDm , IdAnh = @idAnh where IdMon = @idmon ";
                object[] prams = { Mon.TenMon, Mon.GiaTien, Mon.SoLanGoiMon, Mon.IdDanhMuc, Mon.IdAnh, Mon.IdMon };
                return DBHelper.Instance.ExecuteNonQuery(query, prams) > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
