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
using System.IO;

namespace Diagnostic_Center
{
    public partial class Doctor_Appointment : Form
    {
        string user_name = "";
        string password = "";
        string user_type = "";
        string reg = "";
        string pathString = "";
        connection db = new connection();
        public Doctor_Appointment(string user, string pass, string type)
        {
            InitializeComponent();
            user_name = user;

            password = pass;
            user_type = type;
            show_appointment();
            show_doctor();
            label99.Visible = false; label100.Visible = false; label101.Visible = false; label102.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                int count = 0;
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = Convert.ToDateTime(d).ToString("yyyy-MM-dd");
                //d.ToString("yyyy/MM/dd");
                db.sql.Close();
                db.sql.Open();
                SqlCommand comand = new SqlCommand("select * from appointment where reg_no='" + richTextBox1.Text + "' and date='" + dd + "'", db.sql);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                if (count > 0)
                {
                    MessageBox.Show("Appointment already Taken");
                }
                else
                {
                    try
                    {

                        if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "" || richTextBox4.Text == "" || comboBoxEx3.Text == "" || dateTimePicker1.Text == "" || comboBoxEx1.Text == "" || richTextBox6.Text == "")
                        {
                            MessageBox.Show("Provide all the information");
                        }
                        else
                        {
                            DialogResult r = MessageBox.Show("Do you want to save it ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (r == DialogResult.Yes)
                            {
                                int fees = Convert.ToInt32(richTextBox6.Text);
                                db.sql.Close();
                                db.sql.Open();
                                int c = 0;
                                SqlCommand check = new SqlCommand("select reg_no from appointment where reg_no='" + richTextBox1.Text + "'", db.sql);
                                SqlDataReader read = check.ExecuteReader();
                                while (read.Read())
                                {
                                    c++;
                                }

                                //**************************id generate**********************
                                int idd = 0;
                                int new_id = 0;
                                db.sql.Close();
                                db.sql.Open();
                                try
                                {
                                    SqlCommand cmd1 = new SqlCommand("select max(id) from appointment where date='" + dd + "' and doctor='" + comboBoxEx1.Text + "'", db.sql);
                                    SqlDataReader rd = cmd1.ExecuteReader();
                                    while (rd.Read())
                                    {
                                        idd = Convert.ToInt32(rd[0].ToString());
                                    }
                                }
                                catch
                                {

                                }
                                new_id = idd + 1;
                                db.sql.Close();
                                db.sql.Open();
                                SqlCommand cmd = new SqlCommand("insert into appointment(reg_no,name,address,age,sex,referance,date,doctor,fees,weight,id)values(N'" + richTextBox1.Text + "',N'" + richTextBox2.Text + "',N'" + richTextBox3.Text + "',N'" + richTextBox4.Text + "',N'" + comboBoxEx3.Text + "',N'" + richTextBox5.Text + "',N'" + dd + "',N'" + comboBoxEx1.Text + "',N'" + fees + "',N'" + richTextBox47.Text + "','" + new_id + "')", db.sql);
                                SqlCommand cmd2 = new SqlCommand("insert into symptoms(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd3 = new SqlCommand("insert into patient_test(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd4 = new SqlCommand("insert into advice(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd5 = new SqlCommand("insert into diagnosis(reg_no)values('" + richTextBox1.Text + "')", db.sql);




                                if (c > 0)
                                {

                                }
                                else
                                {
                                    x = cmd.ExecuteNonQuery();
                                    cmd2.ExecuteNonQuery();
                                    cmd3.ExecuteNonQuery();
                                    cmd4.ExecuteNonQuery();
                                    cmd5.ExecuteNonQuery();
                                }


                                if (x > 0)
                                {
                                    MessageBox.Show("Data Inserted Successfully");
                                }
                                else
                                {
                                    MessageBox.Show("Data not Inserted");
                                }
                                db.sql.Close();
                                show_appointment();
                            }
                            else
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch
            {

            }
        }

        void show_appointment()
        {
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                string dd = DateTime.Now.ToString("yyyy-MM-dd");
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from appointment where date='" + date + "'", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[7].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = date;
                    dataGridViewX1.Rows[n].Cells[4].Value = item[10].ToString();

                }
                db.sql.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                int count = 0;
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString();
                db.sql.Close();
                db.sql.Open();
                SqlCommand comand = new SqlCommand("select * from appointment where reg_no='" + richTextBox1.Text + "' and date='" + date + "'", db.sql);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                if (count > 0)
                {
                    MessageBox.Show("Appointment already Taken");
                }
                else
                {
                    try
                    {
                        int x = 0;
                        if (string.IsNullOrEmpty(comboBoxEx1.Text))
                        {
                            MessageBox.Show("Please Select Doctor!!!");
                        }
                        else if (string.IsNullOrEmpty(richTextBox6.Text))
                        {
                            MessageBox.Show("Valid Doctor Fees is Required!!!");
                        }
                        else if (string.IsNullOrEmpty(richTextBox1.Text))
                        {
                            MessageBox.Show("Valid Appointment No is Required.");
                        }
                        else if (string.IsNullOrEmpty(richTextBox2.Text))
                        {
                            MessageBox.Show("Patient Name is Required!!!");
                        }
                        else if (string.IsNullOrEmpty(richTextBox3.Text))
                        {
                            MessageBox.Show("Address is Required!!!");
                        }
                        else if (string.IsNullOrEmpty(richTextBox4.Text))
                        {
                            MessageBox.Show("Valid Age is Required!!!");
                        }
                        else if (string.IsNullOrEmpty(comboBoxEx3.Text))
                        {
                            MessageBox.Show("Gender is Required!!!");
                        }
                        else if (string.IsNullOrEmpty(dateTimePicker1.Text))
                        {
                            MessageBox.Show("Appointment Date is Required!!!");
                        }
                        else
                        {
                            DialogResult r = MessageBox.Show("Do you want to save it ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (r == DialogResult.Yes)
                            {
                                int fees = Convert.ToInt32(richTextBox6.Text);
                                db.sql.Close();
                                db.sql.Open();
                                int c = 0;
                                SqlCommand check = new SqlCommand("select reg_no from symptoms where reg_no='" + richTextBox1.Text + "'", db.sql);
                                SqlDataReader read = check.ExecuteReader();
                                while (read.Read())
                                {
                                    c++;
                                }
                                File.Copy(txtImageTitle.Text, Path.Combine(@"E:\Upload", Path.GetFileName(txtImageTitle.Text)),true);
                                pathString = @"E:\Upload\" + Path.GetFileName(txtImageTitle.Text);
                                db.sql.Close();
                                db.sql.Open();
                                if (label101.Visible == true)
                                {
                                    SqlCommand cmd1 = new SqlCommand("insert into appointment(reg_no,name,address,age,sex,referance,date,doctor,fees,weight,card_id,card_type,hospital_id,image)values(N'" + richTextBox1.Text + "',N'" + richTextBox2.Text + "',N'" + richTextBox3.Text + "',N'" + richTextBox4.Text + "',N'" + comboBoxEx3.Text + "',N'" + richTextBox5.Text + "',N'" + dd + "',N'" + comboBoxEx1.Text + "',N'" + fees + "',N'" + richTextBox47.Text + "','" + richTextBox27.Text + "','F',N'" + richTextBox8.Text + "',N'" + pathString + "')", db.sql);
                                    x = cmd1.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd = new SqlCommand("insert into appointment(reg_no,name,address,age,sex,referance,date,doctor,fees,weight,card_id,card_type,hospital_id,image)values(N'" + richTextBox1.Text + "',N'" + richTextBox2.Text + "',N'" + richTextBox3.Text + "',N'" + richTextBox4.Text + "',N'" + comboBoxEx3.Text + "',N'" + richTextBox5.Text + "',N'" + dd + "',N'" + comboBoxEx1.Text + "',N'" + fees + "',N'" + richTextBox47.Text + "','" + richTextBox27.Text + "','P',N'" + richTextBox8.Text + "',N'"+pathString + "')", db.sql);
                                    x = cmd.ExecuteNonQuery();
                                }
                                SqlCommand cmd2 = new SqlCommand("insert into symptoms(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd3 = new SqlCommand("insert into patient_test(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd4 = new SqlCommand("insert into advice(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd5 = new SqlCommand("insert into diagnosis(reg_no)values('" + richTextBox1.Text + "')", db.sql);
                                SqlCommand cmd6 = new SqlCommand("insert into user_cash_collection_doctor(user_name,password,reg_no,name,paid,date,date2)values('" + user_name + "','" + password + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox6.Text + "','" + date + "','" + dd + "')", db.sql);

                                if (c > 0)
                                {

                                }
                                else
                                {

                                    cmd2.ExecuteNonQuery();
                                    cmd3.ExecuteNonQuery();
                                    cmd4.ExecuteNonQuery();
                                    cmd5.ExecuteNonQuery();
                                    cmd6.ExecuteNonQuery();

                                }


                                if (x > 0)
                                {
                                    MessageBox.Show("Data Inserted Successfully");
                                    token token = new token(richTextBox1.Text);
                                    token.Show();
                                    CLearAllField();
                                }
                                else
                                {
                                    MessageBox.Show("Data not Inserted");
                                }
                                db.sql.Close();
                                show_appointment();
                            }
                            else
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch
            {

            }
        }

        private void CLearAllField()
        {
            richTextBox1.Text = "";
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            comboBoxEx3.Text = "";
            richTextBox5.Clear();
            comboBoxEx1.Text = "";
            richTextBox6.Clear();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            token token = new token(richTextBox1.Text);
            token.Show();
        }
        void show_doctor()
        {
            try
            {
                int c = 0;
                string name = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from refer_doctor", db.sql);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {

                    c++;
                    name = r[1].ToString();
                    if (c > 0)
                    {
                        comboBoxEx1.Items.Add(name);
                        comboBoxEx2.Items.Add(name);

                    }
                }

                db.sql.Close();
            }
            catch
            { }
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker2.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");

                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from appointment where date='" + dd + "' and doctor='" + comboBoxEx2.Text + "' ", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[7].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = date;
                    dataGridViewX1.Rows[n].Cells[4].Value = item[10].ToString();

                }
                db.sql.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker2.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");

                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from appointment where  date='" + dd + "' ", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[7].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = date;
                    dataGridViewX1.Rows[n].Cells[4].Value = item[10].ToString();

                }
                db.sql.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker2.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");

                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from appointment where  name like'%" + richTextBox7.Text + "%'and date='" + dd + "' ", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[7].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = date;
                    dataGridViewX1.Rows[n].Cells[4].Value = item[10].ToString();

                }
                db.sql.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                db.sql.Close();
                db.sql.Open();
                int c = 0;
                SqlCommand cmd = new SqlCommand("select max(reg_no) from appointment", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    x = Convert.ToInt32(read[0].ToString());
                    c++;
                }

                if (c > 0)
                {
                    richTextBox1.Text = (x + 1).ToString();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    comboBoxEx3.Text = "";
                    richTextBox5.Clear();
                    comboBoxEx1.Text = "";
                    richTextBox6.Clear();
                }
                else
                {
                    richTextBox1.Text = "1";
                }
                db.sql.Close();
            }
            catch
            {
                richTextBox1.Text = "1";
            }
        }

        void id_generate()
        {
            try
            {
                int x = 0;
                db.sql.Close();
                db.sql.Open();
                int c = 0;
                SqlCommand cmd = new SqlCommand("select max(reg_no) from appointment", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    x = Convert.ToInt32(read[0].ToString());
                    c++;
                }

                if (c > 0)
                {
                    richTextBox1.Text = (x + 1).ToString();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    comboBoxEx3.Text = "";
                    richTextBox5.Clear();
                    comboBoxEx1.Text = "";
                    richTextBox6.Clear();
                }
                else
                {
                    richTextBox1.Text = "1";
                }
                db.sql.Close();
            }
            catch
            {
                richTextBox1.Text = "1";
            }

        }
        private void dataGridViewX1_Click(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                reg = dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString();
                string name = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                string doctor = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                string date = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                string fees = "";
                string age = "";
                string sex = "";
                string weight = "";
                string referance = "";
                string address = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from appointment where reg_no='" + reg + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;

                    age = read[3].ToString();
                    sex = read[4].ToString();
                    weight = read[9].ToString();
                    referance = read[5].ToString();
                    address = read[2].ToString();
                    fees = read[8].ToString();

                }
                if (c > 0)
                {

                    comboBoxEx1.Text = doctor;

                    richTextBox1.Text = reg;
                    richTextBox2.Text = name;
                    richTextBox6.Text = fees;
                    richTextBox3.Text = address;
                    richTextBox4.Text = age;
                    comboBoxEx3.Text = sex;
                    richTextBox47.Text = weight;
                    richTextBox5.Text = referance;
                    dateTimePicker1.Text = date;

                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker1.Text;
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("update appointment set name=N'" + richTextBox2.Text + "',address=N'" + richTextBox3.Text + "',age=N'" + richTextBox4.Text + "',sex=N'" + comboBoxEx3.Text + "',doctor=N'" + comboBoxEx1.Text + "',fees=N'" + richTextBox6.Text + "',date='" + dd + "',weight=N'" + richTextBox47.Text + "',referance=N'" + richTextBox5.Text + "',card_id=N'" + richTextBox27.Text + "',hospital_id=N'" + richTextBox8.Text + "' where reg_no=N'" + richTextBox1.Text + "'", db.sql);
                SqlCommand cmd1 = new SqlCommand("update user_cash_collection_doctor set paid='" + richTextBox6.Text + "' where reg_no=N'" + richTextBox1.Text + "'", db.sql);
                int a = cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Update Sucessfull");
                    show_appointment();
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int a = 0;
                db.sql.Close();
                db.sql.Open();
                DialogResult r = MessageBox.Show("Do You want to delete this..??", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (r == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete from appointment where reg_no='" + richTextBox1.Text + "'", db.sql);
                    SqlCommand cmd1 = new SqlCommand("delete from symptoms where reg_no='" + richTextBox1.Text + "'", db.sql);
                    SqlCommand cmd2 = new SqlCommand("delete from patient_test where reg_no='" + richTextBox1.Text + "'", db.sql);
                    SqlCommand cmd3 = new SqlCommand("delete from advice where reg_no='" + richTextBox1.Text + "'", db.sql);
                    SqlCommand cmd4 = new SqlCommand("delete from diagnosis where reg_no='" + richTextBox1.Text + "'", db.sql);
                    SqlCommand cmd5 = new SqlCommand("delete from user_cash_collection_doctor where reg_no='" + richTextBox1.Text + "'", db.sql);

                    try
                    {
                        a = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                    try
                    {
                        cmd1.ExecuteNonQuery();
                    }
                    catch
                    { }
                    try
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    catch
                    { }
                    try
                    {
                        cmd3.ExecuteNonQuery();
                    }
                    catch
                    { }
                    try
                    {
                        cmd4.ExecuteNonQuery();
                    }
                    catch
                    { }
                    if (a > 0)
                    {
                        MessageBox.Show("Delete Sucessfull");
                        show_appointment();
                    }
                    db.sql.Close();
                }
            }
            catch
            {

            }
        }

        private void Doctor_Appointment_Load(object sender, EventArgs e)
        {
            id_generate();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                string fees = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from refer_doctor where name='" + comboBoxEx1.Text + "'", db.sql);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {

                    c++;
                    fees = r[5].ToString();
                }
                if (c > 0)
                {
                    richTextBox6.Text = fees.ToString();
                }
                else
                {
                    richTextBox6.Clear();
                }
                db.sql.Close();
            }
            catch
            { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                string name = "";
                string address = "";
                string sex = "";
                string age = "";
                string weight = "";
                string referance = "";
                string doctor = "";
                string fees = "";
                string card = "";
                string hospital = "";
                db.sql.Close();
                db.sql.Open();

                SqlCommand cmd = new SqlCommand("select * from appointment where reg_no='" + richTextBox1.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;
                    name = read[1].ToString();
                    address = read[2].ToString();
                    sex = read[4].ToString();
                    age = read[3].ToString();
                    weight = read[9].ToString();
                    referance = read[5].ToString();
                    doctor = read[7].ToString();
                    fees = read[8].ToString();
                    card = read[11].ToString();
                    hospital = read[14].ToString();
                }
                if (c > 0)
                {
                    richTextBox2.Text = name;
                    richTextBox3.Text = address;
                    richTextBox4.Text = age;
                    comboBoxEx3.Text = sex;
                    richTextBox47.Text = weight;
                    richTextBox5.Text = referance;
                    comboBoxEx1.Text = doctor;
                    richTextBox6.Text = fees;
                    richTextBox27.Text = card;
                    richTextBox8.Text = hospital;
                }
                else
                {
                    richTextBox2.Text = "";
                    richTextBox3.Text = "";
                    richTextBox4.Text = "";
                    comboBoxEx3.Text = "";
                    richTextBox47.Text = "";
                    richTextBox5.Text = "";
                    comboBoxEx1.Text = "";
                    richTextBox6.Text = "";
                    richTextBox27.Text = "";
                    richTextBox8.Text = "";
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            previlege pv = new previlege();
            pv.Show();
        }

        private void richTextBox27_TextChanged(object sender, EventArgs e)
        {
            show_card_details();
        }
        void show_card_details()
        {
            try
            {

                dateTimePicker1.Text = DateTime.Now.ToShortDateString();

                db.sql.Close();
                db.sql.Open();
                int loop = 0;
                SqlCommand cmd = new SqlCommand("select * from card_details where reg_no='" + richTextBox27.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    loop++;

                    richTextBox2.Text = read[3].ToString();
                    richTextBox3.Text = read[6].ToString();
                    richTextBox4.Text = read[4].ToString();

                    DateTime convert = DateTime.ParseExact(read[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime convert2 = DateTime.ParseExact(read[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);



                    string todays = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime today = DateTime.ParseExact(todays, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var totaldays = (convert2 - today).TotalDays;
                    if (totaldays < 0)
                    {
                        label100.Visible = true;
                        label102.Visible = false;
                    }
                    else
                    {
                        label102.Visible = true;
                        label100.Visible = false;
                    }

                    if (read[11].ToString() == "F")
                    {
                        label101.Visible = true;
                        label99.Visible = false;
                    }
                    else
                    {
                        label101.Visible = false;
                        label99.Visible = true;
                    }


                }
                if (loop == 0)
                {


                    label99.Visible = false; label100.Visible = false; label101.Visible = false; label102.Visible = false;
                    richTextBox7.Clear();

                }
                db.sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                int loop = 0;
                SqlCommand cmd = new SqlCommand("select * from patient_histroy where reg_no='" + richTextBox8.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    loop++;
                    richTextBox2.Text = read[1].ToString();
                    richTextBox3.Text = read[5].ToString();
                    richTextBox4.Text = read[4].ToString();
                    comboBoxEx3.Text = read[3].ToString();



                }
                if (loop == 0)
                {
                    //richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();


                }
                db.sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            All_Appointment_print app = new All_Appointment_print();
            app.Show();
        }

        private void btnImageUpload_Click(object sender, EventArgs e)
        {            
            try
            {
                OpenFileDialog open = new OpenFileDialog();                
                open.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtImageTitle.Text = open.FileName;
                    pBoxPatient.Image = new Bitmap(open.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
