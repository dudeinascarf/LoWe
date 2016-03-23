using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircularButton : MonoBehaviour {


	public enum State {Active, Ready, Charging};

	public State status;

	[SerializeField]
	private Image circle;

	//Time button active
	private float activeTime = 5f;
	//Time button charging
	private float chargingTime = 3f;
	public float counter;

	[SerializeField]
	private GameplayController gameplayController;

	[SerializeField]
	private CollectablesManager collectablesManager;

	public bool noObstaclesMode;


	void Start () {

		status = State.Ready;
		circle.color = Color.blue;

		}

	void Update () {

		if(status == State.Active){
			counter += Time.deltaTime;
			circle.fillAmount = 1 - counter / activeTime;

			if(counter >= activeTime){
				status = State.Charging;
				counter = 0;
			}
		}		

		if(status == State.Ready){
			counter = 0;
			circle.fillAmount = 1;
			circle.color = Color.blue;
		}	

		if(status == State.Charging){
			circle.color = Color.gray;
			counter += Time.deltaTime;
			circle.fillAmount = counter / chargingTime;

			if(counter >= chargingTime){
				status = State.Ready;
			}
		}	
	}

	//Button Pressed
	public void Pressed(){

		if(status == State.Ready){
			status = State.Active;

			//Destroying obstacles
			collectablesManager.ActivateCollectablePower(noObstaclesMode, activeTime);
			//Taking collectables
			gameplayController.collectableScore -= 30;
			//Saving collectables
			PlayerPrefs.SetInt("Collectable", gameplayController.collectableScore);
		}
	}
}
