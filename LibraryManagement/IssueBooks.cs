﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(txtEnrollement.Text == "")
            {
                txtName.Clear();
                txtDep.Clear();
                txtSem.Clear();
                txtEmail.Clear();
            }
        }


        int count;
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtEnrollement.Text != "")
            {
                String eid = txtEnrollement.Text;
                SqlConnection con = new SqlConnection();
                //database
                con.ConnectionString = "data source =DESKTOP-E4UJ6RE\\SQLEXPRESS; database= library; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where enroll = '" + eid + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);


                //---------------------------------------------------------------
                //Code to Count how many book has been issued on this enrollement number
                cmd.CommandText = "select count(std_enroll) from IRBook where std_enroll = '" + eid + "' and book_return_date is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
                DataSet DS1 = new DataSet();
                DA.Fill(DS1);

                count = int.Parse(DS1.Tables[0].Rows[0][0].ToString());


                //---------------------------------------------------------------


                if (DS.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = DS.Tables[0].Rows[0][1].ToString();
                    txtDep.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtSem.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = DS.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = DS.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    txtName.Clear();
                    txtDep.Clear();
                    txtSem.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid Enrollement No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            //database
            con.ConnectionString = "data source =DESKTOP-E4UJ6RE\\SQLEXPRESS; database= library; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("select bName from NewBook", con);
            SqlDataReader Sdr = cmd.ExecuteReader();

            while(Sdr.Read())
            {
                for(int i=0; i<Sdr.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
            con.Close();
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "")
            {
                if(comboBoxBooks.SelectedIndex != -1 && count <=2)
                {
                    String enroll = txtEnrollement.Text;
                    String sname = txtName.Text;
                    String sdep = txtDep.Text;
                    String sem = txtSem.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookname = comboBoxBooks.Text;
                    String bookIssueDate = dateTimePicker.Text;

                    String eid = txtEnrollement.Text;
                    SqlConnection con = new SqlConnection();
                    //database
                    con.ConnectionString = "data source =DESKTOP-E4UJ6RE\\SQLEXPRESS; database= library; integrated security = True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = cmd.CommandText = "insert into IRBook (std_enroll, std_name, std_dep, std_sem, std_contact, std_email, book_name, book_issue_date) values ('"  + enroll + "','" + sname + "','" + sdep + "','" + sem + "'," + contact + ",'" + email + "','" + bookname + "','" + bookIssueDate + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Select Book. OR Maximum number of Book Has been ISSUE", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Enrollement No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollement.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirmation",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
            
        }
    }
}
