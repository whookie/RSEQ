using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngameScript.RSEQ
{
    class RseqCommandRun : IRseqCommand
    {
        private string m_sequence;

        public RseqCommandRun(string sequence)
        {
            m_sequence = sequence;
        }

        public void run()
        {
            RseqSequence sequence = RseqIndex.GetSequence(m_sequence);
            foreach (var command in sequence.GetCommands())
                command.run();
        }
    }
}
