// Necessary Unity Installments
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for generating size and shape of planet
public class ShapeGenerator
{
    // Initializes settings variables
    ShapeSettings settings;

    // Constructor Class
    public ShapeGenerator (ShapeSettings settings)
    {
        // Assigning settings
        this.settings = settings;
    }

    // Method to calculate a point on the planet's surface given a point on a unit sphere
    public Vector3 CalculatePointOnPlanet (Vector3 pointOnUnitSphere)
    {

        // Returns the distance of the specified point according to the planets radius
        return pointOnUnitSphere * settings.planetRadius;
    }
}
