using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public Animation animator;
	public float forward = 0f;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	public float AccelerationSpeed = 10;
	public float MaxSpeed = 1;
	private bool walk;
	private bool idle = false;
	private bool run = false;
	public Transform camera;

	void Start(){

		animator = GetComponent<Animation>();
	}

	void Update()
	{
		if (networkView.isMine)
		{
			InputMovement();
			InputColorChange();
		}
		else
		{
			SyncedMovement();
		}
	}

	void InputMovement()
	{
		/*if (Input.GetKey(KeyCode.W))
			rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey(KeyCode.S))
			rigidbody.MovePosition(rigidbody.position - Vector3.forward * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.D))
			rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.A))
			rigidbody.MovePosition(rigidbody.position - Vector3.right * speed * Time.deltaTime);*/
		if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)){
			walk = true;
		}
		else{
			walk = false;
		}
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)){
			run = true;
		}
		else{
			run = false;
		}
		if(walk == true){
			//animation.CrossFade("Walk",0.2f);
			rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation,camera.rigidbody.transform.rotation,0.02f);
			//rigidbody.transform.Translate(Vector3.forward * 1 * Time.deltaTime);
			Vector3 maxSpeed = transform.forward * MaxSpeed;
			if(rigidbody.velocity.magnitude > maxSpeed.magnitude){
				rigidbody.velocity = maxSpeed;
			}
			else{
				rigidbody.AddForce(transform.forward * AccelerationSpeed);
			}
			Debug.Log("Velocity is: " + rigidbody.velocity.magnitude);
		}
		else if(run == true){
			//animation.CrossFade("Run",0.2f);
			rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation,camera.rigidbody.transform.rotation,0.02f);
			//rigidbody.transform.Translate(Vector3.forward * 1 * Time.deltaTime);
			Vector3 maxSpeed = transform.forward * MaxSpeed * 5;
			if(rigidbody.velocity.magnitude > maxSpeed.magnitude){
				rigidbody.velocity = maxSpeed;
			}
			else{
				rigidbody.AddForce(transform.forward * AccelerationSpeed);
			}
			Debug.Log("Velocity is: " + rigidbody.velocity.magnitude);
		}
		else{
			//animation.CrossFade("idle",0.2f);
			if(rigidbody.velocity.magnitude > 0){
				rigidbody.velocity = Vector3.Lerp (rigidbody.velocity, Vector3.zero, 0.03f);
				Debug.Log("Velocity is reducing: " + rigidbody.velocity.magnitude);
			}
		}
	}
	private void InputColorChange()
	{
		if (Input.GetKeyDown(KeyCode.R))
			ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{
		renderer.material.color = new Color(color.x, color.y, color.z, 1f);
		
		if (networkView.isMine)
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered,color);
	}

	private void SyncedMovement(){
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime/syncDelay);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		Quaternion syncRotation = Quaternion.identity;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);

			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);

			syncRotation = rigidbody.rotation;
			stream.Serialize(ref syncRotation);


		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			stream.Serialize(ref syncRotation);

			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
}
