// See https://aka.ms/new-console-template for more information
namespace BattleTechModPathLinker;

public class Program
{

    public static void Main(string[] args)
    {
        bool hasTwoArgs = args.Length >= 2;
        Console.WriteLine("Hello, World!");
        switch (hasTwoArgs)
        {
            case true:
            {
                string[] basePath = Directory.GetDirectories(args[1]);

                foreach (var dir in basePath)
                {
                    MakeSymbolicLinks(args[0],dir);
                    Console.WriteLine(dir);
                }
            }
                break;
        }
    }
    private static void MakeSymbolicLinks(string destination, string source)
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
}

