using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorList", menuName = "ColorList", order = 1)]
public class ColorScriptableObject : ScriptableObject
{
    public Color[] colors;
}
