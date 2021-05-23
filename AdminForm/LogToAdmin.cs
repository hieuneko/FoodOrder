﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm
{
    public partial class LogToAdmin : Form
    {
        public LogToAdmin()
        {
            InitializeComponent();
        }
        private string UserName = "pbl3";
        private string Password = "123456";

        private void ptEixt_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtuser_Click(object sender, EventArgs e)
        {
            txtuser.Clear();
            ptuser.Image = Properties.Resources.user2;
            ptuser.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.ForeColor = Color.DeepSkyBlue;
            txtuser.ForeColor = Color.WhiteSmoke;

            ptpw.Image = Properties.Resources.pw;
            ptpw.SizeMode = PictureBoxSizeMode.StretchImage;
            panel2.ForeColor = Color.WhiteSmoke;
            txtpw.ForeColor = Color.WhiteSmoke;
        }

        private void txtpw_Click(object sender, EventArgs e)
        {
            txtpw.Clear();
            txtpw.PasswordChar = '*';
            ptpw.Image = Properties.Resources.pw2;
            ptpw.SizeMode = PictureBoxSizeMode.StretchImage;
            panel2.ForeColor = Color.DeepSkyBlue;
            txtpw.ForeColor = Color.WhiteSmoke;

            ptuser.Image = Properties.Resources.user;
            ptuser.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.ForeColor = Color.WhiteSmoke;
            txtuser.ForeColor = Color.WhiteSmoke;
        }
        private void form_close(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void btlog_Click(object sender, EventArgs e)
        {
            if(txtuser.Text == UserName && txtpw.Text == Password)
            {
                MainForm mf = new MainForm();
                mf.FormClosed += new FormClosedEventHandler(form_close);
                mf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("User or Password incorrect!");
                txtpw.Clear();
            }
        }
    }
}
