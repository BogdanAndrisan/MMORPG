using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private Character character;
	// Use this for initialization
	void Start () {
		character = GetComponentInParent<Character>();
	}

	void OnGUI(){
		Vector2 hpBarPos=Camera.main.WorldToScreenPoint(this.transform.position);
        GUI.Box(new Rect(hpBarPos.x - 50, Screen.height - hpBarPos.y, 100, 20), character.attributes.HitPoints.ToString() + "/" + character.attributes.MaxHitPoints.ToString());
	}
	// Update is called once per frame
	void Update () {
	
	}
}
