// See https://aka.ms/new-console-template for more information
namespace BattleTechModPathLinker;

public class Program
{
    private static bool _modsDirectory = false;
    private static bool _filesOnly = false;
    private static bool _subFoldersOnly = false;
    private static bool _hasTwoArgs = false;
    private static string[] _directoryPaths = new string[2];
    public static void Main(string[] args)
    {
        FilesOnly(args);
        SubFoldersOnly(args);
        DirectoryPaths(args);
        ModsDirectory(args);
        if (_hasTwoArgs && !(_subFoldersOnly || _filesOnly))
        {
            Console.WriteLine("Directory 1 is: " + _directoryPaths[0]);
            Console.WriteLine("Directory 2 is: " + _directoryPaths[1]);
            MakeDirectorySymbolicLinks(_directoryPaths[0],_directoryPaths[1]);

            if (_modsDirectory)
            {
                string dirName = "Mods";
                Directory.Move(_directoryPaths[0] + Path.DirectorySeparatorChar + new DirectoryInfo(_directoryPaths[1]).Name,
                    _directoryPaths[0] + Path.DirectorySeparatorChar + dirName);
            }
        }else if (_subFoldersOnly && !(_modsDirectory || _filesOnly))
        {
            string[] BaseDir = Directory.GetDirectories(_directoryPaths[1]);
            foreach (var dir in BaseDir)
            {
                MakeDirectorySymbolicLinks(_directoryPaths[0],dir);  
            }
        }
        else if (_filesOnly && !(_modsDirectory || _subFoldersOnly))
        {
            string[] BaseDir = Directory.GetFiles(_directoryPaths[1]);
            foreach (var dir in BaseDir)
            {
                MakeDirectorySymbolicLinks(_directoryPaths[0],dir);  
            }
        }
    }
    private static void MakeDirectorySymbolicLinks(string destination, string source)
    {
        try
        {
            Directory.CreateSymbolicLink(destination + Path.DirectorySeparatorChar + new DirectoryInfo(source).Name, source);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void SubFoldersOnly(string[] args)
    {
        foreach (var arg in args)
        {
            if (arg == "--S")
            {
              _subFoldersOnly = true;
            }
        }
    }

    private static void FilesOnly(string[] args)
    {
        foreach (var arg in args)
        {
            if (arg == "--F")
            {
                _filesOnly = true;
            }
        }
    }

    private static void ModsDirectory(string[] args)
    {
        foreach (var arg in args)
        {
            if (arg == "--M")
            {
                _modsDirectory = true;
            }
        }
    }

    private static void DirectoryPaths(string[] args)
    {
        int numFound = 0;
        foreach (var arg in args)
        {
            if (Directory.Exists(arg) && _hasTwoArgs == false)
            {
                _directoryPaths[numFound] = arg;
                numFound++;
            }

            if (numFound == 2)
            {
                _hasTwoArgs = true;
            }
        }
    }
}

