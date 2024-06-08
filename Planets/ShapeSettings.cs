// Necessary Unity Installments
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows Unity to create a special menu dedicated to the shape of the planet
[CreateAssetMenu ( )]

//public class defining the settings necessary for the shape of the planet
public class ShapeSettings : ScriptableObject
{
    // Class Declarations
    public float planetRadius = 1;
}
