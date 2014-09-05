using UnityEngine;
using System.Collections;

public class AnimSync : MonoBehaviour {

	public Animator animator;
	public float Forward;
	private float delay = 2f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Forward = animator.GetFloat("Forward");
		if(delay <=2){
			delay-=Time.deltaTime;
			Debug.Log("Animator:" + Forward);
		}
		if(delay <=0){
			delay=2;
		}
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
			
			stream.Serialize(ref Forward);
			Debug.Log ("Forward value:" + Forward);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			stream.Serialize(ref Forward);

		}
	}
}
