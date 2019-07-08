using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginModel : BubbleElement {
    #region Variables
    // Static variables
    private static LoginModel Single;

    // Public variables
    public static string Email = "";
    public static string Password = "";

    // Private variables
    private string LogInToAccountUrl = "http://localhost/FoamBubble/Login.php";
    #endregion

    private void Awake()
    {
        if(Single == null)
        {
            Single = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LogIn(string email, string password)
    {
        Email = email;
        Password = password;
        
    }
}
