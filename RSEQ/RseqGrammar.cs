using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngameScript.RSEQ
{
    class RseqGrammar
    {
        public static string[] DefinitionGrammar = { "SEQ" };
        public static string[] ControlGrammar = { "RUN", "CALL", "SET" };
        public static string[] MovementGrammar = { "ROTATE", "EXTEND" };
        public static string[] ModifierGrammar = { "RELATIVE" };

        public static string[] CommandGrammar = ControlGrammar.Union(MovementGrammar).ToArray();
        public static string[] FirstStageGrammar = CommandGrammar.Union(ModifierGrammar).ToArray();
    }
}
