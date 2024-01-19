/**
  * Name: Arnob Das Ucchwas
  * Program: Business Information Technology
  * Course: ADEV-2008 Programming 2
  * Created: 16/10/2023
  * Updated: 03/11/2023
  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DasUcchwas.Arnob.Business
{
    public class CarWashInvoice : Invoice
    {
        /// <summary>
        /// The amount charged for the chosen package.
        /// </summary>
        private decimal packageCost;

        /// <summary>
        /// The amount charged for the chosen fragrance.
        /// </summary>
        private decimal fragranceCost;

        /// <summary>
        /// Occurs when the package cost changes.
        /// </summary>
        public event EventHandler PackageCostChanged;

        /// <summary>
        /// Occurs when the fragrance cost changes.
        /// </summary>
        public event EventHandler FragranceCostChanged;

        /// <summary>
        /// Gets and sets the amount charged for the chosen package.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the property is set to less than 0. </exception>
        public decimal PackageCost
        {
            get
            {
                return this.packageCost;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                else if(this.packageCost != value)
                {
                    this.packageCost = value;
                    OnPackageCostChanged();
                }
            }

        }

        /// <summary>
        /// Gets and sets the amount charged for the chosen fragrance.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the property is set to less than 0. </exception>
        public decimal FragranceCost
        {
            get
            {
                return this.fragranceCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                else if(this.fragranceCost != value)
                {
                    this.fragranceCost = value;
                    OnFragranceCostChanged();
                }
            }
        }

        /// <summary>
        /// Gets the amount of provincial sales tax charged to the customer.
        /// </summary>
        public override decimal ProvincialSalesTaxCharged
        {
            get => 0;
        }

        /// <summary>
        /// Gets the subtotal of the Invoice. 
        /// The subtotal is the sum of the package and fragrance cost.
        /// </summary>
        public override decimal Subtotal
        {
            get
            {
                return Math.Round((this.packageCost + this.fragranceCost), 2);
            }
        }

        /// <summary>
        /// Gets the amount of goods and services tax charged to the customer (Rounded to 2 decimal places). 
        /// The tax is calculated by multiplying the tax rate by the subtotal.
        /// </summary>
        public override decimal GoodsAndServicesTaxCharged
        {
            get
            {
                return Math.Round(GoodsAndServicesTaxRate * Subtotal, 2);
            }
        }

        /// <summary>
        /// Raises the PackageCostChanged event.
        /// </summary>
        protected virtual void OnPackageCostChanged()
        {
            if (PackageCostChanged != null)
            {
                PackageCostChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the FragranceCostChanged event.
        /// </summary>
        protected virtual void OnFragranceCostChanged()
        {
            if (FragranceCostChanged != null)
            {
                FragranceCostChanged(this, new EventArgs());
            }
        }
        /// <summary>
        /// Initializes an instance of CarWashInvoice with a provincial and goods, services tax rate, package cost and fragrance cost.
        /// </summary>
        /// <param name="provincialSalesTaxRate">The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        /// <param name="packageCost">The cost of the chosen package.</param>
        /// <param name="fragranceCost">The cost of the chosen fragrance.</param>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate, decimal packageCost, decimal fragranceCost) : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {
            base.ProvincialSalesTaxRate = provincialSalesTaxRate;
            base.GoodsAndServicesTaxRate = goodsAndServicesTaxRate;
            PackageCost = packageCost;
            FragranceCost = fragranceCost;
        }

        /// <summary>
        /// Initializes an instance of CarWashInvoice with a provincial and goods and services tax rates. 
        /// The package cost and fragrance cost are zero.
        /// </summary>
        /// <param name="provincialSalesTaxRate">The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate)
            : this(provincialSalesTaxRate, goodsAndServicesTaxRate, 0, 0)
        {

        }
        
    }
}
