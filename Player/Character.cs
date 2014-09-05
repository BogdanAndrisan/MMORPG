using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Animator animator;
	public Transform camera;
    public Attributes attributes;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
        attributes = GetComponent<Attributes>();
	}



	// Update is called once per frame
	void Update () {
		if(networkView.isMine){
			transform.Translate(Vector3.forward * 0.001f);
			transform.Translate(-Vector3.forward * 0.001f);
            animator.SetFloat("Forward", Input.GetAxis("Vertical"));
            //animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
            if (animator.GetFloat("Forward") >= 0.3f && animator.GetFloat("Turn") == 0)
            {
                rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation, camera.rigidbody.transform.rotation, 3f * Time.deltaTime);
            }
            if (animator.GetFloat("Turn") < -0.8f || animator.GetFloat("Turn") > 0.8f)
            {
                rigidbody.transform.Rotate(Vector3.up, animator.GetFloat("Turn") * 10 * Time.deltaTime);
            }
		}
	}

   
    
	/*void Attack(){
		if(Input.GetKey(KeyCode.Alpha1)){
            attack = true;
		}else{
			//if(!animation.IsPlaying("Attack")){
				attack = false;
			//}
		}
	}
	void Movement(){
        //FIX THE VELOCITY INPUT IT'S AFFECTING THE GRAVITY, TYVM U FAGHET;
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
		if(attack == false && walk == false && run == false){
			idle = true;
		}else{
			idle = false;
		}
		if(walk == true){
			//animation.CrossFade("Walk",0.2f);
			rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation,camera.rigidbody.transform.rotation,0.02f);
			//rigidbody.transform.Translate(Vector3.forward * 1 * Time.deltaTime);
			Vector3 maxSpeed = transform.forward * MaxSpeed;
			if(rigidbody.velocity.magnitude >= maxSpeed.magnitude){
				rigidbody.velocity = maxSpeed;
			}
			else{
				rigidbody.AddForce(transform.forward * AccelerationSpeed);
			}
			//Debug.Log("Velocity is: " + rigidbody.velocity.magnitude);
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
			//Debug.Log("Velocity is: " + rigidbody.velocity.magnitude);
		}
		else{
			//animation.CrossFade("idle",0.2f);
			if(rigidbody.velocity.magnitude > 0){
				rigidbody.velocity = Vector3.Lerp (rigidbody.velocity, Vector3.zero, 0.03f);
				//Debug.Log("Velocity is reducing: " + rigidbody.velocity.magnitude);
			}
		}
	}

	void SwitchBoolStates(bool walk,bool idle,bool run ,bool attack){
		this.walk = walk;
		this.idle = idle;
		this.run = run;
		this.attack = attack;
	}*/
}
