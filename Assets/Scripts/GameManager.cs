using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<int> rolls;

    private PinSetter pinSetter;

    private ActionController actionController;

    private Ball ball;

	// Use this for initialization
	void Start ()
    {
        this.rolls = new List<int>();
        this.actionController = new ActionController();

        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
        this.ball = GameObject.FindObjectOfType<Ball>();
	}

    public void Bowl(int pinsFallen)
    {
        rolls.Add(pinsFallen);

        var response = this.actionController.Bowl(pinsFallen);

        pinSetter.HandleResponse(response);

        this.ball.Reset();
    }
}
