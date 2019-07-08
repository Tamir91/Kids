using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FileLoader : BubbleElement {
    #region Variables
    // Static Variables
    private static FileLoader Single;
    // Private Variables
    private string PersonRoute = "https://project-1c5c7.firebaseio.com/";
    private DatabaseReference DatabaseReference;
    
 
    // Public Variables

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

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(PersonRoute);
        DatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    /// <summary>
    /// This function get person from FireBase and set him into text in Cabinet View.
    /// </summary>
    /// <param name="keyPhoneNumber">Person phone number</param>
    /// <returns>IEnumerator</returns>
    public void SendPersonRequestToFireBase(string keyPhoneNumber)
    {
        Debug.Log("SendPersonRequestToFireBase with key:" + keyPhoneNumber);
        RestClient.Get<Person>(PersonRoute + keyPhoneNumber + ".json").Then(response =>
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
    public void GetAllPersons()
    {
        string str = "";
        FirebaseDatabase.DefaultInstance
            .GetReference("")
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


                    //Person person = JsonUtility.FromJson<Person>(PersonObjects2);
                    //JsonHelper.ArrayToJsonString<Person>();

                    // Do something with snapshot...
                }
            });
    }

  
 
}
