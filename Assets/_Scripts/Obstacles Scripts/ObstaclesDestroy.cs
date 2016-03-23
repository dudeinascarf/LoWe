using UnityEngine;
using System.Collections;

public class ObstaclesDestroy : MonoBehaviour {


	private PlayerDies playerDies;


	void Awake(){
		if(gameObject.name == "Player"){
			playerDies = GetComponent<PlayerDies>();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Destroyer"){
			ObjectController.Destroy(gameObject);
		}
	}
}
