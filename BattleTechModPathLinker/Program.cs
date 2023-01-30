// See https://aka.ms/new-console-template for more information
namespace BattleTechModPathLinker;

public class Program
{
    private static bool _subFoldersOnly = false;
    private static bool _hasTwoArgs = false;
    private static string[] _directoryPaths = new string[2];
    public static void Main(string[] args)
    {
        SubFoldersOnly(args);
        DirectoryPaths(args);
        Console.WriteLine("Hello, World!");
        if (_hasTwoArgs)
        {
            Console.WriteLine("Directory 1 is: " + _directoryPaths[0]);
            Console.WriteLine("Directory 2 is: " + _directoryPaths[1]);
            if (_subFoldersOnly)
            {
                string[] BaseDir = Directory.GetDirectories(_directoryPaths[1]);
                foreach (var dir in BaseDir)
                {
                    MakeDirectorySymbolicLinks(_directoryPaths[0],dir);  
                }
            }
            else
            {
                MakeDirectorySymbolicLinks(_directoryPaths[0],_directoryPaths[1]);
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

