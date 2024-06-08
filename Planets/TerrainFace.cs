// Necessary Unity Installments
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class focussed on defining and constructing the logic behind the surface normals of the planet
public class TerrainFace
{
    // Class declarations
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    // Constructor class
    public TerrainFace (ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        // Local value assignments
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // Stores Longitudinal axis data
        axisA = new Vector3 (localUp.y, localUp.z, localUp.x);

        // Stores lattitudinal axis data
        axisB = Vector3.Cross (localUp, axisA);
    }

    // Method to create the mesh / colliders of the planet
    public void ConstructMesh ( )
    {
        // Initialize an array to store the necessary vertices
        Vector3 [ ] vertices = new Vector3 [resolution * resolution];

        // Calculate the number of necessary vertices
        int [ ] triangles = new int [(resolution - 1) * (resolution - 1) * 6];

        // Initialize an index of the amount of triangles necessary
        int triIndex = 0;

        // Per row in the mesh coordinate plane
        for (int y = 0; y < resolution; y++)
        {
            // Per column in the mesh coordinate plane
            for (int x = 0; x < resolution; x++)
            {
                // Calculating the index in the vertices array for the current grid point
                int i = x + y * resolution;

                // Declaring vector responsible for initializing coordinate plane positions
                Vector2 percent = new Vector2 (x, y) / (resolution - 1);

                // Calculating the location of an object on the coordinate plane
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;

                // Normalizes the distance of each point from the center of the planet, turning it into a sphere 
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                // Calculating the final position of the vertices, and storing the info to the proper index
                vertices [i] = shapeGenerator.CalculatePointOnPlanet (pointOnUnitSphere);

                // If the grid is with in the current boundary
                if (x != resolution - 1 && y != resolution - 1)
                {
                    // Define the triangles necessary to fill the grid

                    // Setting the vertices of the first triangle
                    triangles [triIndex] = i;
                    triangles [triIndex + 1] = i + resolution + 1;
                    triangles [triIndex + 2] = i + resolution;

                    // Setting the vertices of the second triangle
                    triangles [triIndex + 3] = i;
                    triangles [triIndex + 4] = i + 1;
                    triangles [triIndex + 5] = i + resolution + 1;

                    // Adding the newly created vertices to the triangle registry
                    triIndex += 6;
                }
            }
        }

        // Clear all mesh data
        mesh.Clear ( );

        // Assign number of vertices to the mesh
        mesh.vertices = vertices;

        // Assigning the triangles array to the mesh
        mesh.triangles = triangles;

        // Recalculating the normals for the mesh to ensure correct rendering
        mesh.RecalculateNormals ( );
    }
}
