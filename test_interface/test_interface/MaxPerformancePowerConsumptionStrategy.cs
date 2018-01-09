using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_interface
{
    class MaxPerformancePowerConsumptionStrategy : IPowerConsumptionStrategy
    {
        public string StrategyName { get; set; }

        public string GetEstimatedTime(int batteryLevel)
        {
            return (batteryLevel * 3) / 2 + " minutes";
        }
    }
}
