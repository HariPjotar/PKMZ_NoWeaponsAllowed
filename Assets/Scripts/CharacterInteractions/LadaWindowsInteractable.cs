using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadaWindowsInteractable : Interactable
{
    private ProximityManager proximityManager;

    [SerializeField] private Material _objectMaterial;

    public bool CanBeInteractedWithOnlyOnce;
    [SerializeField] private bool _hasBeenInteractedWith;

    private void Start()
    {
        _objectMaterial = GetComponent<Renderer>().material;

        proximityManager = CustomGameManager.Instance.ProximityManager;
        proximityManager.RegisterObject(this);
    }

    private void OnDestroy()
    {
        proximityManager.UnregisterObject(this);
    }

    public override void Interact()
    {
        if(CanBeInteractedWithOnlyOnce && _hasBeenInteractedWith)
            return;

        _objectMaterial.SetInt("_IsWindowBroken", 1);

        if (CanBeInteractedWithOnlyOnce)
        {
            _hasBeenInteractedWith = true;
            DeactivateShader();
        }
    }

    public override void ActivateShader()
    {
        if (CanBeInteractedWithOnlyOnce && _hasBeenInteractedWith)
            return;

        _objectMaterial.SetInt("_CanBeInteractedWith", 1);
    }

    public override void DeactivateShader()
    {
        _objectMaterial.SetInt("_CanBeInteractedWith", 0);
    }
}