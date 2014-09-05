using UnityEngine;
using System.Collections;

public class SwordAttTest : MonoBehaviour {

	NetworkView netView;
	Character character;
	void Start () {
		netView = GetComponentInParent<NetworkView>();
		character = GetComponentInParent<Character>();
	}
	

	void Update () {

	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Player" && !netView.isMine){
			Debug.Log ("The col entered " + collider.gameObject.name );
			collider.gameObject.GetComponent<Character>().attributes.HitPoints--;
		
		}
	}
	void OnTriggerExit(Collider collider){
		if(collider.tag == "Player" && !netView.isMine){
			Debug.Log ("The col exited " + collider.gameObject.name );
			//collider.gameObject.GetComponent<Character>().HitPoints--;
		}
	}
}
