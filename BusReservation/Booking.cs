using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BusReservation
{
    public partial class Booking : Form
    {
        String date="",cs = "", q = "";
        MySqlConnection c1;
        MySqlCommand cmd;
        MySqlDataReader d;
        int total = 0,fare=0,seats=0;
        int day = 0, month = 0, year = 0;
        String zeroBeforeDay = "",zeroBeforeMonth="";
        public Booking()
        {
            InitializeComponent();
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            cs = "server=localhost;database=bussystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
            //MessageBox.MessageBox.Show("connected dayabase success");

            LoadId();
        }
        void LoadId()
        {
            c1.Open();
            try
            {
                q = "select max(ticket_no) from passengers";
                cmd = new MySqlCommand(q,c1);
                int ticket_no = Convert.ToInt32(cmd.ExecuteScalar());

                textBox2.Text = ( ticket_no + 1).ToString();
              }
            catch(Exception ex)
            {                
                textBox2.Text = "1";
            }
            finally
            {
                if(c1.State==ConnectionState.Open)
                    c1.Close();
            }

        }
    
    

        private void button2_Click(object sender, EventArgs e)
        {
            c1.Open();
            try
            {
                
                day = dateTimePicker1.Value.Day;
                month = dateTimePicker1.Value.Month;
                year = dateTimePicker1.Value.Year;

                if (day < 10)
                    zeroBeforeDay = "0";
                if (month < 10)
                    zeroBeforeMonth = "0";

                date ="" + year+ zeroBeforeMonth+ month + zeroBeforeDay + day  ;
                String time=comboBox3.SelectedItem.ToString();
                int busno = Convert.ToInt32(textBox4.Text);
                int ticketno = Convert.ToInt32(textBox2.Text);
                fare = Convert.ToInt32(textBox6.Text);
                seats = Convert.ToInt32(textBox5.Text);
                
                long contact = Convert.ToInt64(textBox3.Text);
                String name = textBox1.Text;
                String from = comboBox1.SelectedItem.ToString();
                String to = comboBox2.SelectedItem.ToString();

                               
                q = "insert into passengers values(" + busno + "," + ticketno + ",'" + name + "'," + contact + ",'" + from + "','" + to + "',@travel_date," + seats + "," + fare +",'"+time+"',"+total+ ")";
                cmd = new MySqlCommand(q,c1);
                
                cmd.Parameters.AddWithValue("@travel_date", date);
                
                int r=cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    MessageBox.Show("Booked");
                    
                }
                else
                {
                    MessageBox.Show("Error");
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                {
                    c1.Close();
                    ClearAll();
                    LoadId();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            c1.Open();
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Select traveeling from");
                    comboBox1.Focus();
                }
                else
                {
                    comboBox2.Items.Clear();
                    q = "select travelling_to from bus where depature_from='" + comboBox1.Text + "'";
                    cmd = new MySqlCommand(q, c1);
                    d = cmd.ExecuteReader();

                    while (d.Read())
                    {
                        comboBox2.Text = "";
                        textBox4.Text = "";
                        comboBox3.Text = "";
                        comboBox3.Items.Clear();
                        textBox6.Text = "";
                        textBox5.Text = "";
                        comboBox2.Items.Add(d["travelling_to"].ToString());
                       // comboBox3.Items.Add(d["time"].ToString());
                    }
                  
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            c1.Open();
            try
            {
               if(comboBox1.Text=="")
               {
                   MessageBox.Show("Please select city");
                   comboBox1.Focus();
               }
               else if (comboBox2.Text == "")
               {
                   MessageBox.Show("select city where u want to go");
                   comboBox2.Focus();
               }
               else
               {
                   q = "select fare from bus where depature_from='" + comboBox1.Text + "' and travelling_to='" + comboBox2.Text + "'";
                   cmd = new MySqlCommand(q, c1);
                   int fare = Convert.ToInt32(cmd.ExecuteScalar());
                   textBox6.Text = fare.ToString();

                   q = "select bus_no from bus where depature_from='" + comboBox1.Text + "' and travelling_to='" + comboBox2.Text + "'";
                   cmd = new MySqlCommand(q, c1);
                   int bus_no = Convert.ToInt32(cmd.ExecuteScalar());
                   textBox4.Text = bus_no.ToString();

                   q = "select time from bus where depature_from='" + comboBox1.Text + "' and travelling_to='" + comboBox2.Text + "'";
                   cmd = new MySqlCommand(q, c1);
                   d = cmd.ExecuteReader();

                   while (d.Read())
                   {
                      // comboBox3.Text=d["time"].ToString();
                       comboBox3.Items.Add(d["time"].ToString());
                   }
               }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)////Clear Button
        {
            ClearAll();
        }
        void ClearAll()
        {
            textBox1.Text = "";
            
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox7.Text = "";
               // MessageBox.Show("enter seats");
                textBox5.Focus();
            }
            else
            {
                seats = Convert.ToInt32(textBox5.Text);
                fare = Convert.ToInt32(textBox6.Text);
               // MessageBox.Show(""+fare+"\n"+seats);
                total = fare * seats;
                textBox7.Text = total.ToString();
            
            }
        }
    }
}
