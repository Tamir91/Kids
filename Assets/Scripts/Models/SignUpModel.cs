using UnityEngine;

public class SignUpModel : BubbleElement {

    #region Variables
    // Static Variables
    private static SignUpModel single;

    // Public Variables

    // Private Vriables
    private string Email = "";
    private string FirstName = "";
    private string SecondName = "";

    #endregion

    private void Awake()
    {
        if(single == null)
        {
            single = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SignUp(string FirstName, string SecondName, string Email)
    {
        this.Email = Email;
        this.FirstName = FirstName;
        this.SecondName = SecondName;

        SaveInPlayerPrefs();
    }

    private void SaveInPlayerPrefs()
    {
        PlayerPrefs.SetString("Email_" + Email, Email);
        PlayerPrefs.SetString("Name_" + Email, FirstName);
        PlayerPrefs.SetString("SecName_" + Email, SecondName);
        Debug.Log("SaveInPlayerPrefs::" + "  " + Email + " " + FirstName + " " + SecondName);
    }
}
