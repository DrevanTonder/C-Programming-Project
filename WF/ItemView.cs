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

        private ItemRepository itemRepository;

        public ItemView()
        {
            InitializeComponent();
        }

        private void ItemView_Load(object sender, EventArgs e)
        {
            itemRepository = new ItemRepository();

            ItemDataGridView.DataError += ItemDataGridView_DataError;

            CreateColumns();
        }

        /// <summary>
        /// Populates the rows of the DataGridView
        /// </summary>
        /// <param name="items">An IEnumerable of the items to populate the DataGridView with</param>
        private void PopulateRows(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                PopulateRow(item);
            }
        }

        /// <summary>
        /// Adds a row to the DataGridView
        /// </summary>
        /// <param name="item">The Item to populate the DataGridView with</param>
        private void PopulateRow(Item item)
        {
            var row = new object[4] { item.Code, item.Description, item.CurrentCount, item.OnOrder };
            ItemDataGridView.Rows.Add(row);
        }

        /// <summary>
        /// Add the Columns to the DataGridView
        /// </summary>
        private void CreateColumns()
        {
            ItemDataGridView.ColumnCount = 4;

            CreateColumn("Item Code",ColumnType.Code, typeof(string));
            CreateColumn("Description", ColumnType.Description, typeof(string));
            CreateColumn("Current Count", ColumnType.CurrentCount, typeof(int), readOnly:false); 
            CreateColumn("On Order", ColumnType.OnOrder, typeof(bool));
        }

        /// <summary>
        /// Creates a Column
        /// </summary>
        /// <param name="name">Name of the Column</param>
        /// <param name="columnType">The column type of the column, used to set the position</param>
        /// <param name="type">The type of the column</param>
        /// <param name="readOnly">Is the column editable</param>
        private void CreateColumn(string name,ColumnType columnType,Type type, bool readOnly = true)
        {
            DataGridViewColumn column = ItemDataGridView.Columns[(int)columnType];
            column.Name = name;
            column.ValueType = type;
            column.ReadOnly = readOnly;
        }

        /// <summary>
        /// This function is run when the user ends editing a field.
        /// It then updates the item edited.
        /// </summary>
        private void ItemDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var itemCode = (string)ItemDataGridView.Rows[e.RowIndex].Cells[0].Value;
            var itemCurrentCount = (int)ItemDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            itemRepository.Update(itemCode,itemCurrentCount);
        }

        /// <summary>
        /// Save the file when export menu item is clicked.
        /// </summary>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Open a Save File dialog
        /// Save the file
        /// </summary>
        private void SaveFile()
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
                    itemRepository.Save(stream);

                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Open a file when import menu item is clicked.
        /// </summary>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Open a Open File Dialog
        /// Display the Contents
        /// </summary>
        private void OpenFile()
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
                            PopulateRows(itemRepository.Retrieve(stream));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Custom Errors
        /// </summary>
        private void ItemDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.ColumnIndex == (int)ColumnType.CurrentCount && e.Context.HasFlag(DataGridViewDataErrorContexts.Commit))
            {
                MessageBox.Show($"{ColumnType.CurrentCount.ToString()} is an integer only column.");
            }       
        }
    }
}
