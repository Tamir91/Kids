using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVFileController: BubbleElement {
    private static CSVFileController writeToCSV;
    private string PATH;

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
        string delimiter = ",";
        int length = arr.GetLength(0);

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, arr[index]));

       
        var CsvReadWrite = FindObjectOfType<CsvReadWrite>();
        CsvReadWrite.Save(sb.ToString());
    }
}
