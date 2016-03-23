using UnityEngine;
using System.Collections;

public class CollectablesGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject collectable;


	public void SpawnCollectables(Vector3 startPosition){
		ObjectController.Instantiate(collectable, transform.position = startPosition);
	}
}
