using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadaColorScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer _ladaRenderer;

    private void Start()
    {
        ColorLada();
    }

    private bool DecideIfRusty()
    {
        return Random.Range(1, 10) < 7;
    }

    private void ColorLada()
    {
        List<Color> colors = CustomGameManager.Instance.GlobalCharacterColors.NPCClothesColors;

        Color carColor = colors[Random.Range(0, colors.Count)];

        _ladaRenderer.material.SetColor("_CarColor", carColor);

        if (DecideIfRusty())
        {
            _ladaRenderer.material.SetInt("_HasRust", 1);
            _ladaRenderer.material.SetFloat("_RustingAmount", Random.Range(0f, 1f));
            _ladaRenderer.material.SetFloat("_RustingResolution", Random.Range(200f, 800f));
        }
    }
}
