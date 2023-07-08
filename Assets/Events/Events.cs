using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Events : ScriptableObject
{
    public string eventContent;
    #if UNITY_EDITOR 
       [MenuItem("Assets/Create/news event")]
       public static void createEvent()
        {
        string path = EditorUtility.SaveFilePanelInProject("save event","new event","asset","save event", "Assets/Events");
        if (path == "")
        { return; }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Events>(),path);
        }
    #endif
}
