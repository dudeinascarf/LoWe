using UnityEngine;
using System.Collections;

public class Recycler : MonoBehaviour {

	//Turning On and Off gameobjects 

	public void TurnOn(){
		gameObject.SetActive(true);
	}

	public void TurnOff(){
		gameObject.SetActive(false);
	}
}
