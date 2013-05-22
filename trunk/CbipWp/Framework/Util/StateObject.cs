using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lxt2.Communication.Framework.Util
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Receive buffer.
        public byte[] buffer;

        public StateObject(int bufferSize)
        {
            buffer = new byte[bufferSize];
        }
    }
}
