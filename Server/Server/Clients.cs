﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Data;
using Data.Services;
using DMod.Models;

namespace Server
{
    class Clients
    {
        public int connectionID;
        public string ip;
        public TcpClient socket;
        public NetworkStream myStream;
        private byte[] readBuff;
        public ByteBuffer playerBuffer;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            myStream = socket.GetStream();
            readBuff = new byte[4096];
            myStream.BeginRead(readBuff, 0, socket.ReceiveBufferSize, OnReceiveData, null);
        }

        private void OnReceiveData(IAsyncResult result)
        {
            try
            {
                var readbytes = myStream.EndRead(result);
                if (readbytes <= 0)
                {
                    //client is not connected to the server anymore
                    CloseSocket(connectionID);
                    return;
                }
                var newBytes = new byte[readbytes];
                Buffer.BlockCopy(readBuff, 0, newBytes, 0, readbytes);
                ServerHandleData.HandleData(connectionID, newBytes);
                myStream.BeginRead(readBuff, 0, socket.ReceiveBufferSize, OnReceiveData, null);

            }
            catch (Exception)
            {

                CloseSocket(connectionID);
            }
        }

        public void CloseSocket(int index)
        {
            Cnsl.Log("Connection from " + ip + " has been terminated");
            var player = Program._userService.ActiveUsers.Find(p => p.Id == Globals.PlayerIDs[index]);
            if (player != null)
            {
                ServerTcp.SendMessage(-1,player.Name + @" has disconnected.", (int)ChatPackets.Notification);
                player.inGame = false;
                player.receiving = false;
            }
            Program._gameService.SaveGame(new List<User> { player });
            socket.Close();
            socket = null;
            Globals.PlayerIDs[index] = null;
            Program._userService.ActiveUsers.Remove(player);
        }
    }
}
