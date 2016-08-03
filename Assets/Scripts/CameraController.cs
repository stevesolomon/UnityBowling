using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Ball ball;

    public Vector3 ballOffset;

    private new Camera camera;

	// Use this for initialization
	void Start ()
    {
        this.camera = GetComponent<Camera>();        
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCameraPosition();
	}

    private void UpdateCameraPosition()
    {
        //As long as we're not at the end of the lane, keep following the ball.
        if (this.ball.transform.position.z <= (1700 - this.ballOffset.z))
        {
            this.camera.transform.position = this.ball.transform.position + this.ballOffset;
        }
    }
}
