using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class TiltController : NetworkBehaviour {
/* ========================================================================================================
 * 70:30 TILT STEERING SCRIPT - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * used to steer an object by tilting the mobile device!
 * ========================================================================================================
 */

	//bool to set the steer on or off (used for games that offer tilt OR touch steering)
	public bool tiltSteerActive = true;
	//used for lerping the player. default 10
	public float smooth = 10;
	private float lerpingFactor;
	//the modifyer for the tilting speed. default 1
	public float tiltSpeedX = 1;
	//the modifyer for the tilting speed. default 1
	public float tiltSpeedY = 1;
	//private vector3 to store the x,y,z values temporarely
	private Vector3 localPosition;
	//as the tilt steer uses a double lerp for more softness, here is the storage variable
	Vector3 betweenLerp;
	//the speed which will be assigned from the gyroscope of the device
	private float speedX;
	private float speedY;
	//an offset you can change to move the tilted object to a default location
	public float offsetX;
	public float offsetY;


	void Start () 
	{
		//enable the device gyroscope by default
		Input.gyro.enabled = true;
	}

	void Update () 
	{
		if (!isLocalPlayer)
		{
			return;
		}
		//use the smooth value for internal lerping
		lerpingFactor = 1 / smooth;

		//this is the actual tilt steer section
		if (tiltSteerActive) 
		{
			//store the current position
			Vector3 pos = transform.position;
			//assign tilt speeds from the device gyroscope multiplied by a modifyer you can change from unity to set the tilt speed
			speedY = Input.gyro.rotationRateUnbiased.x * tiltSpeedX;
			speedX = Input.gyro.rotationRateUnbiased.y * tiltSpeedY;
			//set the local position to the stored gyroscope value modified with an exponential function (for acceleration) and the offset
			localPosition.y = -speedY * Mathf.Exp (2) + offsetY;
			localPosition.x = speedX * Mathf.Exp (2) + offsetX;
            localPosition.z = transform.position.z;
			//lerp for the first time and store the position	
			betweenLerp = Vector3.Lerp (pos, localPosition, 0.1f);
			//lerp for the secont time and use the defined smooth factor this time
			transform.position = Vector3.Lerp (pos, betweenLerp, lerpingFactor);
		}

		//managing the gyroscope of the device
		//if tilt steer is active and gyroscope is not, activate it
		if (tiltSteerActive && !Input.gyro.enabled) 
		{
			Input.gyro.enabled = true;
		}
		//if tilt steer is inactive and gyroscope is not, deactivate it
		if (!tiltSteerActive && Input.gyro.enabled) 
		{
			Input.gyro.enabled = false;
		}
	}
}
