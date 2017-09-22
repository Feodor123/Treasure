using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Treasure_
{
    public class Connector
    {
        public int Connect()
        {
            RTCPeerConnection rtppeerConnection = new RTCPeerConnection(iceServers: this.iceServers);
        }
    }
}
