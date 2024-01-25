using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInteractable : Interactable
{
    private ProximityManager proximityManager;

    [SerializeField] private Material _objectMaterial;

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
        // Implement interaction logic
        Debug.Log("SASAAAAAAAAAAAAAAAAAAAAAA");
    }

    public override void ActivateShader()
    {
        _objectMaterial.SetInt("_CanBeInteractedWith", 1);
    }

    public override void DeactivateShader()
    {
        _objectMaterial.SetInt("_CanBeInteractedWith", 0);
    }
}