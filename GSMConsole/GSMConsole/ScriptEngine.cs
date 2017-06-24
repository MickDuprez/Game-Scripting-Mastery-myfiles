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
                while((line = file.ReadLine()) != null) {
                    _commandsList.Add(line);
                }
            }
        }

        public void RunScript()
        {
            for(int i = 0; i < _commandsList.Count; i++) {
                string commandStr = _commandsList[i];
                string[] commandParams = commandStr.Split('"');
                string command = commandParams[0].ToUpper();

                switch (command) {
                    case "PRINTSTRING":
                        if (commandParams[1] != null){
                            Console.WriteLine("\t" + commandParams[1]);
                        }
                        break;
                    case "PRINTSTRINGLOOP":
                        if (commandParams[2] != null) {
                            int loopCnt;
                            if (int.TryParse(commandParams[2], out loopCnt)){
                                for (int j = 0; j < loopCnt; j++) {
                                    Console.WriteLine("\t{0}: {1}", j, commandParams[1]);
                                }
                            }else {
                                Console.WriteLine(
                                    "\t\tError: Invalid count parameter for PrintStringLoop command, use an integer only."
                                    );
                                break;
                            }  
                        }
                        break;
                    case "NEWLINE":
                        Console.WriteLine();
                        break;
                    case "WAITFORKEYPRESS":
                        while (!Console.KeyAvailable) {
                            // just waiting for a key stroke...
                        }
                        break;
                    default:
                        Console.WriteLine("\t\tError: Invalid Command");
                        break;
                }
            }
        }
    }
}
