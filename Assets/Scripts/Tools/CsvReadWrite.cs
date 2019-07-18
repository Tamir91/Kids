using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour
{
    private string filepath;

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
        try
        {
            if (!File.Exists(filepath)){
                using (StreamWriter file = new StreamWriter(filepath, true))
                {
                    file.WriteLine("דואל,נייד,מין,גיל,שם משפחה,שם\n");
                }
            }
            using (StreamWriter file = new StreamWriter(filepath, true))
            {
                file.WriteLine(data);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error write string" + ex);
        }
    }
}
