using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.RSEQ
{
    class RseqCommandFactory
    {
        private static Action<string> _echo;
        private static int lineno = 0;
        private static string currentSequence = "default";

        public static void Init(MyGridProgram program)
        {
            _echo = program.Echo;
        }

        public static RseqSequence newSequence(string[] tokens)
        {
            if (tokens[0] != "SEQ")
                return null;
            return new RseqSequence(tokens[1]);
        }

        public static IRseqCommand newCommand(string[] tokens)
        {
            // Syntax is:
            // <modifier...> [command] [target] <argument>

            int argumentIndex = 0;
            bool isRelative = false;

            // Is the command a control command?
            if (RseqGrammar.ControlGrammar.Contains(tokens[argumentIndex]))
            {
                switch (tokens[argumentIndex])
                {
                    case "RUN":
                        return new RseqCommandRun(tokens[argumentIndex + 1]);
                    case "CALL":
                        RseqIndex.AddDeviceOrGroup(tokens[argumentIndex + 1]);
                        return new RseqCommandCall(tokens[argumentIndex + 1], tokens[argumentIndex + 2]);
                    case "SET":
                        RseqIndex.AddDeviceOrGroup(tokens[argumentIndex + 1]);
                        return new RseqCommandSet(tokens[argumentIndex + 1], tokens[argumentIndex + 2], tokens[argumentIndex + 3]);
                    default:
                        return null;
                }
            }

            // Is there a modifier before the command?
            if (RseqGrammar.ModifierGrammar.Contains(tokens[argumentIndex]))
            {
                switch (tokens[argumentIndex])
                {
                    case "RELATIVE":
                        isRelative = true;
                        break;
                    default:
                        break;
                }
                argumentIndex++;
            }

            if (RseqGrammar.MovementGrammar.Contains(tokens[argumentIndex]))
            {
                switch(tokens[argumentIndex])
                {
                    case "ROTATE":
                        RseqIndex.AddDeviceOrGroup(tokens[argumentIndex + 1]);
                        return new RseqCommandRotate(tokens[argumentIndex + 1], tokens[argumentIndex + 2], isRelative);
                    case "EXTEND":
                        RseqIndex.AddDeviceOrGroup(tokens[argumentIndex + 1]);
                        return new RseqCommandExtend(tokens[argumentIndex + 1], tokens[argumentIndex + 2], isRelative);
                }
            }

            return null;
        }

        public static string[] tokenize(string line)
        {
            string[] words = line.Split(' ');
            List<string> tokens = new List<string>();

            StringBuilder compositeString = null;
            foreach (var word in words)
            {
                if (word.StartsWith("\"") && word.EndsWith("\""))
                {
                    tokens.Add(word.Replace("\"", ""));
                }
                else if (word.StartsWith("\""))
                {
                    compositeString = new StringBuilder();
                    compositeString.Append(word.Replace("\"", ""));
                    compositeString.Append(" ");
                }
                else if (word.EndsWith("\""))
                {
                    compositeString.Append(word.Replace("\"", ""));
                    tokens.Add(compositeString.ToString().Trim());
                    compositeString = null;
                }
                else
                {
                    if (compositeString == null)
                        tokens.Add(word);
                    else
                    {
                        compositeString.Append(word);
                        compositeString.Append(" ");
                    }
                }
            }
            return tokens.ToArray();
        }

        public static void parseLine(string line)
        {
            lineno++;

            string[] tokens = tokenize(line);
            _echo($"Line {lineno}: {string.Join(", ", tokens)}");

            RseqSequence sequence = newSequence(tokens);
            if (sequence != null)
            {
                RseqIndex.AddSequence(sequence);
                currentSequence = sequence.Name;
                return;
            }

            IRseqCommand command = newCommand(tokens);
            if (command != null)
            {
                RseqIndex.GetSequence(currentSequence).AddCommand(command);
                return;
            }

            throw new Exception($"Command line unknown (line {lineno})!");
        }

        public static void parse(string text)
        {
            foreach (var line in text.Split('\n'))
            {
                if (line.Trim().Length < 5)
                    continue;
                parseLine(line.Trim());
            }
        }
    }
}
