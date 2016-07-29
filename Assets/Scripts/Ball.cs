using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity = new Vector3(0f, 0f, 150f);

    private new Rigidbody rigidbody;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();

        LaunchBall();        
    }
	
	// Update is called once per frame
	void Update ()
    {
	

	}

    public void LaunchBall()
    {
        this.rigidbody.velocity = launchVelocity;
        audioSource.Play();
    }
}
