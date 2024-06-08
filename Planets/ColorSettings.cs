// Necessary Unity Installments
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows Unity to create a special menu dedicated to the color of the planet
[CreateAssetMenu()]

//public class defining the settings necessary for the color of the planet
public class ColorSettings : ScriptableObject
{
    // Class Declarations
    public Color planetColor;
}
