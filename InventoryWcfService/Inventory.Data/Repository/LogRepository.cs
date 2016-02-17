using System;
using System.IO;
using Inventory.Data.Interfaces;

namespace Inventory.Data.Repository
{
    public class LogRepository : ILogRepository
    {

        private static readonly string Filename = Environment.CurrentDirectory + "\\LogInventory.txt";

        public void Add(string message)
        {
            using (StreamWriter sw = File.AppendText(Filename))
            {
                sw.WriteLine(string.Format("{0}: {1}.", DateTime.Now, message));
            }

        }

        public string GetLog()
        {
            return File.ReadAllText(Filename);
        }
       
    }
}