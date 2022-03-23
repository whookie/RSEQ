using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;

namespace IngameScript.RSEQ
{
    class RseqCommandExtend : IRseqCommand
    {
        private string m_target;
        private float m_value;
        private bool m_isRelative;

        public RseqCommandExtend(string target, string value, bool isRelative = false)
        {
            m_target = target;
            m_isRelative = isRelative;
            m_value = float.Parse(value);
        }

        public void run()
        {
            IMyTerminalBlock[] blocks = RseqIndex.GetDevices(m_target);
            foreach (IMyPistonBase piston in blocks)
            {
                float currentValue = piston.CurrentPosition;
                float newLimit = m_value;

                if (m_isRelative)
                    newLimit = currentValue - m_value;

                piston.MinLimit = newLimit;
                piston.MaxLimit = newLimit;

                if (newLimit > currentValue)
                    piston.Extend();
                else
                    piston.Retract();
            }
        }
    }
}
