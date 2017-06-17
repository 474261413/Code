using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AiErLan.Utils
{
    public class MonitorProcess
    {
        private static Process _currentProcess;
        public static Process CurrentProcess
        {
            get
            {
                if (_currentProcess == null)
                    _currentProcess = Process.GetCurrentProcess();
                return _currentProcess;
            }
        }

        private static AiErLan.Utils.MonitorProcess _monitorProcess;
        public static MonitorProcess InstanceMonitorProcess
        {
            get
            {
                if (_monitorProcess == null)
                    _monitorProcess = new Utils.MonitorProcess();
                return _monitorProcess;
            }
        }


        #region Private Static Var
        private Dictionary<int, PerformanceCounter> _counterPool;
        private Dictionary<int, DateTime> _updateTimePool;
        private Dictionary<int, int> _cpuUsagePool;
        #endregion
        public MonitorProcess()
        {
        }
        public Process GetProcessByName(string name)
        {
            return null;// this.processes.FirstOrDefault(p=>p.)
        }


        public int GetCurrentProcessCount()
        {
            return GetCpuUsage(CurrentProcess);
        }

        public string GetCurrentCpuUsage()
        {
            return string.Format("process:{0},占用计数:{1}", CurrentProcess.ProcessName, GetCurrentProcessCount());
        }

        #region Private Static Property
        private Dictionary<int, PerformanceCounter> m_CounterPool
        {
            get
            {
                return _counterPool ?? (_counterPool = new Dictionary<int, PerformanceCounter>());
            }
        }

        private Dictionary<int, DateTime> m_UpdateTimePool
        {
            get
            {
                return _updateTimePool ?? (_updateTimePool = new Dictionary<int, DateTime>());
            }
        }

        private Dictionary<int, int> m_CpuUsagePool
        {
            get
            {
                return _cpuUsagePool ?? (_cpuUsagePool = new Dictionary<int, int>());
            }
        }
        #endregion


        #region Private Static Method
        private string GetProcessInstanceName(int pid)
        {
            var category = new PerformanceCounterCategory("Process");

            var instances = category.GetInstanceNames();
            foreach (var instance in instances)
            {

                using (var counter = new PerformanceCounter(category.CategoryName,
                     "ID Process", instance, true))
                {
                    int val = (int)counter.RawValue;
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }
            throw new ArgumentException("Invalid pid!");
        }

        private int GetCpuUsage(int pid)
        {
            if (!m_CounterPool.ContainsKey(pid))
            {
                m_CounterPool.Add(pid, new PerformanceCounter("Process", "% Processor Time", GetProcessInstanceName(pid)));
            }

            var lastUpdateTime = default(DateTime);

            m_UpdateTimePool.TryGetValue(pid, out lastUpdateTime);

            var interval = DateTime.Now - lastUpdateTime;

            if (interval.TotalSeconds > 1)
            {
                m_CpuUsagePool[pid] = (int)(m_CounterPool[pid].NextValue() / Environment.ProcessorCount);
            }

            return m_CpuUsagePool[pid];
        }
        #endregion


        #region Public Static Method
        public string GetInstanceName(Process process)
        {
            return GetProcessInstanceName(process.Id);
        }

        public int GetCpuUsage(Process process)
        {
            return GetCpuUsage(process.Id);
        }
        #endregion
    }
}
