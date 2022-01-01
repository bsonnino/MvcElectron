namespace MvcElectron.Models;

public class FilesViewModel
{
    public List<FileInfo> Files { get; private set; }

    public FilesViewModel()
    {
        Files = GetFiles();
    }

    private List<FileInfo> GetFiles()
    {
        return new DirectoryInfo(Path)
        .GetFiles()
        .OrderByDescending(f => f.Length)
        .Take(15)
        .ToList();
    }

    public string Path => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public void DeleteFile(string fileName)
    {
        var filePath = Path + "\\" + fileName;
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        Files = GetFiles();
    }
}
