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
    public partial class Register : Form
    {
        String cs = "", q = "";
        MySqlConnection c1;
        MySqlCommand cmd;

        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        void ClearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void Register_Load(object sender, EventArgs e)
        {
            cs = "server=localhost;database=bussystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c1.Open();
            try
            {
                

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Enter your Name");
                    textBox1.Focus();
                }
                else if(textBox2.Text=="")
                {
                    MessageBox.Show("enter contact no");
                    textBox2.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Cretae passwowrd");
                    textBox3.Focus();
                }
                else if (textBox3.Text == textBox4.Text)
                {
                    long contact = Convert.ToInt64(textBox2.Text);
                    String name = textBox1.Text;
                    String pass = textBox3.Text;
                    q = "insert into users values(@uid,@name,@pass)";
                    cmd = new MySqlCommand(q, c1);

                    cmd.Parameters.AddWithValue("@uid", contact);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    int r = cmd.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("User created");
                        ClearAll();
                    }
                    else
                        MessageBox.Show("Registration failed");
                }
                else
                {
                    MessageBox.Show("Password Missmatch");
                    textBox4.Text = "";
                    textBox4.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Contact Entered Is Already registered Try Different Contact");
                ClearAll();
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }
    }
}
