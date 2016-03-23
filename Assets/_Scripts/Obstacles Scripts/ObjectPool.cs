using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public Recycler prefab;

	//Keeping track of all instances
	private List<Recycler> pool = new List<Recycler>();


	private Recycler CreateInstance(Vector3 pos){

		var clone = GameObject.Instantiate(prefab);
		clone.transform.position = pos;
		clone.transform.parent = transform;
		pool.Add(clone);
		return clone;
	}

	public Recycler NextObject(Vector3 pos){

		Recycler instance = null;

		foreach(var obj in pool){

			if(!obj.gameObject.activeSelf){
				instance = obj;
				instance.transform.position = pos;
			}
		}
		 
		if(instance == null){
			instance = CreateInstance(pos);
		}

		instance.TurnOn();
		return instance;
	}

}
