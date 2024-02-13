using System.Reflection.Metadata.Ecma335;

namespace RoomData
{
    public class ObjectEntry // https://wiki.spiralframework.info/view/RoomObject
    {
        public int Type { get; set; } // ??

        public int Id { get; set; } // Id used for characters/objects in scrips

        public uint ModelId { get; set; } // determines whether to show or hide model

        public float[] Position { get; set; } = new float[3]; // X,y,z  

        public float[] Size { get; set; } = new float[2]; // Width, Height

        public float Rotation { get; set; }

        public int Unknown { get; set; }

        //private static float scale { get; } = 1;

        public void ReadData(Stream stream)
        {
            byte[] buffer = new byte[4];

            stream.Read(buffer);
            Type = BitConverter.ToInt32(buffer);

            stream.Read(buffer);
            Id = BitConverter.ToInt32(buffer);

            stream.Read(buffer);
            ModelId = BitConverter.ToUInt32(buffer);


            for (int i = 0; i < Position.Length; i++)
            {
                stream.Read(buffer);
                Position[i] = (float)BitConverter.ToSingle(buffer);
            }

            for (int i = 0; i < Size.Length; i++)
            {
                stream.Read(buffer);
                Size[i] = (float)BitConverter.ToSingle(buffer);
            }

            stream.Read(buffer);
            Rotation = (float)BitConverter.ToSingle(buffer);

            stream.Read(buffer);
            Unknown = BitConverter.ToInt32(buffer);
        }

        public void WriteData(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(Type));

            stream.Write(BitConverter.GetBytes(Id));

            stream.Write(BitConverter.GetBytes(ModelId));

            for (int i = 0; i < Position.Length; i++)
            {
                stream.Write(BitConverter.GetBytes(Position[i]));
            }

            for (int i = 0; i < Size.Length; i++)
            {
                stream.Write(BitConverter.GetBytes(Size[i]));
            }

            stream.Write(BitConverter.GetBytes(Rotation));

            stream.Write(BitConverter.GetBytes(Unknown));
        }

    }
}
