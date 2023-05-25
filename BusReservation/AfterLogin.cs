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
    public partial class AfterLogin : Form
    {
        String cs = "", q = "";
        MySqlConnection c1;
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable t;
        long uid = 0;
        public AfterLogin()
        {
            InitializeComponent();
        }
        public AfterLogin(long uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void AfterLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Booking obj = new Booking();
            obj.Show();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cancelling obj = new Cancelling();
            obj.Show();
        }
    }
}
