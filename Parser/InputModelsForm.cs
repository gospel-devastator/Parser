using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parser
{
    public partial class InputModelsForm : Form
    {
        public InputModelsForm()
        {
            InitializeComponent();
        }

        private SQLiteConnection conn;
        private SQLiteDataAdapter adapter;
        private DataTable dTable;

        private void button1_Click(object sender, EventArgs e)
        {
            SQLitePCL.Batteries.Init();
            //Inserting data to database
            string filepathfordb = "parsing.db";
            using (var conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();
                string queryins = "INSERT INTO models (name, brand) VALUES (@name, @brand);";
                using (var comm = new SQLiteCommand(queryins, conn))
                {
                    comm.Parameters.AddWithValue("@name", textBox1.Text);
                    comm.Parameters.AddWithValue("@brand", textBox2.Text);
                    try
                    {
                        int rowsAffected = comm.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} запись(и) успешно добавлена.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при добавлении записи: {ex.Message}");
                    }

                }
                string queryselect = "SELECT name, brand FROM models;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    DataTable dTable = new DataTable();
                    adapter.Fill(dTable);
                    dataGridView1.DataSource = dTable;
                }

                conn.Close();
            }
            //updating data in Datagridview

            textBox1.Clear();
            textBox2.Clear();
        }

        private void InputModelsForm_Load(object sender, EventArgs e)
        {
            SQLitePCL.Batteries.Init();
            //Inserting data to database
            string filepathfordb = "parsing.db";
            using (var conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();

                string queryselect = "SELECT * FROM models;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    DataTable dTable = new DataTable();
                    adapter.Fill(dTable);
                    dataGridView1.DataSource = dTable;
                }

                conn.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathfordb = "parsing.db";
            var conn = new SQLiteConnection($"Data Source={filepathfordb};");
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand("UPDATE site SET name = @name, brand = @brand WHERE id = @id", conn))
            {

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    command.Parameters.AddWithValue("@id", dTable.Rows[i]["id"]);
                    command.Parameters.AddWithValue("@name", dTable.Rows[i]["name"]);
                    command.Parameters.AddWithValue("@brand", dTable.Rows[i]["brand"]);
                }
            }
        }
    }
}
