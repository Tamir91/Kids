using UnityEngine;

public class BubbleController : BubbleElement {
#region variables
    // Static methods
    private static BubbleController Controller;
    // Private methods
    private enum AppState {AboutApp, Login, Signup, Cabinet};
    private AppState appState;
    private Admin CurrAdmin;

    // Public methods

    #endregion

    private void Awake()
    {
        if(Controller == null)
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
        //appState = AppState.AboutApp;
        appState = AppState.AboutApp;
        ChangeAppState(appState);
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
                    break;
                }
            case BubbleNotification.LoadKid:
                {
                    string key = CurrAdmin.UserName + CurrAdmin.UserPassword;
                    string phoneNumber = App.View.CabinetView.SearchField;
                    App.Model
                        .CabinetModel
                        .UpdateCabinetViewWithKid(key, phoneNumber);
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
}
 