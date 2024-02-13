// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using RoomData;

public partial class Program
{
    
    public static bool Loop(string[] args)
    {
        Console.Clear();
        Console.Title = "Room Editor";
        string filePath = "";
        if (args.Length > 0 ) 
        {
            filePath = args[0];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Drag and Drop the Room Object File\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("(Always the third file in a map PAK file)\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("OR\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("The JSON you want to repack\n\n");
            filePath = Console.ReadLine().Replace("\"", string.Empty);
        }
        if (!filePath.EndsWith(".json"))
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            RoomEntry entry = new RoomEntry();
            entry.ReadData(stream);
            stream.Close();
            string json = entry.ToJson();
            File.WriteAllText(filePath + ".json", json);
            Process.Start("notepad.exe", filePath + ".json");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Extracted! Opening in notepad...");

        }
        else
        {
            RoomEntry entry = new RoomEntry();
            entry.ReadJson(File.ReadAllText(filePath));
            FileStream stream = new FileStream(filePath + "_repacked", FileMode.Create, FileAccess.Write);
            entry.WriteData(stream);
            stream.Close();
            entry = null;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Repacked!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(filePath + "_repacked");

        }

        return args.Length > 0;
    }
    public static void Main(string[] args)
    {
        while (true) 
        {
            if (Loop(args))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Press any key to close");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}

