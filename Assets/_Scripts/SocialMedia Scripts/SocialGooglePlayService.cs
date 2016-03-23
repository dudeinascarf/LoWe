using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SocialGooglePlayService : MonoBehaviour {

	public static SocialGooglePlayService instance;
	private const string ID = "CgkI5q-Yn9QeEAIQAQ";
	private GameplayController gameplayController;


	void Awake() {
		MakeSingleton();
	}

	void Start () {

		PlayGamesPlatform.Activate();
		gameplayController = FindObjectOfType<GameplayController>();
	}

	void OnLevelWasLoaded(){

	}

	void MakeSingleton(){
		if(instance != null){
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}
	}

	public void OpenLeaderboardScore(){

		long bestScore = (long)gameplayController.highScore;

		if(Social.localUser.authenticated){
			PlayGamesPlatform.Instance.ShowLeaderboardUI(ID);
			Social.ReportScore(bestScore, ID, (bool success) => {

			});	
		} else {
			Social.localUser.Authenticate((bool success) => {

			});
		}
	}
}
