using Mosframe;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : BubbleElement
{
    #region variables
    // Static methods
    private static BubbleController Controller;
    // Private methods
    private enum AppState { AboutApp, Login, Signup, Cabinet };
    private AppState appState;
    private Admin CurrAdmin;

    private FileLoader FileLoader;

    // Public methods

    #endregion

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        appState = AppState.AboutApp;
        //appState = AppState.Login;
        ChangeAppState(appState);

        FileLoader = FindObjectOfType<FileLoader>();
    }

    void ChangeAppState(AppState appState)
    {
        switch (appState)
        {
            case AppState.AboutApp:
                {

                    break;
                }
            case AppState.Login:
                {
                    Debug.Log("AppState.Login:");
                    App.View
                        .LoginView
                        .ShowLogInPage();
                    break;
                }
            case AppState.Signup:
                {
                    Debug.Log("AppState.Signup:");
                    App.View
                        .SignUpView
                        .ShowSignUpPage();

                   /* App.View
                        .CabinetView
                        .HideCabinetPage();*/

                    break;
                }
            case AppState.Cabinet:
                {
                    Debug.Log("AppState.Cabinet:");
                    App.View
                       .CabinetView
                       .ShowCabinetPage();
                    break;
                }

        }
    }

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case BubbleNotification.GoToLogInPageClicked:
                {
                    appState = AppState.Login;
                    ChangeAppState(appState);
                    break;
                }
            case BubbleNotification.GoToSignUpClicked:
                {
                    appState = AppState.Signup;
                    ChangeAppState(appState);
                    break;
                }
            case BubbleNotification.AdminLoaded:
                {
                    Debug.Log("BubbleController" + p_event_path);
                    appState = AppState.Signup;
                    ChangeAppState(appState);
                    //Next line in test!!
                    FindObjectOfType<RealTimeInsertItemExample>().DeleteAllItems();
                    break;
                }
            case BubbleNotification.OnSaveKidClicked:
                {
                    var V = App.View.SignUpView;
                    var key = CurrAdmin.UserName + CurrAdmin.UserPassword;

                    App.Model
                       .KidModel
                       .SaveKidPersonalData(
                            V.GetFirstName(), V.GetSecondName(), V.GetAge(), V.GetGender(), V.GetPhoneNumber(), V.GetEmail(), key);

                    V.CleanInputFields();

                    break;
                }
            case BubbleNotification.OnLogInClicked:
                {
                    var V = App.View.LoginView;

                    CurrAdmin = new Admin(V.GetCUserName(), V.GetCPassword());

                    App.Model
                        .LoginModel
                        .CheckIfAdminExsist(CurrAdmin);

                    break;
                }
            case BubbleNotification.LoadAllKids:
                {
                    string key = CurrAdmin.UserName + CurrAdmin.UserPassword;
                    App.Model
                        .CabinetModel
                        .UpdateCabinetViewWithAllKids(key);

                    FindObjectOfType<RealTimeInsertItemExample>().DeleteAllItems();
                    break;
                }
            case BubbleNotification.KidsDownloaded:
                {
                    
                    App.View.CabinetView.ClearFromKids();
                    List<Kid> list = new List<Kid>(FileLoader.kidsList);

                    Debug.Log("BubbleNotification.KidsDownloaded: list count = " + list);
                    App.View.CabinetView.SetKidsInScrollView(list);

                    FileLoader.ClearKidList();
                    break;
                }
            case BubbleNotification.LoadKidByNumber:
                {
                    string key = CurrAdmin.UserName + CurrAdmin.UserPassword;
                    string phoneNumber = App.View.CabinetView.SearchField;
                    App.Model
                        .CabinetModel
                        .UpdateCabinetViewWithKid(key, phoneNumber);

                    FindObjectOfType<RealTimeInsertItemExample>().DeleteAllItems();
                    break;
                }
            case BubbleNotification.DeleteKidByNumber:
                {
                    var RealTimeInsertItemExample = FindObjectOfType<RealTimeInsertItemExample>();
                    if(RealTimeInsertItemExample.Kids.Count == 0)
                    {
                        Debug.Log("ERROR");
                        Debug.Log("BubbleNotification.DeleteKidByNumber length = 0");
                    }
                    else{
                        Debug.Log("RealTimeInsertItemExample.Kids length = " + RealTimeInsertItemExample.Kids.Count);
                    }
                    Debug.Log("RealTimeInsertItemExample.deleteIndex  = " + RealTimeInsertItemExample.deleteIndex);
                    string phoneNumber = RealTimeInsertItemExample.Kids[RealTimeInsertItemExample.deleteIndex].PhoneNumber;
                    string key = CurrAdmin.UserName + CurrAdmin.UserPassword;

                    App.Model.CabinetModel.DeleteKid( key, phoneNumber);
                    RealTimeInsertItemExample.DeleteKidFromLists();
                    
                    break;
                }
            case BubbleNotification.EditKidByNumber:
                {
                    var RealTimeInsertItemExample = FindObjectOfType<RealTimeInsertItemExample>();
                    
                    Kid Kid = RealTimeInsertItemExample.Kids[RealTimeInsertItemExample.editIndex];
                    ChangeAppState (AppState.Signup);
                    App.View.CabinetView.HideCabinetPage();

                    var V = App.View.SignUpView;
                    V.FillFieldsWithData(Kid.FirstName, Kid.SecondName, Kid.Age, Kid.Gender, Kid.PhoneNumber, Kid.Email);
                    break;
                }  
             case BubbleNotification.OnSignUpInLoginClicked:
                {
                    App.View
                        .LoginView
                        .ShowSignUpGUI();
                    break;
                }
            case BubbleNotification.OnCreateProfileClicked:
                {
                    string userName = App.View
                        .LoginView.GetCUserName();

                    string userPassword = App.View
                        .LoginView.GetCPassword();

                    Admin Admin = new Admin(userName, userPassword);

                    if (App.View.LoginView.GetRegistrationInputCorection())
                    {
                        App.Model.LoginModel.CreateNewProfile(Admin);
                    }

                    App.View.LoginView.CleanAndHideSignUpGUI();
                    break;
                }
            case BubbleNotification.OnCreateExcelClicked:
                {
                    string key = CurrAdmin.UserName + CurrAdmin.UserPassword;
                    App.Model.CabinetModel.SaveInExcel(key);
                    break;
                }
        }
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}


 