using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserRegisrator : MonoBehaviour {
#region Variables
    //Static variables
    public static string Email = "";
    public static string Password = "";

    //Public Variables
    public string CurrentMenu = "Login";

    //Private Variables
    private string CreateAccountUrl = "http://localhost/FoamBubble/CreateAccountT.php";
    private string LogInToAccountUrl = "http://localhost/FoamBubble/Login.php";
    private string LoginUrl = "";
    private string ConfirmEmail = "";
    private string ConfirmPass = "";
    private string CEmail = "";
    private string CPassword = ""; 
    private bool isEmailValid = false;

    //GUI Test Section
    public float X;
    public float Y;
    public float Width;
    public float Heigth;
    #endregion

    // Use this for initialization
    void Start () {
		
	}// End Start method

    // Main GUI Function
    void OnGUI()
    {
        if(CurrentMenu == "Login")
        {
            LoginGUI();
        }else if(CurrentMenu == "SaveInPlayPrefs")
        {
            CreateAccountGUI();
        }else if(CurrentMenu == "Cabinet")
        {
            CreateCabinetGUI();
        }
    }// End OnGUI

#region Custom method

    // Tis method will login the account
    void LoginGUI()
    {
        // Create box to simulate window.
        GUI.Box(new Rect(280, 120, (Screen.width / 4) + 200, (Screen.height / 4) + 250), "Login");

        // Create Account button and login button.
        // Open Create Account Window
        if (GUI.Button(new Rect(370, 360, 120, 30), "Create Account"))
        {
            CurrentMenu = "SaveInPlayPrefs";
        }
        // Log User In
        if (GUI.Button(new Rect(520, 360, 120, 30), "Log In"))
        {
            StartCoroutine("LogIn");
        }// End button

        // Email and Password.
        GUI.Label(new Rect(400, 200, 220, 25), "Email:");
        Email = GUI.TextField(new Rect(400, 225, 220, 25), Email);
        GUI.Label(new Rect(400, 250, 220, 25), "Password:");
        Password = GUI.TextField(new Rect(400, 275, 220, 25), Password);
       
    }// End of LoginGUI

    //This method will be the GUI for creating account.
    void CreateAccountGUI()
    {
        // Create box to simulate window.
        GUI.Box(new Rect(280, 120, (Screen.width / 4) + 200, (Screen.height / 4) + 250), "Create Account");

        // Email and Password.
        GUI.Label(new Rect(400, 200, 220, 25), "Email:");
        CEmail = GUI.TextField(new Rect(400, 225, 220, 25), CEmail);
        GUI.Label(new Rect(400, 250, 220, 25), "Password:");
        CPassword = GUI.TextField(new Rect(400, 275, 220, 25), CPassword);

        // Email and Password, plus confirmation.
        GUI.Label(new Rect(400, 300, 220, 25), "Confirm Email:");
        ConfirmEmail = GUI.TextField(new Rect(400, 325, 220, 25), ConfirmEmail);
        GUI.Label(new Rect(400, 350, 220, 25), "Confirm Password:");
        ConfirmPass = GUI.TextField(new Rect(400, 375, 220, 25), ConfirmPass);

        // Bot protection;

        // Create Account button and login button.
        // Open Create Accaunt Window
        if (GUI.Button(new Rect(370, 460, 120, 30), "Create Account"))
        {
            if(ConfirmPass == CPassword && ConfirmEmail == CEmail)
            {
                Debug.Log("Email and password - OK");
                StartCoroutine("SaveInPlayPrefs");
            }
            else
            {
                Debug.Log("Email or password - are not similar");
                //StartCoroutine();
            }
        }
        // Log User In
        if (GUI.Button(new Rect(520, 460, 120, 30), "Back"))
        {
            CurrentMenu = "Login";
        }// End button
      
    }// End of CreateAccountGUI

    //This method will be the GUI Home Page
    void CreateCabinetGUI()
    {
        //Create box
        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Your Cabinet");

        // Email and Password.
        GUI.Label(new Rect(30, 20, 220, 25), "Email:");
        GUI.Label(new Rect(70, 20, 220, 25), Email);

        if (GUI.Button(new Rect(400, 40, 25, 25), "1")) { };
        if (GUI.Button(new Rect(450, 40, 25, 25), "2")) { };
        if (GUI.Button(new Rect(500, 40, 25, 25), "3")) { };
        if (GUI.Button(new Rect(550, 40, 25, 25), "4")) { };

    }

#endregion

    #region Coroutines

    // Actually create account.
    IEnumerator CreateAccount()
    {
        // This is what sends messages to our php script.
        WWWForm Form = new WWWForm();

        Form.AddField("userEmailPost", CEmail);
        Form.AddField("userPasswordPost", CPassword);

        WWW CreateAccountWWW = new WWW(CreateAccountUrl, Form);

        // Wait for php to send something back to Unity.
        yield return CreateAccountWWW;

        if(CreateAccountWWW.error != null)
        {
            Debug.LogError("SaveInPlayPrefs::Cannot conect to Account Creation");
        }
        else
        {
            string CreateAccountReturn = CreateAccountWWW.text;
            if(CreateAccountReturn == "Success")
            {
                Debug.Log("SaveInPlayPrefs::Success: Account created");
                CurrentMenu = "Login";
            }
        }

    }// End SaveInPlayPrefs
    
    IEnumerator LogIn()
    {
        WWWForm Form = new WWWForm();

        Form.AddField("loginUserPost", Email);
        Form.AddField("loginPassPost", Password);

        WWW LogIN = new WWW(LogInToAccountUrl, Form);
        // Wait for php to send something back to Unity.
        yield return LogIN;

        switch (LogIN.text) {
            case "Login Success":
                {
                    Debug.Log("Login Success");
                    CurrentMenu = "Cabinet";
                    break;
                }
            case "Wrong Credentials":
                {
                    Debug.Log("Login Wrong Credentials");
                    break;
                }
            case "Username does not exists":
                {
                    Debug.Log("LoginUsername does not exists");
                    break;
                }
        }
    }

#endregion

}// End Class
