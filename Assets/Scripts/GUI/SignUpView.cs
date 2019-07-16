using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : BubbleElement
{
    #region Variables
    //Static variables
    private static string FirstName = "";
    private static string SecondName = "";
    private static string Gender = "";
    private static string PhoneNumber = "";
    private static string Age = "";
    private static string Email = "";

   

    // Private Variables
    private string SubscribeString = "המשרה";
    private string SaveString = "רומש";
    private string FirstNameString = "יטרפ םש";
    private string SecondNameString = "החפשמה םש";
    private string EmailString = "ינורטקלא ראוד";
    private string AgeString = "ליג";
    private string PhoneNumberString = "ןופאלפ רפסמ";

    private GUIStyle LabelGUIStyle;
    private GUIStyle TextFieldGUIStyle;

    private static SignUpView single;

    // Public Variables
    public GameObject SignUpCanvas;
    //public GameObject StartCanvas;
    // Test Variables
 
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

    public void ShowSignUpPage()
    {
        StartCoroutine(MoveSignUpPage(400f, 250f));
    }

    public void HideSignUp()
    {
        StartCoroutine(MoveSignUpPage(-400f, 250f));
    }

    private IEnumerator MoveSignUpPage(float x, float y, float maxDistanceDelta = 50f)
    {
        Debug.Log("MoveSignUpPage");
        var rect = SignUpCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(x, y);

        while(rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, maxDistanceDelta);
            yield return null;
        }
    } 

    public string GetFirstName() => FirstName;

    public string GetSecondName() => SecondName;

    public string GetAge() => Age;

    public string GetGender() => Gender;

    public string GetEmail() => Email;

    public string GetPhoneNumber() => PhoneNumber;

    public void OnSaveKidClicked()
    {
        SetData();
        if (PhoneNumber == "")
        {
            ChangePlaceholdersColor(Color.red);   
        }
        else
        {
            ChangePlaceholdersColor(Color.grey);
            App.Notify(BubbleNotification.OnSaveKidClicked, this);
        }   
    }

    private void ChangePlaceholdersColor(Color Color)
    {
        SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color;
        SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color;
        SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color;
    }

    private void SetData()
    {
        FirstName = SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text;
        SecondName = SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text;

        Age = SignUpCanvas.transform.GetChild(2).GetComponent<Dropdown>()
            .options[SignUpCanvas.transform.GetChild(2).GetComponent<Dropdown>().value].text;

        Gender = SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>()
            .options[SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>().value].text;

        PhoneNumber = SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().text;
        Email = SignUpCanvas.transform.GetChild(5).GetComponent<InputField>().text;
    }

    public void CleanInputFields()
    {
        SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(5).GetComponent<InputField>().text = "";
    }

    public void OnGoToLogInPageClicked() => App.Notify(BubbleNotification.GoToLogInPageClicked, this);

    //public void OnStartSignUpViewClicked() => App.Notify(BubbleNotification.GoToSignUpClicked, this);
}
