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
    public partial class Login : Form
    {
        String cs = "", q = "";
        MySqlCommand cmd;
        MySqlConnection c1;


        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cs = "server=localhost;database=bussystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
            //MessageBox.Show("connected");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("enter contact No");
                textBox1.Focus();
            }
            else if(textBox2.Text=="")
            {
                MessageBox.Show("Enter password");
                textBox2.Focus();
            }
            else
            {
                
                c1.Open();
                try
                {
                    long uid = Convert.ToInt64(textBox1.Text);
                    q = "select pass from users where uid=@uid";
                    cmd = new MySqlCommand(q,c1);
                    cmd.Parameters.AddWithValue("@uid",uid);
                    String pass = cmd.ExecuteScalar().ToString();

                    if (textBox2.Text == pass)
                    {
                        //MessageBox.Show("Logged in");
                        AfterLogin obj = new AfterLogin(uid);
                        obj.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Pass");
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid UID ");
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                finally
                {
                    if (c1.State == ConnectionState.Open)
                        c1.Close();
                }
            }
        }
    }
}
