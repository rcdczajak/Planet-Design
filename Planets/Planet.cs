using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range (2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;

    [HideInInspector]
    public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void Initialize ( )
    {
        shapeGenerator = new ShapeGenerator (shapeSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter [6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3 [ ] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)   
        {
            if (meshFilters [i] == null)
            {
                GameObject meshObject = new GameObject ("mesh");
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer> ( ).sharedMaterial = new Material (Shader.Find ("Standard"));
                meshFilters [i] = meshObject.AddComponent<MeshFilter> ( );
                meshFilters [i].sharedMesh = new Mesh ( );
            }

            terrainFaces [i] = new TerrainFace (shapeGenerator, meshFilters[i].sharedMesh, resolution, directions [i]);
        }
    }

    public void GeneratePlanet ( )
    {
        Initialize ( );
        GenerateMesh ( );
        GenerateColors ( );
    }

    public void OnShapeSettingsUpdated ( )
    {
        if (autoUpdate)
        {
            Initialize ( );
            GenerateMesh ( );
        }
    }

    public void OnColorSettingsUpdated ( )
    {
        if (autoUpdate)
        {
            Initialize ( );
            GenerateMesh ( );
        }
    }

    void GenerateMesh ( )
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColors ( )
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer> ( ).sharedMaterial.color = colorSettings.planetColor;
        }
    }
}
