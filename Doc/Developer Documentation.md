# Developer Documentation

# Table of Contents

1. [Object Model](#object%20model)
2. [CsvHelper](#csvhelper)
3. [Testing](#testing)
4. [Business Logic](#bl)
5. [Windows Forms](#wf)

# Object Model

The object model can be found in the same directory as this file with the filename *ObjectModel.png*

# CsvHelper

CsvHelper is an external NuGet package. Read the Documentation at http://joshclose.github.io/CsvHelper/

# Testing

The unit tests can be found in the BL.Test Project.

| Method Name                    | Description                              |
| ------------------------------ | ---------------------------------------- |
| RetrieveTest                   | Normal Usage                             |
| RetrieveTestStreamNull         | Throw `ArgumentNullException` when `stream` is null |
| RetrieveTestMissingItems       | Make sure that the Retrieve method returns a empty `IEnumerable` when `stream` has no items. |
| RetrieveTestIncompleteItems    | Throw `ArgumentException` when the `stream` has incomplete items. |
| SaveTest                       | Normal Usage                             |
| SaveTestStreamNull             | Throw `ArgumentNullException` when `stream` is null |
| UpdateTest                     | Normal Usage                             |
| UpdateTestItemCodeNull         | Throw `ArgumentNullException` when `itemCode` is null |
| UpdateTestItemCodeDoesNotExist | Throw `ArgumentException` when `itemCode` is not in the `ItemRepository` items dictionary |



# BL

This can be found in the BL Project

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

| Name   | Description                              |
| ------ | ---------------------------------------- |
| stream | *System.IO.Stream*<br>The stream to read to |

#### Returns

A IEnumerable of the items in the stream

*System.ArgumentNullException:* Thrown when the stream is null

*System.ArgumentException:* Thrown when the stream has incomplete items

### BL.ItemRepository.Save(stream)

Saves a list of items from the supplied stream

| Name   | Description                              |
| ------ | ---------------------------------------- |
| stream | *System.IO.Stream*<br>The stream to save to |

*System.ArgumentNullException:* Thrown when the stream is null

### BL.ItemRepository.Update(itemCode, currentCount)

Updates an Item's Current Count.

| Name         | Description                              |
| ------------ | ---------------------------------------- |
| itemCode     | *System.String*<br>The Item's code used to find the item |
| currentCount | *System.Int32*<br>The Item's new CurrentCount |

*System.ArgumentNullException:* Thrown when itemCode is null

*System.ArgumentException:* Thrown when itemCode is not in the itemRepository items dictionary

# WF
This can be found in the WF Project

### WF.ItemView.CreateColumn(name, columnType, type, readOnly)

Creates a Column

| Name       | Description                              |
| ---------- | ---------------------------------------- |
| name       | *System.String*<br>Name of the Column    |
| columnType | *WF.ColumnType*<br>The column type of the column, used to set the position |
| type       | *System.Type*<br>The type of the column  |
| readOnly   | *System.Boolean*<br>Is the column editable |

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

| Name | Description                              |
| ---- | ---------------------------------------- |
| item | *BL.Item*<br>The Item to populate the DataGridView with |

### WF.ItemView.PopulateRows(items)

Populates the rows of the DataGridView

| Name  | Description                              |
| ----- | ---------------------------------------- |
| items | *System.Collections.Generic.IEnumerable{BL.Item}*<br>An IEnumerable of the items to populate the DataGridView with |

### WF.ItemView.SaveFile

Open a Save File dialog and then save the file

### WF.ItemView.SaveToolStripMenuItem_Click(System.Object,System.EventArgs)

Save the file when export menu item is clicked.