using UnityEngine;
using System.Collections;

public class mecanimSyncTest : MonoBehaviour {

	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		float syncForward = 0;
		float syncTurn = 0;
		float syncJump = 0;
		float syncJumpLeg = 0;
		bool syncCrouch = false;
		bool syncOnGround = false;
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;
        Quaternion syncRotation = Quaternion.identity;
		if (stream.isWriting)
		{
            syncPosition = transform.position;
            stream.Serialize(ref syncPosition);

            syncVelocity = rigidbody.velocity;
            stream.Serialize(ref syncVelocity);

            syncRotation = transform.rotation;
            stream.Serialize(ref syncRotation);

			syncForward = animator.GetFloat("Forward");
			stream.Serialize(ref syncForward);

			syncTurn = animator.GetFloat("Turn");
			stream.Serialize(ref syncTurn);

			syncJump = animator.GetFloat("Jump");
			stream.Serialize(ref syncJump);

			syncJumpLeg = animator.GetFloat("JumpLeg");
			stream.Serialize(ref syncJumpLeg);

			syncCrouch = animator.GetBool("Crouch");
			stream.Serialize(ref syncCrouch);

			syncOnGround = animator.GetBool("OnGround");
			stream.Serialize(ref syncOnGround);
		}
		else
		{
            stream.Serialize(ref syncPosition);
            transform.position = syncPosition;

            stream.Serialize(ref syncVelocity);
            rigidbody.velocity = syncVelocity;

            stream.Serialize(ref syncRotation);
            transform.rotation = syncRotation;
            
			stream.Serialize(ref syncForward);
			animator.SetFloat("Forward",syncForward);

			stream.Serialize(ref syncTurn);
			animator.SetFloat("Turn",syncTurn);

			stream.Serialize(ref syncJump);
			animator.SetFloat("Jump",syncJump);

			stream.Serialize(ref syncJumpLeg);
			animator.SetFloat("JumpLeg",syncJumpLeg);

			stream.Serialize(ref syncCrouch);
			animator.SetBool("Crouch",syncCrouch);

			stream.Serialize(ref syncOnGround);
			animator.SetBool("OnGround",syncOnGround);
		}
	}
}
