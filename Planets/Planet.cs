// Necessary Unity Installments
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main class
public class Planet : MonoBehaviour
{
    // Class declarations
    // Range of the minimun to the maximum number of tirangles on the planet
    [Range (2, 256)]

    // Number of triangles on the planet
    public int resolution = 10;

    // Initializes bool value
    public bool autoUpdate = true;

    // Initialize planets shape settings
    public ShapeSettings shapeSettings;

    // Initialize planets color settings
    public ColorSettings colorSettings;

    // Command to hide this tab in the Unity inspector
    [HideInInspector]

    // Initialize the state of the shape editing menu in the Unity editor
    public bool shapeSettingsFoldout;

    // Command to hide this tab in the Unity inspector
    [HideInInspector]

    // Initialize the state of the color editing menu in the Unity editor
    public bool colorSettingsFoldout;

    // Initialize the logic from the ShapeGenerator script
    ShapeGenerator shapeGenerator;

    // Constantly save data values
    [SerializeField, HideInInspector]

    // Initialize array to store planets meshes
    MeshFilter[] meshFilters;

    // Initialize array to store planets terrain faces
    TerrainFace[] terrainFaces;

    // Method to initialize the planet's mesh and terrain facees
    void Initialize ( )
    {
        // Creating a new ShapeGenerator instance with the planets saved settings
        shapeGenerator = new ShapeGenerator (shapeSettings);

        // Checking if meshFilters is null or empty
        if (meshFilters == null || meshFilters.Length == 0)
        {
            // Sets how many sides the planet has
            meshFilters = new MeshFilter [6];
        }

        // Sets how many terrain faces are necessary depending on how many sides the planet has
        terrainFaces = new TerrainFace[6];

        // Initializes a vector holding all possible directions of the faces of thre planet 
        Vector3 [ ] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        // Per side of the planet
        for (int i = 0; i < 6; i++)   
        {
            // Checking if meshFilters is null or empty
            if (meshFilters [i] == null)
            {
                // Create a new mesh gameobject for the individual side of the planet
                GameObject meshObject = new GameObject ("mesh");

                // Apply the planets settings to the mesh
                meshObject.transform.parent = transform;

                // Adding a Renderer component to the face and setting its material to a standard shader
                meshObject.AddComponent<MeshRenderer> ( ).sharedMaterial = new Material (Shader.Find ("Standard"));

                // Add the mesh to the meshFilter array
                meshFilters [i] = meshObject.AddComponent<MeshFilter> ( );

                // Create the new mesh and store it in the respective array
                meshFilters [i].sharedMesh = new Mesh ( );
            }

            // Creating a new TerrainFace for the current face and storing it in the terrainFaces array.
            terrainFaces [i] = new TerrainFace (shapeGenerator, meshFilters[i].sharedMesh, resolution, directions [i]);
        }
    }

    // Method to actually create the planet
    public void GeneratePlanet ( )
    {
        // Initialize the planets mesh and terrain faces
        Initialize ( );

        // Construct the terrain face of each face of the planet
        GenerateMesh ( );

        // Apply the planets color to the planets mesh
        GenerateColors ( );
    }

    // Update the planets shape settings live in the Unity editor
    public void OnShapeSettingsUpdated ( )
    {
        // Always auto update is set to true
        if (autoUpdate)
        {
            // Re- load the planets terrain faces and mesh to reflect the changes in shape 
            Initialize ( );

            // Re- load the mesh to reflect the changes in shape for each terrain face
            GenerateMesh ( );
        }
    }

    // Update the planets color settings live in the unity editor
    public void OnColorSettingsUpdated ( )
    {
        // If any of the planets settings have been updated
        if (autoUpdate)
        {
            // Re-Load the planets terrain faces and mesh to reflect the changes in color
            Initialize ( );

            // Re-Load the mesh to reflect the changes in color for each terrain face
            GenerateMesh ( );
        }
    }


    // Method to construst the mesh of each terrain face
    void GenerateMesh ( )
    {
        // Per terrain face on the planet
        foreach (TerrainFace face in terrainFaces)
        {
            // Constructthe mesh for that face
            face.ConstructMesh();
        }
    }

    // Method to apply the planets color to its mesh
    void GenerateColors ( )
    {
        // For each mesh filter
        foreach (MeshFilter m in meshFilters)
        {
            // Apply the planets color to the mesh 
            m.GetComponent<MeshRenderer> ( ).sharedMaterial.color = colorSettings.planetColor;
        }
    }
}
