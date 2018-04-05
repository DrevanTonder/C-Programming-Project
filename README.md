# C# Programming Project
## Description
This is a simple graphical user interface that will read a text file and display the
contents on screen. The program will then allow alterations to be made to the “Current Count” field
only, and when the required adjustments have been made, the text file is adjusted (or a new text file
is written) with the adjusted amounts being represented.  

## Links

1. [Windows Forms](#wf)
2. [Business Logic](#bl)

# WF

<table>
<tbody>
<tr>
<td><a href="#resources">Resources</a></td>
</tr>
</tbody>
</table>

### WF.ItemView.CreateColumn(name, columnType, type, readOnly)

Creates a Column

| Name | Description |
| ---- | ----------- |
| name | *System.String*<br>Name of the Column |
| columnType | *WF.ColumnType*<br>The column type of the column, used to set the position |
| type | *System.Type*<br>The type of the column |
| readOnly | *System.Boolean*<br>Is the column editable |

### WF.ItemView.CreateColumns

Add the Columns to the DataGridView

### WF.ItemView.ItemDataGridView_CellEndEdit(System.Object,System.Windows.Forms.DataGridViewCellEventArgs)

This function is run when the user ends editing a field. It then updates the item edited.

### WF.ItemView.ItemDataGridView_DataError(System.Object,System.Windows.Forms.DataGridViewDataErrorEventArgs)

Custom Errors

### WF.ItemView.OpenFile

Open a Open File Dialog and then display the Contents

### WF.ItemView.OpenToolStripMenuItem_Click(System.Object,System.EventArgs)

Open a file when import menu item is clicked.

### WF.ItemView.PopulateRow(item)

Adds a row to the DataGridView

| Name | Description |
| ---- | ----------- |
| item | *BL.Item*<br>The Item to populate the DataGridView with |

### WF.ItemView.PopulateRows(items)

Populates the rows of the DataGridView

| Name | Description |
| ---- | ----------- |
| items | *System.Collections.Generic.IEnumerable{BL.Item}*<br>An IEnumerable of the items to populate the DataGridView with |

### WF.ItemView.SaveFile

Open a Save File dialog and then save the file

### WF.ItemView.SaveToolStripMenuItem_Click(System.Object,System.EventArgs)

Save the file when export menu item is clicked.

### WF.Program.Main

The main entry point for the application.

# BL

<table>
<tbody>
<tr>
<td><a href="#item">Item</a></td>
<td><a href="#itemrepository">ItemRepository</a></td>
</tr>
<tr>
<td><a href="#itemmap">ItemMap</a></td>
<td><a href="#mybooleanconverter">MyBooleanConverter</a></td>
</tr>
</tbody>
</table>


## Item

Item Class

### Code

The Item's Code

### CurrentCount

The Current Count of the amount of this Item in stock

### Description

A Description of the Item

### OnOrder

Whether the Item is On Order or not


## ItemRepository

This class contains the methods to read and write items too a csv file

### Constructor

ItemRepository Constructor


## ItemMap

This class tells CsvHelper how to convert the file rows into items and vice versa


## MyBooleanConverter

This class just converts Yes/No values into True/False

### BL.ItemRepository.Retrieve(stream)

Retrieves a list of items from the supplied stream

| Name | Description |
| ---- | ----------- |
| stream | *System.IO.Stream*<br>The stream to read to |

#### Returns

A IEnumerable of the items in the stream

*System.ArgumentException:* Thrown when the stream is null

*System.ArgumentException:* Thrown when the stream has incomplete items

### BL.ItemRepository.Save(stream)

Saves a list of items from the supplied stream

| Name | Description |
| ---- | ----------- |
| stream | *System.IO.Stream*<br>The stream to save to |

*System.ArgumentException:* Thrown when the stream is null

### BL.ItemRepository.Update(itemCode, currentCount)

Updates an Item's Current Count.

| Name | Description |
| ---- | ----------- |
| itemCode | *System.String*<br>The Item's code used to find the item |
| currentCount | *System.Int32*<br>The Item's new CurrentCount |
