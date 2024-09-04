using System.Reflection;

namespace KFA.Threading.DirSize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполняется");

            new Thread(() => 
            {
                Console.WriteLine(".");
                Thread.Sleep(1000);

            }).Start();
            
            new Thread(() => 
            {
                var progDir = Environment.CurrentDirectory;
                var dirSize = SizeCounter.GetDirSize(progDir);
                Console.WriteLine($"Выполнено{Environment.NewLine}");
                Console.WriteLine($"Папка {progDir} занимает {dirSize} на диске");
            }
            ).Start();
        }
    }
}