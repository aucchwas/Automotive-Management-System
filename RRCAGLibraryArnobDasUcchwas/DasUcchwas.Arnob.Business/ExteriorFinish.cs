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
    /// <summary>
    /// Represents the Exterior Finish of the vahicle.
    /// </summary>
    public enum ExteriorFinish
    {
        /// <summary>
        /// The standard exterior finish.
        /// </summary>
        Standard = 0,

        /// <summary>
        /// The pearlized exterior finish.
        /// </summary>
        Pearlized = 1,

        /// <summary>
        /// The custom exterior finish.
        /// </summary>
        Custom = 2,

        /// <summary>
        /// None of the exterior finishes.
        /// </summary>
        None = 3
    }
}
