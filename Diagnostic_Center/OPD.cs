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
namespace Diagnostic_Center
{
    public partial class OPD : Form
    {
        connection db = new connection();
        string update_time = "";
        string select_delete = "";
        string bill_id = "";
        string bill_title = "";
        string bill = "";
        string user_name = "";
        string password = "";
        string user_type = "";
        string consultant = "";
        string referance = "";
        public OPD(string user, string pass, string type)
        {
            InitializeComponent();
            refere_doctor2();
            refere_doctor();
            show_bill_list();
            user_name = user;
            password = pass;
            user_type = type;
            label99.Visible = false; label100.Visible = false; label101.Visible = false; label102.Visible = false;
        }

        void refere_doctor2()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select name from refer_doctor", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                comboBox3.Items.Clear();
                while (read.Read())
                {
                    String name = read[0].ToString();
                    comboBox3.Items.Add(name);
                }

                db.sql.Close();
            }

            catch
            {


            }

        }


        void refere_doctor()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select name from doctors", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                comboBox4.Items.Clear();
                while (read.Read())
                {
                    String name = read[0].ToString();
                    comboBox4.Items.Add(name);
                }

                db.sql.Close();
            }

            catch
            {


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                if (consultant == "")
                {
                    consultant = comboBox3.Text;
                }

                if (referance == "")
                {
                    referance = comboBox4.Text;
                }
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                if (patient_id.Text == "" || richTextBox1.Text == "" || richTextBox3.Text == "" || dateTimePicker1.Text == "")
                {
                    MessageBox.Show("Give Reg no,Patient Name,admit date,and select room");
                }
                else
                {
                    db.sql.Close();
                    db.sql.Open();
                    if (label101.Visible == true)
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into opd(reg_no,name,contact,gender,age,address,date,date2,time,gurdian,word,thana,district,consultant,referance,card_id,card_type)values('" + patient_id.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox3.Text + "','" + dateTimePicker1.Text + "','"+dd+"','" + label66.Text + "','" + richTextBox10.Text + "','" + richTextBox4.Text + "','" + richTextBox8.Text + "','" + richTextBox9.Text + "','" + consultant + "','" + referance + "','"+richTextBox27.Text+"','F')", db.sql);

                        x = cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd6 = new SqlCommand("insert into opd(reg_no,name,contact,gender,age,address,date,date2,time,gurdian,word,thana,district,consultant,referance,card_id,card_type)values('" + patient_id.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox3.Text + "','" + dateTimePicker1.Text + "','"+dd+"','" + label66.Text + "','" + richTextBox10.Text + "','" + richTextBox4.Text + "','" + richTextBox8.Text + "','" + richTextBox9.Text + "','" + consultant + "','" + referance + "','" + richTextBox27.Text + "','F')", db.sql);

                        x = cmd6.ExecuteNonQuery();
                    }
                
                 
                    
                   
                    if (x > 0)
                    {
                        MessageBox.Show("Registration Complete");

                       
                    }
                    else
                    {
                        MessageBox.Show("Registration Failed");

                    }
                    db.sql.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label66.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }


        void show_bill_list()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from bill_list", db.sql);
                dataGridView6.Rows.Clear();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView6.Rows.Add();
                    dataGridView6.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView6.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView6.Rows[n].Cells[2].Value = item[2].ToString();

                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void dataGridView6_Click(object sender, EventArgs e)
        {
            try
            {
                bill_id = dataGridView6.SelectedRows[0].Cells[0].Value.ToString();
                bill_title = dataGridView6.SelectedRows[0].Cells[1].Value.ToString();
                bill = dataGridView6.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("insert into opd_bill(reg_no,bill_title,bill,bill_id)values('" + patient_id.Text + "','" + bill_title + "','" + bill + "','" + bill_id + "')", db.sql);
                int a = cmd.ExecuteNonQuery();

                show_bill();
                db.sql.Close();
            }
            catch
            {

            }
        }


        void show_bill()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from opd_bill where reg_no='" + patient_id.Text + "'", db.sql);
                dataGridView5.Rows.Clear();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView5.Rows.Add();
                    dataGridView5.Rows[n].Cells[0].Value = item[1].ToString();
                    dataGridView5.Rows[n].Cells[1].Value = item[2].ToString();
                    dataGridView5.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridView5.Rows[n].Cells[3].Value = item[3].ToString();
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        void bill_calculate()
        {
            try
            {

            }
            catch
            { 
            
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                int sum = 0;
                for (int i = 0; i < dataGridView5.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridView5.Rows[i].Cells[2].Value);
                }
                label55.Text = sum.ToString();
                label49.Text = sum.ToString();
                richTextBox17.Text = sum.ToString();
            }
            catch
            {

            }
        }

        private void patient_id_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (patient_id.Text == "")
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox10.Clear();
                    richTextBox4.Clear();
                    richTextBox8.Clear();
                    richTextBox9.Clear();
                  
                    comboBox2.Text = "";
                  
                    richTextBox7.Clear();
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                }
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from opd where reg_no='" + patient_id.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    richTextBox1.Text = read[1].ToString();
                    richTextBox2.Text = read[2].ToString();
                    comboBox2.Text = read[3].ToString();
                    richTextBox7.Text = read[4].ToString();
                    richTextBox3.Text = read[5].ToString();

                    richTextBox10.Text = read[8].ToString();
                    richTextBox4.Text = read[9].ToString();
                    richTextBox8.Text = read[10].ToString();
                    richTextBox9.Text = read[11].ToString();
                    comboBox3.Text = read[12].ToString();
                    comboBox4.Text = read[13].ToString();

                    string date = read[6].ToString();
                 //   update_time = read[7].ToString();

                    DateTime convert = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dateTimePicker1.Text = convert.ToString();
               
                }

                db.sql.Close();
            }
            catch
            {

            }
            show_bill();
            show_paid();
            show_diagnostic();
        }


        void show_paid()
        {
            try
            {
                string total = "";
                string discount = "";
                string paid = "";
                string due = "";
                int c = 0;
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from user_cash_collection_OPD where reg_no='"+patient_id.Text+"'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;
                    total = read[4].ToString();
                    discount = read[5].ToString();
                    paid = read[6].ToString();
                    due = read[7].ToString();
                }
                if (c > 0)
                {
                    label55.Text = total;
                    richTextBox16.Text = discount;
                    richTextBox17.Text = paid;
                    label52.Text = due;
                }
                else
                {
                    label55.Text = "";
                    richTextBox16.Text = "0";
                    richTextBox17.Text = "";
                    label52.Text = "";
                    label49.Text = "";
                }
                db.sql.Close();
            }
            catch
            { 
            
            }
        }
        private void richTextBox16_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int y = 0;
                int x = 0;
                int due = 0;
                a = Convert.ToInt32(label55.Text);
                b = Convert.ToInt32(richTextBox16.Text);
                c = a - b;
                label49.Text = c.ToString();
                richTextBox17.Text = c.ToString();
               


            }
            catch
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int exe = 0;
                int id = 0; ;
                string val1 = "";
                string val2 = "";
                string val3 = "";
                string val4 = "";
                int n = 0;
                int c = dataGridView5.Rows.Count;
                db.sql.Close();
                db.sql.Open();
                for (n = 0; n <= c - 1; n++)
                {

                    val3 = dataGridView5.Rows[n].Cells[2].Value.ToString();
                    val4 = dataGridView5.Rows[n].Cells[3].Value.ToString();

                    SqlCommand cmd = new SqlCommand("update opd_bill set bill='" + val3 + "' where reg_no='" + patient_id.Text + "' and bill_id='" + val4 + "'", db.sql);
                    exe = cmd.ExecuteNonQuery();

                }
                db.sql.Close();
            }
            catch
            {

            }

            try
            {
                int aa = Convert.ToInt32(label52.Text);
                int b = Convert.ToInt32(richTextBox17.Text);

                if (richTextBox17.Text == "")
                {
                    MessageBox.Show("Give a valid amount");
                }
                else
                {

                   
                        string date = DateTime.Now.ToString("dd/MM/yyyy");
                        DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string dd = d.ToString("yyyy/MM/dd");
                        db.sql.Close();
                        db.sql.Open();
                  
                        SqlCommand cmd3 = new SqlCommand("delete from user_cash_collection_OPD where reg_no='" + patient_id.Text + "' and type='OPD Bill'", db.sql);
                   
                     
                        cmd3.ExecuteNonQuery();
                   
                        db.sql.Close();
                        db.sql.Open();

                        SqlCommand cmd = new SqlCommand("insert into user_cash_collection_OPD(user_name,password,reg_no,name,total,discount,paid,due,date,type,date2) values('" + user_name + "','" + password + "','" + patient_id.Text + "','" + richTextBox1.Text + "','" + label55.Text + "','" + richTextBox16.Text + "','" + richTextBox17.Text + "','" + label52.Text+ "','" + date + "','OPD Bill','" + dd + "')", db.sql);

                    
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Bill taken Sucessfully");
                            OPD_Print opd = new OPD_Print(patient_id.Text);
                            opd.Show();
                        }
                        else
                        {
                            MessageBox.Show("Not Paid");
                        }
                        db.sql.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox17_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                int y = 0;
                int due = 0;
                x = Convert.ToInt32(label49.Text);
                y = Convert.ToInt32(richTextBox17.Text);
                due = x - y;
                label52.Text = due.ToString();
            }
            catch
            { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");

                if (consultant == "")
                {
                    consultant = comboBox3.Text;
                }
                if (referance == "")
                {
                    referance = comboBox4.Text;
                }
                DialogResult r = MessageBox.Show("Do You want to Update this Records ..?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    db.sql.Close(); ;
                    db.sql.Open(); ;

                    SqlCommand cmd1 = new SqlCommand("update opd set name='" + richTextBox1.Text + "',contact='" + richTextBox2.Text + "',gender='" + comboBox2.Text + "',age='" + richTextBox7.Text + "',address='" + richTextBox3.Text + "',date='" + dateTimePicker1.Text + "',date2='" +dd+ "',time='" + update_time + "',gurdian='" + richTextBox10.Text + "',word='" + richTextBox4.Text + "',thana='" + richTextBox8.Text + "',district='" + richTextBox9.Text + "',referance='" + referance + "',consultant='" + consultant + "' where reg_no='" + patient_id.Text + "'", db.sql);
                
                    int b = cmd1.ExecuteNonQuery();
                    if ( b > 0)
                    {
                        MessageBox.Show("Update Sucessfull");
                      

                    }
                    else
                    {
                        MessageBox.Show("Update Failed");
                    }
                    db.sql.Close(); ;
                }

            }
            catch
            {
            }
        }

        private void dataGridView5_Click(object sender, EventArgs e)
        {
            try
            {
                select_delete = dataGridView5.SelectedRows[0].Cells[3].Value.ToString();
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();

                db.sql.Open();

                SqlCommand cmd = new SqlCommand("delete from opd_bill where reg_no='" + patient_id.Text + "' and bill_id='" + select_delete + "'", db.sql);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Delete Sucessfull");
                    show_bill();
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                DialogResult r = MessageBox.Show("Do You want to Cancel this Admission..??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {

                    if (user_type == "Admin")
                    {
                        SqlCommand cmd = new SqlCommand("delete from opd where reg_no='" + patient_id.Text + "'", db.sql);

                        SqlCommand cmd2 = new SqlCommand("delete from opd_bill where reg_no='" + patient_id.Text + "'", db.sql);
                        SqlCommand cmd3 = new SqlCommand("delete from user_cash_collection_OPD where reg_no='" + patient_id.Text + "'", db.sql);
                        SqlCommand cmd4 = new SqlCommand("delete from opd_due_collection where reg_no='" + patient_id.Text + "'", db.sql);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                        try
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        catch
                        {
                        }
                        try
                        {
                            cmd3.ExecuteNonQuery();
                        }
                        catch
                        {
                        }

                        try
                        {
                            cmd4.ExecuteNonQuery();
                        }
                        catch
                        {
                        }
                        MessageBox.Show("Admission Cancel Sucessfull");
                       
                        db.sql.Close();
                    }
                    else
                    {
                        MessageBox.Show("Only Admin Can Delete");
                    }
                }

            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                db.sql.Close(); ;
                db.sql.Open(); ;
                int c = 0;
                SqlCommand cmd = new SqlCommand("select max(reg_no) from opd", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    x = Convert.ToInt32(read[0].ToString());
                    c++;
                }

                if (c > 0)
                {
                    patient_id.Text = (x + 1).ToString();
                    
                }
                else
                {
                    patient_id.Text = "1";
                }
                db.sql.Close(); ;
            }
            catch
            {
                patient_id.Text = "1";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Opd_Due_Collection opd = new Opd_Due_Collection(user_name,password,user_type);
            opd.Show();
        }



        void show_diagnostic()
        {
            try
            {
                int xx = Convert.ToInt32(patient_id.Text);
                int total = 0;
                int paid = 0;
                int due = 0;
                int due_paid = 0;
                int less = 0;
                int net_bill = 0;
                int net_paid = 0;
                int net_due = 0;
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("select sum(discount_total),sum(paid),sum(less) from paid_and_due,diagnostic_person where paid_and_due.invoice_no=diagnostic_person.id and diagnostic_person.others_id='" + patient_id.Text + "' and other_type='outdoor' ", db.sql);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        total = Convert.ToInt32(read[0]);
                        paid = Convert.ToInt32(read[1]);
                        less = Convert.ToInt32(read[2]);
                    }

                    db.sql.Close();
                }
                catch
                {

                }
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd1 = new SqlCommand("select sum(due),sum(due_paid) from due_collection,diagnostic_person where due_collection.reg_no=diagnostic_person.id and due_collection.test_date!=due_collection.paid_date and diagnostic_person.others_id='" + patient_id.Text + "' and other_type='outdoor' ", db.sql);
                    SqlDataReader read1 = cmd1.ExecuteReader();
                    while (read1.Read())
                    {
                        due = Convert.ToInt32(read1[0]);
                        due_paid = Convert.ToInt32(read1[1]);

                    }
                    net_bill = total - less;
                    net_paid = paid + (due_paid);
                    net_due = net_bill - net_paid;
                    label23.Text = net_bill.ToString();
                    label22.Text = net_paid.ToString();
                    label21.Text = net_due.ToString();
                    db.sql.Close();
                }
                catch
                {
                    label23.Text = "0";
                    label22.Text = "0";
                    label21.Text = "0";
                }
            }
            catch
            {
                label23.Text = "0";
                label22.Text = "0";
                label21.Text = "0";
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                Diagnostic dd = new Diagnostic(user_name, password, user_type);
                dd.Show();
            }
            catch
            { 
            
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            consult();
        }

        void consult()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from refer_doctor where name='" + comboBox3.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                // comboBox1.Items.Clear();
                while (read.Read())
                {
                    String name = read[1].ToString();
                    String desig = read[2].ToString();

                    consultant = name + "\n" + desig;
                    // refere_doctor();
                }

                db.sql.Close();
            }

            catch
            {


            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from doctors where name='" + comboBox4.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                // comboBox1.Items.Clear();
                while (read.Read())
                {
                    String name = read[1].ToString();
                    String desig = read[2].ToString();

                    referance= name + "," + desig;
                    // refere_doctor();
                }

                db.sql.Close();
            }

            catch
            {


            }
        }

        private void dataGridView5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int exe = 0;
                int id = 0; ;
                string val1 = "";
                string val2 = "";
                string val3 = "";
                string val4 = "";
                int n = 0;
                int c = dataGridView5.Rows.Count;
                db.sql.Close();
                db.sql.Open();
                for (n = 0; n <= c - 1; n++)
                {

                    val3 = dataGridView5.Rows[n].Cells[2].Value.ToString();
                    val4 = dataGridView5.Rows[n].Cells[3].Value.ToString();
                    val1 = dataGridView5.Rows[n].Cells[0].Value.ToString();
                    SqlCommand cmd = new SqlCommand("update opd_bill set bill_title='"+val1+"', bill='" + val3 + "' where reg_no='" + patient_id.Text + "' and bill_id='" + val4 + "'", db.sql);
                    exe = cmd.ExecuteNonQuery();

                }
                db.sql.Close();
            }
            catch
            {

            }
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

                    richTextBox1.Text = read[3].ToString();
                    richTextBox2.Text = read[5].ToString();

                    richTextBox7.Text = read[4].ToString();
                    richTextBox3.Text = read[6].ToString();

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
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox10.Clear();
                    richTextBox4.Clear();
                    richTextBox8.Clear();
                    richTextBox9.Clear();

                    comboBox2.Text = "";
                    label99.Visible = false; label100.Visible = false; label101.Visible = false; label102.Visible = false;
                    richTextBox7.Clear();
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                }
                db.sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            previlege pv = new previlege();
            pv.Show();
        }
    }
}
