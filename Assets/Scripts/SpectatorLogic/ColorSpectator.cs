using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpectator : MonoBehaviour
{
    [SerializeField] private Color _skinColor;
    [SerializeField] private Color _clothesColor;

    [SerializeField] private MeshRenderer _renderer;

    void Start()
    {
        GlobalCharacterColors globalColors = CustomGameManager.Instance.GlobalCharacterColors;

        _skinColor = globalColors.GetRandomSkinColor();
        _clothesColor = globalColors.GetRandomClothesColor();

        ApplyColor();
    }

    private void ApplyColor()
    {
        _renderer.material.SetColor("_SkinColor", _skinColor);
        _renderer.material.SetColor("_ClothesColor", _clothesColor);
    }

}
