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
    public partial class Home : Form
    {
        String cs = "", q = "";
        MySqlConnection c1;
        //MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable t;

        String from = "", to = "";
        int index = 0;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            cs = "server=localhost;database=bussystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
           // MessageBox.Show("conected");
            c1.Open();
            try
            {
                q = "select * from bus where depature_from='" + from + "' and travelling_to='" + to + "'";
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();

                da.Fill(t);
                dataGridView1.DataSource = t;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            from = comboBox1.Text;
            index = comboBox1.SelectedIndex;

            if (index == 0)        //ambjaogai
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Beed");
                comboBox2.Items.Add("latur");
                comboBox2.Items.Add("parali");
                
            }
            else if (index == 1)        //beed
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ambajogai");
                comboBox2.Items.Add("latur");
                comboBox2.Items.Add("parali");

            }
            else if (index == 2)        //latur
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ambajogai");
                comboBox2.Items.Add("beed");
                comboBox2.Items.Add("parali");

            }
            else if (index == 3)        //parali
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Beed");
                comboBox2.Items.Add("latur");
                comboBox2.Items.Add("ambajogai");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("please select city");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("please select city");
                comboBox2.Focus();
            }
            else
            {
                c1.Open();
                try
                {
                    from = comboBox1.Text;
                    to = comboBox2.Text;
                    q = "select *from bus where depature_from='" + from + "' and travelling_to='" + to + "'";
                    da = new MySqlDataAdapter(q, c1);
                    t = new DataTable();

                    da.Fill(t);
                    dataGridView1.DataSource = t;
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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("clicked button");
            Login obj = new Login();
            obj.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register obj = new Register();
            obj.Show();
        }
    }
}











