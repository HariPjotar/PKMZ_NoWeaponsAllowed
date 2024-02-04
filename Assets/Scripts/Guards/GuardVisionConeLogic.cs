using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVisionConeLogic : MonoBehaviour
{
    [SerializeField] private GuardWanderingManager _manager;

    private void Start()
    {
        _manager.OnKnockOut += GuardVisionConeLogic_OnKnockOut;
    }

    private void OnDestroy()
    {
        _manager.OnKnockOut -= GuardVisionConeLogic_OnKnockOut;
    }

    private void GuardVisionConeLogic_OnKnockOut()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_manager.CurrentGuardState == GuardStates.KNOCKED_OUT || !_manager.enabled)
            return;

        if (other.gameObject.tag.Equals("Player"))
        {
            _manager.SetNewDestination(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_manager.CurrentGuardState == GuardStates.KNOCKED_OUT || !_manager.enabled)
            return;

        if (other.gameObject.tag.Equals("Player"))
        {
            _manager.SetNewDestination(other.gameObject, true);
        }
    }
}
