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
using System.Globalization;
using Microsoft.Reporting.WinForms;
using System.IO;
namespace Diagnostic_Center
{
    public partial class user_Account : Form
    {
        connection db = new connection();
        Double doctor_visit = 0;

        public user_Account()
        {
            InitializeComponent();
            user();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent=100;

        }


        void doctor()

        {

            try
            {
                string date = dateTimePicker1.Text;

                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select sum(paid) from user_cash_collection_doctor where date='" + date + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    doctor_visit = Convert.ToDouble(read[0].ToString());
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        Image logo;
        string base64String;


        void header()
        {
            try
            {
                doctor();
                string name = "";
                string address = "";
                string phone = "";
                string mobile = "";
                string establish = "";
                string date1 = dateTimePicker1.Text;
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from print_head", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    name = read[1].ToString();
                    address = read[2].ToString();
                    phone = read[3].ToString();
                    mobile = read[4].ToString();
                    establish = read[5].ToString();
                    MemoryStream ms = new MemoryStream((byte[])read[6]);
                    logo = Image.FromStream(ms);
                    try
                    {

                        // Convert Image to byte[]

                        byte[] imageBytes = ms.ToArray();

                        // Convert byte[] to Base64 String
                        base64String = Convert.ToBase64String(imageBytes);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                ReportParameterCollection r = new ReportParameterCollection();
                r.Add(new ReportParameter("name", name.ToString()));
                r.Add(new ReportParameter("address", address.ToString()));
                r.Add(new ReportParameter("phone", phone.ToString()));
                r.Add(new ReportParameter("mobile", mobile.ToString()));
                r.Add(new ReportParameter("establish", establish.ToString()));
                r.Add(new ReportParameter("doctor_visit", doctor_visit.ToString()));
                r.Add(new ReportParameter("logo", base64String.ToString()));
                r.Add(new ReportParameter("date1", date1));

                this.reportViewer1.LocalReport.SetParameters(r);
                db.sql.Close();
            }
            catch
            {

            }
        }
        private void user_Account_Load(object sender, EventArgs e)
        {
            try
            {

                header();
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                ReportParameterCollection r = new ReportParameterCollection();
                r.Add(new ReportParameter("user", comboBox1.Text));
                this.reportViewer1.LocalReport.SetParameters(r);
                // TODO: This line of code loads data into the 'DataSet18.diagnostic_user_account' table. You can move, or remove it, as needed.
                this.diagnostic_user_accountTableAdapter.Fill(this.DataSet18.diagnostic_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_user_account' table. You can move, or remove it, as needed.
                this.hospital_user_accountTableAdapter.Fill(this.DataSet18.hospital_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_OPD' table. You can move, or remove it, as needed.
                this.user_OPDTableAdapter.Fill(this.DataSet18.user_cash_collection_OPD, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_pharmacy' table. You can move, or remove it, as needed.
                this.user_pharmacyTableAdapter.Fill(this.DataSet18.user_cash_collection_pharmacy, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_due_collection' table. You can move, or remove it, as needed.
                this.hospital_due_collectionTableAdapter.Fill(this.DataSet18.hospital_due_collection, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.opd_due_collection' table. You can move, or remove it, as needed.
                this.opd_due_collectionTableAdapter.Fill(this.DataSet18.opd_due_collection, comboBox1.Text, dd);
                this.reportViewer1.RefreshReport();
            }
            catch
            { 
            
            }
        }

        void user()
        {
            try
            {
                comboBox1.Items.Clear();
                string x = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select user_name from log", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    x = read[0].ToString();
                    comboBox1.Items.Add(x);
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
                header();
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                ReportParameterCollection r = new ReportParameterCollection();
                r.Add(new ReportParameter("user", comboBox1.Text));
                this.reportViewer1.LocalReport.SetParameters(r);
                // TODO: This line of code loads data into the 'DataSet18.diagnostic_user_account' table. You can move, or remove it, as needed.
                this.diagnostic_user_accountTableAdapter.Fill(this.DataSet18.diagnostic_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_user_account' table. You can move, or remove it, as needed.
                this.hospital_user_accountTableAdapter.Fill(this.DataSet18.hospital_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_OPD' table. You can move, or remove it, as needed.
                this.user_OPDTableAdapter.Fill(this.DataSet18.user_cash_collection_OPD, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_pharmacy' table. You can move, or remove it, as needed.
                this.user_pharmacyTableAdapter.Fill(this.DataSet18.user_cash_collection_pharmacy, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_due_collection' table. You can move, or remove it, as needed.
                this.hospital_due_collectionTableAdapter.Fill(this.DataSet18.hospital_due_collection, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.opd_due_collection' table. You can move, or remove it, as needed.
                this.opd_due_collectionTableAdapter.Fill(this.DataSet18.opd_due_collection, comboBox1.Text, dd);
                this.reportViewer1.RefreshReport();
            }
            catch
            { 
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                header();
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                ReportParameterCollection r = new ReportParameterCollection();
                r.Add(new ReportParameter("user", comboBox1.Text));
                this.reportViewer1.LocalReport.SetParameters(r);
                // TODO: This line of code loads data into the 'DataSet18.diagnostic_user_account' table. You can move, or remove it, as needed.
                this.diagnostic_user_accountTableAdapter.Fill(this.DataSet18.diagnostic_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_user_account' table. You can move, or remove it, as needed.
                this.hospital_user_accountTableAdapter.Fill(this.DataSet18.hospital_user_account, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_OPD' table. You can move, or remove it, as needed.
                this.user_OPDTableAdapter.Fill(this.DataSet18.user_cash_collection_OPD, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.user_cash_collection_pharmacy' table. You can move, or remove it, as needed.
                this.user_pharmacyTableAdapter.Fill(this.DataSet18.user_cash_collection_pharmacy, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.hospital_due_collection' table. You can move, or remove it, as needed.
                this.hospital_due_collectionTableAdapter.Fill(this.DataSet18.hospital_due_collection, comboBox1.Text, dd);
                // TODO: This line of code loads data into the 'DataSet18.opd_due_collection' table. You can move, or remove it, as needed.
                this.opd_due_collectionTableAdapter.Fill(this.DataSet18.opd_due_collection, comboBox1.Text, dd);
                this.reportViewer1.RefreshReport();
            }
            catch
            { 
            
            }
        }
    }
}
