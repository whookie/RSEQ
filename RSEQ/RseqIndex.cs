using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;

namespace IngameScript.RSEQ
{
    class RseqIndex
    {
        private static IMyGridTerminalSystem GridTerminalSystem;

        private static Dictionary<string, RseqSequence> sequences = new Dictionary<string, RseqSequence>()
        {
            { "default", new RseqSequence("default") }
        };
        private static Dictionary<string, List<IMyTerminalBlock>> deviceIndex = new Dictionary<string, List<IMyTerminalBlock>>();

        public static void Init(MyGridProgram program)
        {
            GridTerminalSystem = program.GridTerminalSystem;
        }

        public static bool AddDeviceOrGroup(string name)
        {
            if (deviceIndex.ContainsKey(name))
                return true;

            IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName(name);
            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            if (group != null)
            {
                group.GetBlocks(blocks);
            }
            else
            {
                IMyTerminalBlock block = GridTerminalSystem.GetBlockWithName(name);
                if (block == null)
                {
                    return false;
                }
                blocks.Add(block);
            }

            deviceIndex.Add(name, blocks);
            return true;
        }

        public static IMyTerminalBlock[] GetDevices(string name)
        {
            if (deviceIndex.ContainsKey(name))
                return deviceIndex[name].ToArray();
            return null;
        }
    
        public static void AddSequence(RseqSequence seq)
        {
            sequences.Add(seq.Name, seq);
        }

        public static RseqSequence GetSequence(string name)
        {
            return sequences.GetValueOrDefault(name, null);
        }
    }
}
