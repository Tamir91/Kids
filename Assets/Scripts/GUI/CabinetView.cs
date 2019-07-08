using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetView : BubbleElement {
    #region Variables

    // Static variables
    private static CabinetView Single;

    // Private Variables
    private string text;

    // Public Variables
    public GameObject CabinetCanvas;
    public string SearchField { get; set; }

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

        SearchField = "Write phone number here";
        //SetTextAboutKids();
    }
  
    public void ShowCabinetPage()
    {
        StartCoroutine(MoveCabinetPage());
    }

    private IEnumerator MoveCabinetPage()
    {
        Debug.Log("MoveCabinetPage");
        var rect = CabinetCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(400f, 640f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnSearchClicked()
    {
        SearchField = CabinetCanvas.transform.GetChild(1).GetComponent<InputField>().text;
        App.Notify(BubbleNotification.SearchPhoneNumberClicked, this);
    }

    public void OnBackClicked()
    {
        StartCoroutine(MoveBackCabinetPage());
    }

    private IEnumerator MoveBackCabinetPage()
    {
        Debug.Log("MoveCabinetPage");
        var rect = CabinetCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(-400f, 640f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void SetTextAboutKids(string text)
    {
        CabinetCanvas.transform.GetChild(0).GetComponent<Text>().text = text;
        this.text = text;
    }
}
