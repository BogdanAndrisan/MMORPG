using UnityEngine;
using System.Collections;

public class CameraMP : MonoBehaviour {

	void Start (){
		
		if(networkView.isMine){
			GetComponent<Camera>().enabled = true;
			GetComponent<AudioListener>().enabled = true;
		}
		else{
			GetComponent<Camera>().enabled = false;
			GetComponent<AudioListener>().enabled = false;
		}
	}
}
