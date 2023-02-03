using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType { locked, unlocked }

public class Interactable : MonoBehaviour
{
    #region member variables

    public InteractionType _interactionType = InteractionType.unlocked;
    public UnityEvent _onInteractionSuccess, _onInteractionFail;

    #endregion

    public void Interact()
    {
        if (_interactionType == InteractionType.locked)
            _onInteractionFail?.Invoke();
        else
        {
            _onInteractionSuccess?.Invoke();
            //print("Interacting with " + gameObject.name);
        }
    }
}
