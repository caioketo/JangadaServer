﻿using Jangada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class Parser
    {
        public static bool Parse(Networkmessage.Types.Type type, Networkmessage message, ClientConnection connection)
        {
            switch (type)
            {
                case Networkmessage.Types.Type.LOGIN:
                    Console.WriteLine("Login: " + message.LoginPacket.Login);
                    Console.WriteLine("Password: " + message.LoginPacket.Password);
                    List<Character> chars = new List<Character>();
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto")
                        .SetInfo("Level: 1")
                        .Build());
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto2")
                        .SetInfo("Level: 100")
                        .Build());
                    MessagesHelper.SendCharacterList(connection, chars);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
