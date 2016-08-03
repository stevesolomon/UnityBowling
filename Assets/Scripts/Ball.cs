using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public bool InPlay
    {
        get;
        private set;
    }

    public Vector3 launchVelocity = new Vector3(0f, 0f, 150f);

    private new Rigidbody rigidbody;

    private AudioSource audioSource;

    private Vector3 OriginalPosition { get; set; }

	// Use this for initialization
	void Start ()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();

        this.rigidbody.useGravity = false;

        this.OriginalPosition = this.transform.position; 
    }
	
	// Update is called once per frame
	void Update ()
    {
	

	}

    public void Launch(Vector3 launchVelocity)
    {
        if (!this.InPlay)
        {
            this.InPlay = true;

            this.rigidbody.useGravity = true;
            this.rigidbody.velocity = launchVelocity;
            audioSource.Play();
        }
    }

    public void Reset()
    {
        this.InPlay = false;

        this.transform.position = this.OriginalPosition;
        this.rigidbody.velocity = Vector3.zero;
        this.rigidbody.rotation = Quaternion.identity;
        this.rigidbody.useGravity = false;
    }

    public void MoveStartPosition(float amount)
    {
        if (!InPlay)
        {
            this.transform.Translate(new Vector3(amount, 0f, 0f));
        }
    }
}
