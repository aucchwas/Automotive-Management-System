/**
  * Name: Arnob Das Ucchwas
  * Program: Business Information Technology
  * Course: ADEV-2008 Programming 2
  * Created: 17/11/2023
  * Updated: 26/11/2023
  */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DasUcchwas.Arnob.Business;

namespace Ucchwas.ArnobDas.RRCAGApp
{
    public partial class CarWashInvoiceForm : ACE.BIT.ADEV.Forms.InvoiceForm
    {
        private CarWashForm carWashForm;

        /// <summary>
        /// Inializes an instancs of the CarWashInvoiceForm Class with a CarWashInvoice Object as parameter.
        /// </summary>
        public CarWashInvoiceForm(CarWashInvoice car)
        {
            InitializeComponent();
            BindingLabel(car);
        }

        

        /// <summary>
        /// binds the Labels to the carwashinvoice class.
        /// </summary>
        private void BindingLabel(CarWashInvoice car)
        {
            this.lblSubtotal.DataBindings.Clear();
            carWashForm =new CarWashForm();

            car = new CarWashInvoice(0, 0.05m, car.PackageCost, car.FragranceCost);

            Binding fragrance = new Binding("Text", car, "FragranceCost");
            this.lblFragrancePrice.DataBindings.Add(fragrance);
            fragrance.FormattingEnabled = true;
            fragrance.FormatString = "C";

            Binding package = new Binding("Text", car, "PackageCost");
            this.lblPackagePrice.DataBindings.Add(package);
            package.FormattingEnabled = true;
            package.FormatString = "C";

            Binding GST = new Binding("Text", car, "GoodsAndServicesTaxCharged");
            this.lblGST.DataBindings.Add(GST);
            GST.FormattingEnabled = true;
            GST.FormatString = "0.##";

            Binding PST = new Binding("Text", car, "ProvincialSalesTaxCharged");
            this.lblPST.DataBindings.Add(PST);
            PST.FormattingEnabled = true;
            PST.FormatString = "0.##";


            Binding subTotal = new Binding("Text", car, "Subtotal");
            this.lblSubtotal.DataBindings.Add(subTotal);
            subTotal.FormattingEnabled = true;
            subTotal.FormatString = "C";

            Binding total = new Binding("Text", car, "Total");
            this.lblTotal.DataBindings.Add(total);
            total.FormattingEnabled = true;
            total.FormatString = "C";
        }
    }
}
