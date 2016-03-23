using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SocialGameCenter : MonoBehaviour {
	#if UNITY_IOS
	private GameplayController gameplayController;
	private string leaderboardID_1 = "Best_Score_Leaderboard";


	void Start () {
        // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API
        Social.localUser.Authenticate (ProcessAuthentication);

        gameplayController = FindObjectOfType<GameplayController>();
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication (bool success) {
        if (success) {
            Debug.Log ("Authenticated, checking achievements");

            // Request loaded achievements, and register a callback for processing them
            Social.LoadAchievements (ProcessLoadedAchievements);
        }
        else
            Debug.Log ("Failed to authenticate");
    }

    // This function gets called when the LoadAchievement call completes
    void ProcessLoadedAchievements (IAchievement[] achievements) {
        if (achievements.Length == 0)
            Debug.Log ("Error: no achievements found");
        else
            Debug.Log ("Got " + achievements.Length + " achievements");
     
        // You can also call into the functions like this
        Social.ReportProgress ("Achievement01", 100.0, result => {
            if (result)
                Debug.Log ("Successfully reported achievement progress");
            else
                Debug.Log ("Failed to report achievement");
        });
    }

    public void ReprotScoreButton(){
    	long bestScore = (long)gameplayController.highScore;	
		Social.ShowLeaderboardUI();
		Social.ReportScore(bestScore, leaderboardID_1, HighScoreCheck);

    }

   	static void HighScoreCheck(bool result){
    	if(result){
    		Debug.Log("Score submission successful");
    	}
    	else{
    		Debug.Log("Score submission failed");
    	}
    }
    #endif
}
