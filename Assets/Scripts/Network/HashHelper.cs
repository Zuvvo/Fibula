using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
namespace Fibula
{
    public enum HashType
    {
        MD5, SHA1, SHA256, SHA512
    }


    public class HashHelper
    {
        public static string ReturnHash(string _myinput, HashType _myhashType)
        {
            byte[] _hashbytes = new byte[0];
            HashAlgorithm _hashcode;
            switch (_myhashType)
            {
                case HashType.MD5:
                    _hashcode = MD5.Create();
                    _hashbytes = _hashcode.ComputeHash(Encoding.UTF8.GetBytes(_myinput));
                    break;
                case HashType.SHA1:
                    _hashcode = SHA1.Create();
                    _hashbytes = _hashcode.ComputeHash(Encoding.UTF8.GetBytes(_myinput));
                    break;
                case HashType.SHA256:
                    _hashcode = SHA256.Create();
                    _hashbytes = _hashcode.ComputeHash(Encoding.UTF8.GetBytes(_myinput));
                    break;
                case HashType.SHA512:
                    _hashcode = MD5.Create();
                    _hashbytes = _hashcode.ComputeHash(Encoding.UTF8.GetBytes(_myinput));
                    break;
            }
            StringBuilder _returnstring = new StringBuilder();
            foreach(byte _byte in _hashbytes)
            {
                _returnstring.Append(_byte.ToString("X2"));
            }
            return _returnstring.ToString();
        }


    }
}
