
using System;
using UnityEngine;

public class CabinetModel : BubbleElement {
    #region Variables
    // Staitic Variables
    private static CabinetModel Single;
    // Private Variables
    private string filepath = "";
    //Public Variables

    #endregion

    private void Awake()
    {
        if (Single == null)
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
#if UNITY_EDITOR
        filepath = Application.dataPath + "/Members.csv";
#elif UNITY_ANDROID
        filepath = Application.persistentDataPath + "/Members.csv";
#elif UNITY_IPHONE
        filepath = Application.persistentDataPath + "/Members.csv";
#else
        filepath = Application.dataPath + "/Members.csv";
#endif
    }

    /// <summary>
    /// This function set person form FireBase into view
    /// </summary>
    /// <param name="key">Kid phone number</param>
    public void UpdateCabinetViewWithKid(string Key, string PhoneNumber)
    {
        FileLoader FileLoader = FindObjectOfType<FileLoader>();
        FileLoader.SendKidRequestToFireBase(Key, PhoneNumber);
    }

    /// <summary>
    /// This function set all persons form FireBase into view
    /// </summary>
    public void UpdateCabinetViewWithAllKids(string key)
    {
        Debug.Log("UpdateCabinetViewWithAllPersons key - " + key);
        FileLoader FileLoader = FindObjectOfType<FileLoader>();
        FileLoader.GetAllKids(key);
    }

    internal void SaveInExcel(string key)
    {
        Debug.Log("SaveInExcel");
        var loader = FindObjectOfType<FileLoader>();
        loader.LoadAllKidsInExcelstring(key);
    }

    public void DeleteKid(string key, string phoneNumber)
    {
        FileSaver FileSaver = FindObjectOfType<FileSaver>();
        FileSaver.DeleteKidFromFireBase(key, phoneNumber);
    }
}
