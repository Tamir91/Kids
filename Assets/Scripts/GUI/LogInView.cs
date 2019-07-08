using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInView : BubbleElement
{
    #region Variables
    // Static Variables
    private static LogInView single;

    //Privat Variables
    private string CUserName = "";
    private string CPassword = "";
    private bool isEmailValid = false;

    //Public Variables
    public GameObject LogInCanvas;

    #endregion

    private void Awake()
    {
        if (single == null)
        {
            single = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CUserName = LogInCanvas.transform.GetChild(0).GetComponent<InputField>().text;
        CPassword = LogInCanvas.transform.GetChild(1).GetComponent<InputField>().text;
    }
    
    public void ShowLoginPage()
    {
        StartCoroutine(MoveSignUpPage());
    }

    private IEnumerator MoveSignUpPage()
    {
        Debug.Log("MoveSignUpPage");
        var rect = LogInCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(400f, 640f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnBackClicked()
    {
        StartCoroutine(MoveBackLogInPage());
    }

    private IEnumerator MoveBackLogInPage()
    {
        Debug.Log("MoveBackSignUpPage");
        var rect = LogInCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(-400f, 640f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnLogInClicked()
    {
        if(!CPassword.Equals("admin"))
        {
           
        }
        if(!CUserName.Equals("admin"))
        {
            
        }
        if(CPassword.Equals("admin") && CUserName.Equals("admin"))
        {
            Debug.Log("OnLogInClicked::User and Password correct");
            App.Notify(BubbleNotification.GoToCabinetClicked, this);
        }
    }

    public string GetCUserName()
    {
        return CUserName;
    }

    public string GetCPassword()
    {
        return CPassword;
    }
}
