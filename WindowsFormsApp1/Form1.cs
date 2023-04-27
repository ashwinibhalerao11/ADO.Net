using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ClearForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtSalary.Clear();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "insert into Employee1 values(@name,@sal)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToDecimal(txtSalary.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record inserted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee1 where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtSalary.Text = dr["salary"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                    ClearForm();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                //step 1
                string qry = "update Employee1 set name=@name, salary=@sal where id=@id";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToDecimal(txtSalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record updated");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "delete from Employee1 where id=@id";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record deleted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void BtnAllEmp_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee1";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    DataTable table = new DataTable();
                    table.Load(dr);// Load() convert object in to table format
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}

