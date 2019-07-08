using UnityEngine;

public class PersonModel : BubbleElement{

    #region Variables
    // Static Variables
    private static PersonModel Single;
    private Person Person;
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

    public void SaveUserPersonalData (Person Person)
    {
        Debug.Log("SaveUserPersonalData::" + Person.FirstName);
        FileSaver FileSaver = FindObjectOfType<FileSaver>();
        FileSaver.PutPersonToFireBase(Person);
    }
}
