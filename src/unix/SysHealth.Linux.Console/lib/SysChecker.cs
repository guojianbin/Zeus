﻿namespace SysHealth.Linux.Console.lib
{
    using System;
    using System.Collections.Generic;


    public abstract class SysChecker
    {
        protected int memory_conversion_value = 1024;


        public SysChecker(HealthCheckerConfiguration config = null)
        {
            this.config = config;
        }


        private HealthCheckerConfiguration config { get; set; }


        public abstract int GetTotalPhysicalMemory();

        public abstract int GetAvailablePhysicalMemory();

        public abstract int GetUsedPhysicalMemory();

        public abstract IEnumerable<DriveMeta> GetAllDrives();

        public abstract DriveMeta GetDriveInfo(string driveName);

        public abstract float GetCPUUsage(string processName);

        public abstract string GetFullStatus();


        public void CheckAvailablePhysicalMemoryForCritical(float result)
        {
            if (config != null && config.CriticalPointAvailablePhysicalMemory != null)
            {
                if (result <= config.CriticalPointAvailablePhysicalMemory)
                    throw new ArgumentOutOfRangeException("physical_memory", "CRITICAL! Current machine physical memory is above the critical point - " + result + " MB");
            }
        }

        public void CheckDrivesForCritical(IEnumerable<DriveMeta> drivesArray)
        {
            if (config != null && config.CriticalPointDriveFreeSpace != null)
            {
                foreach (var drive in drivesArray)
                {
                    if (drive.TotalFreeSpace / memory_conversion_value <= config.CriticalPointDriveFreeSpace)
                        throw new ArgumentOutOfRangeException("drive_memory", string.Format("CRITICAL! Current machine drive \"{0}\" available free space is below the critical point - {1} MB", drive.Name, (drive.TotalFreeSpace / memory_conversion_value).ToString("N0")));
                }

            }
        }

        public void CheckCPUForCritical(float secondValue)
        {
            if (config != null && config.CriticalPointCPU != null)
            {
                if (secondValue >= config.CriticalPointCPU)
                    throw new ArgumentOutOfRangeException("cpu", "CRITICAL! Current machine CPU usage is above the critical point - " + secondValue + " %");
            }
        }

    }
}

