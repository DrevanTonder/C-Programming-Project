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
        // This dictionary will store the items retrieved by the ItemRepository
        private Dictionary<string,Item> itemDictionary;

        public ItemView()
        {
            InitializeComponent();
        }

        private void ItemView_Load(object sender, EventArgs e)
        {
            itemDictionary = new Dictionary<string, Item>();

            //Add our own custom error handler so that the user can actually understand what went wrong
            ItemDataGridView.DataError += ItemDataGridView_DataError;
        }

        private void PopulateRows(IEnumerable<Item> items)
        {
            // add a row for all the items
            foreach (var item in items)
            {
                PopulateRow(item);
            }
        }

        private void PopulateRow(Item item)
        {
            // create a temporary object
            var row = new object[4] { item.Code, item.Description, item.CurrentCount, item.OnOrder };
            // add the row
            ItemDataGridView.Rows.Add(row);
        }

        private void CreateColumns()
        {
            // set the number of columns
            ItemDataGridView.ColumnCount = 4;

            // create all the columns needed
            CreateColumn("Item Code",ColumnType.Code, typeof(string));
            CreateColumn("Description", ColumnType.Description, typeof(string));
            // this is the only column that is editable
            CreateColumn("Current Count", ColumnType.CurrentCount, typeof(int), readOnly:false); 
            CreateColumn("On Order", ColumnType.OnOrder, typeof(bool));
        }

        private void CreateColumn(string name,ColumnType columnType,Type type, bool readOnly = true)
        {
            DataGridViewColumn column = ItemDataGridView.Columns[(int)columnType];
            column.Name = name;
            column.ValueType = type;
            column.ReadOnly = readOnly;
        }

        private void ItemDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var itemCode = (string)ItemDataGridView.Rows[e.RowIndex].Cells[0].Value;
            var itemCurrentCount = (int)ItemDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            ItemRepository.Instance.Update(itemCode,itemCurrentCount);
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Stream stream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "csv files (*.csv)|*.csv",
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
                Filter = "csv files (*.csv)|*.csv",
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

        private void ItemDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.ColumnIndex == (int)ColumnType.CurrentCount && e.Context.HasFlag(DataGridViewDataErrorContexts.Commit))
            {
                MessageBox.Show($"{ColumnType.CurrentCount.ToString()} is an integer only column.");
            }       
        }
    }
}
