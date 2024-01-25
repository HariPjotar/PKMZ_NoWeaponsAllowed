using System.Collections.Generic;
using UnityEngine;

public class ProximityManager : MonoBehaviour
{
    [SerializeField] public float InteractionRadius = 3f;
    [SerializeField] private List<Interactable> _nearbyObjects;

    [SerializeField] private Interactable _currentInteractable;

    private void Awake()
    {
        InputManager.OnInteractPerformed += InputManager_OnInteractPerformed;    
    }

    private void OnDestroy()
    {
        InputManager.OnInteractPerformed -= InputManager_OnInteractPerformed;
    }

    private void InputManager_OnInteractPerformed()
    {
        if (_currentInteractable == null)
            return;

        _currentInteractable.Interact();
    }

    public void RegisterObject(Interactable obj)
    {
        if (_nearbyObjects.Contains(obj))
        {
            return;
        }

        _nearbyObjects.Add(obj);
    }

    public void UnregisterObject(Interactable obj)
    {
        _nearbyObjects.Remove(obj);
    }

    private void Update()
    {
        CheckForInteractions();
    }

    private void CheckForInteractions()
    {
        foreach (var obj in _nearbyObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance <= InteractionRadius)
            {
                SetCurrentInteractible(obj);
                return;
            }

            RemoveCurrentInteractible();
        }
    }

    private void SetCurrentInteractible(Interactable obj)
    {
        _currentInteractable = obj;

        obj.GetComponent<GeneralInteractable>().ActivateShader();
    }

    private void RemoveCurrentInteractible()
    {
        if (_currentInteractable == null)
            return;

        _currentInteractable.DeactivateShader();

        _currentInteractable = null;
    }
}