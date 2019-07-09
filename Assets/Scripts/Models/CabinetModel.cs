
using UnityEngine;

public class CabinetModel : BubbleElement {
    #region Variables
    // Staitic Variables
    private static CabinetModel Single;
    // Private Variables

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

    /// <summary>
    /// This function set person form FireBase into view
    /// </summary>
    /// <param name="key">Kid phone number</param>
    public void UpdateCabinetViewWithKid(string key)
    {
        FileLoader FileLoader = FindObjectOfType<FileLoader>();
        FileLoader.SendPersonRequestToFireBase(key);
    }

    /// <summary>
    /// This function set all persons form FireBase into view
    /// </summary>
    public void UpdateCabinetViewWithAllKids(string key)
    {
        Debug.Log("UpdateCabinetViewWithAllPersons");
        FileLoader FileLoader = FindObjectOfType<FileLoader>();
        FileLoader.GetAllKids(key);
    }
}
