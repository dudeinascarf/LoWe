using UnityEngine;
using System.Collections;

public class DisableEnableObjects : MonoBehaviour {


	void OnEnable(){
		GameplayController.turnOff += TurnOff;
	}

	void OnDisable(){
		GameplayController.turnOff -= TurnOff;
	}

	void TurnOff(){
		if(gameObject.activeInHierarchy){
			gameObject.SetActive(false);
		}
	}

}
