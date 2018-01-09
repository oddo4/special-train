using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_interface
{
    class Computer
    {
        public IPowerConsumptionStrategy BatteryConsumptionStrategy = new BalancedPowerConsumption();
        private int batteryLevel;

        public int BatteryLevel
        {
            get { return batteryLevel; }
            set { batteryLevel = value; }
        }

        public string ComputerName { get; set; }

        public string GetEstimatedBatteryTime()
        {
            
            return BatteryConsumptionStrategy.GetEstimatedTime(BatteryLevel);
        }

        public Computer(string ComputerName, int BatteryLevel)
        {
            this.ComputerName = ComputerName;
            this.BatteryLevel = BatteryLevel;
        }


    }
}
