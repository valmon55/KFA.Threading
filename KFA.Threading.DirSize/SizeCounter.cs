public static class SizeCounter
{
    /// <summary>
    /// Возможные единицы измерения
    /// </summary>
    private static readonly string[] MeasureUnits = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    /// <summary>
    /// публичный метод для получения строки с данными об объеме директории 
    /// </summary>
    public static string GetDirSize(string path)
    {
        var dir = new DirectoryInfo(path);
        var sizeBytes = DirSize(dir);
        return FormatSize(sizeBytes);
    }
    
    /// <summary>
    ///  Получаем объем директории вместе со всеми вложенными файлами
    /// </summary>
    private static long DirSize(DirectoryInfo d) 
    {    
        long size = 0;  
        
        // Суммируем размер файлов
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis) 
        {      
            size += fi.Length;    
        }

        // Суммируем размер директорий
        try
        {
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
        }
        catch(Exception ex) 
        { 
            Console.WriteLine(d.FullName + ":" + ex.ToString());
        }
        return size;  
    }
    
    /// <summary>
    /// Переводим байты в более удобные единицы в зависимости от объема директории
    /// </summary>
    private static string FormatSize(Int64 value, int decimalPlaces = 1)
    {
        if (value < 0)
            return "-" + FormatSize(-value, decimalPlaces);

        int i = 0;
        decimal dValue = value;
        
        while (Math.Round(dValue, decimalPlaces) >= 1000)
        {
            dValue /= 1024;
            i++;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, MeasureUnits[i]);
    }
}