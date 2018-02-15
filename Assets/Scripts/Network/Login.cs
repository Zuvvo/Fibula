using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkRift;
using Fibula.Login;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
    public InputField UsernameInput;
    public InputField PasswordInput;


    private void Start()
    {
        LoginManager.onSuccessfullLogin += LoadLevel;
    }
    public void ButtonLogin()
    {
        LoginManager.Login(UsernameInput.text, PasswordInput.text);
    }
    public void BUttonAddUser()
    {
        LoginManager.AddUser(UsernameInput.text, PasswordInput.text);
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }
    public void LoadLevel(int _playerID, bool _hasuma)
    {
        if (!_hasuma)
            SceneManager.LoadScene("Char Builder");
        else
            SceneManager.LoadScene("Start");
    }
}
