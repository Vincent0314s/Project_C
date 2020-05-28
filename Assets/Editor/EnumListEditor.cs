using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnumListEditor : EditorWindow
{
    [SerializeField]
    private List<EnumList> enumLists = new List<EnumList> {
        new EnumList("VisualEffect"),
        new EnumList("SoundEffect"),
        new EnumList("UI_SoundEffects"),
        new EnumList("AIOwner"),
    };

    private List<EnumList> savedList;


    [MenuItem("QuickTool/Vincent_EnumGenerator")]
    public static void ShowWindow()
    {
         GetWindow<EnumListEditor>("EnumList");
    }

    private void OnDestroy()
    {
        savedList = new List<EnumList>(enumLists);
        if (savedList.Count > 0)
        {
            var newWin = Instantiate(this);
            newWin.enumLists = savedList;
        }
    }

    void OnGUI()
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("enumLists");

        EditorGUILayout.PropertyField(stringsProperty,true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Update Enum List"))
        {
            EnumListManager.AddNewEnum(enumLists);
        }
    }
}
