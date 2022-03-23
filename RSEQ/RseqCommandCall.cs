using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;

namespace IngameScript.RSEQ
{
    class RseqCommandCall : IRseqCommand
    {
        private string m_target;
        private string m_action;

        public RseqCommandCall(string target, string action)
        {
            m_target = target;
            m_action = action;
        }

        public void run()
        {
            IMyTerminalBlock[] blocks = RseqIndex.GetDevices(m_target);
            foreach (var device in blocks)
            {
                device.ApplyAction(m_action);
            }
        }
    }
}
