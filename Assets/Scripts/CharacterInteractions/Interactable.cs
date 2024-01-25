using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    public abstract void ActivateShader();

    public abstract void DeactivateShader();
}
