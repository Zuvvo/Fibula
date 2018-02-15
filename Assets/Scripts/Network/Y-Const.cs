using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fibula
{

    public class NT
    {
        public const byte LoginT = 10;
        public class LoginS // Login Subject
        {
            public const ushort loginUser = 1;
            public const ushort logoutUser = 2;
            public const ushort addUser = 3;
            public const ushort loginUserSuccess = 4;
            public const ushort loginUserFailed = 5;
            public const ushort logoutSuccess = 6;
            public const ushort addUserSuccess = 7;
            public const ushort addUserFailed = 8;

        }
    }
}
