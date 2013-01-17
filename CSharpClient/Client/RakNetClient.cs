﻿using System;
using System.Collections.Generic;
using System.Text;
using RakNet;
using System.Threading;

namespace Client
{
    public enum RakNetClientState
    {
        None = 0,
        PeerInitError,
        PeerInitOK,
        StartUpError,
        StartUpOK,
        ConnectError,
        ConnectOK,
    }

    public class Log
    {

        public static void Debug(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class RakNetClient : IDisposable
    {
        class RakNetClientResultHandler : FT_ConnectProcessResultHandler
        {
            private RakNetClient _client;

            public RakNetClientResultHandler(RakNetClient client)
            {
                _client = client;
            }

            public override void OnConnectedToServer()
            {
                Log.Debug("OnConnectedToServer");
                _client.SuccessConnect();
            }

            public override void OnFailedToConnect()
            {
                Log.Debug("OnFailedToConnect");
                _client.FaildConnect();
            }

            public override void OnDisconnectedFromServer()
            {
                Log.Debug("OnDisconnectedFromServer");
            }

            public override void OnLostConnection()
            {
                Log.Debug("OnLostConnection");
            }

            public override void ReceiveLog()
            {
                Log.Debug("ReceiveLog");
            }

            public override void DebugReceive(int flag)
            {
                Log.Debug(flag.ToString());
            }

        }

        private RakNet.RakPeerInterface _peer;
        private RakNet.SocketDescriptor _socketDesc;
        private RakNet.Packet _packet;
        private string _sServerIP;
        private ushort _nServerPort;
        private RakNetClientState _State;
        private Thread _ReadThread;
        private bool _RequestReadThreadStop;
        private bool _bConnectSuccess;
        private System.Action<Packet> processMessageHandler;

        public RakNetClientState ClientState
        {
            get
            {
                return _State;
            }
            private set
            {
                _State = value;
            }
        }

        public RakNetClient(string ip, ushort port, System.Action<Packet> msgHandler)
        {
            _sServerIP = ip;
            _nServerPort = port;
            processMessageHandler = msgHandler;
            _RequestReadThreadStop = false;
            _bConnectSuccess = false;

            _socketDesc = new RakNet.SocketDescriptor();

            _ReadThread = new Thread(new ThreadStart(_RunRead));
            _ReadThread.IsBackground = true;

            _State = RakNetClientState.None;
        }

        public void InitPeer()
        {
            if (_peer != null) return;
            try
            {
                _peer = RakNet.RakPeerInterface.GetInstance();

                _State = RakNetClientState.PeerInitOK;
            }
            catch
            {
                _State = RakNetClientState.PeerInitError;
            }
        }

        public void Start()
        {
            InitPeer();

            if (_State == RakNetClientState.PeerInitOK)
            {
                StartupResult result = _peer.Startup(1, _socketDesc, 1);
                Log.Debug("Start Result: " + result);
                if (result == StartupResult.RAKNET_STARTED)
                {
                    _State = RakNetClientState.StartUpOK;

                    RakNet.FT_ConnectProcess process = new RakNet.FT_ConnectProcess();
                    process.SetResultHandler(new RakNetClientResultHandler(this));
                    AttachInterface2(process);

                    ConnectionAttemptResult connectResult = _peer.Connect(_sServerIP, _nServerPort, "", 0);
                    Log.Debug("Connect Result: " + connectResult);
                    if (ConnectionAttemptResult.CONNECTION_ATTEMPT_STARTED == connectResult)
                    {
                        _State = RakNetClientState.ConnectOK;

                        _ReadThread.Start();
                    }
                    else
                    {
                        _State = RakNetClientState.ConnectError;
                    }
                }
                else
                {
                    Log.Debug("RakNet.RakPeerInterface.GetInstance() Error : " + result);
                    _State = RakNetClientState.StartUpError;
                }
            }
        }

        public void AttachInterface2(PluginInterface2 plugin)
        {
            if (plugin != null)
            {
                _peer.AttachPlugin(plugin);
            }
        }

        void _RunRead()
        {
            while (!_RequestReadThreadStop)
            {
                _packet = _peer.Receive();
                while (_packet != null)
                {
                    ProcessMessage(_packet);
                    _peer.DeallocatePacket(_packet);
                    _packet = _peer.Receive();
                }
                Thread.Sleep(10);
            }
            Log.Debug("Thread Stop!");
        }

        void ProcessMessage (Packet packet){
            if ( processMessageHandler != null){
                processMessageHandler(packet);
            }
        }

        void SuccessConnect (){
            _bConnectSuccess = true;
        }

        void FaildConnect()
        {
            StopThread();
        }

        void StopThread()
        {
            _RequestReadThreadStop = true;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            StopThread();
            if (_peer != null)
            {
                _peer.Shutdown(300);
            }
        }

        #endregion
    }
}
