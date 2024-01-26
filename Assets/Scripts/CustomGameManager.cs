using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CustomGameManager : Singleton<CustomGameManager>
{

    [SerializeField] public GameObject Player;
    [SerializeField] public ProximityManager ProximityManager;

    [SerializeField] public Transform CameraTransform;

    private void Awake()
    {
        ProximityManager = Player.GetComponent<ProximityManager>();
    }

}
