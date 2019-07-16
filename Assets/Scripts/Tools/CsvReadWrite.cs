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


    public void Save(string data)
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/Kids List.xls";
#elif UNITY_ANDROID
        filepath = Application.persistentDataPath + "/Members.csv";
#elif UNITY_IPHONE
        filepath = Application.persistentDataPath + "/Members.csv";
#else
        filepath = Application.dataPath + "/Members.csv";
#endif

        if (!File.Exists(filepath))
        {
            StreamWriter outStream = File.CreateText(filepath);
            outStream.Close();
        }
        if (File.Exists(filepath))
        {
            Debug.Log("Save::In");

            StreamWriter outStream = File.CreateText(filepath);
            outStream.WriteLine(data);
            outStream.Close();
        }
       
    }
}
