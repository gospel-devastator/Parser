using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;



namespace Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void yourModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputModelsForm imf = new InputModelsForm();
            imf.ShowDialog();
        }

        private void yourCompetitorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputCompetitorsForm icf = new InputCompetitorsForm();
            icf.ShowDialog();
        }

        private void yourSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, but this block is under development.\r\n");
            //InputYourSiteForm iysf = new InputYourSiteForm();
            //iysf.ShowDialog();
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, but this block is under development.\r\n");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
                  
            
            // Create a Database file
            string filepathfordb = "parsing.db";
            try
            {
                if (!File.Exists(filepathfordb))
                {
                    // Create a Database file
                    File.Create(filepathfordb).Close(); // Closing stream
                }
                using (var connection = new SqliteConnection($"Data Source={filepathfordb};"))
                {
                    connection.Open();

                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS models (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL, 
                    brand TEXT NOT NULL
                );";

                    using (var command = new SqliteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    createTableQuery = @"
                CREATE TABLE IF NOT EXISTS site (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL, 
                    url TEXT NOT NULL, 
                    price_block TEXT NOT NULL, 
                    price_block_type TEXT NOT NULL, 
                    model_block TEXT NOT NULL, 
                    model_block_type TEXT NOT NULL
                );";

                    using (var command = new SqliteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("База создана");
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            //Insert all data from db to datagridView
            SQLitePCL.Batteries.Init();
            using (var conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();
                //Inserting models
                string queryselect = "SELECT name FROM models;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    DataTable dTable = new DataTable();
                    adapter.Fill(dTable);
                    dataGridView1.DataSource = dTable;
                }
                //Inserting sites
                queryselect = "SELECT name FROM site;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    DataTable dTable = new DataTable();
                    adapter.Fill(dTable);
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        DataRow dr = dTable.Rows[i];
                        dataGridView1.Columns.Add($"Column{i}", (string)dr["name"]);
                    }
                }

                conn.Close();

            }
            dataGridView1.Dock = DockStyle.Fill;

        }

        private async void updateDataInTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Extracting sites and models
            //tables for storing data from the database
            DataTable modelsTable = new DataTable();
            DataTable siteTable = new DataTable();

            SQLitePCL.Batteries.Init();
            string filepathfordb = "parsing.db";
            using (var conn = new SQLiteConnection($"Data Source={filepathfordb};"))
            {
                conn.Open();
                //Inserting models
                string queryselect = "SELECT name FROM models;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    adapter.Fill(modelsTable);
                }
                //Inserting sites
                queryselect = "SELECT name, url, price_block, price_block_type, model_block, model_block_type FROM site;";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(queryselect, conn))
                {
                    adapter.Fill(siteTable);
                }

                conn.Close();

            }


            //In the first cycle we go around the sites
            for (int i = 0; i < siteTable.Rows.Count; i++)
            {
                //var site's url
                string siteurl = siteTable.Rows[i][1].ToString();
                //extracting arrays with prices and models on the website
                //name, url, price_block, price_block_type, model_block, model_block_type
                var modelnameslist = new List<string>();
                var priceslist = new List<string>();
                using (HttpClient client = new HttpClient())
                {
                    var responce = await client.GetStringAsync(siteurl);
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(responce);
                    var nameNodes = htmlDoc.DocumentNode.SelectNodes($"//{siteTable.Rows[i][5].ToString()}[contains(@class, '{siteTable.Rows[i][4].ToString()}')]");
                    var priceNodes = htmlDoc.DocumentNode.SelectNodes($"//{siteTable.Rows[i][3].ToString()}[contains(@class, '{siteTable.Rows[i][2].ToString()}')]");

                    if (nameNodes != null)
                    {
                        foreach (var node in nameNodes)
                        {
                            modelnameslist.Add(node.InnerText.Trim());
                        }
                    }

                    if (priceNodes != null)
                    {
                        foreach (var node in priceNodes)
                        {
                            priceslist.Add(node.InnerText.Trim());
                        }
                    }

                }
                //We extract prices and names from the web page into two separate arrays. We look for our product in the array of products, and if there is a match, we extract the cell index.
                //We access the cell with the same index and retrieve the price. We record the price in the datagridview

                for (int y = 0; y < modelsTable.Rows.Count; y++)
                {
                    var modelname = modelsTable.Rows[y][0].ToString();
                    //int index_model = modelnameslist.IndexOf(modelname, StringComparer.OrdinalIgnoreCase); //get the model index
                    int index_model = modelnameslist.FindIndex(m => string.Equals(m, modelname, StringComparison.OrdinalIgnoreCase));

                    if (index_model != -1) //checking value
                    {
                        dataGridView1.Rows[y].Cells[i + 1].Value = priceslist[index_model];
                    }
                    else
                    {
                        dataGridView1.Rows[y].Cells[i + 1].Value = "Нет на сайте";
                    }


                }

            }
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, but this block is under development.\r\n");
        }


    }
}
