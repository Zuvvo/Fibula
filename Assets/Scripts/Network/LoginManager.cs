using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using Fibula;

namespace Fibula.Login
{


    public class LoginManager : MonoBehaviour
    {

        public static int userID { private set; get; }
        public static bool isLoggedIn { private set; get; }

        public delegate void SuccessfulLoginEventHandler(int userID, bool hasuma);
        public static event SuccessfulLoginEventHandler onSuccessfullLogin;

        public static HashType myHashType = HashType.SHA256;

        public static void Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return;

            using (DarkRiftWriter writer = new DarkRiftWriter())
            {
                writer.Write(username);
                writer.Write(HashHelper.ReturnHash(password,myHashType));
                SendToServer(NT.LoginT, NT.LoginS.loginUser, writer);
            }
        }
        public static void AddUser(string _username, string _password)
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password)) return;
            using (DarkRiftWriter writer = new DarkRiftWriter())
            {
                writer.Write(_username);
                writer.Write(HashHelper.ReturnHash(_password, myHashType));
                SendToServer(NT.LoginT, NT.LoginS.addUser, writer);
            }

        }
        private static void SendToServer(byte tag, ushort subject, object data)
        {
            if (DarkRiftAPI.isConnected)
            {
                DarkRiftAPI.SendMessageToServer(tag, subject, data);
            }
            else
            {
                Debug.LogError("[Fibula Login] You can't add a user if you're not connected to the server!");
            }
            BindToOnDataEvent();
        }

        private static void BindToOnDataEvent()
        {
            if (DarkRiftAPI.isConnected)
            {
                DarkRiftAPI.onData -= OnDataHandler;
                DarkRiftAPI.onData += OnDataHandler;
            }
        }
        private static void OnDataHandler(byte _loginT, ushort _loginS, object data)
        {
            if(_loginT == NT.LoginT)
            {
                if(_loginS == NT.LoginS.loginUserSuccess)
                {
                    bool _hasuma;
                    DarkRiftReader reader = (DarkRiftReader)data;

                    _hasuma = reader.ReadBoolean();
                    userID = reader.ReadInt32();

                    isLoggedIn = true;

                    if (onSuccessfullLogin != null)
                        onSuccessfullLogin(userID, _hasuma);
                }
            }
        }
    }
}
