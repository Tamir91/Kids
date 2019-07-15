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

 

    public void Save(string str)
    {
        //StreamWriter outStream = File.CreateText(PATH);
        //outStream.WriteLine(str);

        //outStream.Close();

        //test
        //string[] arr = { "111", "222", "333" };
        var CsvReadWrite = FindObjectOfType<CsvReadWrite>();
        CsvReadWrite.Save(str);
    }
}
