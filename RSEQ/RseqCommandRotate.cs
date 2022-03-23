using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;

namespace IngameScript.RSEQ
{
    class RseqCommandRotate : IRseqCommand
    {
        private string m_target;
        private float m_value;
        private bool m_isRelative;

        public RseqCommandRotate(string target, string value, bool isRelative)
        {
            m_target = target;
            m_isRelative = isRelative;
            m_value = float.Parse(value);
        }

        public void run()
        {
            IMyTerminalBlock[] rotors = RseqIndex.GetDevices(m_target);
            foreach (IMyMotorStator rotor in rotors)
            {
                if (rotor.TargetVelocityRPM > 0)
                    rotor.TargetVelocityRPM = -rotor.TargetVelocityRPM;
                else if (rotor.TargetVelocityRPM == 0)
                    rotor.TargetVelocityRPM = -5;

                float newValue = m_value;
                if (m_isRelative)
                    newValue = rotor.Angle * (180 / (float)Math.PI);

                rotor.UpperLimitDeg = m_value;
                rotor.LowerLimitDeg = m_value;
            }
        }
    }
}
