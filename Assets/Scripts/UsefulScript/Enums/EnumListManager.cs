#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class EnumListManager
{
    const string extension = ".cs";
    public static string filePath = Application.dataPath+"/";
    public static string fileName = "EnumListStorage";

    //Used By Editor
    public static void AddNewEnum(List<EnumList> data)
    {
        using (StreamWriter sw = File.CreateText(filePath + fileName + extension))
        {
            foreach (var list in data)
            {
                sw.WriteLine("public enum " + list.name + " \n{");
                foreach (var item in list.enumVariable)
                {
                    string lineRep = item.ToString().Replace(" ", string.Empty); //Remove Extra Space
                    if (!string.IsNullOrEmpty(lineRep)) // if line is not null
                    {
                        sw.WriteLine(string.Format("\t{0},", lineRep));
                    }
                }
            sw.WriteLine("\n}");
            }
        }

        AssetDatabase.Refresh();
    }
}
#endif
