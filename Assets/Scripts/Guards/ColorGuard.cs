using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGuard : MonoBehaviour
{
    [SerializeField] private GuardType _guardType;

    [SerializeField] private MeshRenderer _renderer;

    [SerializeField] private Color _skinColor;
    [SerializeField] private Color _ClothesColor;

    [SerializeField] private MeshRenderer _hatRenderer;

    private GlobalCharacterColors _globalColors;

    void Start()
    {
        _globalColors = CustomGameManager.Instance.GlobalCharacterColors;

        DetermineGuardColor();
    }

    private void DetermineGuardColor()
    {
        switch (_guardType)
        {
            case GuardType.WOLF:
                ApplyGuardColor(_globalColors.GetRandomSkinColor(), _globalColors.WolfGuardsUniforms);
                return;
            case GuardType.BITCH:
                ApplyGuardColor(_globalColors.GetRandomSkinColor(), _globalColors.BitchGuardColors);
                return;
            default:
                ApplyGuardColor(_globalColors.GetRandomSkinColor(), _globalColors.LackeyGuardColors);
                return;
        }
    }

    private void ApplyGuardColor(Color skin, Color clothes)
    {
        _skinColor = skin;
        _ClothesColor = clothes;

        _renderer.material.SetColor("_SkinColor", skin);
        _renderer.material.SetColor("_ClothesColor", clothes);

        _hatRenderer.material.color = clothes;
    }
}
public enum GuardType
{
    WOLF = 0, BITCH = 1, LACKEY = 2
}