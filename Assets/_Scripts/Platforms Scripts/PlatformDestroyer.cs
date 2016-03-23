using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Destroyer"){
			ObjectController.Destroy(gameObject);
		}
	}
}
