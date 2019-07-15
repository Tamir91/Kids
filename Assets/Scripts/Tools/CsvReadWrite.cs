using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

public class CsvReadWrite : MonoBehaviour
{
    private string filepath;
    private string delimiter = ",";

    private int length;
    void Start()
    {
        PreparePath();
    }

    private void PreparePath()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/Kids List.csv";
#elif UNITY_ANDROID
        filepath = Application.persistentDataPath + "/Members.csv";
#elif UNITY_IPHONE
        filepath = Application.persistentDataPath + "/Members.csv";
#else
        filepath = Application.dataPath + "/Members.csv";
#endif

        string[] rowDataTemp = new string[9];
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Specialty";
        rowDataTemp[2] = "Country";
        rowDataTemp[3] = "Email";
        rowDataTemp[4] = "Receive news and updates";
        rowDataTemp[5] = "Question 1";
        rowDataTemp[6] = "Question 2";
        rowDataTemp[7] = "Question 3";
        rowDataTemp[8] = "Data";

        length = rowDataTemp.Length;

        if (!File.Exists(filepath))
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, rowDataTemp));

            StreamWriter outStream = File.CreateText(filepath);
            outStream.WriteLine(sb);
            outStream.Close();
        }
    }

    public void Save(string data)
    {
        Debug.Log("Save::In");
        //StringBuilder sb = new StringBuilder();
        //sb.AppendLine(string.Join(delimiter, data));
        File.AppendAllText(filepath, data);
    }
}
