﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.Devices;

namespace DotNet.AccelerateBall.Util
{
    public class MemoryInfo
    {
        private double allMemorySize;
        private Computer computer = new Computer();

        public MemoryInfo()
        {
            allMemorySize = computer.Info.TotalPhysicalMemory / 1024 / 1024;
        }

        public double getAllMemorySize()
        {
            return allMemorySize;
        }

        public double getAvailMemorySize()
        {
            return computer.Info.AvailablePhysicalMemory / 1024 / 1024;
        }

        public double getUsedMemorySize()
        {
            return allMemorySize - getAvailMemorySize();
        }

        public string getUsedMemoryRate()
        {
            return String.Format("{0:N0}", (getUsedMemorySize() / allMemorySize) * 100);
        }
    }
}
