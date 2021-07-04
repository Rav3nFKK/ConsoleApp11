using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(File.AppendText("log.txt")));
            Debug.AutoFlush = true;
            MrDjo j = new MrDjo();
            j.Reshenie();

        }
    }
}
