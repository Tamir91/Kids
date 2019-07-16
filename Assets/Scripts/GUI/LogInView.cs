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
    private string ConfirmPassword = "";
    public bool isRegistrationInputCorrect = false;

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

        if (LogInCanvas.transform.GetChild(5).GetComponent<InputField>().IsActive())
        {
            ConfirmPassword = LogInCanvas.transform.GetChild(5).GetComponent<InputField>().text;
        }

        CheckIfUserInputCorrect();
    }

    private void CheckIfUserInputCorrect()
    {
        if (LogInCanvas.transform.GetChild(5).GetComponent<InputField>().IsActive())//SignUp mode
        {
            if (CPassword.Equals(ConfirmPassword) && CPassword.Length > 0 && CUserName.Length > 0)
            {
                isRegistrationInputCorrect = true;
                LogInCanvas.transform.GetChild(2).GetComponent<Button>().interactable = true;
            }
            else
            {
                LogInCanvas.transform.GetChild(2).GetComponent<Button>().interactable = false;
                isRegistrationInputCorrect = false;
            }
        }
        else//LogInMode
        {
            if(CUserName.Length > 0 && CPassword.Length > 0)
            {
                LogInCanvas.transform.GetChild(2).GetComponent<Button>().interactable = true;
                isRegistrationInputCorrect = true;
            }
        }
    }

    public void ShowLogInPage()
    {
        StartCoroutine(ShowPage());
    }

    private IEnumerator ShowPage()
    {
        Debug.Log("ShowLogInPage");
        var rect = LogInCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(400f, 225f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnBackClicked()
    {
        CleanAndHideSignUpGUI();
        //StartCoroutine(HideLogInPage());
    }

    private IEnumerator HideLogInPage()
    {
        Debug.Log("MoveBackSignUpPage");
        var rect = LogInCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(-400f, 225f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnSignUpClicked()
    {
        App.Notify(BubbleNotification.OnSignUpInLoginClicked, this);
    }

    public void ShowSignUpGUI()
    {
        LogInCanvas.transform.GetChild(4).GetComponent<Button>().gameObject.SetActive(false);//SignUp button
        LogInCanvas.transform.GetChild(5).GetComponent<InputField>().gameObject.SetActive(true);//ConfirmPassword field

        //LogInCanvas.transform.GetChild(2).GetComponent<Button>().interactable = false;
        LogInCanvas.transform.GetChild(2).GetComponent<Button>()
            .transform.GetChild(0).GetComponent<Text>().text = "ןובשח רוצ";//LogIn Button
    }

    public void CleanAndHideSignUpGUI()
    {
        LogInCanvas.transform.GetChild(0).GetComponent<InputField>().text = "";//UserName field
        LogInCanvas.transform.GetChild(1).GetComponent<InputField>().text = "";//Password filed
        LogInCanvas.transform.GetChild(5).GetComponent<InputField>().text = "";//ConfirmPassword field
        LogInCanvas.transform.GetChild(2).GetComponent<Button>()
           .transform.GetChild(0).GetComponent<Text>().text = "סנכ";//LogIn Button

        LogInCanvas.transform.GetChild(2).GetComponent<Button>().gameObject.SetActive(true);//LogIn button
        LogInCanvas.transform.GetChild(4).GetComponent<Button>().gameObject.SetActive(true);//SignUp button
        LogInCanvas.transform.GetChild(5).GetComponent<InputField>().gameObject.SetActive(false);//ConfirmPassword field   
    }

    public void OnLogInSigUpClicked()
    {
        if (LogInCanvas.transform.GetChild(2).GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text == "ןובשח רוצ")
        {
            // Buuton in work like create admin profile.
            App.Notify(BubbleNotification.OnCreateProfileClicked, this);
        }
        else
        {
            // Button work like log in to admin profile.
            App.Notify(BubbleNotification.OnLogInClicked, this);
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

    public bool GetRegistrationInputCorection()
    {
        return isRegistrationInputCorrect;
    }
}
