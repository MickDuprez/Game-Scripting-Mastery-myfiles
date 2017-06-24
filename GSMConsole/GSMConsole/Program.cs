using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMConsole {
    class Program {
        static void Main(string[] args)
        {
            ScriptEngine se = new ScriptEngine();
            
            se.LoadScript(@"..\..\..\ScriptCommands.txt");
            se.RunScript();

            // make the program wait for a key press otherwise it will
            // terminate and we'll miss it all!
            Console.ReadLine();
        }

    }
}
