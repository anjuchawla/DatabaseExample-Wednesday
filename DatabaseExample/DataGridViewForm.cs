using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseExample
{
    public partial class DataGridViewForm : Form
    {
        public DataGridViewForm()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
           try
            {
                this.customersBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.mMABooksDataSet);
            }
            //a value thats beyond the maximium length of a column
            catch(ArgumentException AEex)
            {
                MessageBox.Show(AEex.Message, "Argument Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                customersBindingSource.CancelEdit();
            }
            catch(DBConcurrencyException DBCex)
            {
                MessageBox.Show("A concurrency error occured...update failed", "Concurrency Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.customersTableAdapter.Fill(this.mMABooksDataSet.Customers);
            }
            catch(DataException DEex)
            {
                MessageBox.Show(DEex.Message, DEex.GetType().ToString());
                customersBindingSource.CancelEdit();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error #" + ex.Number + ": " + ex.Message, ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.customersTableAdapter.Fill(this.mMABooksDataSet.Customers);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database Error #" + ex.Number + ": " + ex.Message, ex.GetType().ToString(),
                    MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            customersBindingSource.CancelEdit();
        }
    }
}
