// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using RoomData;

public partial class Program
{
    
    public static bool Loop(string[] args)
    {
        string filePath = "";
        if (args.Length > 0 ) 
        {
            filePath = args[0];
        }
        else
        {
            Console.WriteLine("Please Drag and Drop the room objects file\nor json you want to repack");
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
        }
        else
        {
            RoomEntry entry = new RoomEntry();
            entry.ReadJson(File.ReadAllText(filePath));
            FileStream stream = new FileStream(filePath + "_repacked", FileMode.Create, FileAccess.Write);
            entry.WriteData(stream);
            stream.Close();
            entry = null;
        }

        return args.Length > 0;
    }
    public static void Main(string[] args)
    {
        while (true) 
        {
            if (Loop(args))
            {
                Console.WriteLine("Press any key to close");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}

