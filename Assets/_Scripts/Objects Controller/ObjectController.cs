using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectController {

	private static Dictionary<Recycler, ObjectPool> pools = new Dictionary<Recycler, ObjectPool>();

	public static GameObject Instantiate(GameObject obj, Vector3 pos){

		GameObject instance = null;

		var recycleObj = obj.GetComponent<Recycler>();

		if(recycleObj != null){
			var pool = GetPool(recycleObj);
			instance = pool.NextObject(pos).gameObject;
		}
		else{
			instance = GameObject.Instantiate(obj);
			instance.transform.position = pos;
		}

		return instance;
	}

	public static void Destroy(GameObject obj){

		//Getting Recycler
		var recycler = obj.GetComponent<Recycler>();

		//Checking for the Recycler component
		if(recycler != null){
			recycler.TurnOff();
		}
		else{
			GameObject.Destroy(obj);
		}
	}

	private static ObjectPool GetPool(Recycler refer){

		ObjectPool pool = null;

		if(pools.ContainsKey(refer)){
			pool = pools[refer];
		}
		else{

			//pool container
			var container = new GameObject(refer.gameObject.name + "Pool");
			pool = container.AddComponent<ObjectPool>();
			pool.prefab = refer;
			pools.Add(refer, pool);
		}	

		return pool;
	}
}
