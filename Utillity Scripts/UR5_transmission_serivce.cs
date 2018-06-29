///////////////////////////////////////////////////////////////////////////////
//
//  Original System: UR5_transmission_service.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Standalone
//  Revision:        1.0 - 6/28/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Server and client for Unity Project will be utilized for the following:
//  -Transmitting data for manus and Vive Position
//  -Checking manus is active
//  -Transmitting the positional data of the UR5 robot
//
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Web_Socket_for_Manus
{
    public class UR5_transmission_serivce : MonoBehaviour
    {
        const string DEFAULT_SERVER = "localhost";
        const int DEFAULT_PORT = 27015;

        //Server socket stuff 
        System.Net.Sockets.Socket serverSocket;
        System.Net.Sockets.SocketInformation serverSocketInfo;

        //Client socket stuff 
        System.Net.Sockets.Socket clientSocket;
        System.Net.Sockets.SocketInformation clientSocketInfo;

        

        public string Startup()
        {
            // The chat server always starts up on the localhost, using the default port 
            IPHostEntry hostInfo = Dns.GetHostByName(DEFAULT_SERVER);
            IPAddress serverAddr = hostInfo.AddressList[0];
            var serverEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);

            // Create a listener socket and bind it to the endpoint 
            serverSocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            serverSocket.Bind(serverEndPoint);

            return serverSocket.LocalEndPoint.ToString();
        }

        public string Listen()
        {
            int backlog = 0;
            try
            {
                serverSocket.Listen(backlog);
                return "Server listening";
            }
            catch (Exception ex)
            {
                return "Failed to listen" + ex.ToString();
            }
        }


        public bool SendData(string textdata)
        {
            if (string.IsNullOrEmpty(textdata))
            {
                return false;
            }

            if (textdata.Trim().ToLower() == "exit")
            {
                return true;
            }

            // The chat client always starts up on the localhost, using the default port 
            IPHostEntry hostInfo = Dns.GetHostByName(DEFAULT_SERVER);
            IPAddress serverAddr = hostInfo.AddressList[0];
            var clientEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);

            // Create a client socket and connect it to the endpoint 
            clientSocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            clientSocket.Connect(clientEndPoint);

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(textdata);
            clientSocket.Send(byData);
            clientSocket.Close();

            return false;
        }

        public string ReceiveData()
        {
            System.Net.Sockets.Socket receiveSocket;
            byte[] buffer = new byte[256];

            receiveSocket = serverSocket.Accept();

            var bytesrecd = receiveSocket.Receive(buffer);

            receiveSocket.Close();

            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            return encoding.GetString(buffer);
        }


        Program prog;

        void Start() {
            prog = new Program();
            prog.start_up();
        }

        void Update()
        {
            prog.Main();
        }
    }

    class Program
    {
        string serverInfo;

        public void start_up()
        {
            serverInfo = server.Startup();
            Console.WriteLine("Server started at:" + serverInfo);
            Debug.Log("Server started at:" + serverInfo);
        }


        public void listen_me()
        {
            serverInfo = server.Listen();
            Console.WriteLine(serverInfo);

        }

        static UR5_transmission_serivce server = new UR5_transmission_serivce();
        public void Main()
        {
            //string remoteServerName = server.ParseArgs(args); 



            listen_me();



            //string datatosend = Console.ReadLine();
            string datatosend = "I am spam \n";
            server.SendData(datatosend);

            serverInfo = server.ReceiveData();
            Console.WriteLine(serverInfo);
           
            Debug.Log(serverInfo);

            Console.ReadLine();
            //exit 
        }
    }


}