using ACE.BIT.ADEV.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ucchwas.ArnobDas.RRCAGApp
{
    public partial class VehicleDataForm : ACE.BIT.ADEV.Forms.VehicleDataForm
    {
        private OleDbConnection connection;
        private OleDbDataAdapter adapter;
        private DataSet _dataSet;
        private BindingSource _bindingSource;
        private OleDbCommandBuilder _builder;
        private OleDbCommand command;

        /// <summary>
        /// Inializes an instancs of the VehicleDataForm Class with no parameters.
        /// </summary>
        public VehicleDataForm()
        {
            InitializeComponent();

            this.dgvVehicles.AllowUserToDeleteRows = false;
            this.dgvVehicles.AllowUserToResizeColumns = false;
            



            retrive();

            
            this.mnuFileSave.Click += MnuFileSave_Click;
            this.Load += new System.EventHandler(this.VehicleDataForm_Load);
            this.mnuEditRemove.Click += MnuEditRemove_Click;
            this.mnuFileClose.Click += MnuFileClose_Click;

            this.dgvVehicles.CellValueChanged += DgvVehicles_CellValueChanged;
            this.dgvVehicles.SelectionChanged += DgvVehicles_SelectionChanged;
        }

        /// <summary>
        /// handles the Click event of the Close Menu Item.
        /// </summary>
        private void MnuFileClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wish to save the changes?", "Save",
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

            if (result == DialogResult.No)
            {
                this.Close();
            }
            else if (result == DialogResult.Yes)
            {
                MnuFileSave_Click(sender, e);
                this.Close();
            }
        }


        /// <summary>
        /// handles the Cell Value Changed event of the data grid view vehicles.
        /// </summary>
        private void DgvVehicles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.mnuFileSave.Enabled = true;
            

            decimal ForFun=  0;
            if (ForFun == 0)
            {
                this.Text += "[* Vehicle Data ]";
                ForFun++;
            }
        }

        /// <summary>
        /// handles the Selection Changed event of the data grid view vehicles
        /// </summary>
        private void DgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvVehicles.SelectedRows.Count > 0)
            {
                this.mnuEditRemove.Enabled = true;
            }
        }

        /// <summary>
        /// handles the Click event of the Remove Menu Item.
        /// </summary>
        private void MnuEditRemove_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Remove stock item " + dgvVehicles.SelectedCells[1].Value.ToString(), "Remove Stock Item", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                dgvVehicles.Rows.RemoveAt(dgvVehicles.SelectedCells[0].RowIndex);
            }
        }

        /// <summary>
        /// handles the Click event of the Save Menu Item.
        /// </summary>
        private void MnuFileSave_Click(object sender, EventArgs e)
        {
            try 
            {
                this.adapter.Update(_dataSet.Tables["VehicleStock"]);
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("An error occurred while saving the changes to the vehicle data.", "Save Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Retrives the data from the datebase.
        /// </summary>
        public void retrive() 
        {
            try 
            {
                this.connection = new OleDbConnection();

                this.connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:\Users\ayond\OneDrive - Red River College Polytech\rede river\College files\Term-2\Adev-2008\adev-2008_Arnob_DasUcchwas_assignment_6\RRCAGAppArnobDasUcchwas\Ucchwas.ArnobDas.RRCAGApp\bin\Debug\AMDatabase.mdb""";
                this.connection.Open();

                this.command = new OleDbCommand();
                this.command.CommandText = "SELECT * FROM VehicleStock";
                this.command.Connection = connection;

                this.adapter = new OleDbDataAdapter();
                this.adapter.SelectCommand = command;

                this._dataSet = new DataSet();
                this.adapter.Fill(_dataSet, "VehicleStock");

                this._builder = new OleDbCommandBuilder();
                this._builder.DataAdapter = adapter;

                this.adapter.DeleteCommand = _builder.GetDeleteCommand();
                this.adapter.UpdateCommand = _builder.GetUpdateCommand();
                this.adapter.InsertCommand = _builder.GetInsertCommand();


            }
            catch (Exception) 
            {
                DialogResult result = MessageBox.Show("Unable to load vehicle data.", "Data Load Error",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// handles the Load event of the Vehicle Data Form
        /// </summary>
        private void VehicleDataForm_Load(object sender, EventArgs e)
        {
            try 
            {
                this._bindingSource = new BindingSource();

                this._bindingSource.DataSource = _dataSet.Tables["VehicleStock"];

                this.dgvVehicles.DataSource = _bindingSource;

                this.dgvVehicles.Columns["ID"].Visible = false;
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("Unable to load vehicle data.", "Data Load Error"
                                                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
