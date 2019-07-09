using UnityEngine;

public class KidModel : BubbleElement{

    #region Variables
    // Static Variables
    private static KidModel Single;
    private Kid Person;
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

    public void SaveKidPersonalData (string FirstName, string SecondName, string Age, string Gender, string PhoneNumber, string Email, string key)
    {
        var Kid = new Kid(FirstName, SecondName, Age, Gender, PhoneNumber, Email);
        Debug.Log("SaveUserPersonalData::" + Kid.FirstName);
        FileSaver FileSaver = FindObjectOfType<FileSaver>();
        FileSaver.PutKidToFireBase(Kid, key);
    }
}
