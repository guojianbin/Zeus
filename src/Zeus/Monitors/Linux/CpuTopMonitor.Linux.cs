﻿using System;
using Zeus.Monitors.Linux.Commands;

namespace Zeus.Monitors.Linux
{
    public class CpuTopMonitor : ICpuMonitor
    {
        public CpuInfo GetCpuUsage()
        {
            string availableCpuAsString;
            decimal result = 0;
            if (Top.Execute().TryGetValue("cpu_free", out availableCpuAsString))
            {
                if (!decimal.TryParse(availableCpuAsString, out result))
                {
                    throw new ArgumentException("CPU usage cannot be parsed to float (" + availableCpuAsString + ")");
                }
            }
            return new CpuInfo(result);
        }
    }
}