
using Proyecto26;
using System;
//using UnityEditor;
using UnityEngine;

public class FileSaver : BubbleElement {

    #region Variable
    // Static Variables
    private static FileSaver Single;
    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        //EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }
        // Public Variables

        //Privte Variables
    private string PersonRoute = "https://project-1c5c7.firebaseio.com/";


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
    // Use this for initialization

    private void Start()
    {
        // // Firebase code not work for me
        //Debug.Log(m.ToString());
        //Debug.Log("FirebaseApp.DefaultInstance.SetEditorDatabaseUrl");
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kids-62a38.firebaseio.com/");
        //Reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Need testing. Not Shure if it work
    public void PutPersonToFireBase(Person NewPerson)
    {
        RestClient.Put<Person>(PersonRoute + NewPerson.PhoneNumber + ".json", NewPerson);
        /*
        RestClient.Put<Person>(new RequestHelper
        {
            Uri = PersonRoute,
            Body = new Person(NewPerson),
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) => {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        }, (err, res, body) => {
            if (err != null)
            {
                this.LogMessage("Error", err.Message);
            }
            else
            {
                this.LogMessage("Success", res.Text);
            }
        });
        */
    }

    public void PostPersonToFireBase(Person Person)
    {
        //RestClient.Post(PersonRoute + Person.PhoneNumber + ".json", Person);

        RestClient.Post<Person>(PersonRoute, Person)
        .Then(res => this.LogMessage("Success", JsonUtility.ToJson(res, true)))
        .Catch(err => this.LogMessage("Error", err.Message));
    }
}