using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThresholdDegrees = 5f;

	// Use this for initialization
	void Start ()
    {
	
	}

    public bool IsStanding()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        float xTilt = Mathf.Abs(rotation.x);
        float zTilt = Mathf.Abs(rotation.z);

        if ((xTilt < standingThresholdDegrees || xTilt > 360 - standingThresholdDegrees) && 
            (zTilt < standingThresholdDegrees || zTilt > 360 - standingThresholdDegrees))
        {
            return true;
        }

        return false;
    }
}
