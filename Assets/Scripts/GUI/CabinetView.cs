using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetView : BubbleElement {
    #region Variables

    // Static variables
    private static CabinetView Single;

    // Private Variables
   

    // Public Variables
    public GameObject CabinetCanvas;
    public string SearchField { get; set; }
    public string TextWithKids { get; set; }

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
  
    public void ShowCabinetPage()
    {
        StartCoroutine(MoveCabinetPage());
    }

    private IEnumerator MoveCabinetPage()
    {
        Debug.Log("MoveCabinetPage");
        var rect = CabinetCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(400f, 250f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void OnShowAllKidsClicked()
    { 
        App.Notify(BubbleNotification.LoadAllKids, this);
    }

    public void OnBackClicked()
    {
        StartCoroutine(MoveBackCabinetPage());
    }

    private IEnumerator MoveBackCabinetPage()
    {
        Debug.Log("MoveCabinetPage");
        var rect = CabinetCanvas.GetComponent<RectTransform>();
        Vector2 newPos = new Vector2(-400f, 250f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void SetTextAboutKids(string text)
    {
        CabinetCanvas.transform.GetChild(0).GetComponent<Text>().text = text;
        this.TextWithKids = text;
    }

    public void OnFindKidClicked()
    {
        SearchField = CabinetCanvas.transform.GetChild(1).GetComponent<InputField>().text;
        App.Notify(BubbleNotification.LoadKid, this);
    }

    public void OnCreateExcelClicked()
    {
        App.Notify(BubbleNotification.OnCreateExcelClicked, this);
    }
}
