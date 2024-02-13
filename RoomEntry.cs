using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoomData
{
    public class RoomEntry
    {
        public List<ObjectEntry> Objects { get; set; } = new List<ObjectEntry>();

        public void ReadData(Stream stream)
        {
            stream.Position = 0;
            while (stream.Position < stream.Length)
            {
                ObjectEntry entry = new ObjectEntry();
                entry.ReadData(stream);
                Objects.Add(entry);
            }
        }
        public void WriteData(Stream stream)
        {
            stream.Position = 0;
            foreach (ObjectEntry entry in  Objects) 
            {
                entry.WriteData(stream);
            }
        }
        public string ToJson()
        {
            JsonSerializerOptions opt = new JsonSerializerOptions()
            {
                WriteIndented = true,
               
            };
            return JsonSerializer.Serialize(Objects, options: opt); ;
        }
        public void ReadJson(string js)
        {
            Objects = JsonSerializer.Deserialize<List<ObjectEntry>>(js);
        }
    }
}
