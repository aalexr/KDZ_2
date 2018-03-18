using System;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private DataGridView dataGridView1 = new DataGridView();

    // Declare an ArrayList to serve as the data store. 
    private System.Collections.ArrayList customers = new System.Collections.ArrayList();

    // Declare a Customer object to store data for a row being edited.
    private Customer customerInEdit;

    // Declare a variable to store the index of a row being edited. 
    // A value of -1 indicates that there is no row currently in edit. 
    private int rowInEdit = -1;

    // Declare a variable to indicate the commit scope. 
    // Set this value to false to use cell-level commit scope. 
    private bool rowScopeCommit = true;

    public Form1()
    {
        // Initialize the form.
        dataGridView1.Dock = DockStyle.Fill;
        Controls.Add(dataGridView1);
        Load += new EventHandler(Form1_Load);
        Text = "DataGridView virtual-mode demo (row-level commit scope)";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // Enable virtual mode.
        dataGridView1.VirtualMode = true;

        // Connect the virtual-mode events to event handlers. 
        dataGridView1.CellValueNeeded += new DataGridViewCellValueEventHandler(DataGridView1_CellValueNeeded);
        dataGridView1.CellValuePushed += new DataGridViewCellValueEventHandler(DataGridView1_CellValuePushed);
        dataGridView1.NewRowNeeded += new DataGridViewRowEventHandler(DataGridView1_NewRowNeeded);
        dataGridView1.RowValidated += new DataGridViewCellEventHandler(DataGridView1_RowValidated);
        dataGridView1.RowDirtyStateNeeded += new QuestionEventHandler(dataGridView1_RowDirtyStateNeeded);
        dataGridView1.CancelRowEdit += new QuestionEventHandler(DataGridView1_CancelRowEdit);
        dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(DataGridView1_UserDeletingRow);

        // Add columns to the DataGridView.
        DataGridViewTextBoxColumn companyNameColumn = new DataGridViewTextBoxColumn
        {
            HeaderText = "Company Name",
            Name = "Company Name"
        };
        DataGridViewTextBoxColumn contactNameColumn = new DataGridViewTextBoxColumn
        {
            HeaderText = "Contact Name",
            Name = "Contact Name"
        };
        dataGridView1.Columns.Add(companyNameColumn);
        dataGridView1.Columns.Add(contactNameColumn);
        dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;

        // Add some sample entries to the data store. 
        customers.Add(new Customer(
            "Bon app'", "Laurence Lebihan"));
        customers.Add(new Customer(
            "Bottom-Dollar Markets", "Elizabeth Lincoln"));
        customers.Add(new Customer(
            "B's Beverages", "Victoria Ashworth"));

        // Set the row count, including the row for new records.
        dataGridView1.RowCount = 4;
    }

    private void DataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
        // Если это новый ряд, то значения не требуются
        if (e.RowIndex == dataGridView1.RowCount - 1)
            return;

        Customer customerTmp = null;

        // Store a reference to the Customer object for the row being painted.
        if (e.RowIndex == rowInEdit)
        {
            customerTmp = customerInEdit;
        }
        else
        {
            customerTmp = (Customer)customers[e.RowIndex];
        }

        // Set the cell value to paint using the Customer object retrieved.
        switch (dataGridView1.Columns[e.ColumnIndex].Name)
        {
            case "Company Name":
                e.Value = customerTmp.CompanyName;
                break;

            case "Contact Name":
                e.Value = customerTmp.ContactName;
                break;
        }
    }

    private void DataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
    {
        Customer customerTmp = null;

        // Store a reference to the Customer object for the row being edited.
        if (e.RowIndex < customers.Count)
        {
            // If the user is editing a new row, create a new Customer object.
            if (customerInEdit == null)
            {
                customerInEdit = new Customer(
                    ((Customer)customers[e.RowIndex]).CompanyName,
                    ((Customer)customers[e.RowIndex]).ContactName);
            }
            customerTmp = customerInEdit;
            rowInEdit = e.RowIndex;
        }
        else
        {
            customerTmp = customerInEdit;
        }

        // Set the appropriate Customer property to the cell value entered.
        String newValue = e.Value as String;
        switch (dataGridView1.Columns[e.ColumnIndex].Name)
        {
            case "Company Name":
                customerTmp.CompanyName = newValue;
                break;

            case "Contact Name":
                customerTmp.ContactName = newValue;
                break;
        }
    }

    private void DataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
    {
        // Create a new Customer object when the user edits
        // the row for new records.
        customerInEdit = new Customer();
        rowInEdit = dataGridView1.Rows.Count - 1;
    }

    private void DataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
    {
        // Save row changes if any were made and release the edited 
        // Customer object if there is one.
        if (e.RowIndex >= customers.Count &&
            e.RowIndex != dataGridView1.Rows.Count - 1)
        {
            // Add the new Customer object to the data store.
            customers.Add(customerInEdit);
            customerInEdit = null;
            rowInEdit = -1;
        }
        else if (customerInEdit != null &&
            e.RowIndex < customers.Count)
        {
            // Save the modified Customer object in the data store.
            customers[e.RowIndex] = customerInEdit;
            customerInEdit = null;
            rowInEdit = -1;
        }
        else if (dataGridView1.ContainsFocus)
        {
            customerInEdit = null;
            rowInEdit = -1;
        }
    }

    private void dataGridView1_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
    {
        if (!rowScopeCommit)
        {
            // In cell-level commit scope, indicate whether the value
            // of the current cell has been modified.
            e.Response = dataGridView1.IsCurrentCellDirty;
        }
    }

    private void DataGridView1_CancelRowEdit(object sender, QuestionEventArgs e)
    {
        if (rowInEdit == dataGridView1.Rows.Count - 2 &&
            rowInEdit == customers.Count)
        {
            // If the user has canceled the edit of a newly created row, 
            // replace the corresponding Customer object with a new, empty one.
            customerInEdit = new Customer();
        }
        else
        {
            // If the user has canceled the edit of an existing row, 
            // release the corresponding Customer object.
            customerInEdit = null;
            rowInEdit = -1;
        }
    }

    private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (e.Row.Index < customers.Count)
        {
            // If the user has deleted an existing row, remove the 
            // corresponding Customer object from the data store.
            customers.RemoveAt(e.Row.Index);
        }

        if (e.Row.Index == rowInEdit)
        {
            // If the user has deleted a newly created row, release
            // the corresponding Customer object. 
            rowInEdit = -1;
            customerInEdit = null;
        }
    }
}
