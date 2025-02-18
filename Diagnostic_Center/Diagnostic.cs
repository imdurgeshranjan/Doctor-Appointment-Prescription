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
    public partial class Diagnostic : Form
    {
      
        connection db = new connection();
        connection db2 = new connection();
        int id;
        double amount;
        double paid;
        double total = 0;
        string Date;
        double paid2 = 0;
        double due2 = 0;
        string x = "";
        string y = "";
        string z = "";
        string zz = "";
        double price;
        int delete_id;
        string delete_name = "";
        string user_name = "";
        string password = "";
        string user_type = "";
        string patient_name = "";
        string reagent_reduce_id = "";
        string other_id = "";
        string other_type = "";
        string consultant = "";
        public Diagnostic( string user,string pass,string type)
        {
            InitializeComponent();
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
           
            list();
            id_generate();
            show();
            show2();
            user_name = user;
            label18.Text = type;
            password = pass;
            user_type = type;
            show_category();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
           // auto_complete();
            //card
            label49.Visible = false;
            label50.Visible = false;
            label51.Visible = false;
            label52.Visible = false;
        }

        void list()
        {
            try
            {
               
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from diagnostic_list", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = item[3].ToString();
                }
                db.sql.Close();

            }
            catch
            {


            }

        }

        void id_generate()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select max(id) from diagnostic_person", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    id = Convert.ToInt32(read[0]);
                    richTextBox11.Text = (id + 1).ToString();
                }
                db.sql.Close();
            }
            catch
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

          try
            {
                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("Enter Name..");
                }


                else if (label11.Text == "")
                {
                    MessageBox.Show("Select a Item");
                }
                else if (comboBox2.Text == "")
                {
                    MessageBox.Show("Select Gender");
                }


                else if (comboBox1.Text == "")
                {
                    MessageBox.Show("Select  Referad Doctor");
                }
                else
                {
                    int c = 0;
                    int cc = 0;
                    int match = Convert.ToInt32(richTextBox11.Text);
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("select * from diagnostic_person where id='" + match + "' and name='" + richTextBox1.Text + "' and contact='" + richTextBox2.Text + "'", db.sql);
                    SqlDataReader re = cmd.ExecuteReader();
                    while (re.Read())
                    {
                        c++;
                    }
                    if (c > 0)
                    {
                    
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd4 = new SqlCommand("select * from diagnostic_bill where invoice_id='" + match + "' and test_name='" + y + "'", db.sql);
                        SqlDataReader rd = cmd4.ExecuteReader();
                        while (rd.Read())
                        {
                            cc++;
                        }
                        if (cc > 0)
                        {
                            MessageBox.Show("item already added");
                        }
                        else
                        {
                            string xerox = "";
                            if (zz == "Pathological Test")
                            {
                                xerox = "";
                            }
                            else
                            {
                                xerox = "xerox";
                            }
                           
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd3 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_id,test_name,price,date,date2,xerox,other_id,other_type)values('" + match + "','" + label11.Text + "','" + y + "','" + price + "','" + date + "','" + dd + "','" + xerox + "','" + other_id + "','" + other_type + "')", db.sql);

                            cmd3.ExecuteNonQuery();
                            show_bill();
                            discount();
                            lab_inventory_reduce();
                        }
                    }
                    else
                    {
                        string xerox = "";
                        if (zz == "Pathological Test")
                        {
                            xerox = "";
                        }
                        else
                        {
                            xerox = "xerox";
                        }
                        db.sql.Close();
                        db.sql.Open();
                        if (label52.Visible == true) {
                            SqlCommand cmd1 = new SqlCommand("insert into diagnostic_person(id,name,contact,address,date,gender,age,referdoctor,date2,others_id,referedto,other_type,delevery_date,delevery_time,card_id,card_type)values('" + match + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + date + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox10.Text + "','" + dd + "','" + other_id + "','" + consultant + "','" + other_type + "','" + dateTimePicker3.Text + "','" + comboBox5.Text + "','" + richTextBox15.Text + "','" + label52.Text + "')", db.sql);
                            cmd1.ExecuteNonQuery();
                        }
                        SqlCommand cmd4 = new SqlCommand("insert into diagnostic_person(id,name,contact,address,date,gender,age,referdoctor,date2,others_id,referedto,other_type,delevery_date,delevery_time,card_id,card_type)values('" + match + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + date + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox10.Text + "','" + dd + "','" + other_id + "','" + consultant + "','" + other_type + "','" + dateTimePicker3.Text + "','" + comboBox5.Text + "','" + richTextBox15.Text + "','" + label51.Text + "')", db.sql);

                        SqlCommand cmd2 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_id,test_name,price,date,date2,xerox,other_id,other_type)values('" + match + "','" + label11.Text + "','" + y + "','" + price + "','" + date + "','" + dd + "','" + xerox + "','"+other_id+"','"+other_type+"')", db.sql);
                        try
                        {
                            cmd4.ExecuteNonQuery();
                        }
                        catch
                        { }
                        try
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        catch
                        {
                        }
                        show_bill();
                        count_sum();
                        discount();
                        lab_inventory_reduce();
                    }

                    db.sql.Close();
                }
            }

            catch
            {

            }
        }
        //*********************lab inventory reduce********************************
        void lab_inventory_reduce()
        {
            string x = dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                int c = 0;
                double q = 0;
                string inventory_id = "";
                double reagent_amount = 0;
                double calculate_result = 0;
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select reagent_id,decrease_amount, quantity from reagent_decrease,lab_inventory where id= reagent_id and test_id='" +label11.Text+ "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;
                inventory_id=read[0].ToString();
                q = Convert.ToDouble(read[1].ToString());

                reagent_amount = Convert.ToDouble(read[2].ToString());
                if (c > 0)
                {

                    calculate_result = reagent_amount - q;
                    db2.sql.Close();
                    db2.sql.Open();
                    SqlCommand cmd2 = new SqlCommand("update lab_inventory set quantity='" + calculate_result + "' where id='" + inventory_id + "'", db2.sql);
                    int a = cmd2.ExecuteNonQuery();
                  //  MessageBox.Show(inventory_id + "," + reagent_amount.ToString() + "," + calculate_result.ToString());
                    db2.sql.Close();
                }
                else
                {

                }
             //   MessageBox.Show(c.ToString());
               
                }
                db.sql.Close();
            }
            catch
            { 
            
            }
        }
        void lab_inventory_return()
        {
            try
            {
                string x = dataGridViewX2.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch
            { 
            
            }
           // MessageBox.Show(x);
            try
            {
                int c = 0;
                double q = 0;
                string inventory_id = "";
                double reagent_amount = 0;
                double calculate_result = 0;
                db.sql.Close();
                db.sql.Open();
               
                SqlCommand cmd = new SqlCommand("select reagent_id,decrease_amount, quantity from reagent_decrease,lab_inventory where id= reagent_id and test_id='" + reagent_reduce_id+ "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;
                    inventory_id = read[0].ToString();
                    q = Convert.ToDouble(read[1].ToString());

                    reagent_amount = Convert.ToDouble(read[2].ToString());
                   
                    //   MessageBox.Show(c.ToString());

                    if (c > 0)
                    {

                        calculate_result = reagent_amount + q;
                        db2.sql.Close();
                        db2.sql.Open();
                        SqlCommand cmd2 = new SqlCommand("update lab_inventory set quantity='" + calculate_result + "' where id='" + inventory_id + "'", db2.sql);
                        int a = cmd2.ExecuteNonQuery();
                      //  MessageBox.Show(inventory_id + "," + reagent_amount.ToString() + "," + calculate_result.ToString());
                        db2.sql.Close();
                    }
                    else
                    {

                    }
                }
               // MessageBox.Show(inventory_id + "," + reagent_amount.ToString() + "," + calculate_result.ToString());
                db.sql.Close();
            }
            catch
            {

            }
        
        }

        void minimize()
        {
            
        }
        void show_bill()
        {
            try
            {

                int d = Convert.ToInt32(richTextBox11.Text);
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from diagnostic_bill where invoice_id='" + d + "'", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX2.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX2.Rows.Add();
                    dataGridViewX2.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX2.Rows[n].Cells[1].Value = item[6].ToString();
                    dataGridViewX2.Rows[n].Cells[2].Value = item[1].ToString();
                    dataGridViewX2.Rows[n].Cells[3].Value = item[2].ToString();
                    dataGridViewX2.Rows[n].Cells[4].Value = item[9].ToString();


                }

                db.sql.Close();
                count_sum();
            }

            catch
            {

            }


        }

        void count_sum()
        {
            try
            {
                int match = Convert.ToInt32(richTextBox11.Text);
                db.sql.Close();
                db.sql.Open();
                double x = 0;
                SqlCommand cmd = new SqlCommand("select sum(price) from diagnostic_bill where invoice_id='" + match + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    x = Convert.ToDouble(read[0].ToString());

                }
                db.sql.Close();
                label13.Text = x.ToString();

                if (label13.Text != "")
                {
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
            }


            catch { }

        }

    
        private void dataGridViewX1_Click(object sender, EventArgs e)
        {
            try
            {
                x = dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString();
                label11.Text = x;
                y = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                z = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                zz = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                price = Convert.ToDouble(z);
            }

            catch
            {

            }
        }

        private void dataGridViewX2_Click(object sender, EventArgs e)
        {
            try
            {
                delete_id = Convert.ToInt32(dataGridViewX2.SelectedRows[0].Cells[4].Value.ToString());
                
                delete_name = dataGridViewX2.SelectedRows[0].Cells[2].Value.ToString();
                reagent_reduce_id = dataGridViewX2.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch
            {


            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            lab_inventory_return();
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("delete from diagnostic_bill where id='" + delete_id + "'", db.sql);
                cmd.ExecuteNonQuery();
                db.sql.Close();
                show_bill();

                cal();
                discount();
            }

            catch
            {

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text == "")
            {
                MessageBox.Show("You Have to pay Some Amount");
            }

            else
            {
                  try
                {
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    DialogResult r = MessageBox.Show("Do You Want to save  this data....??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        int c = 0;
                        string paid_date = "0";
                        int match = Convert.ToInt32(richTextBox11.Text);
                        double total = Convert.ToDouble(label13.Text);
                        double paid = Convert.ToDouble(richTextBox5.Text);
                        double due = Convert.ToDouble(label15.Text);
                        double discount = Convert.ToDouble(richTextBox9.Text);
                        double discount_total = Convert.ToDouble(label22.Text);
                        if (due == 0)
                        {
                            paid_date = date;
                        }
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd1 = new SqlCommand("select * from paid_and_due where invoice_no='" + match + "'", db.sql);
                        SqlDataReader read = cmd1.ExecuteReader();
                        while (read.Read())
                        {
                            c++;

                        }
                        if (c > 0)
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd2 = new SqlCommand("update paid_and_due set total='" + total + "', paid='" + paid + "',due='" + due + "',discount='" + discount + "',discount_total='" + discount_total + "',paid_date='" + paid_date + "'  where invoice_no='" + match + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due='" + due + "' where reg_no='" + match + "'", db.sql);
                            SqlCommand cmde = new SqlCommand("update referance set name='" + comboBox1.Text + "',patient_name='" + richTextBox1.Text + "',date='" + date + "',bill='" + label13.Text + "',date2='" + dd + "',after_discount='" + label22.Text + "' where patient_id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmdd = new SqlCommand("update diagnostic_person set referdoctor='" + comboBox1.Text + "' where id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmddd = new SqlCommand("update user_cash_collection set paid='" + richTextBox5.Text + "' where reg_no='" + richTextBox11.Text + "'", db.sql);
                            cmd2.ExecuteNonQuery();

                            int x = cmde.ExecuteNonQuery();

                            cmd4.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            button5.Enabled = true;
                            diagnostic_bill dbb = new diagnostic_bill(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("insert into paid_and_due(invoice_no,total,paid,due,discount,discount_total,paid_date,date2)values('" + match + "','" + total + "','" + paid + "','" + due + "','" + discount + "','" + discount_total + "','" + paid_date + "','" + dd + "')", db.sql);
                            SqlCommand cmdd = new SqlCommand("insert into due_collection(reg_no,name,due,test_date)values('" + match + "','" + richTextBox1.Text + "','" + due + "','" + date + "')", db.sql);
                            SqlCommand cmddd = new SqlCommand("insert into referance(name,patient_id,patient_name,date,bill,date2,after_discount) values('" + comboBox1.Text + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + date + "','" + label13.Text + "','" + dd + "','" + label22.Text + "')", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" +user_name+ "','" + password + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + richTextBox5.Text + "','" + date + "','"+dd+"')", db.sql);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            button5.Enabled = true;
                            button4.Enabled = false;
                            diagnostic_bill dbb = new diagnostic_bill(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                             show2();
                        }
                    }
                }

                 catch
                   {
                       MessageBox.Show("Give a valid Paing Amount","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                   }
            }

        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

            cal();
        }
        void cal()
        {

            try
            {
                if (richTextBox5.Text == "")
                {
                    label15.Text = "";
                }
                else
                {
                    double total = Convert.ToDouble(label22.Text);
                    double paid = Convert.ToDouble(richTextBox5.Text);


                    double due = total - paid;
                    label15.Text = due.ToString();
                }
            }
            catch
            {

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult r = MessageBox.Show("Do You Want to Abort this?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    int match = Convert.ToInt32(richTextBox11.Text);
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("delete from diagnostic_bill where invoice_id='" + match + "'", db.sql);
                    SqlCommand cmd1 = new SqlCommand("delete from diagnostic_person where id='" + match + "'", db.sql);
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    show_daily_status();
                    dataGridViewX2.Rows.Clear();
                    label13.Text = "";
                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    richTextBox3.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";

                    comboBox1.Text = "";
                    richTextBox10.Text = "";
                    label22.Text = "";
                    richTextBox9.Text = "0";
                    button3.Enabled = false;
                    button4.Enabled = false;
                    db.sql.Close();
                }
                else
                {

                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                id_generate();
                dataGridViewX2.Rows.Clear();
                label13.Text = "";
                richTextBox1.Text = "";
                richTextBox2.Text = "";
                richTextBox3.Text = "";
                comboBox2.Text = "";
                richTextBox7.Text = "";

                comboBox1.Text = "";
                richTextBox10.Text = "";
                label22.Text = "";
                richTextBox9.Text = "0";
                button3.Enabled = false;
                button4.Enabled = false;
               
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
                comboBox1.Items.Clear();
                while (read.Read())
                {
                    String name = read[0].ToString();
                    comboBox1.Items.Add(name);
                }

                db.sql.Close();
            }

            catch
            {


            }

        }


        void refere_doctor2()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select name from refer_doctor", db.sql);
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


        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                String name = "";
                String desig = ""; 
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from doctors where name='" + comboBox1.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                // comboBox1.Items.Clear();
                while (read.Read())
                {
                    c++;
                     name = read[1].ToString();
                     desig = read[2].ToString();
                    
                   
                    // refere_doctor();
                }
                if (c > 0)
                {
                    richTextBox10.Text = name + "," + desig;
                }
                else
                {
                    richTextBox10.Text = comboBox1.Text;
                }
                db.sql.Close();
            }

            catch
            {


            }
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    discount();
                }
                else
                {
                    label53.Text = "";
                    discount2();
                    
                }
            }
            catch
            {
            }
        }

        void discount()
        {

              try
              {
                  if (richTextBox9.Text == "")
                  {
                      label53.Text = "0";
                      label15.Text = "0";
                      label22.Text = label13.Text;
                      richTextBox5.Text = label13.Text;
                  }
                  else
                  {
                      double total = Convert.ToDouble(label13.Text);
                      double discount = Convert.ToDouble(richTextBox9.Text);
                      if (discount>50 && label18.Text !="Admin")
                      {

                          MessageBox.Show("You do not have permission");
                          richTextBox9.Text = "0";
                          label22.Text = label13.Text;
                          richTextBox5.Text = label13.Text;
                      }
                      else
                      {
                          
                          double persent = total * (discount / 100);
                          label53.Text = persent.ToString();
                          double cal_discount = total - persent;

                          double subtotal = Math.Ceiling(cal_discount);
                          label22.Text = subtotal.ToString();

                          richTextBox5.Text = subtotal.ToString();
                          

                      }
                  }


              }
              catch
              {
                
              }


              try
              {   int i=0;
                  string id = "";
                  double price = 0;
                  double dis = Convert.ToDouble(richTextBox9.Text);
                  double net_amount = 0;
                  int n=dataGridViewX2.Rows.Count;
                  for (i = 0; i < n; i++)
                  {
                      id = dataGridViewX2.Rows[i].Cells[4].Value.ToString();
                    price = Convert.ToDouble(dataGridViewX2.Rows[i].Cells[3].Value.ToString());
                    net_amount = (price - (price * dis / 100));
                    dataGridViewX2.Rows[i].Cells[6].Value = net_amount.ToString();
                    dataGridViewX2.Rows[i].Cells[5].Value = dis.ToString(); ;
                    
                  }
              }
              catch
              { 
              
              }

              try
              {
                  int exe = 0;
                  int id = 0; ;
                  string val1 = "";
                  string val2 = "";
                  string val3 = "";
                  string val4 = "";
                  int n = 0;
                  int c = dataGridViewX2.Rows.Count;
                  db.sql.Close();
                  db.sql.Open();
                  for (n = 0; n <= c - 1; n++)
                  {
                      val1 = dataGridViewX2.Rows[n].Cells[5].Value.ToString();
                      val3 = dataGridViewX2.Rows[n].Cells[6].Value.ToString();
                      val4 = dataGridViewX2.Rows[n].Cells[4].Value.ToString();
                      SqlCommand cmd = new SqlCommand("update diagnostic_bill set discount='" + val4 + "', net_price='" + val1 + "' where id='" + val3 + "'", db.sql);
                      exe = cmd.ExecuteNonQuery();

                  }
                  db.sql.Close();

                  
              }
              catch
              {

              }
       /*    try
            {
                if (richTextBox9.Text == "")
                {

                    label15.Text = "0";
                    richTextBox5.Text = label13.Text;
                }
                else
                {
                    double total = Convert.ToDouble(label13.Text);
                    double discount = Convert.ToDouble(richTextBox9.Text);
                    double due = total - discount;
                    label22.Text = due.ToString(); ;
                    richTextBox5.Text = due.ToString();
                }
            }
            catch
            {

            }*/

        }

        void discount2()

        {
            try
            {
                label53.Text = "";
                if (richTextBox9.Text == "")
                {

                    label15.Text = "0";
                    richTextBox5.Text = label13.Text;
                }
                else
                {
                    double total = Convert.ToDouble(label13.Text);
                    double discount = Convert.ToDouble(richTextBox9.Text);
                    double due = total - discount;
                    label22.Text = due.ToString(); ;
                    richTextBox5.Text = due.ToString();
                }
            }
            catch
            {

            }
        
        }
        private void dataGridViewX1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F1)
                {
                    add_test at = new add_test();
                    at.Show();


                }
            }
            catch
            {

            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                list();
            }
            else
            {
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select * from diagnostic_list where type='" + comboBox3.Text + "'", db.sql);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridViewX1.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dataGridViewX1.Rows.Add();
                        dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                        dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                        dataGridViewX1.Rows[n].Cells[2].Value = item[2].ToString();
                        dataGridViewX1.Rows[n].Cells[3].Value = item[3].ToString();
                    }
                    db.sql.Close();

                }
                catch
                {


                }
            }
        }


        void show_daily_status()
        {
            try
            {
                string number = "";
                string total = "";
                string paid = "";
                string due = "";

                string date = dateTimePicker1.Text;

                DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dd = d.ToString("yyyy/MM/dd");
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select  count(invoice_no),sum(total) ,sum(paid),sum(due) from diagnostic_person,paid_and_due where paid_and_due.invoice_no=diagnostic_person.id and date='" + date + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    number = read[0].ToString();
                    total = read[1].ToString();
                    paid = read[2].ToString();
                    due = read[3].ToString();

                }

                label26.Text = number;
                label27.Text = total;
                label29.Text = paid;
                label31.Text = due;
                db.sql.Close();
            }
            catch
            {

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int c = 0;
                string x = "";
                login log = new login(x);
                log.ShowDialog();

                string user = log.val1;
                string pass = log.val2;
                string type = log.val3;
                if (user == "" || pass == "")
                {

                }
                else
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("select * from log where pass='" + pass + "' and user_name='" + user + "'  and type='" + type + "'", db.sql);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        c++;
                    }
                    if (c > 0)
                    {
                        MessageBox.Show("Login Sucessfull");
                        label18.Text = type;
                        richTextBox11.Visible = true;
                        button6.Visible = true;
                        button7.Visible = false;

                    }
                    else
                    {
                        MessageBox.Show("Failed to Login");
                    }
                    db.sql.Close();
                }
            }
            catch
            { 
            
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox11.Visible = false;
                button6.Visible = false;
                button7.Visible = true;
                label18.Text = user_name;
            }
            catch
            { 
            
            }
        }

        private void richTextBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                

                int d = Convert.ToInt32(richTextBox11.Text);
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from diagnostic_bill where invoice_id='" + d + "'", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX2.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX2.Rows.Add();
                    dataGridViewX2.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX2.Rows[n].Cells[1].Value = item[6].ToString();
                    dataGridViewX2.Rows[n].Cells[2].Value = item[1].ToString();
                    dataGridViewX2.Rows[n].Cells[3].Value = item[2].ToString();
                    dataGridViewX2.Rows[n].Cells[4].Value = item[9].ToString();
                }

                db.sql.Close();


            }

            catch
            {
                dataGridViewX2.Rows.Clear();
            }

//**************************************************************************************************************
            try
            {
                double dis = 0;
                double total = 0;
                double paid = 0;
                double due = 0;
                double discount_total = 0;
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select max(discount_percent),max(total),max(paid),max(due),max(discount_total) from diagnostic_bill,paid_and_due where invoice_no=invoice_id and invoice_no='" + richTextBox11.Text + "' group by invoice_no", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    dis = Convert.ToDouble(read[0].ToString());
                    total = Convert.ToDouble(read[1].ToString());
                    paid = Convert.ToDouble(read[2].ToString());
                    due = Convert.ToDouble(read[3].ToString());
                    discount_total = Convert.ToDouble(read[4].ToString());

                }
                richTextBox9.Text = dis.ToString();
                label13.Text = total.ToString();
                label22.Text = discount_total.ToString();
                richTextBox5.Text = paid.ToString();
               
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
           /* db.sql.Close();
            int sum = 0;
            for (int i = 0; i < dataGridViewX2.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridViewX2.Rows[i].Cells[3].Value);
            }
            label13.Text = sum.ToString();*/
           // count_sum();
            //discount();
            patient_info_load();
        }


        void patient_info_load()
        {
            int c = 0;
            try
            {
                string test_date = "";
                string con = "";
                db.sql.Close();
                db.sql.Open();
                string date = "";
                SqlCommand cmd = new SqlCommand("select * from diagnostic_person where id='" + richTextBox11.Text + "'", db.sql);
                SqlDataReader re = cmd.ExecuteReader();
                while (re.Read())
                {
                    c++;
                    richTextBox1.Text = re[1].ToString();
                    richTextBox2.Text = re[2].ToString();
                    comboBox2.Text = re[6].ToString();
                    richTextBox7.Text = re[7].ToString();
                    richTextBox3.Text = re[3].ToString();

                   
                    comboBox5.Text = re[14].ToString();
                    test_date = re[5].ToString();
                    date = re[13].ToString();
                    DateTime convert = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime t_date = DateTime.ParseExact(test_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dateTimePicker3.Text = convert.ToString();
                    dateTimePicker1.Text = t_date.ToString();
                    
                  try
                  {
                      comboBox4.Text = re[11].ToString();
                      comboBox1.Text = re[9].ToString();
                  }
                    catch
                  {
                     
                    }
                    
                  
                }
          
                if (c == 0)
                {

                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";
                    richTextBox3.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text = "";
                    richTextBox10.Text = "";
                    comboBox5.Text = "";
                    dateTimePicker3.Text = DateTime.Now.ToString();
                    dateTimePicker1.Text = DateTime.Now.ToString();
                }
                db.sql.Close();
            }
            catch
            {
            }

        }

        private void Diagnostic_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            header();
            // TODO: This line of code loads data into the 'DataSet13.user_cash_collection' table. You can move, or remove it, as needed.
            this.user_cash_collectionTableAdapter.Fill(this.DataSet13.user_cash_collection,user_name,password,dateTimePicker2.Text);
            // TODO: This line of code loads data into the 'DataSet12.referance' table. You can move, or remove it, as needed.

            this.reportViewer1.RefreshReport();
            try
            {
                refere_doctor();
                refere_doctor2();
                show_daily_status();
            }
            catch
            {
            }
            
        }

        
//************************************due collection****************************



        void show()
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select id,name,contact,date,total,discount,discount_total,paid,due from diagnostic_person,paid_and_due where diagnostic_person.id=paid_and_due.invoice_no and paid_and_due.paid_date='0'", db.sql);
                DataTable dt = new DataTable();
                dataGridViewX3.Rows.Clear();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX3.Rows.Add();

                    dataGridViewX3.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX3.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX3.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX3.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridViewX3.Rows[n].Cells[4].Value = item[4].ToString();
                    dataGridViewX3.Rows[n].Cells[5].Value = item[5].ToString();
                    dataGridViewX3.Rows[n].Cells[6].Value = item[6].ToString();
                    dataGridViewX3.Rows[n].Cells[7].Value = item[7].ToString();
                    dataGridViewX3.Rows[n].Cells[8].Value = item[8].ToString();

                }
            }
            catch
            {

            }

        }

        private void richTextBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(richTextBox12.Text);
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select id,name,contact,date,total,discount,discount_total,paid,due from diagnostic_person,paid_and_due where diagnostic_person.id=paid_and_due.invoice_no and paid_and_due.paid_date='0' and diagnostic_person.id='" + id + "'", db.sql);
                DataTable dt = new DataTable();
                dataGridViewX3.Rows.Clear();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX3.Rows.Add();

                    dataGridViewX3.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX3.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX3.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX3.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridViewX3.Rows[n].Cells[4].Value = item[4].ToString();
                    dataGridViewX3.Rows[n].Cells[5].Value = item[5].ToString();
                    dataGridViewX3.Rows[n].Cells[6].Value = item[6].ToString();
                    dataGridViewX3.Rows[n].Cells[7].Value = item[7].ToString();
                    dataGridViewX3.Rows[n].Cells[8].Value = item[8].ToString();

                }


                db.sql.Close();
            }
            catch
            {
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select id,name,contact,date,total,discount,discount_total,paid,due from diagnostic_person,paid_and_due where diagnostic_person.id=paid_and_due.invoice_no and paid_and_due.paid_date='0' and diagnostic_person.name like'%" + richTextBox12.Text + "%'", db.sql);
                    DataTable dt = new DataTable();
                    dataGridViewX3.Rows.Clear();
                    sda.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dataGridViewX3.Rows.Add();

                        dataGridViewX3.Rows[n].Cells[0].Value = item[0].ToString();
                        dataGridViewX3.Rows[n].Cells[1].Value = item[1].ToString();
                        dataGridViewX3.Rows[n].Cells[2].Value = item[2].ToString();
                        dataGridViewX3.Rows[n].Cells[3].Value = item[3].ToString();
                        dataGridViewX3.Rows[n].Cells[4].Value = item[4].ToString();
                        dataGridViewX3.Rows[n].Cells[5].Value = item[5].ToString();
                        dataGridViewX3.Rows[n].Cells[6].Value = item[6].ToString();
                        dataGridViewX3.Rows[n].Cells[7].Value = item[7].ToString();
                        dataGridViewX3.Rows[n].Cells[8].Value = item[8].ToString();

                    }


                    db.sql.Close();
                }
                catch
                {

                }
            }
        }

        private void dataGridViewX3_Click(object sender, EventArgs e)
        {
            try
            {
                id = Convert.ToInt32(dataGridViewX3.SelectedRows[0].Cells[0].Value.ToString());
                amount = Convert.ToDouble(dataGridViewX3.SelectedRows[0].Cells[8].Value.ToString());
                paid = Convert.ToDouble(dataGridViewX3.SelectedRows[0].Cells[7].Value.ToString());
                total = Convert.ToDouble(dataGridViewX3.SelectedRows[0].Cells[6].Value.ToString());
                paid2 = Convert.ToDouble(dataGridViewX3.SelectedRows[0].Cells[7].Value.ToString());
                due2 = Convert.ToDouble(dataGridViewX3.SelectedRows[0].Cells[8].Value.ToString());
                Date = dataGridViewX3.SelectedRows[0].Cells[3].Value.ToString();
                patient_name = dataGridViewX3.SelectedRows[0].Cells[1].Value.ToString();
                label39.Text = id.ToString();
                //textBox2.Text = id.ToString();
                label41.Text = amount.ToString();
            }

            catch
            {

            }
        }

       

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                diagnostic_bill2 dbb = new diagnostic_bill2(richTextBox11.Text);
               
                dbb.Show();
            }
            catch
            {

            }
        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double pay = Convert.ToDouble(richTextBox8.Text);
                double due = Convert.ToDouble(label41.Text);
                double dis2 = due - pay;

                label36.Text = dis2.ToString();

            }

            catch
            {
                label36.Text = "0";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label39.Text == "")
            {

                MessageBox.Show("Select  an Id  first...");
            }
            else
            {
                try
                {
                    double unpaid_paid = Convert.ToDouble(richTextBox8.Text);
                    double total_paid = unpaid_paid + paid;
                    double x = 0;
                    double discount = total - total_paid; ;
                    string date = DateTime.Now.ToShortDateString();
                    DateTime d = DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("dd/MM/yyyy");
                    string dd2 = d.ToString("yyyy/MM/dd");
                    if (Date == dd)
                    {
                        paid2 = total_paid;
                        due2 = 0;
                    }
                    DialogResult r = MessageBox.Show("Did You Get The Unpaid Amount Confirm Please", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        try
                        {

                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("update paid_and_due set paid='" + paid2 + "',due='" + due2 + "', less='" + discount + "',paid_date='" + dd + "' where invoice_no='" + id + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due_paid='" + unpaid_paid + "',discount2='" + label36.Text + "',paid_date='" + dd + "',date2='" + dd2 + "' where reg_no='" + id + "'", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" + user_name + "','" + password + "','" + label39.Text + "','" + patient_name + "','" + richTextBox8.Text + "','" + dd + "','"+dd2+"')", db.sql);
                            int a = cmd.ExecuteNonQuery();
                            cmd4.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            show();
                            if (a > 0)
                            {
                                MessageBox.Show("Paid Successfully....");
                                Diagnostic_Due_Collection due = new Diagnostic_Due_Collection(label39.Text, user_name, richTextBox8.Text);
                                due.Show();
                                show2();
                                label39.Text = "";
                                //  diagnostic_bill dbb = new diagnostic_bill(label39.Text);
                               // dbb.Show();
                            }
                        }
                        catch
                        {

                        }
                    }


                    else
                    {

                    }
                }
                catch
                { }
            }
        }


        //*******************show patient******************************


        void show2()
        {

             try
            {
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select id,diagnostic_person.name,contact,date,total,discount,discount_total,paid,paid_and_due.due,less,paid_and_due.paid_date,due_paid from diagnostic_person,paid_and_due,due_collection where diagnostic_person.id=paid_and_due.invoice_no and diagnostic_person.id=due_collection.reg_no", db.sql);
                DataTable dt = new DataTable();
                dataGridViewX4.Rows.Clear();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX4.Rows.Add();

                    dataGridViewX4.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX4.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX4.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX4.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridViewX4.Rows[n].Cells[4].Value = item[4].ToString();
                    dataGridViewX4.Rows[n].Cells[5].Value = item[5].ToString();
                    dataGridViewX4.Rows[n].Cells[6].Value = item[6].ToString();
                    dataGridViewX4.Rows[n].Cells[7].Value = item[7].ToString();
                    dataGridViewX4.Rows[n].Cells[8].Value = item[8].ToString();
                    dataGridViewX4.Rows[n].Cells[9].Value = item[9].ToString();
                    dataGridViewX4.Rows[n].Cells[10].Value = item[10].ToString();
                    dataGridViewX4.Rows[n].Cells[11].Value = item[11].ToString();
                    string due_check = dataGridViewX4.Rows[n].Cells[8].Value.ToString();
                    string paid = dataGridViewX4.Rows[n].Cells[7].Value.ToString();
                    string due_paid = dataGridViewX4.Rows[n].Cells[11].Value.ToString();
                    string date1 = dataGridViewX4.Rows[n].Cells[3].Value.ToString();
                    string date2 = dataGridViewX4.Rows[n].Cells[10].Value.ToString();
                    double c = 0;
                    try
                    {
                        double a = Convert.ToDouble(paid);
                        double b = Convert.ToDouble(due_paid);
                         c = a + b;
                    }
                    catch
                    { 
                    }
                    if (due_paid != "0")
                    {
                        dataGridViewX4.Rows[n].Cells[8].Value = "0";
                        //  dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                    }
                    if (due_paid != "0" && date1 != date2)
                    {

                        dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                    }
                    if (due_check == "0" && due_paid == "0")
                    {
                        dataGridViewX4.Rows[n].Cells[10].Value = "No Due";
                    }

                }


                db.sql.Close();
            }
             catch
             { 
            
             }

        }

        private void richTextBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(richTextBox13.Text);
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select id,diagnostic_person.name,contact,date,total,discount,discount_total,paid,paid_and_due.due,less,paid_and_due.paid_date,due_paid from diagnostic_person,paid_and_due,due_collection where diagnostic_person.id=paid_and_due.invoice_no and diagnostic_person.id=due_collection.reg_no and diagnostic_person.id='" + id + "'", db.sql);
                DataTable dt = new DataTable();
                dataGridViewX4.Rows.Clear();
                sda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX4.Rows.Add();

                    dataGridViewX4.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX4.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX4.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX4.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridViewX4.Rows[n].Cells[4].Value = item[4].ToString();
                    dataGridViewX4.Rows[n].Cells[5].Value = item[5].ToString();
                    dataGridViewX4.Rows[n].Cells[6].Value = item[6].ToString();
                    dataGridViewX4.Rows[n].Cells[7].Value = item[7].ToString();
                    dataGridViewX4.Rows[n].Cells[8].Value = item[8].ToString();
                    dataGridViewX4.Rows[n].Cells[9].Value = item[9].ToString();
                    dataGridViewX4.Rows[n].Cells[10].Value = item[10].ToString();
                    dataGridViewX4.Rows[n].Cells[11].Value = item[11].ToString();
                    string due_check = dataGridViewX4.Rows[n].Cells[8].Value.ToString();
                    string paid = dataGridViewX4.Rows[n].Cells[7].Value.ToString();
                    string due_paid = dataGridViewX4.Rows[n].Cells[11].Value.ToString();
                    string date1 = dataGridViewX4.Rows[n].Cells[3].Value.ToString();

                    string date2 = dataGridViewX4.Rows[n].Cells[10].Value.ToString();
                         double c =0;
                    try
                    {
                    double a = Convert.ToDouble(paid);
                    double b = Convert.ToDouble(due_paid);
                    c = a + b;
                    }
                    catch
                    {
                    
                    }

                    if (due_paid != "0")
                    {
                        dataGridViewX4.Rows[n].Cells[8].Value = "0";
                        //  dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                    }
                    if (due_paid != "0" && date1 != date2)
                    {

                        dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                    }
                    if (due_check == "0" && due_paid == "0")
                    {
                        dataGridViewX4.Rows[n].Cells[10].Value = "No Due";
                    }
                }


                db.sql.Close();
            }
            catch
            {
                try
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select id,diagnostic_person.name,contact,date,total,discount,discount_total,paid,paid_and_due.due,less,paid_and_due.paid_date,due_paid from diagnostic_person,paid_and_due,due_collection where diagnostic_person.id=paid_and_due.invoice_no and diagnostic_person.id=due_collection.reg_no and diagnostic_person.name like'%" + richTextBox13.Text + "%'", db.sql);
                    DataTable dt = new DataTable();
                    dataGridViewX4.Rows.Clear();
                    sda.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dataGridViewX4.Rows.Add();

                        dataGridViewX4.Rows[n].Cells[0].Value = item[0].ToString();
                        dataGridViewX4.Rows[n].Cells[1].Value = item[1].ToString();
                        dataGridViewX4.Rows[n].Cells[2].Value = item[2].ToString();
                        dataGridViewX4.Rows[n].Cells[3].Value = item[3].ToString();
                        dataGridViewX4.Rows[n].Cells[4].Value = item[4].ToString();
                        dataGridViewX4.Rows[n].Cells[5].Value = item[5].ToString();
                        dataGridViewX4.Rows[n].Cells[6].Value = item[6].ToString();
                        dataGridViewX4.Rows[n].Cells[7].Value = item[7].ToString();
                        dataGridViewX4.Rows[n].Cells[8].Value = item[8].ToString();
                        dataGridViewX4.Rows[n].Cells[9].Value = item[9].ToString();
                        dataGridViewX4.Rows[n].Cells[10].Value = item[10].ToString();
                        dataGridViewX4.Rows[n].Cells[11].Value = item[11].ToString();
                        string due_check = dataGridViewX4.Rows[n].Cells[8].Value.ToString();
                        string paid = dataGridViewX4.Rows[n].Cells[7].Value.ToString();
                        string due_paid = dataGridViewX4.Rows[n].Cells[11].Value.ToString();
                        string date1 = dataGridViewX4.Rows[n].Cells[3].Value.ToString();
                        string date2 = dataGridViewX4.Rows[n].Cells[10].Value.ToString();
                          double c=0;
                        try
                        {
                        double a = Convert.ToDouble(paid);
                        double b = Convert.ToDouble(due_paid);
                         c = a + b;
                        }
                        catch
                        {
                        
                        }
                        if (due_paid != "0")
                        {
                            dataGridViewX4.Rows[n].Cells[8].Value = "0";
                            //  dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                        }
                        if (due_paid != "0" && date1 != date2)
                        {

                            dataGridViewX4.Rows[n].Cells[7].Value = c.ToString();

                        }
                        if (due_check == "0" && due_paid == "0")
                        {
                            dataGridViewX4.Rows[n].Cells[10].Value = "No Due";
                        }
                    }


                    db.sql.Close();
                }
                catch
                {

                }
                
            }
            finally
            {
                // show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {

                Diagnostic_xl_print dbb = new Diagnostic_xl_print(label43.Text);

                dbb.Show();
            }
            catch
            {

            }
        }

        private void dataGridViewX4_Click(object sender, EventArgs e)
        {
            try
            {
                string id = (dataGridViewX4.SelectedRows[0].Cells[0].Value.ToString());
                label43.Text = id;

            }

            catch
            {

            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            show();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                this.user_cash_collectionTableAdapter.Fill(this.DataSet13.user_cash_collection, user_name, password, dateTimePicker2.Text);
                // TODO: This line of code loads data into the 'DataSet12.referance' table. You can move, or remove it, as needed.

                this.reportViewer1.RefreshReport();
            }
            catch
            { 
            
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                header();
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                this.user_cash_collectionTableAdapter.Fill(this.DataSet13.user_cash_collection, user_name, password, dateTimePicker2.Text);
                // TODO: This line of code loads data into the 'DataSet12.referance' table. You can move, or remove it, as needed.

                this.reportViewer1.RefreshReport();
            }
            catch
            {
            
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text == "")
            {
                MessageBox.Show("You Have to pay Some Amount");
            }

            else
            {
                try
                {
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    DialogResult r = MessageBox.Show("Do You Want to save  this data....??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        int c = 0;
                        string paid_date = "0";
                        int match = Convert.ToInt32(richTextBox11.Text);
                        double total = Convert.ToDouble(label13.Text);
                        double paid = Convert.ToDouble(richTextBox5.Text);
                        double due = Convert.ToDouble(label15.Text);
                        double discount = Convert.ToDouble(richTextBox9.Text);
                        double discount_total = Convert.ToDouble(label22.Text);
                        if (due == 0)
                        {
                            paid_date = date;
                        }
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd1 = new SqlCommand("select * from paid_and_due where invoice_no='" + match + "'", db.sql);
                        SqlDataReader read = cmd1.ExecuteReader();
                        while (read.Read())
                        {
                            c++;

                        }
                        if (c > 0)
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd2 = new SqlCommand("update paid_and_due set total='" + total + "', paid='" + paid + "',due='" + due + "',discount='" + discount + "',discount_total='" + discount_total + "',paid_date='" + paid_date + "',date2='"+dd+"'  where invoice_no='" + match + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due='" + due + "' where reg_no='" + match + "'", db.sql);
                            SqlCommand cmde = new SqlCommand("update referance set name='" + comboBox1.Text + "',patient_name='" + richTextBox1.Text + "',date='" + date + "',bill='" + label13.Text + "',date2='" + dd + "',after_discount='" + label22.Text + "' where patient_id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmdd = new SqlCommand("update diagnostic_person set referdoctor='" + comboBox1.Text + "',name='" + richTextBox1.Text + "',contact='" + richTextBox2.Text + "',address='" + richTextBox3.Text + "',gender='" + comboBox2.Text + "',age='" + richTextBox7.Text + "',referedto='" + comboBox4.Text + "',delevery_date='" + dateTimePicker3.Text + "',delevery_time='" + comboBox5.Text+ "',date='"+dateTimePicker1.Text+"',date2='"+dd+"' where id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmddd = new SqlCommand("update user_cash_collection set paid='" + richTextBox5.Text + "',name='" + richTextBox1.Text + "',date='" + dateTimePicker1.Text+ "',date2='"+dd+"' where reg_no='" + richTextBox11.Text + "'", db.sql);
                             SqlCommand diagnostic_bill = new SqlCommand("update diagnostic_bill set date='" + dateTimePicker1.Text+ "',date2='"+dd+"' where invoice_id='" + richTextBox11.Text + "'", db.sql);
                            cmd2.ExecuteNonQuery();
                            
                            int x = cmde.ExecuteNonQuery();

                            cmd4.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            diagnostic_bill.ExecuteNonQuery();
                            button5.Enabled = true;

                            discount_update();
                            MessageBox.Show("Updaate Sucessfull");

                            id_generate();
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("insert into paid_and_due(invoice_no,total,paid,due,discount,discount_total,paid_date,date2)values('" + match + "','" + total + "','" + paid + "','" + due + "','" + discount + "','" + discount_total + "','" + paid_date + "','" + dd + "')", db.sql);
                            SqlCommand cmdd = new SqlCommand("insert into due_collection(reg_no,name,due,test_date)values('" + match + "','" + richTextBox1.Text + "','" + due + "','" + date + "')", db.sql);
                            SqlCommand cmddd = new SqlCommand("insert into referance(name,patient_id,patient_name,date,bill,date2,after_discount) values('" + comboBox1.Text + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + date + "','" + label13.Text + "','" + dd + "','" + label22.Text + "')", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" +user_name+ "','" + password + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + richTextBox5.Text + "','" + date + "','"+dd+"')", db.sql);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            button5.Enabled = true;
                            button4.Enabled = false;                          
                            show_daily_status();
                            discount_update();
                            MessageBox.Show("Save Sucessfull");
                            id_generate();
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("Give a valid Paing Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void richTextBox14_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (richTextBox14.Text == "")
                {
                    db.sql.Close();
                    db.sql.Open();
                    SqlCommand cmd = new SqlCommand("select name from doctors", db.sql);
                    SqlDataReader read = cmd.ExecuteReader();
                    comboBox1.Items.Clear();
                    while (read.Read())
                    {
                        String name = read[0].ToString();
                        comboBox1.Items.Add(name);
                    }

                    db.sql.Close();
                }
                else
                {
                    comboBox1.Items.Clear();
                    comboBox1.Text ="";
                    db.sql.Close();
                    db.sql.Open();
                    //SqlCommand cmd = new SqlCommand("select name from doctors where name like '%"+richTextBox14.Text+"%' ", db.sql);
                    
                    SqlCommand cmd = new SqlCommand("select name from doctors where id='" + richTextBox14.Text + "' or name like '%"+richTextBox14.Text+"%' ", db.sql);
                    SqlDataReader read = cmd.ExecuteReader();
                    comboBox1.Items.Clear();
                    while (read.Read())
                    {
                        String name = read[0].ToString();
                        //MessageBox.Show(name);
                        comboBox1.Items.Add(name);
                        //comboBox1.Text = name;
                    }

                    db.sql.Close();
                }
            }

            catch
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select name from doctors", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                comboBox1.Items.Clear();
                while (read.Read())
                {
                    String name = read[0].ToString();
                    comboBox1.Items.Add(name);
                }

                db.sql.Close();

            }
        
        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox6.Text != "")
            {
                richTextBox4.Clear();
                prescription_id.Clear();
                other_id = richTextBox6.Text;
                other_type = "indoor";
            }
            int c = 0;
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from patient_histroy where reg_no='" + richTextBox6.Text + "'", db.sql);
                SqlDataReader re = cmd.ExecuteReader();
                while (re.Read())
                {
                   

                    c++;
                    richTextBox1.Text = re[1].ToString();
                    richTextBox2.Text = re[2].ToString();
                    comboBox2.Text = re[3].ToString();
                    richTextBox7.Text = re[4].ToString();
                    richTextBox3.Text = re[5].ToString();
                    comboBox4.Text = re[17].ToString();
                    comboBox1.Text = re[18].ToString();
                }
                if (c == 0)
                {
                   // MessageBox.Show("NOT FOUND");
                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";
                    richTextBox3.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text ="";
                  
                    richTextBox10.Text = "";
                }
                db.sql.Close();
            }
            catch
            {
            }
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            /*try
            {
                ComboBox cbo = sender as ComboBox;
                cbo.Focus();
                cbo.DroppedDown = true;
            }
            catch
            { 
            
            }*/
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
            /*try
            {
                ComboBox cbo = sender as ComboBox;
                cbo.Focus();
                cbo.DroppedDown = true;
            }
            catch
            { 
            
            }*/
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
          /*  try
            {
                ComboBox cbo = sender as ComboBox;
                cbo.Focus();
                cbo.DroppedDown = true;
            }
            catch
            { }*/
        }
        void auto_complete()
        {

            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select name from diagnostic_list", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    col.Add(read[0].ToString());
                }
                db.sql.Close();
            }

            catch
            {

            }
            textBox1.AutoCompleteCustomSource = col;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from diagnostic_list where id like'" + textBox1.Text + "%' or name like'%" + textBox1.Text + "%'", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridViewX1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridViewX1.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridViewX1.Rows[n].Cells[3].Value = item[3].ToString();

                }
                db.sql.Close();

            }
            catch
            {


            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    x = dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString();
                    label11.Text = x;
                    y = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                    z = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                    price = Convert.ToDouble(z);
                }

                catch
                {

                }


                try
                {
                    if (richTextBox1.Text == "")
                    {
                        MessageBox.Show("Enter Name..");
                    }


                    else if (label11.Text == "")
                    {
                        MessageBox.Show("Select a Item");
                    }
                    else if (comboBox2.Text == "")
                    {
                        MessageBox.Show("Select Gender");
                    }


                    else if (comboBox1.Text == "")
                    {
                        MessageBox.Show("Select  Referad Doctor");
                    }
                    else
                    {
                        int c = 0;
                        int cc = 0;
                        int match = Convert.ToInt32(richTextBox11.Text);
                        string date = dateTimePicker1.Text;
                        DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string dd = d.ToString("yyyy/MM/dd");
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd = new SqlCommand("select * from diagnostic_person where id='" + match + "' and name='" + richTextBox1.Text + "' and contact='" + richTextBox2.Text + "'", db.sql);
                        SqlDataReader re = cmd.ExecuteReader();
                        while (re.Read())
                        {
                            c++;
                        }
                        if (c > 0)
                        {

                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd4 = new SqlCommand("select * from diagnostic_bill where invoice_id='" + match + "' and test_name='" + y + "'", db.sql);
                            SqlDataReader rd = cmd4.ExecuteReader();
                            while (rd.Read())
                            {
                                cc++;
                            }
                            if (cc > 0)
                            {
                                MessageBox.Show("item already added");
                            }
                            else
                            {
                                db.sql.Close();
                                db.sql.Open();
                                SqlCommand cmd3 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_name,price,date)values('" + match + "','" + y + "','" + price + "','" + date + "')", db.sql);

                                cmd3.ExecuteNonQuery();
                                show_bill();
                                discount();
                            }
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            //MessageBox.Show("ok");
                            SqlCommand cmd1 = new SqlCommand("insert into diagnostic_person(id,name,contact,address,date,gender,age,referdoctor,date2,others_id,referedto)values('" + match + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + date + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox10.Text + "','" + dd + "','" + richTextBox6.Text + "','" + comboBox4.Text + "')", db.sql);
                            SqlCommand cmd2 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_name,price,date)values('" + match + "','" + y + "','" + price + "','" + date + "')", db.sql);
                            cmd1.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            show_bill();
                            count_sum();
                            discount();
                        }

                        db.sql.Close();
                    }
                }

                catch
                {

                }
            }
            else if (e.KeyCode==Keys.Down)
            {
                dataGridViewX1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                dataGridViewX1.Focus();
            }
        }

        private void dataGridViewX1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    x = dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString();
                    label11.Text = x;
                    y = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                    z = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                    zz = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                    price = Convert.ToDouble(z);
                }

                catch
                {

                }


                try
                {
                    if (richTextBox1.Text == "")
                    {
                        MessageBox.Show("Enter Name..");
                    }


                    else if (label11.Text == "")
                    {
                        MessageBox.Show("Select a Item");
                    }
                    else if (comboBox2.Text == "")
                    {
                        MessageBox.Show("Select Gender");
                    }


                    else if (comboBox1.Text == "")
                    {
                        MessageBox.Show("Select  Referad Doctor");
                    }
                    else
                    {
                        int c = 0;
                        int cc = 0;
                        int match = Convert.ToInt32(richTextBox11.Text);
                        string date = dateTimePicker1.Text;
                        DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string dd = d.ToString("yyyy/MM/dd");
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd = new SqlCommand("select * from diagnostic_person where id='" + match + "' and name='" + richTextBox1.Text + "' and contact='" + richTextBox2.Text + "'", db.sql);
                        SqlDataReader re = cmd.ExecuteReader();
                        while (re.Read())
                        {
                            c++;
                        }
                        if (c > 0)
                        {

                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd4 = new SqlCommand("select * from diagnostic_bill where invoice_id='" + match + "' and test_name='" + y + "'", db.sql);
                            SqlDataReader rd = cmd4.ExecuteReader();
                            while (rd.Read())
                            {
                                cc++;
                            }
                            if (cc > 0)
                            {
                                MessageBox.Show("item already added");
                            }
                            else
                            {
                                string xerox = "";
                                if (zz == "Pathological Test")
                                {
                                    xerox = "";
                                }
                                else
                                {
                                    xerox = "xerox";
                                }
                                
                                db.sql.Close();
                                db.sql.Open();
                                SqlCommand cmd3 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_id,test_name,price,date,date2,xerox)values('" + match + "','" + label11.Text + "','" + y + "','" + price + "','" + date + "','" + dd + "','" + xerox + "')", db.sql);

                                cmd3.ExecuteNonQuery();
                                show_bill();
                                discount();
                                lab_inventory_reduce();
                            }
                        }
                        else
                        {
                            string xerox = "";
                            if (zz == "Pathological Test")
                            {
                                xerox = "";
                            }
                            else
                            {
                                xerox = "xerox";
                            }
                                
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd1 = new SqlCommand("insert into diagnostic_person(id,name,contact,address,date,gender,age,referdoctor,date2,others_id,referedto,other_type,delevery_date,delevery_time)values('" + match + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + date + "','" + comboBox2.Text + "','" + richTextBox7.Text + "','" + richTextBox10.Text + "','" + dd + "','" + other_id + "','" + consultant + "','" + other_type + "','" + dateTimePicker3.Text + "','" + comboBox5.Text + "')", db.sql);
                            SqlCommand cmd2 = new SqlCommand("insert into diagnostic_bill(invoice_id,test_id,test_name,price,date,date2,xerox,other_id,other_type)values('" + match + "','" + label11.Text + "','" + y + "','" + price + "','" + date + "','" + dd + "','" + xerox + "','" + other_id + "','" + other_type + "')", db.sql);
                            cmd1.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            show_bill();
                            count_sum();
                            discount();
                            lab_inventory_reduce();
                        }

                        db.sql.Close();
                    }
                }

                catch
                {

                }
                textBox1.Focus();

            }
            if (e.KeyCode == Keys.Back)
            {
                textBox1.Focus();
            
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox9.Text == "")
                {

                    label15.Text = "0";
                    label22.Text = label13.Text;
                    richTextBox5.Text = label13.Text;
                }
                else
                {
                    double total = Convert.ToDouble(label13.Text);
                    double discount = Convert.ToDouble(String.Concat(richTextBox9.Text.Where(Char.IsDigit)));
                    if (discount > 25 && label18.Text != "Admin")
                    {

                        MessageBox.Show("You do not have permission");
                        richTextBox9.Text = "0";
                        label22.Text = label13.Text;
                        richTextBox5.Text = label13.Text;
                    }
                    else
                    {
                        double persent = total * (discount / 100);
                        double cal_discount = total - persent;

                        double subtotal = Math.Ceiling(cal_discount);
                        label22.Text = subtotal.ToString();

                        richTextBox5.Text = subtotal.ToString();

                    }
                }
            }
            catch
            {

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox9.Text == "")
                {
                    label53.Text = "";
                    label15.Text = "0";
                    richTextBox5.Text = label13.Text;
                }
                else
                {
                    label53.Text = "";
                    double total = Convert.ToDouble(label13.Text);
                    double discount = Convert.ToDouble(richTextBox9.Text);
                    double due = total - discount;
                    label22.Text = due.ToString(); ;
                    richTextBox5.Text = due.ToString();
                }
            }
            catch
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult r = MessageBox.Show("Do You Want to delete this","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                  
                    if (user_type == "Admin")
                    {
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd = new SqlCommand("delete from diagnostic_bill where invoice_id='" + richTextBox11.Text+ "'", db.sql);
                        SqlCommand cmd1 = new SqlCommand("delete from diagnostic_person where id='" + richTextBox11 .Text+ "'", db.sql);
                        SqlCommand cmd2 = new SqlCommand("delete from paid_and_due where invoice_no='" + richTextBox11.Text + "'", db.sql);
                        SqlCommand cmd3 = new SqlCommand("delete from due_collection where reg_no='" + richTextBox11.Text + "'", db.sql);
                        SqlCommand cmd4 = new SqlCommand("delete from referance where patient_id='" + richTextBox11.Text + "'", db.sql);
                        SqlCommand cmd5 = new SqlCommand("delete from user_cash_collection where reg_no='" + richTextBox11.Text + "'", db.sql);
                        int a = cmd.ExecuteNonQuery();
                        int b = cmd1.ExecuteNonQuery();
                        int c = cmd2.ExecuteNonQuery();
                        int d = cmd3.ExecuteNonQuery();
                        int ee = cmd4.ExecuteNonQuery();
                        int f = cmd5.ExecuteNonQuery();
                        if (a > 0 || b > 0)
                        {
                            MessageBox.Show("Deleted Sucessfully");
                            show_daily_status();
                            dataGridViewX2.Rows.Clear();
                            label13.Text = "";
                            richTextBox1.Text = "";
                            richTextBox2.Text = "";
                            richTextBox3.Text = "";
                            comboBox2.Text = "";
                            richTextBox7.Text = "";

                            comboBox1.Text = "";
                            richTextBox10.Text = "";
                            label22.Text = "";
                            richTextBox9.Text = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only Admin Can Delete");
                    }
                    db.sql.Close();
                }
            }
            catch
            { 
            
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.YellowGreen;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }


        void test_by_doctor()
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from patient_test where reg_no='" + prescription_id.Text + "'", db.sql);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[2].ToString();

                    string x = dataGridView1.Rows[n].Cells[0].Value.ToString();
                    if (x == "")
                    {
                        dataGridView1.Rows[n].Visible = false;
                    }
                }
            }
            catch
            {

            }

        }

        private void prescription_id_TextChanged(object sender, EventArgs e)
        {
            if (prescription_id.Text != "")
            {
                richTextBox6.Clear();
                richTextBox4.Clear();
                other_id = prescription_id.Text;
                other_type = "prescription";
            }
            test_by_doctor();
            prescription_info();
        }
        void prescription_info()
        {
            try
            {
                string name = "";
                string contact = "";
                string age = "";
                string gender = "";
                string address = "";
                int c = 0;
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from appointment where reg_no='" + prescription_id.Text+ "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    c++;
                    name = read[1].ToString();
                   
                    age = read[3].ToString();
                    gender = read[4].ToString();
                    address = read[2].ToString();
                }
                if (c > 0)
                {
                    richTextBox1.Text = name;
                    comboBox2.Text = gender;
                    richTextBox7.Text = age;
                    richTextBox3.Text = address;
                }
                else
                {
                    richTextBox1.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";
                    richTextBox3.Text = "";
                }
                db.sql.Close();

            }
            catch
            {
                richTextBox1.Text = "";
                comboBox2.Text = "";
                richTextBox7.Text = "";
                richTextBox3.Text = "";
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text == "")
            {
                MessageBox.Show("You Have to pay Some Amount");
            }

            else
            {
                try
                {
                    if (consultant == "")
                    {
                        consultant = comboBox4.Text;
                    }
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    DialogResult r = MessageBox.Show("Do You Want to save  this data....??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        int c = 0;
                        string paid_date = "0";
                        int match = Convert.ToInt32(richTextBox11.Text);
                        double total = Convert.ToDouble(label13.Text);
                        double paid = Convert.ToDouble(richTextBox5.Text);
                        double due = Convert.ToDouble(label15.Text);
                        //double discount = Convert.ToDouble(richTextBox9.Text);
                        double discount = Convert.ToDouble(Convert.ToInt32(label13.Text) - Convert.ToInt32(label22.Text));
                        double discount_total = Convert.ToDouble(label22.Text);
                        if (due == 0)
                        {
                            paid_date = date;
                        }
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd1 = new SqlCommand("select * from paid_and_due where invoice_no='" + match + "'", db.sql);
                        SqlDataReader read = cmd1.ExecuteReader();
                        while (read.Read())
                        {
                            c++;

                        }
                        if (c > 0)
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd2 = new SqlCommand("update paid_and_due set total='" + total + "', paid='" + paid + "',due='" + due + "',discount='" + discount + "',discount_total='" + discount_total + "',paid_date='" + paid_date + "',date2='" + dd + "'  where invoice_no='" + match + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due='" + due + "' where reg_no='" + match + "'", db.sql);
                            SqlCommand cmde = new SqlCommand("update referance set name='" + comboBox1.Text + "',patient_name='" + richTextBox1.Text + "',date='" + date + "',bill='" + label13.Text + "',date2='" + dd + "',after_discount='" + label22.Text + "' where patient_id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmdd = new SqlCommand("update diagnostic_person set referdoctor='" + comboBox1.Text + "',name='" + richTextBox1.Text + "',contact='" + richTextBox2.Text + "',address='" + richTextBox3.Text + "',gender='" + comboBox2.Text + "',age='" + richTextBox7.Text + "',referedto='" + comboBox4.Text + "',delevery_date='" + dateTimePicker3.Text + "',delevery_time='" + comboBox5.Text + "',date='" + dateTimePicker1.Text + "',date2='" + dd + "' where id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmddd = new SqlCommand("update user_cash_collection set paid='" + richTextBox5.Text + "',name='" + richTextBox1.Text + "',date='" + dateTimePicker1.Text + "',date2='" + dd + "' where reg_no='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand diagnostic_bill = new SqlCommand("update diagnostic_bill set date='" + dateTimePicker1.Text + "',date2='" + dd + "' where invoice_id='" + richTextBox11.Text + "'", db.sql);
                            cmd2.ExecuteNonQuery();

                            int x = cmde.ExecuteNonQuery();

                            cmd4.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            diagnostic_bill.ExecuteNonQuery();
                            button5.Enabled = true;
                            discount_update();
                            MessageBox.Show("Updaate Sucessfull");
                            Diagnostic_xl_print dbb = new Diagnostic_xl_print(richTextBox11.Text);
                            dbb.Show();
                            show_daily_status();
                            
                            show2();
                            id_generate();
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("insert into paid_and_due(invoice_no,total,paid,due,discount,discount_total,paid_date,date2)values('" + match + "','" + total + "','" + paid + "','" + due + "','" + discount + "','" + discount_total + "','" + paid_date + "','" + dd + "')", db.sql);
                            SqlCommand cmdd = new SqlCommand("insert into due_collection(reg_no,name,due,test_date)values('" + match + "','" + richTextBox1.Text + "','" + due + "','" + date + "')", db.sql);
                            SqlCommand cmddd = new SqlCommand("insert into referance(name,patient_id,patient_name,date,bill,date2,after_discount) values('" + comboBox1.Text + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + date + "','" + label13.Text + "','" + dd + "','" + label22.Text + "')", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" + user_name + "','" + password + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + richTextBox5.Text + "','" + date + "','" + dd + "')", db.sql);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            discount_update();
                            button5.Enabled = true;
                            button4.Enabled = false;
                            Diagnostic_xl_print dbb = new Diagnostic_xl_print(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                            id_generate();
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("Give a valid Paing Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void discount_update()
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
                int c = dataGridViewX2.Rows.Count;
                db.sql.Close();
                db.sql.Open();
                for (n = 0; n <= c - 1; n++)
                {
                    val1 = dataGridViewX2.Rows[n].Cells[5].Value.ToString();
                    val3 = dataGridViewX2.Rows[n].Cells[6].Value.ToString();
                    val4 = dataGridViewX2.Rows[n].Cells[4].Value.ToString();
                    SqlCommand cmd = new SqlCommand("update diagnostic_bill set discount_percent='" + val1 + "', net_price='" + val3+ "' where id='" + val4 + "'", db.sql);
                    exe = cmd.ExecuteNonQuery();

                }
                db.sql.Close();


            }
            catch
            {

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text == "")
            {
                MessageBox.Show("You Have to pay Some Amount");
            }

            else
            {
                try
                {
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    DialogResult r = MessageBox.Show("Do You Want to save  this data....??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        int c = 0;
                        string paid_date = "0";
                        int match = Convert.ToInt32(richTextBox11.Text);
                        double total = Convert.ToDouble(label13.Text);
                        double paid = Convert.ToDouble(richTextBox5.Text);
                        double due = Convert.ToDouble(label15.Text);
                        double discount = Convert.ToDouble(richTextBox9.Text);
                        double discount_total = Convert.ToDouble(label22.Text);
                        if (due == 0)
                        {
                            paid_date = date;
                        }
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd1 = new SqlCommand("select * from paid_and_due where invoice_no='" + match + "'", db.sql);
                        SqlDataReader read = cmd1.ExecuteReader();
                        while (read.Read())
                        {
                            c++;

                        }
                        if (c > 0)
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd2 = new SqlCommand("update paid_and_due set total='" + total + "', paid='" + paid + "',due='" + due + "',discount='" + discount + "',discount_total='" + discount_total + "',paid_date='" + paid_date + "'  where invoice_no='" + match + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due='" + due + "' where reg_no='" + match + "'", db.sql);
                            SqlCommand cmde = new SqlCommand("update referance set name='" + comboBox1.Text + "',patient_name='" + richTextBox1.Text + "',date='" + date + "',bill='" + label13.Text + "',date2='" + dd + "',after_discount='" + label22.Text + "' where patient_id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmdd = new SqlCommand("update diagnostic_person set referdoctor='" + comboBox1.Text + "' where id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmddd = new SqlCommand("update user_cash_collection set paid='" + richTextBox5.Text + "' where reg_no='" + richTextBox11.Text + "'", db.sql);
                            cmd2.ExecuteNonQuery();

                            int x = cmde.ExecuteNonQuery();

                            cmd4.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            button5.Enabled = true;
                            Diagnostic_Bill3 dbb = new Diagnostic_Bill3(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("insert into paid_and_due(invoice_no,total,paid,due,discount,discount_total,paid_date,date2)values('" + match + "','" + total + "','" + paid + "','" + due + "','" + discount + "','" + discount_total + "','" + paid_date + "','" + dd + "')", db.sql);
                            SqlCommand cmdd = new SqlCommand("insert into due_collection(reg_no,name,due,test_date)values('" + match + "','" + richTextBox1.Text + "','" + due + "','" + date + "')", db.sql);
                            SqlCommand cmddd = new SqlCommand("insert into referance(name,patient_id,patient_name,date,bill,date2,after_discount) values('" + comboBox1.Text + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + date + "','" + label13.Text + "','" + dd + "','" + label22.Text + "')", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" + user_name + "','" + password + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + richTextBox5.Text + "','" + date + "','" + dd + "')", db.sql);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            button5.Enabled = true;
                            button4.Enabled = false;
                            Diagnostic_Bill3 dbb = new Diagnostic_Bill3(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("Give a valid Paing Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        Image logo;
        string base64String;


        void header()
        {
            try
            {
                string name = "";
                string address = "";
                string phone = "";
                string mobile = "";
                string establish = "";

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
                r.Add(new ReportParameter("logo", base64String.ToString()));

                this.reportViewer1.LocalReport.SetParameters(r);
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text == "")
            {
                MessageBox.Show("You Have to pay Some Amount");
            }

            else
            {
                try
                {
                    string date = dateTimePicker1.Text;
                    DateTime d = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dd = d.ToString("yyyy/MM/dd");
                    DialogResult r = MessageBox.Show("Do You Want to save  this data....??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        int c = 0;
                        string paid_date = "0";
                        int match = Convert.ToInt32(richTextBox11.Text);
                        double total = Convert.ToDouble(label13.Text);
                        double paid = Convert.ToDouble(richTextBox5.Text);
                        double due = Convert.ToDouble(label15.Text);
                        double discount = Convert.ToDouble(richTextBox9.Text);
                        double discount_total = Convert.ToDouble(label22.Text);
                        if (due == 0)
                        {
                            paid_date = date;
                        }
                        db.sql.Close();
                        db.sql.Open();
                        SqlCommand cmd1 = new SqlCommand("select * from paid_and_due where invoice_no='" + match + "'", db.sql);
                        SqlDataReader read = cmd1.ExecuteReader();
                        while (read.Read())
                        {
                            c++;

                        }
                        if (c > 0)
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd2 = new SqlCommand("update paid_and_due set total='" + total + "', paid='" + paid + "',due='" + due + "',discount='" + discount + "',discount_total='" + discount_total + "',paid_date='" + paid_date + "'  where invoice_no='" + match + "'", db.sql);
                            SqlCommand cmd4 = new SqlCommand("update due_collection set due='" + due + "' where reg_no='" + match + "'", db.sql);
                            SqlCommand cmde = new SqlCommand("update referance set name='" + comboBox1.Text + "',patient_name='" + richTextBox1.Text + "',date='" + date + "',bill='" + label13.Text + "',date2='" + dd + "',after_discount='" + label22.Text + "' where patient_id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmdd = new SqlCommand("update diagnostic_person set referdoctor='" + comboBox1.Text + "' where id='" + richTextBox11.Text + "'", db.sql);
                            SqlCommand cmddd = new SqlCommand("update user_cash_collection set paid='" + richTextBox5.Text + "' where reg_no='" + richTextBox11.Text + "'", db.sql);
                            cmd2.ExecuteNonQuery();

                            int x = cmde.ExecuteNonQuery();

                            cmd4.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            button5.Enabled = true;
                            Diagnostic_xl_print dbb = new Diagnostic_xl_print(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                        }
                        else
                        {
                            db.sql.Close();
                            db.sql.Open();
                            SqlCommand cmd = new SqlCommand("insert into paid_and_due(invoice_no,total,paid,due,discount,discount_total,paid_date,date2)values('" + match + "','" + total + "','" + paid + "','" + due + "','" + discount + "','" + discount_total + "','" + paid_date + "','" + dd + "')", db.sql);
                            SqlCommand cmdd = new SqlCommand("insert into due_collection(reg_no,name,due,test_date)values('" + match + "','" + richTextBox1.Text + "','" + due + "','" + date + "')", db.sql);
                            SqlCommand cmddd = new SqlCommand("insert into referance(name,patient_id,patient_name,date,bill,date2,after_discount) values('" + comboBox1.Text + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + date + "','" + label13.Text + "','" + dd + "','" + label22.Text + "')", db.sql);
                            SqlCommand cmdddd = new SqlCommand("insert into user_cash_collection(user_name,password,reg_no,name,paid,date,date2) values('" + user_name + "','" + password + "','" + richTextBox11.Text + "','" + richTextBox1.Text + "','" + richTextBox5.Text + "','" + date + "','" + dd + "')", db.sql);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                            cmddd.ExecuteNonQuery();
                            cmdddd.ExecuteNonQuery();
                            db.sql.Close();
                            button5.Enabled = true;
                            button4.Enabled = false;
                            Diagnostic_xl_print dbb = new Diagnostic_xl_print(richTextBox11.Text);
                            show_daily_status();
                            dbb.Show();
                            show2();
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("Give a valid Paing Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox4.Text != "")
            {
                richTextBox6.Clear();
                prescription_id.Clear();
                other_id = richTextBox4.Text;
                other_type = "outdoor";
            }
            int c = 0;
            try
            {
                richTextBox1.Text = "";
                richTextBox2.Text = "";
                comboBox2.Text = "";
                richTextBox7.Text = "";
                richTextBox3.Text = "";
                comboBox1.Text = "";
                comboBox4.Text = "";
                comboBox3.Text = "";
                richTextBox10.Text = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from opd where reg_no='" + richTextBox4.Text + "'", db.sql);
                SqlDataReader re = cmd.ExecuteReader();
                while (re.Read())
                {
                    c++;
                    richTextBox1.Text = re[1].ToString();
                    richTextBox2.Text = re[2].ToString();
                    comboBox2.Text = re[3].ToString();
                    richTextBox7.Text = re[4].ToString();
                    richTextBox3.Text = re[5].ToString();
                    comboBox4.Text = re[12].ToString();
                    comboBox1.Text = re[13].ToString();

                }
                if (c == 0)
                {

                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";
                    richTextBox3.Text = "";
                    comboBox1.Text = "";
                }
                db.sql.Close();
            }
            catch
            {
            }
        }

        private void dataGridViewX2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                int c = dataGridViewX2.Rows.Count;
                db.sql.Close();
                db.sql.Open();
                for (n = 0; n <= c - 1; n++)
                {
                    val1 = dataGridViewX2.Rows[n].Cells[3].Value.ToString();
                    val3 = dataGridViewX2.Rows[n].Cells[2].Value.ToString();
                    val4 = dataGridViewX2.Rows[n].Cells[4].Value.ToString();
                    SqlCommand cmd = new SqlCommand("update diagnostic_bill set test_name='" + val3+ "', price='" + val1 + "' where id='" + val4 + "'", db.sql);
                    exe = cmd.ExecuteNonQuery();

                }
                db.sql.Close();

                int sum = 0;
                for (int i = 0; i < dataGridViewX2.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridViewX2.Rows[i].Cells[3].Value);
                }
                label13.Text = sum.ToString();
                count_sum();
                discount();
            }
            catch
            {

            }
        }

        void show_category()
        {

            try
            {
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from test_category", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox3.Items.Add(read[1].ToString());
                }
                db.sql.Close();
            }
            catch
            {

            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            consult();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           // consult();
        }
        void consult()
        {
            try
            {
                int c = 0;
                String name ="";
                String desig = "";
                db.sql.Close();
                db.sql.Open();
                SqlCommand cmd = new SqlCommand("select * from refer_doctor where name='" + comboBox4.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                // comboBox1.Items.Clear();
                while (read.Read())
                {
                     name = read[1].ToString();
                     desig = read[2].ToString();

                   
                    // refere_doctor();
                }
                if (c > 0)
                {
                    consultant = name + "," + desig;
                }
                else
                {
                    consultant = comboBox4.Text;
                }

                db.sql.Close();
            }

            catch
            {


            }
        }

        private void richTextBox15_TextChanged(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("select * from card_details where reg_no='" + richTextBox15.Text + "'", db.sql);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    loop++;

                    richTextBox1.Text = read[3].ToString();
                    richTextBox2.Text = read[5].ToString();
                    comboBox2.Text = read[3].ToString();
                    richTextBox7.Text = read[4].ToString();
                    richTextBox3.Text = read[6].ToString();
                    
                    DateTime convert = DateTime.ParseExact(read[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime convert2 = DateTime.ParseExact(read[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    


                    string todays = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime today = DateTime.ParseExact(todays, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var totaldays = (convert2 - today).TotalDays;
                    if (totaldays < 0)
                    {
                        label49.Visible = true;
                        label50.Visible = false;
                    }
                    else
                    {
                        label50.Visible = true;
                        label49.Visible = false;
                    }
                    if (read[11].ToString() == "F")
                    {
                        label51.Visible = true;
                        label52.Visible = false;
                    }
                    else
                    {
                        label51.Visible = false;
                        label52.Visible = true;
                    }

                    
                }
                if (loop == 0)
                {
                    
                    /*richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    comboBox2.Text = "";
                    richTextBox7.Text = "";
                    richTextBox3.Text = "";
                    comboBox1.Text = "";*/
                    label50.Visible = false;
                    label49.Visible = false;
                    label51.Visible = false;
                    label52.Visible = false;
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

        private void button17_Click(object sender, EventArgs e)
        {

            try
            {

                diagonostic_Patient_History_print dbb = new diagonostic_Patient_History_print();

                dbb.Show();
            }
            catch
            {

            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            show_doctor sd = new show_doctor();
            sd.ShowDialog();
            string val = sd.val1;
            if (val == "1")
            {
                refere_doctor();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.ToUpper();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            refer_doctor refer = new refer_doctor();
            refer.ShowDialog();
            string val = refer.val1;
            if (val == "1")
            {
                refere_doctor2();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Print_Diagnostic_Due_List pdl = new Print_Diagnostic_Due_List();
            pdl.Show();
        }

        private void richTextBox16_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox16.Text == "")
                {
                    label53.Text = "0";
                    label15.Text = "0";
                    richTextBox5.Text = label13.Text;
                    richTextBox9.Text = "0";

                }
                else
                {
                    // label53.Text = richTextBox16.Text;
                    double total = Convert.ToDouble(label13.Text);
                    double discount = Convert.ToDouble(richTextBox16.Text);
                    double persent = (discount * 100) / total;
                    richTextBox9.Text = persent.ToString();

                }
            }
            catch
            { 
            
            }
        }
    }
}
