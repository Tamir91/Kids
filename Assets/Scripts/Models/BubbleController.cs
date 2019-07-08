using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : BubbleElement {
#region variables
    // Static methods
    private static BubbleController Controller;
    // Private methods
    private enum AppState {AboutApp, Login, Signup, Cabinet};
    private AppState appState;

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
        appState = AppState.Cabinet;
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
                        .ShowLoginPage();
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
            case BubbleNotification.GoToCabinetClicked:
                {
                    appState = AppState.Cabinet;
                    ChangeAppState(appState);
                    break;
                }  
            case BubbleNotification.SignUpClicked:
                { 
                    string FirstName = App.View
                        .SignUpView
                        .GetFirstName();
                    string SecondName = App.View
                       .SignUpView
                       .GetSecondName();
                    string Email = App.View
                        .SignUpView
                        .GetEmail();
                    string Age = App.View
                        .SignUpView
                        .GetAge();
                    string Gender = App.View
                        .SignUpView
                        .GetGender();
                    string PhoneNumber = App.View
                      .SignUpView
                      .GetPhoneNumber();

                    Person Person = new Person(FirstName, SecondName, Age, Gender, PhoneNumber, Email);
                    App.Model
                        .PersonModel
                        .SaveUserPersonalData(Person);

                    break;
                }

            case BubbleNotification.SearchPhoneNumberClicked:
                {
                    App.Model
                        .CabinetModel
                        .UpdateCabinetViewWithAllPersons(/*App.View.CabinetView.SearchField*/);
                    break;
                }
        }
    }
}
 