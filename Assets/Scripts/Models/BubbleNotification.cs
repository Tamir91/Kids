﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleNotification : MonoBehaviour {

    public const  string OnLogInClicked = "login.clicked";
    public const string OnSaveKidClicked = "on.save.kid.clicked";
    public const string LoadAllKids = "load.all.kids.clicked";
    public const string KidsDownloaded = "all.kids.downloaded";
    public const string OnCreateExcelClicked = "create.excel.clicked";
    public const string LoadKidByNumber = "load.kid.clicked";
    public const string DeleteKidByNumber = "delete.kid.bynumber";
    public const string EditKidByNumber = "edit.kid.bynumber";
    public const string CreateAccount = "create.account.clicked";

    // In LoginView
    public const string OnSignUpInLoginClicked = "signup.in.view.clicked";
    public const string OnCreateProfileClicked = "create.profile.clicked";

    public const string GoToSignUpClicked = "backto.signup.clicked";
    public const string GoToLogInPageClicked = "goto.login.clicked";
    public const string AdminLoaded = "adminn.loaded";

    public const string PersonRecievedFromFireBase = "PersonRecievedFromFireBase";
}
