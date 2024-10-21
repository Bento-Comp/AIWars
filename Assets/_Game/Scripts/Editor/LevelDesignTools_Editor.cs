using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelDesignTools))]
public class LevelDesignTools_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        LevelDesignTools myScript = (LevelDesignTools)target;


        if (GUILayout.Button("Gain Gold"))
        {
            myScript.GainGold();
        }

        if (GUILayout.Button("Level Up"))
        {
            myScript.LevelUp();
        }


        for (int i = 0; i < myScript.TargetToTeleportToList.Count; i++)
        {
            if (GUILayout.Button("Teleport to area " + (i+1).ToString()))
            {
                myScript.TeleportObject(myScript.TargetToTeleportToList[i]);
            }
        }

        if (GUILayout.Button("Delete Save File"))
        {
            myScript.DeleteSaveFile();
        }
    }
}
