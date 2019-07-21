using Mosframe;
using System;
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
        Vector2 newPos = new Vector2(400f, 225f);

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
        Vector2 newPos = new Vector2(-400f, 225f);

        while (rect.anchoredPosition != newPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, newPos, 50f);
            yield return null;
        }
    }

    public void SetKidsInScrollView(List<Kid> Kids)
    {
        int counter = 0;
        var realTimeInsertItemExample = FindObjectOfType<RealTimeInsertItemExample>();
        realTimeInsertItemExample.SetKidsInList(Kids);

        foreach (Kid Kid in Kids)
        {
            realTimeInsertItemExample.InsertKidToView(counter++, Kid.ToString());
        }
    }

    public void OnFindKidClicked()
    {
        SearchField = CabinetCanvas.transform.GetChild(1).GetComponent<InputField>().text;
        App.Notify(BubbleNotification.LoadKidByNumber, this);
    }

    public void OnCreateExcelClicked()
    {
        App.Notify(BubbleNotification.OnCreateExcelClicked, this);
    }

    public void ShowDeleteDialog()
    {
        CabinetCanvas.transform.GetChild(7).gameObject.SetActive(true);
    }

    internal void HideDeleteDialog()
    {
        CabinetCanvas.transform.GetChild(7).gameObject.SetActive(false);
    }
}
