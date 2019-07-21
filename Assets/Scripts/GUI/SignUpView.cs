using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : BubbleElement
{
    #region Variables
    //Static variables
    private static string FirstName = "";
    private string BufferFirstName;
    private static string SecondName = "";
    private string BufferSecondName;
    private static string Gender = "";
    private static string PhoneNumber = "";
    private static string Age = "";
    private static string Email = "";

   

    // Private Variables
    private string SubscribeString = "המשרה";
    private string SaveString = "רומש";
    private string FirstNameString = "יטרפ םש";
    private int LengthFirstName = 0;
    private string SecondNameString = "החפשמה םש";
    private int LengthSecondName = 0;
    private string EmailString = "ינורטקלא ראוד";
    private string AgeString = "ליג";
    private string PhoneNumberString = "ןופאלפ רפסמ";

    private GUIStyle LabelGUIStyle;
    private GUIStyle TextFieldGUIStyle;

    private static SignUpView single;

    // Public Variables
    public GameObject SignUpCanvas;

    public static string FirstName1
    {
        get
        {
            return FirstName;
        }

        set
        {
            FirstName = value;
        }
    }

    public string BufferFirstName1
    {
        get
        {
            return BufferFirstName;
        }

        set
        {
            BufferFirstName = value;
        }
    }

    public static string SecondName1
    {
        get
        {
            return SecondName;
        }

        set
        {
            SecondName = value;
        }
    }

    public string BufferSecondName1
    {
        get
        {
            return BufferSecondName;
        }

        set
        {
            BufferSecondName = value;
        }
    }

    public static string Gender1
    {
        get
        {
            return Gender;
        }

        set
        {
            Gender = value;
        }
    }

    public static string PhoneNumber1
    {
        get
        {
            return PhoneNumber;
        }

        set
        {
            PhoneNumber = value;
        }
    }

    public static string Age1
    {
        get
        {
            return Age;
        }

        set
        {
            Age = value;
        }
    }

    public static string Email1
    {
        get
        {
            return Email;
        }

        set
        {
            Email = value;
        }
    }

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

    private void Update()
    {
        HebrewSwopFirstName();
        HebrewSwopSecondName();
    }

    private void HebrewSwopFirstName()
    {

            string templ = SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text;

            if (templ.Length > 0 && templ.Length > LengthFirstName)
            {
                char ch = templ[templ.Length - 1];
                templ = templ.Remove(templ.Length - 1, 1);
                templ = ch + templ;
                SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text = templ;
            }
            else if (templ.Length > 0 && templ.Length < LengthFirstName)
            {
                templ = BufferFirstName1.Remove(0, 1);
                SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text = templ;
            }

            LengthFirstName = templ.Length;
            BufferFirstName1 = templ;    
    }

    private void HebrewSwopSecondName()
    {

        string templ = SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text;

        if (templ.Length > 0 && templ.Length > LengthSecondName)
        {
            char ch = templ[templ.Length - 1];
            templ = templ.Remove(templ.Length - 1, 1);
            templ = ch + templ;
            SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text = templ;
        }
        else if (templ.Length > 0 && templ.Length < LengthSecondName)
        {
            templ = BufferSecondName1.Remove(0, 1);
            SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text = templ;
        }

        LengthSecondName = templ.Length;
        BufferSecondName1 = templ;
    }

    public void ShowSignUpPage()
    {
        StartCoroutine(MoveSignUpPage(400f, 225f));
    }

    public void HideSignUp()
    {
        StartCoroutine(MoveSignUpPage(-400f, 225f));
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

    public string GetFirstName() => FirstName1;

    public string GetSecondName() => SecondName1;

    public string GetAge() => Age1;

    public string GetGender() => Gender1;

    public string GetEmail() => Email1;

    public string GetPhoneNumber() => PhoneNumber1;

    public void OnSaveKidClicked()
    {
        SetData();
        if (PhoneNumber1 == "")
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
        FirstName1 = SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text;
        SecondName1 = SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text;

        Age1 = SignUpCanvas.transform.GetChild(2).GetComponent<InputField>().text;

        Gender1 = SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>()
            .options[SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>().value].text;

        PhoneNumber1 = SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().text;
        Email1 = SignUpCanvas.transform.GetChild(5).GetComponent<InputField>().text;
    }

    public void FillFieldsWithData(string FirstName, string SecondName, string Age, string Gender, string PhoneNumber, string Email)
    {
        
        SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text = CorrectBug( FirstName);
        SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text = CorrectBug(SecondName);
        SignUpCanvas.transform.GetChild(2).GetComponent<InputField>().text = Age;

        // SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>()
        //.options[SignUpCanvas.transform.GetChild(3).GetComponent<Dropdown>().value] = 
        SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().text = PhoneNumber;
        SignUpCanvas.transform.GetChild(5).GetComponent<InputField>().text = Email;
    }

    private string CorrectBug(string word)
    {
        return word.Remove(0, 1) + word[0];
    }

    public void CleanInputFields()
    {
        SignUpCanvas.transform.GetChild(0).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(1).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(2).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(4).GetComponent<InputField>().text = "";
        SignUpCanvas.transform.GetChild(5).GetComponent<InputField>().text = "";
    }

    public void OnGoToLogInPageClicked() => App.Notify(BubbleNotification.GoToLogInPageClicked, this);

    //public void OnStartSignUpViewClicked() => App.Notify(BubbleNotification.GoToSignUpClicked, this);
}
