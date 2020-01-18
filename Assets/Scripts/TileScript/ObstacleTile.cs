using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleTile : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/ObstacleTile")]
    public static void CreateTreeTile()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save ObstacleTile",
            "ObstacleTile",
            "asset",
            "Save obstacleTile",
            "Assets");
        if (path.Length == 0)
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ObstacleTile>(), path);
    }
#endif
}