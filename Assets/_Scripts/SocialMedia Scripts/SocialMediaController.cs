using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Profile;

public class SocialMediaController : MonoBehaviour {

	public static SocialMediaController instance;

	public bool isLoggedIn;


	void Awake(){
		MakeSingleton();
	}

	void Start () {

		SoomlaProfile.Initialize();
	}

	void OnEnable(){
		ProfileEvents.OnLoginFailed += LoginFailed;
		ProfileEvents.OnLoginCancelled += LoginCanceled;
		ProfileEvents.OnLoginFinished += LoginFinished;
		ProfileEvents.OnLogoutFinished += LogOutFinished;
		ProfileEvents.OnLogoutFailed += LogOutFailed;
		ProfileEvents.OnSocialActionFinished += SocialActionFinished;
		ProfileEvents.OnSocialActionFailed += SocialActionFailed;
		ProfileEvents.OnSocialActionCancelled += SocialActionCanceled;
	}

	void OnDisable(){
		ProfileEvents.OnLoginFailed -= LoginFailed;
		ProfileEvents.OnLoginCancelled -= LoginCanceled;
		ProfileEvents.OnLoginFinished -= LoginFinished;
		ProfileEvents.OnLogoutFinished -= LogOutFinished;
		ProfileEvents.OnLogoutFailed -= LogOutFailed;
		ProfileEvents.OnSocialActionFinished -= SocialActionFinished;
		ProfileEvents.OnSocialActionFailed -= SocialActionFailed;
		ProfileEvents.OnSocialActionCancelled -= SocialActionCanceled;
	}

	void MakeSingleton(){
		if(instance != null){
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	//
	public void SocialActionFinished(Provider provider, SocialActionType action, string payload){
		if(provider == Provider.FACEBOOK){
			if(action == SocialActionType.UPDATE_STATUS){
				GameplayController.instance.PostSucceded();
			}
		}
	}

	public void SocialActionFailed(Provider provider, SocialActionType action, string message, string payload){
		if(provider == Provider.FACEBOOK){
			if(action == SocialActionType.UPDATE_STATUS){
				GameplayController.instance.Notification("Post Failed");
			}
		}
	}

	public void SocialActionCanceled(Provider provider, SocialActionType action, string payload){
		if(provider == Provider.FACEBOOK){
			if(action == SocialActionType.UPDATE_STATUS){
				GameplayController.instance.Notification("Post Canceled");
			}
		}
	}

	void LoginFailed(Provider provider, string message, bool autoLogin, string payload){
		if(Application.loadedLevelName == "Main"){
			GameplayController.instance.Notification("Login Failed");
		}
	}

	void LoginCanceled(Provider provider, bool autoLogin, string payload){
		if(Application.loadedLevelName == "Main"){
			GameplayController.instance.Notification("Login Canceled");
		}
	}

	void LoginFinished(UserProfile userProfileJson, bool autoLogin, string payload){
		if(Application.loadedLevelName == "Main"){
			GameplayController.instance.Notification("Login Finished");
		}
	}

	void LogOutFinished(Provider provider){
		if(Application.loadedLevelName == "Main"){
			GameplayController.instance.Notification("LogOut Finished");
		}
	}

	void LogOutFailed(Provider provider, string message){
		if(Application.loadedLevelName == "Main"){
			GameplayController.instance.Notification("Login Failed");
		}
	}

	public bool IsLogedIn(){
		return SoomlaProfile.IsLoggedIn(Provider.FACEBOOK);
	}

	//Put the links in Soomla edit
	public void RateOurApp(){
		SoomlaProfile.OpenAppRatingPage();
	}

	public void LogIn(){
		SoomlaProfile.Login(Provider.FACEBOOK);
	}

	public void LogOut(){
		SoomlaProfile.Logout(Provider.FACEBOOK);
	}

	public void ShareUsOnFacebook(){
		SoomlaProfile.UpdateStatus(
			Provider.FACEBOOK,
			"Check out my new BEST Score: " + Mathf.Round(GameplayController.instance.highScore).ToString() + " meters" +
			"\n Can you beat my Fatty?" +
			"\n Download and play Lossy Weighty https:// ",
			null,
			null
			);
	}
}
