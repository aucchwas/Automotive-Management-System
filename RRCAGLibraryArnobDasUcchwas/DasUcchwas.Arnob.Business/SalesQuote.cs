/**
  * Name: Arnob Das Ucchwas
  * Program: Business Information Technology
  * Course: ADEV-2008 Programming 2
  * Created: 16/10/2023
  * Updated: 03/11/2023
  */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasUcchwas.Arnob.Business
{
    public class SalesQuote
    {

        private decimal vehicleSalePrice;
        private decimal tradeInAmount;
        private decimal salesTaxRate;
        private Accessories accessoriesChosen;
        private ExteriorFinish exteriorFinishChosen;

        /// <summary>
        /// Occurs when the price of the vehicle being quoted on changes.
        /// </summary>
        public event EventHandler VehiclePriceChanged;

        /// <summary>
        ///  Occurs when the amount for the trade in vehicle changes.
        /// </summary>
        public event EventHandler TradeInAmountChanged;

        /// <summary>
        /// Occurs when the chosen accessories change.
        /// </summary>
        public event EventHandler AccessoriesChosenChanged;

        /// <summary>
        /// Occurs when the chosen exterior finish changes.
        /// </summary>
        public event EventHandler ExteriorFinishChosenChanged;

        /// <summary>
        /// Gets and sets the sale price of the vehicle.
        /// </summary>
        public decimal VehicleSalePrice
        {
            get
            {
                return this.vehicleSalePrice;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than or equal to 0.");
                }
                else if(this.vehicleSalePrice != value)
                {
                    this.vehicleSalePrice = value;
                    OnVehiclePriceChanged();
                }
            }
        }

        /// <summary>
        /// Gets and sets the trade in amount.
        /// </summary>
        public decimal TradeInAmount
        {
            get
            {
                return this.tradeInAmount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                else if(this.tradeInAmount != value)
                {
                    this.tradeInAmount = value;
                    OnTradeInAmountChanged();
                }

            }
        }

        /// <summary>
        /// Gets and sets the accessories that were chosen.
        /// </summary>
        public Accessories AccessoriesChosen
        {
            get
            {
                return this.accessoriesChosen;
            }
            set
            {
                if (Enum.IsDefined(typeof(Accessories), value))
                {
                    if (this.accessoriesChosen != value)
                    { 
                        this.accessoriesChosen = value;
                        OnAccessoriesChosenChanged();
                    }

                }
                else
                {
                    throw new InvalidEnumArgumentException("The value is an invalid enumeration value");
                }

            }
        }

        /// <summary>
        /// Gets and sets the exterior finish that was chosen.
        /// </summary>
        public ExteriorFinish ExteriorFinishChosen
        {
            get
            {
                return exteriorFinishChosen;
            }
            set
            {
                if (Enum.IsDefined(typeof(ExteriorFinish), value))
                {
                    if (this.exteriorFinishChosen != value)
                    { 
                        this.exteriorFinishChosen = value;
                        OnExteriorFinishChosenChanged();
                    }
                }
                else
                {
                    throw new InvalidEnumArgumentException("The value is an invalid enumeration value");
                }

            }
        }

        /// <summary>
        /// Gets the cost of accessories chosen.
        /// </summary>
        public decimal AccessoryCost
        {
            get
            {
                decimal cost = 0;

                const decimal StereoSystemCost = 505.05m;
                const decimal ComputerNavigationCost = 1515.15m;
                const decimal LeatherInteriorCost = 1010.10m;

                if (this.accessoriesChosen == Accessories.StereoSystem)
                {
                    cost = StereoSystemCost;
                }
                else if (this.accessoriesChosen == Accessories.LeatherInterior)
                {
                    cost = LeatherInteriorCost;
                }
                else if (this.accessoriesChosen == Accessories.ComputerNavigation)
                {
                    cost = ComputerNavigationCost;
                }
                else if (this.accessoriesChosen == Accessories.StereoAndLeather)
                {
                    cost = StereoSystemCost + LeatherInteriorCost;
                }
                else if (this.accessoriesChosen == Accessories.StereoAndNavigation)
                {
                    cost = StereoSystemCost + ComputerNavigationCost;
                }
                else if (this.accessoriesChosen == Accessories.LeatherAndNavigation)
                {
                    cost = LeatherInteriorCost + ComputerNavigationCost;
                }
                else if (this.accessoriesChosen == Accessories.All)
                {
                    cost = StereoSystemCost + LeatherInteriorCost + ComputerNavigationCost;
                }

                return cost;
            }
        }

        /// <summary>
        /// Gets the cost of the exterior finish chosen.
        /// </summary>
        public decimal FinishCost
        {
            get
            {
                decimal cost = 0;

                const decimal  StandadCost = 202.02m;
                const decimal CustomCost = 606.06m;
                const decimal PearlizedCost = 404.04m;

                if (this.exteriorFinishChosen == ExteriorFinish.Standard)
                {
                    cost = StandadCost;    
                }
                else if (exteriorFinishChosen == ExteriorFinish.Pearlized)
                {
                    cost = PearlizedCost;
                }
                else if (this.exteriorFinishChosen == ExteriorFinish.Custom)
                {
                    cost = CustomCost;
                }

                return cost;
            }
        }

        /// <summary>
        ///  Gets the sum of the cost of the chosen accessories and exterior finish.
        /// </summary>
        public decimal TotalOptions
        {
            get
            {
                decimal cost = Math.Round(this.AccessoryCost + this.FinishCost, 2);

                return cost;
            }
        }

        /// <summary>
        /// Gets the sum of the vehicle’s sale price and the Accessory and Finish Cost.
        /// </summary>
        public decimal SubTotal
        {
            get
            {
                decimal cost = this.vehicleSalePrice + this.TotalOptions;
                return Math.Round(cost, 2);
            }
        }

        /// <summary>
        ///  Gets the amount of tax to charge based on the subtotal.
        /// </summary>
        public decimal SalesTax
        {
            get
            {
                decimal totalTax = VehicleSalePrice * this.salesTaxRate;
                totalTax = (decimal)Math.Round(totalTax, 2);

                return totalTax;
            }
        }

        /// <summary>
        /// Gets the sum of the subtotal and taxes.
        /// </summary>
        public decimal Total
        {
            get
            { 
                decimal totalTax = SubTotal + SalesTax;

                return totalTax;
            }
        }

        /// <summary>
        /// Gets the result of subtracting the trade-in amount from the total.
        /// </summary>
        public decimal AmountDue
        {
            get
            {
                decimal totalAmountDue = Math.Round(Total - this.tradeInAmount, 2);

                return totalAmountDue;
            }
        }

        /// <summary>
        /// Raises the VehiclePriceChanged event. 
        /// </summary>
        protected virtual void OnVehiclePriceChanged()
        {
            if (VehiclePriceChanged != null)
            {
                VehiclePriceChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the TradeInAmountChanged event.
        /// </summary>
        protected virtual void OnTradeInAmountChanged()
        {
            if (TradeInAmountChanged != null)
            {
                TradeInAmountChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the AccessoriesChosenChanged event.
        /// </summary>
        protected virtual void OnAccessoriesChosenChanged()
        {
            if (AccessoriesChosenChanged != null)
            {
                AccessoriesChosenChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the ExteriorFinishChosenChanged event.
        /// </summary>
        protected virtual void OnExteriorFinishChosenChanged()
        {
            if (ExteriorFinishChosenChanged != null)
            { 
                ExteriorFinishChosenChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Initializes an instance of SalesQuote with a vehicle price, trade-in value, 
        /// sales tax rate, accessories chosen and exterior finish chosen.
        /// </summary>
        /// <param name="vehicleSalePrice">The selling price of the vehicle being sold.</param>
        /// <param name="tradeInAmount">The amount offered to the customer for the trade in of their vehicle.</param>
        /// <param name="salesTaxRate">The tax rate applied to the sale of a vehicle.</param>
        /// <param name="accessoriesChosen">The value of the chosen accessories.</param>
        /// <param name="exteriorFinishChosen">The value of the chosen exterior finish.</param>
        /// <exception cref="ArgumentOutOfRangeException">throws an </exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount, decimal salesTaxRate,
                            Accessories accessoriesChosen, ExteriorFinish exteriorFinishChosen)
        {
            if (salesTaxRate > 1 || salesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The salesTaxRate cannot be greater than 1 or less than 0.");
            }
            this.VehicleSalePrice = vehicleSalePrice;
            this.TradeInAmount = tradeInAmount;
            this.salesTaxRate = salesTaxRate;
            this.AccessoriesChosen = accessoriesChosen;
            this.ExteriorFinishChosen = exteriorFinishChosen;
        }

        /// <summary>
        /// Initializes an instance of SalesQuote with a vehicle price, trade-in amount, 
        /// sales tax rate, no accessories chosen and no exterior finish chosen.
        /// </summary>
        /// <param name="vehicleSalePrice">The selling price of the vehicle being sold.</param>
        /// <param name="tradeInAmount">The amount offered to the customer for the trade in of their vehicle.</param>
        /// <param name="salesTaxRate">The tax rate applied to the sale of a vehicle.</param>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount, decimal salesTaxRate)
                            : this(vehicleSalePrice, tradeInAmount, salesTaxRate, Accessories.None, ExteriorFinish.None)
        {

        }
    }
}
