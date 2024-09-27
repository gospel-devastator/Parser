using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Parser
{

    public partial class InputCompetitorsForm : Form
    {
        public InputCompetitorsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLitePCL.Batteries.Init();
            //Inserting data to database
            string filepathfordb = "parsing.db";
            using (var conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();
                string queryins = "INSERT INTO site (name, url, price_block, price_block_type, model_block, model_block_type) VALUES (@name, @url, @price_block, @price_block_type, @model_block, @model_block_type);";
                using (var comm = new SQLiteCommand(queryins, conn))
                {
                    comm.Parameters.AddWithValue("@name", textBox1.Text);
                    comm.Parameters.AddWithValue("@url", textBox2.Text);
                    comm.Parameters.AddWithValue("@price_block", textBox3.Text);
                    comm.Parameters.AddWithValue("@price_block_type", comboBox1.Text);
                    comm.Parameters.AddWithValue("@model_block", textBox4.Text);
                    comm.Parameters.AddWithValue("@model_block_type", comboBox2.Text);
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
                string queryselect = "SELECT name, url, price_block, price_block_type, model_block, model_block_type FROM site;";
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
            textBox3.Clear();
            textBox4.Clear();


        }

        private SQLiteConnection conn;
        private SQLiteDataAdapter adapter;
        private DataTable dTable;


        private void InputCompetitorsForm_Load(object sender, EventArgs e)
        {
            string filepathfordb = "parsing.db";
            using (conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();
                string queryselect = "SELECT * FROM site;"; //id, name, url, price_block, price_block_type, model_block, model_block_type
                using (adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    dTable = new DataTable();
                    adapter.Fill(dTable);
                    dataGridView1.DataSource = dTable;
                }
                conn.Close();

            }
            dataGridView1.Dock = DockStyle.Bottom;


        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string filepathfordb = "parsing.db";
            var conn = new SQLiteConnection($"Data Source={filepathfordb};");
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand("UPDATE site SET name = @name, url = @url, price_block = @price_block, price_block_type = @price_block_type, model_block = @model_block, model_block_type = @model_block_type WHERE id = @id", conn))
            {

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    command.Parameters.AddWithValue("@id", dTable.Rows[i]["id"]);
                    command.Parameters.AddWithValue("@name", dTable.Rows[i]["name"]);
                    command.Parameters.AddWithValue("@url", dTable.Rows[i]["url"]);
                    command.Parameters.AddWithValue("@price_block", dTable.Rows[i]["price_block"]);
                    command.Parameters.AddWithValue("@price_block_type", dTable.Rows[i]["price_block_type"]);
                    command.Parameters.AddWithValue("@model_block", dTable.Rows[i]["model_block"]);
                    command.Parameters.AddWithValue("@model_block_type", dTable.Rows[i]["model_block_type"]);

                }
            }
        }


    }
}
