using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all elements in this application.
public class BubbleElement : MonoBehaviour
{
    public BubbleApplication App{ get { return FindObjectOfType<BubbleApplication>(); } }
}

public class BubbleApplication : MonoBehaviour
{
    static BubbleApplication single;
    // Reference to the root instances of the MVC.
    public BubbleModel Model{ get { return FindObjectOfType<BubbleModel>(); } }
    public BubbleView View { get { return FindObjectOfType<BubbleView>(); } }

    public BubbleController controller;

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

    // Init things here
    private void Start()
    {
        /*
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase.DependencyStatus.Available");
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        */
    }

    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        List<BubbleController> list = GetAllControllers();
        foreach (BubbleController c in list)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }

    private List<BubbleController> GetAllControllers()
    {
        List<BubbleController> controller_list = new List<BubbleController>() ;
        controller_list.Add(FindObjectOfType<BubbleController>());
        return controller_list;
    }
}
