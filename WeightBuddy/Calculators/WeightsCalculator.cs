using System;
using System.Collections.Generic;
using WeightBuddy.Models;
using System.Linq;

namespace WeightBuddy.Calculators
{
    /// <summary>
    /// Weight calculator.
    /// </summary>
    public static class WeightsCalculator
    {
        /// <summary>
        /// Gets the weights needed to load on the bar.
        /// </summary>
        /// <returns>The weights.</returns>
        /// <param name="model">Model.</param>
        public static List<KeyValuePair<string, int>> GetWeights(Weights model)
        {
            var weightVal = model.DesiredLoad - model.Bar;
            var toLoad = new List<KeyValuePair<string, int>>();

            var allowedWeights = model.AllowableWeights.OrderByDescending(w => w).ToList();

            foreach (var wt in allowedWeights)
            {
                weightVal -= CheckWeight(weightVal, wt, ref toLoad);
            }

            return toLoad;
        }

        /// <summary>
        /// Checks the weight, if it can be added to the bar, it adds it to the list.
        /// </summary>
        /// <returns>The weight added.</returns>
        /// <param name="weightLeft">Weight left to load.</param>
        /// <param name="weightVal">Weight value to try and load.</param>
        /// <param name="currentWeights">Current weights.</param>
        private static int CheckWeight(int weightLeft, double weightVal, ref List<KeyValuePair<string, int>> currentWeights)
        {
            var count = (int)(weightLeft / (weightVal * 2));

            if (count > 0)
            {
                currentWeights.Add(new KeyValuePair<string, int>(weightVal.ToString(), count));
            }

            return (int)(count * (weightVal * 2));
        }
    }
}

