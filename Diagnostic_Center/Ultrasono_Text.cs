﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
namespace Diagnostic_Center
{
    public partial class Ultrasono_Text : Form
    {
        connection db = new connection();
        string xx = "";
        int reg = 0;
        public Ultrasono_Text(string x)
        {
            InitializeComponent();
            xx = x;
            reg = Convert.ToInt32(x);
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            pathologist();
        }

        private void Ultrasono_Text_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet60.ultrasono_text' table. You can move, or remove it, as needed.
            this.ultrasono_textTableAdapter.Fill(this.DataSet60.ultrasono_text,xx);
            // TODO: This line of code loads data into the 'DataSet60.diagnostic_person' table. You can move, or remove it, as needed.
            this.diagnostic_personTableAdapter.Fill(this.DataSet60.diagnostic_person,reg);

            this.reportViewer1.RefreshReport();
        }

        void pathologist()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from pathologist", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox1.Items.Add(read[1].ToString());
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = "";
                string designation = "";
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("select * from pathologist where pathologist='" + comboBox1.Text + "'", db.sql);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        name = read[1].ToString();
                        designation = read[2].ToString();
                    }
                    db.sql.Close();
                }
                catch
                {

                }
                ReportParameterCollection r = new ReportParameterCollection();
                r.Add(new ReportParameter("pathologist", name.ToString()));
                r.Add(new ReportParameter("designation", designation.ToString()));
                this.reportViewer1.LocalReport.SetParameters(r);
                db.sql.Close();
            }
            catch
            {

            }
            this.reportViewer1.RefreshReport();
        }


    }
}
