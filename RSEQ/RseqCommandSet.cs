using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;

namespace IngameScript.RSEQ
{
    class RseqCommandSet : MyGridProgram, IRseqCommand
    {
        private string m_target;
        private string m_action;
        private string m_value;

        public RseqCommandSet(string target, string action, string value)
        {
            m_target = target;
            m_action = action;
            m_value = value;
        }

        public void run()
        {
            IMyTerminalBlock[] blocks = RseqIndex.GetDevices(m_target);
            foreach (var block in blocks)
            {
                try { block.SetValueBool(m_action, bool.Parse(m_value)); } catch { }
                try { block.SetValueFloat(m_action, float.Parse(m_value)); } catch { }
                try { block.SetValue(m_action, m_value); } catch { }
            }
        }
    }
}
