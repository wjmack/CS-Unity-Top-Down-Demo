using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestInteraction : MonoBehaviour 
{
	float maxDistance = 1.8f;
	// Update is called once per frame
	void Update () {
		FindClosest();
	}

	void FindClosest()
	{
		bool canInteract;
		float distanceToClosestInteractable = Mathf.Infinity;
		GameObject closestInteractable = null;
		GameObject[] allInteractables = GameObject.FindGameObjectsWithTag("Interactable"); 

		foreach (GameObject currentInteractable in allInteractables) {
			float distanceToInteractable = (currentInteractable.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToInteractable < distanceToClosestInteractable) {
				distanceToClosestInteractable = distanceToInteractable;
				closestInteractable = currentInteractable;
				canInteract = distanceToClosestInteractable <= maxDistance;
        			closestInteractable.transform.GetChild(0).gameObject.SetActive(canInteract);
				if(Input.GetKeyDown("e") && canInteract) {
					Debug.Log("You interacted with "+currentInteractable.name);
					closestInteractable.GetComponent<AudioSource>().Play(0);

					Sign isSign = closestInteractable.GetComponent<Sign>();
				}
			}
		}
	}
}
