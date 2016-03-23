using UnityEngine;
using System.Collections;

public class BackgroundLooper : MonoBehaviour {


	public Vector2 speed = Vector2.zero;

	private Vector2 offset = Vector2.zero;

	[SerializeField]
	private Material material;


	void Start () {
		offset = material.GetTextureOffset("_MainTex");
	}

	void Update () {
		offset += speed * Time.deltaTime;
		material.SetTextureOffset("_MainTex", offset);
	}
}
