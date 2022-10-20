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

namespace PR4
{
    public partial class Ticket2 : Form
    {
        DataSet data;
        SqlDataAdapter For;
        SqlCommandBuilder FF;
        //Подключение к SQL Servery
        string connectionString = @"Data Source=MSI\SQLEXPRESS; Initial Catalog=Deka; Integrated Security=True";
        string sql = "SELECT * FROM ticket";
        public Ticket2()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);

                data = new DataSet();
                For.Fill(data);
                dataGridView1.DataSource = data.Tables[0];
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //Обновление таблицы
        void openchild(Panel pen, Form emptyF)
        {
            emptyF.TopLevel = false;
            emptyF.FormBorderStyle = FormBorderStyle.None;
            emptyF.Dock = DockStyle.Fill;
            pen.Controls.Add(emptyF);
            emptyF.BringToFront();
            emptyF.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Ticket2());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);
                FF = new SqlCommandBuilder(For);
                For.InsertCommand = new SqlCommand("ADD_ticket", connection);
                For.InsertCommand.CommandType = CommandType.StoredProcedure;
                For.InsertCommand.Parameters.Add(new SqlParameter("@typa_ticket_id", SqlDbType.Int, 00, "typa_ticket_id"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Money, 0, "Price"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@Date_purchase", SqlDbType.VarChar, 50, "Date_purchase"));

                For.Update(data);
            }
        }
        // кнопка добавления
        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row = data.Tables[0].NewRow();
            data.Tables[0].Rows.Add(row);
        }
        //Удаление выделеной строки
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void Ticket2_Load(object sender, EventArgs e)
        {

        }
    }
}
