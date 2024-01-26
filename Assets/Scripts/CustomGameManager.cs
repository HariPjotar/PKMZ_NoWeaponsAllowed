using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CustomGameManager : Singleton<CustomGameManager>
{

    [SerializeField] public GameObject Player;
    [SerializeField] public ProximityManager ProximityManager;

    [SerializeField] public Transform CameraTransform;

    [SerializeField] public GlobalCharacterColors GlobalCharacterColors;

    private void Awake()
    {
        ProximityManager = Player.GetComponent<ProximityManager>();

        GlobalCharacterColors = Resources.Load("GlobalCharacterColors") as GlobalCharacterColors;
    }

}
