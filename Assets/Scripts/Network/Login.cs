using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkRift;
using Fibula.Login;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Login : MonoBehaviour {
    public InputField UsernameInput;
    public InputField PasswordInput;
    public GameObject LoginFailedPanel;


    private void Start()
    {
        LoginManager.onSuccessfulLogin += LoadLevel;
        LoginManager.onFailedLogin += LoginFailed;
        LoginManager.onSuccessfulAddUser += ButtonLogin;
        LoginManager.onFailedAddUser += ButtonQuit;
    }

    private void OnApplicationQuit()
    {
        LoginManager.onSuccessfulLogin += LoadLevel;
        LoginManager.onFailedLogin += LoginFailed;
        LoginManager.onSuccessfulAddUser += ButtonLogin;
        LoginManager.onFailedAddUser += ButtonQuit;
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

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
    public void ButtonOK()
    {
        LoginFailedPanel.SetActive(false);
    }
    public void LoadLevel(int _playerID, bool _hasuma)
    {
        if (!_hasuma)
            SceneManager.LoadScene("Char Builder");
        else
            SceneManager.LoadScene("Start");
    }
    public void LoginFailed(int _reason)
    {
        if (_reason == 0)
        {
            PasswordInput.text = "";
            LoginFailedPanel.SetActive(true);

        }
        else
        {
            ButtonQuit();
        }
    }

}
