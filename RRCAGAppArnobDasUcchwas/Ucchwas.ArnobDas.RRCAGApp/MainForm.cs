/**
  * Name: Arnob Das Ucchwas
  * Program: Business Information Technology
  * Course: ADEV-2008 Programming 2
  * Created: 01/11/2023
  * Updated: 04/11/2023
  */

using ACE.BIT.ADEV.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ucchwas.ArnobDas.RRCAGApp
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Inializes an instancs of the MainForm Class with no parameters.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            

            this.mnuFileOpenSalesQuote.Click += MnuFileOpenSalesQuote_Click;
            this.mnuFileExit.Click += MnuFileExit_Click;
            this.mnuHelpAbout.Click += MnuHelpAbout_Click;
            this.mnuFileOpenCarWash.Click += MnuFileOpenCarWash_Click;
            this.mnuDataVehicles.Click += MnuDataVehicles_Click;

        }

        /// <summary>
        /// Handles the click event of the Vehicles menu item
        /// </summary>
        private void MnuDataVehicles_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form.Text == "VehicleDataForm")
                {
                    IsOpen = true;
                    form.Focus();
                    break;
                }
            }

            if (IsOpen == false)
            {
                VehicleDataForm vehicle = new VehicleDataForm();
                vehicle.MdiParent = this;
                vehicle.Show();
            }

        }

        /// <summary>
        /// Handles the Click event of the About menu item.
        /// </summary>
        private void MnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();

            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the Exit mennu item.
        /// </summary>
        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// handles the Click event of the Sales Quote menu item.
        /// </summary>
        private void MnuFileOpenSalesQuote_Click(object sender, EventArgs e)
        {
            SalesQuoteForm salesQuote = new SalesQuoteForm();
            salesQuote.Show();
        }

        /// <summary>
        /// Handles the click event of the Car Wash menu item        
        /// </summary>
        private void MnuFileOpenCarWash_Click(object sender, EventArgs e)
        { 
            CarWashForm carWash = new CarWashForm();
            carWash.MdiParent = this;
            carWash.Show();
        }
    }
}
