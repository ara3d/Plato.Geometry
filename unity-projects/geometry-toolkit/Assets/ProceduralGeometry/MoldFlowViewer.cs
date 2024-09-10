using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoldFlowViewer : MonoBehaviour
{
    private Vector3[] Points;
 
    public void DrawMesh(Mesh mesh, Material material, Vector3 position)
    {
        Graphics.DrawMesh(mesh, position, Quaternion.identity, material, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        var lines =
            File.ReadAllLines(
                "C:\\Users\\cdigg\\git\\ara3d-dev\\3d-format-shootout\\data\\files\\moldflow\\MidSize_4-DecimalPoints.txt");

        Points = lines.Select(ConvertMoldFlowPoint).ToArray();
    }

    public static float ReadMoldFlowValue(string s)
    {
        if (float.TryParse(s.Substring(1), out var result))
            return result;
        return 0f;
    }

    public static Vector3 ConvertMoldFlowPoint(string s)
    {
        var data = s.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        return new Vector3(
            ReadMoldFlowValue(data[1]),
            ReadMoldFlowValue(data[2]),
            ReadMoldFlowValue(data[3]));
    }
    
    void Update()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var material = GetComponent<MeshRenderer>().sharedMaterial;
        foreach (var p in Points)
            DrawMesh(mesh, material, p);
    }
}
