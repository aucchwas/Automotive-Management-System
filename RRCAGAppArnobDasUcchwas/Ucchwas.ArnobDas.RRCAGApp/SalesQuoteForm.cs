/**
  * Name: Arnob Das Ucchwas
  * Program: Business Information Technology
  * Course: ADEV-2008 Programming 2
  * Created: 01/11/2023
  * Updated: 04/11/2023
  */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DasUcchwas.Arnob.Business;

namespace Ucchwas.ArnobDas.RRCAGApp
{
    public partial class SalesQuoteForm : Form
    {

        /// <summary>
        /// Initializes an instance of SalesQuoteForm Class.
        /// </summary>
        public SalesQuoteForm()
        {
            InitializeComponent();

            this.Load += SalesQuoteForm_Load;
            this.btnCalculate.Click += BtnCalculate_Click;

            this.btnReset.Click += BtnReset_Click;

            this.chkComputerNavigation.CheckedChanged += Chk_CheckedChanged;
            this.chkLeatherInterior.CheckedChanged += Chk_CheckedChanged;
            this.chkStereoSystem.CheckedChanged += Chk_CheckedChanged;

            this.radCustomizedDetails.CheckedChanged += rad_CheckedChanged;
            this.radPearlized.CheckedChanged += rad_CheckedChanged;
            this.radStandard.CheckedChanged += rad_CheckedChanged;

            this.txtTradeInAmount.TextChanged += Txt_TextChanged;
            this.txtVehicleSalePrice.TextChanged += Txt_TextChanged;

            this.nudAnnualInterestRate.ValueChanged += Nud_ValueChanged;
            this.nudNoofYears.ValueChanged += Nud_ValueChanged;
        }

        /// <summary>
        /// Handles the ValueChanged event for numeric up down.
        /// </summary>
        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            if (lblAmountDue.Text != "")
            {
                Calculate();
            }
        }

        /// <summary>
        /// Handles the checkboxes checkchanged event.
        /// </summary>
        private void Chk_CheckedChanged(Object sender, EventArgs e)
        {
            decimal VehicleSalePrice = 0,
                    TradeInAmount = 0;

            this.errorProvider.SetError(this.txtVehicleSalePrice, String.Empty);
            this.errorProvider.SetError(this.txtTradeInAmount, String.Empty);

            if (this.txtVehicleSalePrice.Text.Equals(String.Empty))
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                                            "Vehicle price is a required field.");
            }

            try
            {
                
                if (Decimal.Parse(this.txtVehicleSalePrice.Text) <= 0)
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot be less than or equal to 0.");
                }
                else
                {
                    VehicleSalePrice = Decimal.Parse(this.txtVehicleSalePrice.Text);
                }

            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot contain letters or special characters.");
            }

            try
            {
                if (this.txtTradeInAmount.Text.Equals(String.Empty))
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value is a required field.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) < 0)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value cannot be less than 0.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) > VehicleSalePrice)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount, "Trade-in value cannot exceed the vehicle sale price.");
                }
                else
                {
                    TradeInAmount = Decimal.Parse(this.txtTradeInAmount.Text);
                }
            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtTradeInAmount,
                        "Trade-in value cannot contain letters or special characters");
            }

            if (lblAmountDue.Text != "")
            {
                Calculate();
            }


        }

        /// <summary>
        /// handles the radio buttons checked changed event.
        /// </summary>
        private void rad_CheckedChanged(Object sender, EventArgs e)
        {
            decimal VehicleSalePrice = 0,
                    TradeInAmount = 0;

            this.errorProvider.SetError(this.txtVehicleSalePrice, String.Empty);
            this.errorProvider.SetError(this.txtTradeInAmount, String.Empty);

            try
            {
                if (this.txtVehicleSalePrice.Text.Equals(String.Empty))
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                                                "Vehicle price is a required field.");
                }
                else if (Decimal.Parse(this.txtVehicleSalePrice.Text) <= 0)
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot be less than or equal to 0.");
                }
                else
                {
                    VehicleSalePrice = Decimal.Parse(this.txtVehicleSalePrice.Text);
                }

            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot contain letters or special characters.");
            }

            try
            {
                if (this.txtTradeInAmount.Text.Equals(String.Empty))
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value is a required field.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) < 0)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value cannot be less than 0.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) > VehicleSalePrice)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount, "Trade-in value cannot exceed the vehicle sale price.");
                }
                else
                {
                    TradeInAmount = Decimal.Parse(this.txtTradeInAmount.Text);
                }
            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtTradeInAmount,
                        "Trade-in value cannot contain letters or special characters");
            }

            if (lblAmountDue.Text != "")
            {
                Calculate();
            }
        }

        /// <summary>
        /// Handles the reset buttons click event.
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            string message = "Do you want to reset the form?";
            string caption = "Reset Form";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

            switch (result)
            {
                case DialogResult.Yes:
                    
                    this.txtTradeInAmount.Text = "0";
                    this.txtVehicleSalePrice.Text = string.Empty;
                    this.chkComputerNavigation.Checked = false;
                    this.chkLeatherInterior.Checked = false;
                    this.chkStereoSystem.Checked = false;

                    this.radCustomizedDetails.Checked = false;
                    this.radPearlized.Checked = false;
                    this.radStandard.Checked = true;
                    break;
            }

            this.errorProvider.SetError(txtVehicleSalePrice, String.Empty);
        }

        ///// <summary>
        ///// HAndles the Text Changed event of the textboxes
        ///// </summary>
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            this.lblVehicleSalePrice.Text = string.Empty;
            this.lblOptions.Text = string.Empty;
            this.lblSubtotal.Text = string.Empty;
            this.lblSalesTax.Text = string.Empty;
            this.lblTotal.Text = string.Empty;
            this.lblTradeInAmount.Text = string.Empty;
            this.lblAmountDue.Text = string.Empty;
            this.lblMonthlyPayment.Text = string.Empty;
        }

        /// <summary>
        /// Returns the chosen Exterior Finsh.
        /// </summary>
        /// <returns>The chosen Exterior Finish</returns>
        public ExteriorFinish ExteriorChosen()
        {
            ExteriorFinish exterior;

            if (this.radStandard.Checked)
            {
                exterior = ExteriorFinish.Standard;
            }
            else if (this.radPearlized.Checked)
            {
                exterior = ExteriorFinish.Pearlized;
            }
            else if (this.radCustomizedDetails.Checked)
            {
                exterior = ExteriorFinish.Custom;
            }
            else
            {
                exterior = ExteriorFinish.None;
            }

            return exterior;
        }

        /// <summary>
        /// Returns the  chosen Accessories.
        /// </summary>
        /// <returns>the chosen Accessories</returns>
        public Accessories AccessoriesChosen()
        {
            Accessories accessories;

            if (this.chkComputerNavigation.Checked && this.chkLeatherInterior.Checked &&
                        this.chkStereoSystem.Checked)
            {
                accessories = Accessories.All;
            }
            else if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked &&
                        !this.chkComputerNavigation.Checked)
            {
                accessories = Accessories.StereoAndLeather;
            }
            else if (this.chkStereoSystem.Checked && this.chkComputerNavigation.Checked &&
                        !this.chkLeatherInterior.Checked)
            {
                accessories = Accessories.StereoAndNavigation;
            }
            else if (this.chkLeatherInterior.Checked && this.chkComputerNavigation.Checked &&
                        !this.chkStereoSystem.Checked)
            {
                accessories = Accessories.LeatherAndNavigation;
            }
            else if (this.chkStereoSystem.Checked && !this.chkLeatherInterior.Checked &&
                        !this.chkComputerNavigation.Checked)
            {
                accessories = Accessories.StereoSystem;
            }
            else if (this.chkLeatherInterior.Checked && !this.chkComputerNavigation.Checked &&
                        !this.chkComputerNavigation.Checked)
            {
                accessories = Accessories.LeatherInterior;
            }
            else if (this.chkComputerNavigation.Checked && !this.chkLeatherInterior.Checked &&
                        !this.chkStereoSystem.Checked)
            {
                accessories = Accessories.ComputerNavigation;
            }
            else
            {
                accessories = Accessories.None;
            }
            return accessories;
        }


        /// <summary>
        /// handle the load event of the class SalesQuoteForm.
        /// </summary>
        private void SalesQuoteForm_Load(object sender, EventArgs e)
        {
            this.txtVehicleSalePrice.Text = String.Empty;
            this.txtTradeInAmount.Text = "0";
        }

        /// <summary>
        /// handles the calculate button's click event 
        /// </summary>
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            decimal VehicleSalePrice = 0,
                    TradeInAmount = 0;

            this.errorProvider.SetError(this.txtVehicleSalePrice, String.Empty);
            this.errorProvider.SetError(this.txtTradeInAmount, String.Empty);

            try
            {
                if (this.txtVehicleSalePrice.Text.Equals(String.Empty))
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                                                "Vehicle price is a required field.");
                }
                else if (Decimal.Parse(this.txtVehicleSalePrice.Text) <= 0)
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot be less than or equal to 0.");
                }
                else
                {
                    VehicleSalePrice = Decimal.Parse(this.txtVehicleSalePrice.Text);
                }

            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                        "Vehicle price cannot contain letters or special characters.");
            }

            try
            {
                if (this.txtTradeInAmount.Text.Equals(String.Empty))
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value is a required field.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) < 0)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount,
                                                    "Trade-in value cannot be less than 0.");
                }
                else if (Decimal.Parse(this.txtTradeInAmount.Text) > VehicleSalePrice)
                {
                    this.errorProvider.SetError(this.txtTradeInAmount, "Trade-in value cannot exceed the vehicle sale price.");
                }
                else
                {
                    TradeInAmount = Decimal.Parse(this.txtTradeInAmount.Text);
                }
            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtTradeInAmount,
                        "Trade-in value cannot contain letters or special characters");
            }

            Calculate();
        }

        /// <summary>
        /// Calculates the total monthly ammount due.
        /// </summary>
        public void Calculate()
        {
            if (this.errorProvider.GetError(this.txtVehicleSalePrice).Equals(string.Empty) &&
                    this.errorProvider.GetError(this.txtTradeInAmount).Equals(string.Empty))
            {
                SalesQuote salesQuote = new SalesQuote(Decimal.Parse(this.txtVehicleSalePrice.Text),
                                                        Decimal.Parse(this.txtTradeInAmount.Text),
                                                        .7m, AccessoriesChosen(), ExteriorChosen());
                this.lblVehicleSalePrice.Text = salesQuote.VehicleSalePrice.ToString("C");
                this.lblOptions.Text = salesQuote.TotalOptions.ToString("N");
                this.lblSubtotal.Text = salesQuote.SubTotal.ToString("C");
                this.lblSalesTax.Text = salesQuote.SalesTax.ToString("N");
                this.lblTotal.Text = salesQuote.Total.ToString("C");
                this.lblTradeInAmount.Text = "-" + salesQuote.TradeInAmount.ToString();
                this.lblAmountDue.Text = salesQuote.AmountDue.ToString("C");

                decimal rate = this.nudAnnualInterestRate.Value / 100;
                int numberOfPayment = (int)this.nudNoofYears.Value * 12;

                this.lblMonthlyPayment.Text = Financial.GetPayment(rate, numberOfPayment, salesQuote.AmountDue).ToString("C");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblSalesTax_Click(object sender, EventArgs e)
        {

        }

        private void lblMonthlyPayment_Click(object sender, EventArgs e)
        {

        }

        private void nudAnnualInterestRate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
