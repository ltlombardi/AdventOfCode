using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Add the day you want to execute in the daysToExecute Array.
            var daysToExecute = new int[] { 10, 14 };
            var IDoableClasses = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IDoable))).Select(t => Activator.CreateInstance(t)).Cast<IDoable>();
            var daysToExecuteString = daysToExecute.Select(d => "Day" + d);
            foreach (var item in IDoableClasses) { if (daysToExecuteString.Contains(item.GetType().Name)) item.Do(); }

            Console.ReadLine();
        }
    }
}
