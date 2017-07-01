using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed = 5.0F;
    [SerializeField] private float rotateSpeed = 5.0F;

    [SerializeField] private float deadzoneX = 0.2F;
    [SerializeField] private float deadzoneY = 0.2F;

	public bool isActive = true;

    CharacterController controller;

	float timer = 0;
    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
		if(isActive)
			Move();
    }

	void Move()
	{
        // Grab this frame's input
		Vector2 touchInput;
		touchInput = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

		bool touchpadPressed = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad));
		
		if(touchpadPressed)
		{
			GameObject camera = Camera.main.gameObject;
		    // Define local vectors
			Vector3 forward = camera.transform.TransformDirection(Vector3.forward);
			Vector3 backward = camera.transform.TransformDirection(Vector3.back);
			Vector3 left = camera.transform.TransformDirection(Vector3.left);
			Vector3 right = camera.transform.TransformDirection(Vector3.right);

			// Initialize movement for this frame; default to nothing
			Vector3 movement = Vector3.zero;

			// Handle X translation
			if (touchInput.x < -deadzoneX)
			{
				movement += left;
			}
			else if (touchInput.x > deadzoneX)
			{
				movement += right;
			}

			// Handle Y translation
			if (touchInput.y > deadzoneY)
			{
				movement += forward;
			}
			else if (touchInput.y < -deadzoneY)
			{
				movement += backward;
			}

			// Move!
			controller.SimpleMove(movement * speed);
		}

		//Swipe Left
		if(OVRInput.Get(OVRInput.Button.Left) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Debug.Log("Swiped Left");
			StartCoroutine(QuickRotate(new Vector3(0,-45,0), .25f));

		}
		//Swipe Right
		if(OVRInput.Get(OVRInput.Button.Right) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			Debug.Log("Swiped Right");
			StartCoroutine(QuickRotate(new Vector3(0,45,0), .25f));
		}
	}

	IEnumerator QuickRotate(Vector3 byAngles, float inTime)
	{
		var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

        for(var t = 0f; t < 1; t += Time.deltaTime/inTime) 
		{
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
		}
    }
		// Debug.Log("Rotating");
		// float rotateTime = .25f;
		// timer += Time.deltaTime;
		// if(timer < rotateTime)
		// {
		// 	Quaternion.Slerp(transform.rotation, dest, timer * 1/rotateTime);
		// 	yield return new WaitForEndOfFrame();
		// }
		// else
		// {
		// 	timer = 0;
		// 	yield return null;
		// }
	
}