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
    public static class Financial
    {
        /// <summary>
        /// Returns the payment amount for an annuity based on periodic, fixed payments and a fixed interest rate.
        /// </summary>
        /// <param name="rate">The interest rate per period. For example.</param>
        /// <param name="numberOfPaymentPeriods">The interest rate per period. For example.</param>
        /// <param name="presentValue">The present value (or lump sum) that a series of payments to be paid in the future is worth now.</param>
        /// <returns>The payment amount for an annuity based on periodic, fixed payments and a fixed interest rate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rate is less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rate is greater than 1.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of payments is less than or equal to zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the present value is less than or equal to zero.</exception>
        public static decimal GetPayment(decimal rate, int numberOfPaymentPeriods, decimal presentValue)
        {
            if (rate < 0)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be less than 0.");
            }
            else if (rate > 1)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be greater than 1.");
            }
            else if (numberOfPaymentPeriods <= 0)
            {
                throw new ArgumentOutOfRangeException("numberOfPaymentPeriods", "The argument cannot be less than or equal to 0.");
            }
            else if (presentValue <= 0)
            {
                throw new ArgumentOutOfRangeException("presentValue", "The argument cannot be less than or equal to 0.");
            }
            else
            {
                decimal futureValue = 0;
                decimal type = 0;
                decimal payment = 0;

                if (rate == 0)
                    payment = presentValue / numberOfPaymentPeriods;
                else
                    payment = rate * (futureValue + presentValue * (decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods)) / (((decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods) - 1) * (1 + rate * type));

                return Math.Round(payment, 2);
            }
        }
    }
}
