using System;
using System.Collections.Generic;

namespace WeightBuddy.Models
{
    /// <summary>
    /// Weights.
    /// </summary>
    public class Weights
    {
        /// <summary>
        /// Gets or sets the weight of the bar.
        /// </summary>
        /// <value>The bar weight.</value>
        public int Bar { get; set; }

        /// <summary>
        /// Gets or sets the desired load of the bar and weights.
        /// </summary>
        /// <value>The desired load.</value>
        public int DesiredLoad { get; set; }

        /// <summary>
        /// Gets or sets the allowable weights.
        /// </summary>
        /// <value>The allowable weights.</value>
        public List<double> AllowableWeights { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightBuddy.Models.Weights"/> class.
        /// </summary>
        public Weights()
        {
            Bar = 45;
            DesiredLoad = 0;
            AllowableWeights = new List<double>();
        }
    }
}

