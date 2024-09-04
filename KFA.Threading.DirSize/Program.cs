using System.Reflection;
using System.Text;

namespace KFA.Threading.DirSize
{
    internal class Program
    {
        private static string SizeInfo;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Получаем путь к текущей директории
            var currentDir = Directory.GetCurrentDirectory();

            // Создание и запуск потока с помощью ParametherizedThreadStart
            var sizeCounterThread = new Thread(CountSize);
            sizeCounterThread.Start(currentDir);

            Console.Write($"Выполняется");

            // Пока наш поток выполняет работу по сканированию файлов - показываем пользователю простейшую визуализацию в консоли
            // Этот цикл также не даст основному потоку завершиться раньше времени
            while (sizeCounterThread.IsAlive)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            Console.WriteLine($"Выполнено");

            // Как только второй поток закончил свою работу - он получает флаг IsAlive = false,
            // а основной поток выпадает из цикла и готов сообщить пользователю результат работы дополнительного
            Console.WriteLine($"Папка {currentDir} занимает {SizeInfo} на диске");
            Console.Read();
        }

        /// <summary>
        /// Вызов метода для подсчета размера директории с файлами
        /// </summary>
        static void CountSize(object? dir)
        {
            if (dir != null)
                SizeInfo = SizeCounter.GetDirSize((string)dir);
        }
    }
}