using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType { locked, unlocked }

public class Interactable : MonoBehaviour
{
    #region member variables

    public InteractionType _interactionType = InteractionType.unlocked;
    public int _unlockRequirement;
    public UnityEvent _onInteractionSuccess, _onInteractionFail;


    private int _currentUnlockCounter;
    #endregion

    public void Interact()
    {
        print("Interacting!");
        if (_interactionType == InteractionType.locked)
            _onInteractionFail?.Invoke();
        else
        {
            _onInteractionSuccess?.Invoke();
            //print("Interacting with " + gameObject.name);
        }
    }

    public void UnlockInteraction()
    {
        _currentUnlockCounter++;
        if (_currentUnlockCounter >= _unlockRequirement)
            _interactionType = InteractionType.unlocked;
    }

    public void LockInteraction()
    {
        _currentUnlockCounter = 0;
        _interactionType = InteractionType.locked;
    }
}
