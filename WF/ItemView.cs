using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            CreateColumns();
            PopulateRows(ItemRepository.Instance.Retrieve());
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

            itemDictionary[item.Code] = item;
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

            UpdateCurrentCount(itemCode,itemCurrentCount);
        }

        private void UpdateCurrentCount(string itemCode,int itemCurrentCount)
        {           
            var item = itemDictionary[itemCode];
            
            item.CurrentCount = itemCurrentCount;
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemRepository.Instance.Save(itemDictionary.Values);
        }
    }
}
