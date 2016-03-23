using UnityEngine;
using System.Collections;

public class CameraMooving : MonoBehaviour {


	//Normal camera speed
	public float cameraSpeed = 7f;
	//Max camera speed
	private float maxSpeed = 22f;
	//Camera accelerate every second
	public float accelerationTime = 0.001f;


	void Update() {

        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);

		cameraSpeed += accelerationTime;
		if(cameraSpeed > maxSpeed){
			cameraSpeed = maxSpeed;
		}
    }
   
}
