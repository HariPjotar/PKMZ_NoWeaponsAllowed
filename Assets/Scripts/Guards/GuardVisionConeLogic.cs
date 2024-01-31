using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVisionConeLogic : MonoBehaviour
{
    [SerializeField] private GuardWanderingManager _manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _manager.SetNewDestination(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _manager.SetNewDestination(other.gameObject, true);
        }
    }
}
