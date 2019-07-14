using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class CSVFileController: BubbleElement {
    private static CSVFileController writeToCSV;
    private readonly string PATH = "/KidsList_" + ".csv";

    private void Awake()
    {
        if (writeToCSV == null)
        {
            writeToCSV = this;
        }
        else
            Destroy(gameObject);
    }

    public void AddRecord(string KidsData)
    {
        StartCoroutine(CrearArchivoCSV(KidsData));
    }

    IEnumerator CrearArchivoCSV(string KidsData)
    {
        string filePath = Application.persistentDataPath + PATH;
        
        var sr = File.CreateText(filePath);
        
        sr.WriteLine(KidsData);

        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(filePath);
        fInfo.IsReadOnly = true;

        //Cerrar
        sr.Close();

        yield return new WaitForSeconds(0.5f);//Esperamos para estar seguros que escriba el archivo

        //Abrimos archivo recien creado
        Application.OpenURL(filePath);
    }
}
