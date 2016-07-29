using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public float launchSpeed = 100f;

    private Rigidbody myRigidbody;

	// Use this for initialization
	void Start ()
    {
        this.myRigidbody = GetComponent<Rigidbody>();

        this.myRigidbody.velocity = new Vector3(0f, 0f, launchSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
	

	}
}
