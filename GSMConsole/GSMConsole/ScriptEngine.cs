using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSMConsole {
    class ScriptEngine {
        private List<string> _commandsList;

        public ScriptEngine()
        {
            _commandsList = new List<string>();
        }

        public void LoadScript(string fileName)
        {
            using (var file = new StreamReader(fileName)) {
                _commandsList.Clear(); // future proofing/sanity check

                string line;
                while ((line = file.ReadLine()) != null) {
                    _commandsList.Add(line);
                }
            }
        }

        private string GetCommand(string scriptString)
        {
            string command = "";
            foreach (var c in scriptString) {
                if (c == ' ') {
                    break;
                } else {
                    command += c;
                }
            }
            return command.ToUpper();
        }

        private string GetStringParam(string scriptString)
        {
            string[] text = scriptString.Split('"');
            return text[1];
        }

        private int GetIntParam(string scriptString)
        {
            int intParam = 0;
            string[] text = scriptString.Split('"');
            if (int.TryParse(text[2], out intParam) == false) {
                Console.WriteLine(@"\t\tError: Invalid count parameter 
                                    for PrintStringLoop command, use an integer only.");
            }
            return intParam;
        }

        public void RunScript()
        {
            for (int i = 0; i < _commandsList.Count; i++) {
                string command = GetCommand(_commandsList[i]);

                switch (command) {
                    case "PRINTSTRING":
                        string text = GetStringParam(_commandsList[i]);
                        if (text != "") {
                            Console.WriteLine("\t{0}", text);
                        }
                        break;
                    case "PRINTSTRINGLOOP":
                        int loopCnt = GetIntParam(_commandsList[i]);
                        string loopText = GetStringParam(_commandsList[i]);

                        if (loopText != "") {
                            for (int j = 0; j < loopCnt; j++) {
                                Console.WriteLine("\t{0}: {1}", j, loopText);
                            }
                        }
                        break;
                    case "NEWLINE":
                        Console.WriteLine();
                        break;
                    case "WAITFORKEYPRESS":
                        while (true) {
                            if (Console.KeyAvailable)
                                Environment.Exit(0);
                        }
                    default:
                        Console.WriteLine("\t\tError: Invalid Command");
                        break;
                }
            }
        }
    }
}
