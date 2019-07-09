using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using UnityEngine;

public class FileLoader : BubbleElement {
    #region Variables
    // Static Variables
    private static FileLoader Single;
    // Private Variables
    private string BaseRoute = "https://project-1c5c7.firebaseio.com/";
    private DatabaseReference DatabaseReference;

    // Public Variables
    private Admin CurrAdmin { get; set; }

    #endregion
    // Use this for initialization

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

    /// <summary>
    /// This function get person from FireBase and set him into text in Cabinet View.
    /// </summary>
    /// <param name="keyPhoneNumber">Kid phone number</param>
    /// <returns>IEnumerator</returns>
    public void SendPersonRequestToFireBase(string keyPhoneNumber)
    {
        Debug.Log("SendPersonRequestToFireBase with key:" + keyPhoneNumber);
        RestClient.Get<Kid>(BaseRoute + keyPhoneNumber + ".json").Then(response =>
        {
            App.View
            .CabinetView
            .SetTextAboutKids(response.ToString());
        });
    }

    /// <summary>
    /// This function get all person from FireBase and set all into text in Cabinet View.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public void GetAllKids(string key)
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(BaseRoute);
        DatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        string str = "";
        FirebaseDatabase.DefaultInstance
            .GetReference("").Child("kids").Child(key + "key")
            .GetValueAsync().ContinueWith(task => { 
                  if (task.IsFaulted)
                  {
                    Debug.Log("GetAllPersons::task.IsFaulted");
                      // Handle the error...
                  }
                  else if (task.IsCompleted)
                  {
                    
                    DataSnapshot snapshot = task.Result;
                    var kids = snapshot.Children;

                    foreach(var pointer in kids)
                    {
                        str += pointer.Child("FirstName").Value.ToString() + " ";
                        str += pointer.Child("SecondName").Value.ToString() + " ";
                        str += pointer.Child("Age").Value.ToString() + " ";
                        str += pointer.Child("Gender").Value.ToString() + " ";
                        str += pointer.Child("PhoneNumber").Value.ToString() + " ";
                        str += pointer.Child("Email").Value.ToString() + "\n";
                    }

                    App.View
                        .CabinetView
                        .SetTextAboutKids(str);
                  }
            });
    }

    /// <summary>
    /// This function get admin from Fire Base if it exists.
    /// </summary>
    /// <param name="key"></param>
    public void SendAdminRequestToFireBase(Admin Admin)
    {
        string key = Admin.UserName + Admin.UserPassword;

        Debug.Log("SendAdminRequestToFireBase with key:" + key);
        RestClient.Get<Admin>(BaseRoute + "admin/" + key + ".json").Then(response =>
        {
            if(response.UserName != null)
            {
                CurrAdmin = response;
                App.Notify(BubbleNotification.AdminLoaded, this);
            }
        });
    }
}
