using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour
{
    public Ball ball;

    private float dragStartTime;
    private Vector3 dragStartPosition;

	// Use this for initialization
	void Start ()
    {
	    if (ball == null)
        {
            ball = GetComponent<Ball>();
        }
	}

    public void MoveStart(float amount)
    {
        this.ball.MoveStartPosition(amount);
    }
	
	public void DragStart()
    {
        dragStartTime = Time.time;
        dragStartPosition = Input.mousePosition;
    }

    public void DragEnd()
    {
        if (this.ball.InPlay)
        {
            return;
        }

        float dragEndTime = Time.time;
        Vector3 dragEndPosition = Input.mousePosition;

        float dragDuration = (dragEndTime - dragStartTime) * 4;

        float launchSpeedX = (dragEndPosition.x - dragStartPosition.x) / dragDuration;
        float launchSpeedZ = (dragEndPosition.y - dragStartPosition.y) / dragDuration; // Dragging "up/down" in UI translates to Z velocity in game.

        Vector3 launchVelocity = new Vector3(launchSpeedX, 0f, launchSpeedZ);

        print("Launch velocity is: " + launchVelocity);

        this.ball.LaunchBall(launchVelocity);
    }
}
