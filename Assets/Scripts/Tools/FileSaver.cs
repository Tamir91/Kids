using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using UnityEngine;

public class FileSaver : BubbleElement {

    #region Variable
    // Static Variables
    private static FileSaver Single;
    private DatabaseReference reference;

    // Public Variables

    //Privte Variables
    private string Route = "https://project-1c5c7.firebaseio.com/";


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

    private void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(Route);

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void PutKidToFireBase(Kid Kid, string key)
    {
        RestClient.Put<Kid>(Route + "kids/" +  key + "key" + "/" + Kid.PhoneNumber + ".json", Kid);
        //string json = JsonUtility.ToJson(Kid);
        //reference.Child("kids").Child(key + "key").Child(Kid.PhoneNumber).SetRawJsonValueAsync(json);
    }

    public void PutAdminToFireBase(Admin admin)
    {
        RestClient.Put<Admin>(Route + "admin/" + admin.UserName + admin.UserPassword + ".json", admin);
        //string json = JsonUtility.ToJson(admin);
        //reference.Child("admin").Child(admin.UserName + admin.UserPassword).SetRawJsonValueAsync(json);
    }
}