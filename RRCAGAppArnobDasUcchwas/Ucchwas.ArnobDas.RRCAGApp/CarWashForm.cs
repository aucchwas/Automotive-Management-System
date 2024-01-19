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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACE.BIT.ADEV.CarWash;
using DasUcchwas.Arnob.Business;

namespace Ucchwas.ArnobDas.RRCAGApp
{
    public partial class CarWashForm : ACE.BIT.ADEV.Forms.CarWashForm
    {
        private CarWashInvoice carWashInvoice;
        private BindingSource carwashSource;


        BindingSource carWashFormSource;



        /// <summary>
        /// Inializes an instancs of the CarWashForm Class with no parameters.
        /// </summary>
        public CarWashForm()
        {
            InitializeComponent();
            this.Load += CarWashForm_Load;

            Populate();

            
            this.carWashFormSource = new BindingSource();
            this.carWashFormSource.DataSource = typeof(CarWashInvoice);
            
            this.carwashSource = new BindingSource();
            this.carwashSource.DataSource = this.carWashInvoice;

            this.mnuToolsGenerateInvoice.Click += MnuToolsGenerateInvoice_Click;
            this.mnuFileClose.Click += MnuFileClose_Click;
            this.cboFragrance.SelectedValueChanged += Cbo_SelectedValueChanged;
            this.cboPackage.SelectedValueChanged += Cbo_SelectedValueChanged;

            MenuFunctions();
        }

        /// <summary>
        /// hadnles the load event of the class
        /// </summary>
        private void CarWashForm_Load(object sender, EventArgs e)
        {
            cboPackage.SelectedIndex = -1;
            
        }


        /// <summary>
        /// 
        /// </summary>
        private void MenuFunctions()
        {
            if (lblTotal.Text == "")
            {
                this.mnuFileClose.Enabled = false;
                this.mnuToolsGenerateInvoice.Enabled = false;
            }
            else
            {
                this.mnuFileClose.Enabled = true;
                this.mnuToolsGenerateInvoice.Enabled = true;
            }
        }

        /// <summary>
        /// handles the click event of the munu item of close.
        /// </summary>
        private void MnuFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// handles the Click event of the generate invoice menu item.
        /// </summary>
        private void MnuToolsGenerateInvoice_Click(object sender, EventArgs e)
        {
            CarWashInvoiceForm car = new CarWashInvoiceForm(carWashInvoice);
            car.Show();

            state();
        }

        /// <summary>
        /// Initializes the state of the whole page
        /// </summary>
        private void state()
        {
            this.cboFragrance.SelectedIndex = 0;
            this.cboPackage.SelectedIndex = -1;
            this.lblGoodsAndServicesTax.Text = string.Empty;
            this.lblProvincialSalesTax.Text = string.Empty;
            this.lblSubtotal.Text = string.Empty;
            this.lblTotal.Text = string.Empty;
            this.lstExterior.ClearSelected();
            this.lstInterior.ClearSelected();
        }

        /// <summary>
        /// handles the Selcted Vlaue changed event combo boces.
        /// </summary>
        private void Cbo_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateLst();
            
            MenuFunctions();

            if (this.cboPackage.SelectedIndex >= 0)
            {
                Calculate();
            }
        }

        /// <summary>
        /// Calulates and data binding of the lbls
        /// </summary>
        public void Calculate()
        {
            this.lblGoodsAndServicesTax.DataBindings.Clear();
            this.lblProvincialSalesTax.DataBindings.Clear();
            this.lblSubtotal.DataBindings.Clear();
            this.lblTotal.DataBindings.Clear();

            (List<string> listname, List<string> listvalue, List<string> service, List<decimal> servicePrice)
                = Readlist();

            int selectedfragrance = (int)cboFragrance.SelectedIndex;
            int selectedService = (int)cboPackage.SelectedIndex;

            decimal fragrance = decimal.Parse(listvalue[selectedfragrance]);
            decimal package = servicePrice[selectedService];

            this.carWashInvoice = new CarWashInvoice
                (0, .05m, package, fragrance);


            Binding total = new Binding("Text", this.carWashInvoice, "Total");
            this.lblTotal.DataBindings.Add(total);
            total.FormattingEnabled = true;
            total.FormatString = "C";

            Binding GST = new Binding("Text", this.carWashInvoice, "GoodsAndServicesTaxCharged");
            this.lblGoodsAndServicesTax.DataBindings.Add(GST);
            GST.FormattingEnabled = true;
            GST.FormatString = "0.##";

            Binding PST = new Binding("Text", this.carWashInvoice, "ProvincialSalesTaxCharged");
            this.lblProvincialSalesTax.DataBindings.Add(PST);
            PST.FormattingEnabled = true;
            PST.FormatString = "0.##";

            Binding subTotal = new Binding("Text", this.carWashInvoice, "Subtotal"); 
            this.lblSubtotal.DataBindings.Add(subTotal);
            subTotal.FormattingEnabled = true;
            subTotal.FormatString = "C";

        }

        /// <summary>
        /// Populates the lists.
        /// </summary>
        private void PopulateLst()
        {
            this.lstExterior.DataBindings.Clear();
            this.lstInterior.DataBindings.Clear();

            (List<string> interiorList, List<string> exteriorList) = InteriorExterior();

            interiorList.Insert(0, "Fragrance - " + (string)cboFragrance.SelectedItem);

            int selectedService = (int)cboPackage.SelectedIndex;

            List<string> inte = new List<string>();
            List<string> exte = new List<string>();

            for (int i = 0; i < cboPackage.SelectedIndex + 1; i++)
            {
                inte.Add(interiorList[i]);
                exte.Add(exteriorList[i]);
            }

            this.lstInterior.DataSource = inte;
            this.lstExterior.DataSource = exte;
        }

        /// <summary>
        /// Populates the comboboxes
        /// </summary>
        private void Populate()
        {
            (List<string> listname, List<string> listvalue, List<string> service, List<decimal> servicePrice)
                = Readlist();
         
            cboFragrance.DataSource = listname;

            cboPackage.DataSource = service;
        }



        /// <summary>
        /// THe list of the exterior interior list
        /// </summary>
        /// <returns>
        /// List<string> the name of the offers for interiors
        /// List<string> the name of the offers for exterior
        /// </returns>
        private (List<string>, List<string>) InteriorExterior()
        {
            List<string> interiorList = new List<string> {"Shampoo Carpets", "Shampoo Upholstery", "Interior Protection Coat"};
            List<string> exteriorList = new List<string> {"Hand Wash", "Hand Wax", "Wheel Polish", "Detail Engine Compartment" };

            return (interiorList, exteriorList);
        }

        /// <summary>
        /// Returns the lists form the text file
        /// </summary>
        /// <returns>
        /// List<string> The name of the fragranccs
        /// List<string> The price of the fragrances
        /// List<string> The name of the package
        /// List<string> The price of the package 
        /// </returns>
        public (List<string>, List<string>, List<string>, List<decimal>) Readlist()
        {
            string filePath = "fragrances.txt";

            FileStream fileSteam = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            StreamReader fileReader = new StreamReader(fileSteam);

            List<string> fragrancelistname = new List<string>();
            List<string> fragrancelistvalue = new List<string>();

            try 
            {
                while (fileReader.Peek() != -1)
                {
                    string record = fileReader.ReadLine();

                    char[] delimeter = { ',' };

                    string[] fields = record.Split(delimeter);

                    string name = fields[0];
                    string value = fields[1];

                    fragrancelistname.Add(name);
                    fragrancelistvalue.Add(value);
                }
                fileReader.Close();
            }
            catch (FileNotFoundException)
            {
                DialogResult show = MessageBox.Show("fragrances data file not found.", "Data File Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (IOException) 
            {
                DialogResult show = MessageBox.Show("An error occurred while reading the data file", "Data File Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            fragrancelistname.Insert(0, "Pine");
            fragrancelistvalue.Insert(0, "0");
            List<string> service = new List<string> {"Standard", "Deluxe", "Executive", "Luxury" };
            List<decimal> servicePrice = new List<decimal> { 7.5m, 15m, 35m, 55};

            return (fragrancelistname, fragrancelistvalue, service, servicePrice);
        }
        
    }
}
