using UnityEngine;

public class LoginModel : BubbleElement {
    #region Variables
    // Static variables
    private static LoginModel Single;

    // Public variables
    public static string Email = "";
    public static string Password = "";
    
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

    public void CreateNewProfile(Admin Admin)
    {
        FileSaver FileSaver = FindObjectOfType<FileSaver>();
        FileSaver.PutAdminToFireBase(Admin);
    }

    public void CheckIfAdminExsist(Admin Admin)
    {
        Debug.Log("CheckIfAdminExsist::" + Admin.UserName + " " + Admin.UserPassword);
        FileLoader FileLoader = FindObjectOfType<FileLoader>();
        FileLoader.SendAdminRequestToFireBase(Admin); 
    }
}
