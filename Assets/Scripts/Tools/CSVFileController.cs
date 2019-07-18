using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVFileController: BubbleElement {
    private static CSVFileController writeToCSV;

    private void Awake()
    {
        if (writeToCSV == null)
        {
            writeToCSV = this;
        }
        else
            Destroy(gameObject);

    }



    public void Save(string[] arr)
    {
        string str = "";

        str += arr[5] + ",";
        str += arr[4] + ",";
        str += HebrewSwop(arr[3]) + ",";
        str += arr[2] + ",";
        str += HebrewSwop(arr[1]) + ",";
        str += HebrewSwop(arr[0]) + "\n";

        var CsvReadWrite = FindObjectOfType<CsvReadWrite>();
        CsvReadWrite.Save(str);
    }

    private string HebrewSwop(string word)
    {
        string str = "";

        for(int i = word.Length - 1; i >= 0; i--)
        {
            str += word[i];
        }

        return str;
    }
}
