using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngameScript.RSEQ
{
    class RseqSequence
    {
        private string m_name;
        private List<IRseqCommand> m_commands;

        public RseqSequence(string name)
        {
            m_name = name;
            m_commands = new List<IRseqCommand>();
        }

        public void AddCommand(IRseqCommand command)
        {
            m_commands.Add(command);
        }

        public void run()
        {
            foreach (var command in m_commands)
                command.run();
        }

        public IEnumerable<IRseqCommand> GetCommands()
        {
            foreach (var command in m_commands)
            {
                yield return command;
            }
        }

        public string Name { get { return m_name; } }
    }
}
