using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialInstancer : Editor {
 
    [MenuItem("GameObject/Instance Material")]
    public static void Instance()
    {
        if(Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<MeshRenderer>() == null){
            Debug.LogError("No Valid Object Selected");
            return;
        }
         
        Material mat = Selection.activeGameObject.GetComponent<MeshRenderer>().sharedMaterial;
        Selection.activeGameObject.GetComponent<MeshRenderer>().sharedMaterial = new Material(mat);
 
    }
 
}
