﻿using Firebase;
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
    private DependencyStatus dependencyStatus;


    // Public Variables
    private Admin CurrAdmin { get; set; }

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

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("DependencyStatus.Available");
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }



    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(BaseRoute);
        DatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// This function get person from FireBase and set him into text in Cabinet View.
    /// </summary>
    /// <param name="keyPhoneNumber">Kid phone number</param>
    /// <returns>IEnumerator</returns>
    public void SendKidRequestToFireBase(string Key, string keyPhoneNumber)
    {
        Debug.Log("SendKidRequestToFireBase with key:" + keyPhoneNumber);
        RestClient.Get<Kid>(BaseRoute + "kids/" + Key + "key/" + keyPhoneNumber + ".json").Then(response =>
        {
            string text = "לאוד                דיינ          ןימ   ליג   החפשמה םש     םש\n\n";
            App.View
            .CabinetView
            .SetTextAboutKids(text + response.ToString());
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

        string str = "לאוד                דיינ          ןימ   ליג   החפשמה םש     םש\n\n";
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
                    Debug.Log("GetAllPersons::task.IsCompleted");
                    DataSnapshot DataSnapshot = task.Result;
                    var kids = DataSnapshot.Children;

                    foreach(var pointer in kids)
                    {
                        str += pointer.Child("Email").Value.ToString() + "\t\t";
                        str += pointer.Child("PhoneNumber").Value.ToString() + "\t\t";
                        str += pointer.Child("Gender").Value.ToString() + "\t\t";
                        str += pointer.Child("Age").Value.ToString() + "\t\t\t";
                        str += pointer.Child("SecondName").Value.ToString() + "\t\t";
                        str += pointer.Child("FirstName").Value.ToString() + "\n";        
                    }

                        App.View
                        .CabinetView
                        .SetTextAboutKids(str);

                    Debug.Log(str);                    
                  }
            });
    }

    public void LoadAllKidsInExcelstring(string key)
    {

        string[] arr = new string[6];

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
                    Debug.Log(key + "key");
                    Debug.Log("GetAllPersons::task.IsCompleted");
                    DataSnapshot DataSnapshot = task.Result;
                    var kids = DataSnapshot.Children;
                    
                    var CsvController = FindObjectOfType<CSVFileController>();

                    foreach (var pointer in kids)
                    {
                        arr[0] = pointer.Child("FirstName").Value.ToString();
                        arr[1] = pointer.Child("SecondName").Value.ToString();
                        arr[2] = pointer.Child("Age").Value.ToString();
                        arr[3] = pointer.Child("Gender").Value.ToString();
                        arr[4] = pointer.Child("PhoneNumber").Value.ToString();
                        arr[5] = pointer.Child("Email").Value.ToString();

                        CsvController.Save(arr);
                    }
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
