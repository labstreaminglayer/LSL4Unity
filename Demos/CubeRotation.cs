using UnityEngine;

public class CubeRotation : MonoBehaviour {

	private float yawSpeed = 1f;
	private float pitchSpeed = 1f;
	private float rollSpeed = 1f;

	void Update () {

		if (Input.GetKey("a"))
			yawSpeed += 1;
		if (Input.GetKey("d") && yawSpeed > 0)
				yawSpeed -= 1;

		if (Input.GetKey("w"))
			pitchSpeed += 1;
		if (Input.GetKey("s") && pitchSpeed > 0)
				pitchSpeed -= 1;

		if (Input.GetKey("e"))
			rollSpeed += 1;
		if (Input.GetKey("q") && rollSpeed > 0)
				rollSpeed -= 1;
		
		transform.rotation *= Quaternion.Euler(yawSpeed * Time.deltaTime, pitchSpeed * Time.deltaTime, rollSpeed * Time.deltaTime);
	}
}
