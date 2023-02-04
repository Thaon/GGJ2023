using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    #region member variables

    public KeyCode _interactKey;
    public float _interactDistanceCheck;
    public Vector3 offset = new Vector3(0,0,0);

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            Interactable closest = GetCloserInteractable();
            closest?.Interact();
        }
    }

    public Interactable GetCloserInteractable()
    {
        List<Interactable> interactables = FindObjectsOfType<Interactable>().ToList();
        Interactable final = null;
        float minDistance = _interactDistanceCheck;
        interactables.ForEach(i =>
        {
            float distance = Vector3.Distance(transform.position + offset, i.transform.position);
            if (distance < minDistance)
            {
                final = i;
                minDistance = distance;
            }
        });
        return final;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + offset, _interactDistanceCheck);
    }
}
