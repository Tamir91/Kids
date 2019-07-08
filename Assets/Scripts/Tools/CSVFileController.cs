using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVFileController: MonoBehaviour {
    private static CSVFileController writeToCSV;
    private readonly string PATH = "data.csv";

    private void Awake()
    {
        if (writeToCSV == null)
        {
            writeToCSV = this;
        }
        else
            Destroy(gameObject);
    }

    public void AddRecord(string Name, string SecondName, string Age, string NumberPhone, string Email, string Gender)
    {
        if (Name.Equals(""))
            Name = "user";

        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH, true))
            {
                file.WriteLine(Name + "," + SecondName + "," + Age + "," + NumberPhone + "," + Email);
                //file.Close();
            }
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Error write string" + ex);
        }
    }

    public string[] GetWinners(string filepath)
    {
        string[] winneres = new string[200];
        string[] userData;
        string name = "user";
        int score = 0;

        string[] allRecords = GetAllRecords(filepath);

        foreach(string line in allRecords)
        {
            userData = line.Split(new char[] { ',' });
         
            if(userData.Length > 2) {
                if (userData[0] != null && userData[0] != "")
                {
                    name = userData[0];
                }

                if (userData[1] != null && !int.TryParse(userData[1], out score))
                {
                    score = 0;
                }

                if (score >= 0)
                    winneres[score] = name;
            }
            
        }
            return winneres;
    }

    private string[] GetAllRecords(string filepath)
    {
        try
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(@filepath))
            {
                string[] arr =  file.ReadToEnd().Split(new char[] { '\n' });
                //file.Close();
                return arr;
            }
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Error read srtings" + ex);
        }
    }
}
