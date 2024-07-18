namespace FolderCleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\path\to\folder"; // Укажите путь к папке, которую нужно очистить

            CleanFolder(folderPath, TimeSpan.FromMinutes(30)); // Очистить папку от файлов и папок, не использовавшихся более 30 минут
        }

        static void CleanFolder(string folderPath, TimeSpan maxAge)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);

                // Удаление файлов
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.LastAccessTime < DateTime.Now - maxAge)
                    {
                        file.Delete();
                        Console.WriteLine($"Удален файл: {file.FullName}");
                    }
                }

                // Удаление пустых папок
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    if (subDirectory.GetFiles().Length == 0 && subDirectory.GetDirectories().Length == 0 && subDirectory.LastAccessTime < DateTime.Now - maxAge)
                    {
                        subDirectory.Delete(true);
                        Console.WriteLine($"Удалена папка: {subDirectory.FullName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}