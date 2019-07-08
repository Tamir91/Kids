using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all data related to the app.
public class BubbleModel : BubbleElement {

	public LoginModel LoginModel { get { return FindObjectOfType<LoginModel>(); } }
    public SignUpModel SignUpModel { get { return FindObjectOfType<SignUpModel>(); } }
    public PersonModel PersonModel { get { return FindObjectOfType<PersonModel>(); } }
    public CabinetModel CabinetModel { get { return FindObjectOfType<CabinetModel>(); } }
}
