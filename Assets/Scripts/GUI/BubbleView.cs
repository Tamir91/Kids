using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all views related to the app.
public class BubbleView : BubbleElement {

    public LogInView LoginView { get { return FindObjectOfType<LogInView>(); } }
    public SignUpView SignUpView { get { return FindObjectOfType<SignUpView>(); } }
    public CabinetView CabinetView { get { return FindObjectOfType<CabinetView>(); } }
   
}
