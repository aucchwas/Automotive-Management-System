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
    public abstract class Invoice
    {
        /// <summary>
        /// The provincial sales tax rate applied to the invoice.
        /// </summary>
        private decimal provincialSalesTaxRate;

        /// <summary>
        /// The goods and services tax rate applied to the invoice.
        /// </summary>
        private decimal goodsAndServicesTaxRate;

        /// <summary>
        /// Occurs when the provincial sales tax rate of the Invoice changes.
        /// </summary>
        public event EventHandler ProvincialSalesTaxRateChanged;

        /// <summary>
        /// Occurs when the goods and services tax rate of the Invoice changes.
        /// </summary>
        public event EventHandler GoodsAndServicesTaxRateChanged;

        /// <summary>
        /// Gets and sets the provincial sales tax rate.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provincial sales tax rate is less than 0. </exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provincial sales tax rate is greater than 1.</exception>
        public decimal ProvincialSalesTaxRate
        {
            get
            {
                return this.provincialSalesTaxRate;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                else if (value > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
                }
                else if (this.provincialSalesTaxRate != value)
                {
                    this.provincialSalesTaxRate = value;
                    OnProvincialSalesTaxRateChanged();
                }
            }
        }

        /// <summary>
        /// Gets and sets the goods and services tax rate.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when the goods and services tax rate is less than 0. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when the goods and services tax rate is greater than 1.</exception>
        public decimal GoodsAndServicesTaxRate
        {
            get
            {
                return this.goodsAndServicesTaxRate;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                else if (value > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
                }
                else if (this.goodsAndServicesTaxRate != value)
                {
                    this.goodsAndServicesTaxRate = value;
                    OnGoodsAndServicesTaxRateChanged();
                }
            }
        }

        /// <summary>
        /// Gets the amount of provincial sales tax charged to the customer (Rounded to two decimal places).
        /// </summary>
        public abstract decimal ProvincialSalesTaxCharged
        {
            get;
        }

        /// <summary>
        /// Gets the amount of goods and services tax charged to the customer (Rounded to two decimal places).
        /// </summary>
        public abstract decimal GoodsAndServicesTaxCharged
        {
            get;
        }

        /// <summary>
        /// Gets the subtotal of the Invoice.
        /// </summary>
        public abstract decimal Subtotal
        {
            get;
        }

        /// <summary>
        /// Gets the total of the Invoice. The total is the sum of the subtotal and taxes.
        /// </summary>
        public virtual decimal Total
        {
            get 
            {
                decimal TotalTax = GoodsAndServicesTaxCharged + ProvincialSalesTaxCharged;

                return Subtotal + TotalTax;
            }
        }

        /// <summary>
        /// Raises the ProvincialSalesTaxRateChanged event.
        /// </summary>
        protected virtual void OnProvincialSalesTaxRateChanged()
        {
            if (ProvincialSalesTaxRateChanged != null)
            {
                ProvincialSalesTaxRateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the GoodsAndServicesTaxRateChanged event.
        /// </summary>
        protected virtual void OnGoodsAndServicesTaxRateChanged()
        {
            if (GoodsAndServicesTaxRateChanged != null)
            {
                GoodsAndServicesTaxRateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Initializes an instance of Invoice with a provincial and goods and services tax rates.
        /// </summary>
        /// <param name="provincialSalesTaxRate">The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provincial sales tax rate is less than 0. </exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provincial sales tax rate is greater than 1.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when the goods and services tax rate is less than 0. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when the goods and services tax rate is greater than 1.</exception>
        public Invoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate)
        {
            ProvincialSalesTaxRate = provincialSalesTaxRate;
            GoodsAndServicesTaxRate = goodsAndServicesTaxRate;
        }
    }
}
