using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThresholdDegrees = 5f;

    public new Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
    {
        this.rigidbody = GetComponent<Rigidbody>();
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

    public void Raise(float distance)
    {
        this.transform.Translate(new Vector3(0f, distance, 0f), Space.World);
        this.transform.rotation = Quaternion.identity;
        this.rigidbody.isKinematic = true;
    }

    public void Lower(float distance)
    {
        this.transform.Translate(new Vector3(0f, -distance, 0f), Space.World);
        this.rigidbody.isKinematic = false;
    }
}
