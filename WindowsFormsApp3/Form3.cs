using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 0;
            while (k == 0)
            {
                string connString = "Data Source = L206-3; Initial Catalog = USERS; Integrated Security = true;";
                bool s = IdentityLogin.CheckIdentityLogin(textBox1.Text, connString);
                if (s == true)
                {
                    Registration.CreateNewUser(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, connString);
                    k++;
                }
                else
                {
                    MessageBox.Show("Данный логин уже существует");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
    public class Registration
    {
        public static void CreateNewUser(string login, string password, string fname, string lname, string phone, string email, string constr)
        {
            string command = String.Format(@"INSERT INTO USERS VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", login, password, fname, lname, phone, email);
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = command;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("МАЛАДЕЦ");
            }
        }
    }
    public class IdentityLogin
    {
        public static bool CheckIdentityLogin(string log, string constr)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM USERS WHERE login = " + log + ";", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
