using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;

namespace WF
{
    public partial class ItemView : Form
    {
        private Dictionary<string,Item> itemDictionary;

        public ItemView()
        {
            InitializeComponent();
        }

        private void ItemView_Load(object sender, EventArgs e)
        {
            itemDictionary = new Dictionary<string, Item>();
        }

        private void PopulateRows(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                PopulateRow(item);
            }
        }

        private void PopulateRow(Item item)
        {
            var row = new object[4] { item.Code, item.Description, item.CurrentCount, item.OnOrder };
            ItemDataGridView.Rows.Add(row);
        }

        private void CreateColumns()
        {
            ItemDataGridView.ColumnCount = 4;

            CreateColumn("Item Code",0);
            CreateColumn("Description", 1);
            CreateColumn("Current Count", 2, readOnly:false);
            CreateColumn("On Order", 3);
        }

        private void CreateColumn(string name,int index, bool readOnly = true)
        {
            DataGridViewColumn column = ItemDataGridView.Columns[index];
            column.Name = name;
            column.ReadOnly = readOnly;
        }

        private void ItemDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var itemCode = (string)ItemDataGridView.Rows[e.RowIndex].Cells[0].Value;
            var itemCurrentCount = int.Parse((string)ItemDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            ItemRepository.Instance.Update(itemCode,itemCurrentCount);
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Stream stream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog1.OpenFile()) != null)
                {
                    ItemRepository.Instance.Save(stream);
                    
                    stream.Close();
                }
            }
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openFileDialog1.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            CreateColumns();
                            PopulateRows(ItemRepository.Instance.Retrieve(stream));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
