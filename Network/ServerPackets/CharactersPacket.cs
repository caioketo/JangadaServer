using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JangadaServer.Enums;

namespace JangadaServer.Network.ServerPackets
{
    public class CharactersPacket
    {
        public List<Character> Characters = new List<Character>();

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.Characters);
            message.AddUInt16((ushort)Characters.Count);
            foreach (Character character in Characters)
            {
                character.Add(message);
            }
        }
      
        public CharactersPacket(List<Jangada.Character> characters)
        {
            foreach(Jangada.Character character in characters)
            {
                this.Characters.Add(new Character(character.Id, character.Name, character.Info));
            }
        }

        public class Character
        {
            public string Name;
            public string Info;
            public int Id;

            public void Add(NetworkMessage message)
            {
                message.AddUInt16((ushort)this.Id);
                message.AddString(this.Name);
                message.AddString(this.Info);
            }

            public Character(int id, string name, string info)
            {
                this.Id = id;
                this.Name = name;
                this.Info = info;
            }
        }
    }
}
