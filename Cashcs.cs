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
    public partial class Cashcs : Form
    {
        DataSet data;
        SqlDataAdapter For;
        SqlCommandBuilder FF;
        //Подключение к SQL Servery
        string connectionString = @"Data Source=MSI\SQLEXPRESS; Initial Catalog=Deka; Integrated Security=True";
        string sql = "SELECT * FROM Cash";
        public Cashcs()
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

        private void Cashcs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
            openchild(panel1, new Buyers());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        //Сохранение изменений
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);
                FF = new SqlCommandBuilder(For);
                For.InsertCommand = new SqlCommand("ADD_Cash", connection);
                For.InsertCommand.CommandType = CommandType.StoredProcedure;
                For.InsertCommand.Parameters.Add(new SqlParameter("@id_worker", SqlDbType.Int, 0, "id_worker"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@id_Schedule", SqlDbType.Int, 0, "id_Schedule"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@id_buyer", SqlDbType.Int, 0, "id_buyer"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@date_purchase", SqlDbType.VarChar, 10, "date_purchase"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@id_ticket", SqlDbType.Int, 0, "id_ticket"));

                For.Update(data);
            }
        }
        //Удаление выделеной строки
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
        // кнопка добавления
        private void button1_Click_1(object sender, EventArgs e)
        {
            DataRow row = data.Tables[0].NewRow();
            data.Tables[0].Rows.Add(row);
        }
    }
}
