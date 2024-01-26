using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalCharacterColors", menuName = "New Character Colors", order = 0)]
public class GlobalCharacterColors : ScriptableObject
{
    public Color PlayerSkinColor;
    public Color PlayerDefaultClothesColor;
    [Space]
    [Space]
    public List<Color> NPCSkinColors;
    [Space]
    public List<Color> NPCClothesColors;
    [Space]
    [Space]
    public Color WolfGuardsUniforms;
    [Space]
    public Color BitchGuardColors;
    [Space]
    public Color LackeyGuardColors;

    public Color GetRandomSkinColor()
    {
        return NPCSkinColors[Random.Range(0, NPCSkinColors.Count)];
    }

    public Color GetRandomClothesColor()
    {
        return NPCClothesColors[Random.Range(0, NPCClothesColors.Count)];
    }
}
