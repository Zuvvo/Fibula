using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using DarkRift.ConfigTools;
using DarkRift.Storage;


namespace LoginPlugin
{

    namespace LoginPlugin
    {
        public class LoginPlugin : Plugin
        {
            public override string name { get { return "LoginPlugin"; } }
            public override string version { get { return "1.0"; } }

            public override string author { get { return "Zuvvo"; } }
            public override string supportEmail { get { return "Zuvvo4@gmail.com"; } }

            public override Command[] commands
            {
                get
                {
                    return new Command[]
                    {
                    new Command("UserCreate", "Turns ON and OFF User creation [on / off]", new Action<string[]>(UserCreateCommand)),
                    new Command("AddUser", "Adds an User to the DB[AddUser name password]", new Action<string[]>(AddUserCommand)),
                    new Command("LPDebug", "Turns on Plugin Debug", new Action<string[]>(DebugCommand))
                    };
                }
            }

            private ConfigReader _settings;

            private byte _loginT;
            private ushort _loginSUserLogin;
            private ushort _loginSLogoutUser;
            private ushort _loginSAddUser;
            private ushort _loginSLoginSuccess;
            private ushort _loginSLoginFailed;
            private ushort _loginSLogoutSuccess;
            private ushort _loginSAddUserSuccess;
            private ushort _loginSAddUserFailed;

            private bool _allowAddUser = true;
            private bool _debug = false;

            private object _obj;

            private int _reason;

            public LoginPlugin()
            {
                if (!IsInstalled())
                {
                    InstallSubdirectory(new Dictionary<string, byte[]>()
                {
                    {
                        "settings.cnf",ASCIIEncoding.ASCII.GetBytes("LoginTag:\t\t\t\t\t10\n"+
                                                                    "LoginSubjectUserLogin:\t\t1\n"+
                                                                    "LoginSubjectLogoutUser:\t\t2\n"+
                                                                    "LoginSubjectAddUser:\t\t3\n"+
                                                                    "LoginSubjectLoginSuccess:\t4\n"+
                                                                    "LoginSubjectLoginFailed:\t5\n"+
                                                                    "LoginSubjectLogoutSuccess:\t6\n"+
                                                                    "LoginSubjectAddUserSuccess:\t7\n"+
                                                                    "LoginSubjectAddUserFailed:\t8\n"+
                                                                    "AllowAddUser:\t\t\tTrue\n"+
                                                                    "Debug:\t\t\t\t\t\tTrue")
                    }
                    }
                        );
                    try
                    {
                        DarkRiftServer.database.ExecuteNonQuery(
                            "CREATE TABLE IF NOT EXISTS tblPlayer(" +
                            "id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
                            "username VARCHAR(50) NOT NULL, " +
                            "password VARCHAR(200) NOT NULL ) ", new QueryParameter[0]);
                    }
                    catch (DatabaseException e)
                    {
                        //  Interface.LogError("[LoginPlugin] SQL error during setup: " + e.ToString());

                        if (_debug) Interface.Log("[LoginPlugin] SQL error during setup: ");
                    }
                }

                _settings = new ConfigReader(GetSubdirectory() + "/settings.cnf");

                if (!byte.TryParse(_settings["LoginTag"], out _loginT))
                {
                    Interface.LogFatal("[LoginPlugin] Plugin Tag could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectUserLogin"], out _loginSUserLogin))
                {
                    Interface.LogFatal("[LoginPlugin] User Login Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectLogoutUser"], out _loginSLogoutUser))
                {
                    Interface.LogFatal("[LoginPlugin] User Logout Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectAddUser"], out _loginSAddUser))
                {
                    Interface.LogFatal("[LoginPlugin] Add User Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectLoginSuccess"], out _loginSLoginSuccess))
                {
                    Interface.LogFatal("[LoginPlugin] User Login Success Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectLoginFailed"], out _loginSLoginFailed))
                {
                    Interface.LogFatal("[LoginPlugin] User Login Failed Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectLogoutSuccess"], out _loginSLogoutSuccess))
                {
                    Interface.LogFatal("[LoginPlugin] User Logout Sucess Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectAddUserSuccess"], out _loginSAddUserSuccess))
                {
                    Interface.LogFatal("[LoginPlugin] Add User Success Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!ushort.TryParse(_settings["LoginSubjectAddUserFailed"], out _loginSAddUserFailed))
                {
                    Interface.LogFatal("[LoginPlugin] Add User Login Failed Subject could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!bool.TryParse(_settings["AllowAddUser"], out _allowAddUser))
                {
                    Interface.LogFatal("[LoginPlugin] Allow Add User could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                if (!bool.TryParse(_settings["Debug"], out _debug))
                {
                    Interface.LogFatal("[LoginPlugin] Debug could not be found in settings.cnf");
                    DarkRiftServer.Close(true);
                }
                ConnectionService.onServerMessage += OnServerMessage;
                LogColor((_allowAddUser ? "Account Creation ON" : "Account Creation OFF"), ConsoleColor.Yellow);
            }
            private void UserCreateCommand(String[] _commandStr)
            {
                if(_commandStr.Length != 1)
                {
                    LogColor("You need to call this command with [UserCreate on] or [UserCreate off]", ConsoleColor.DarkMagenta);
                    return;
                }
                if(_commandStr[0] == "on")
                {
                    this._allowAddUser = true;
                    LogColor("Account Creation: " + _commandStr[0], ConsoleColor.Yellow);
                }
                else if (_commandStr[0] == "off")
                {
                    this._allowAddUser = false;
                    LogColor("Account Creation: " + _commandStr[0], ConsoleColor.Yellow);
                }
            }

            private void LogColor(String _text, ConsoleColor _color)
            {
                Console.ForegroundColor = _color;
                Interface.Log(_text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            private void DebugCommand(String[] _commandStr)
            {
                _debug = !_debug;
                Interface.Log("[LoginPlugin] Debug is " + _debug);
            }
            private void AddUser(String _username, String _password)
            {
                try
                {
                    DarkRiftServer.database.ExecuteNonQuery(
                        "INSERT INTO tblPlayer(username, password) VALUES(@username, @password)",
                        new QueryParameter("username", _username),
                        new QueryParameter("password", _password)
                        );
                    if (_debug)
                        Interface.Log("[LoginPlugin] New User: " + _username);
                }
                catch (DatabaseException e)
                {
                    if (_debug)
                    {
                        Interface.Log("[LoginPlugin] Add user failed.\n" + e.ToString());
                    }
                }
            }
            private bool CheckUser(String _username, String _password)
            {
                try
                {
                    _obj = DarkRiftServer.database.ExecuteScalar(
                        "SELECT EXISTS(SELECT 1 FROM tblPlayer WHERE username = @username )",
                        new QueryParameter("username", _username)

                        );
                    return Convert.ToBoolean(_obj);
                }
                catch (DatabaseException e)
                {
                    if (_debug)
                    {
                        Interface.Log("[LoginPlugin] Add user failed." + e.ToString());
                    }
                    return true;
                }
            }
            private void AddUserCommand(String[] _commandStr)
            {
                if (_commandStr.Length != 2) return;

                string _username = _commandStr[0];
                string _password = HashHelper.ReturnHash(_commandStr[1], HashType.SHA256);

                if (!CheckUser(_username, _password))
                {
                    AddUser(_username, _password);
                }
            }
            private void OnServerMessage(ConnectionService con, NetworkMessage data)
            {
                if (data.tag == _loginT)
                {
                    if (data.subject == _loginSUserLogin)
                    {
                        #region login
                        bool isloggedin = false;
                        if (con.HasData(name, "ISLoggedIn"))
                            isloggedin = (bool)con.GetData(name, "IsLoggedIn");

                        if (isloggedin) return;

                        try
                        {
                            using (DarkRiftReader reader = (DarkRiftReader)data.data)
                            {
                                string _username = reader.ReadString();
                                string _password = reader.ReadString();

                                DatabaseRow[] rows = DarkRiftServer.database.ExecuteQuery(
                                    "SELECT id FROM tblPlayer WHERE username = @username AND password = @password LIMIT 1 ",
                                    new QueryParameter("username", _username),
                                    new QueryParameter("password", _password)
                                    );
                                if(rows.Length == 1)
                                {
                                    bool _hasuma = false;
                                    int id = Convert.ToInt32(rows[0]["id"]);

                                    con.SetData(name, "IsLoggedIn", true);
                                    con.SetData(name, "UserID", id);
                                    DarkRiftWriter writer = new DarkRiftWriter();

                                    writer.Write(_hasuma);
                                    writer.Write(id);
                                    con.SendReply(_loginT, _loginSLoginSuccess, writer);

                                    if (_debug)
                                        Interface.Log("[LoginPlugin] User with id " + id + " logged successfully");
                                }
                                else
                                {
                                    if (_debug)
                                        Interface.Log("[LoginPlugin] User<" + _username + "> tried to login and failed");
                                    _reason = 0;
                                    con.SendReply(_loginT, _loginSLoginFailed, _reason);
                                }
                            }
                        }
                        catch (InvalidCastException)
                        {
                            if (_debug) Interface.Log("[LoginPlugin] Invalid data recieved in a Login request.");
                            _reason = -1;
                            con.SendReply(_loginT, _loginSLoginFailed, _reason);
                        }


                        #endregion
                    }
                    if (data.subject == _loginSLogoutUser)
                    {
                        Interface.Log("Logout");
                    }
                    if (data.subject == _loginSAddUser)
                    {
                        #region adduser
                        if (_allowAddUser)
                        {
                            try
                            {
                                using (DarkRiftReader reader = (DarkRiftReader)data.data)
                                {
                                    string _username = reader.ReadString();
                                    string _password = reader.ReadString();

                                    if (!CheckUser(_username, _password))
                                    {
                                        AddUser(_username, _password);
                                    }
                                    _reason = 1;
                                    con.SendReply(_loginT, _loginSAddUserSuccess, _reason);
                                }
                                Interface.Log("AddUser");
                            }
                            catch (InvalidCastException)
                            {
                                if (_debug) Interface.Log("[LoginPlugin] Add user failed. Invalid data recieved. ");
                                _reason = -1;
                                con.SendReply(_loginT, _loginSAddUserFailed, _reason);
                            }
                        }
                        #endregion
                    }
                }
            }
        }
    }
}
