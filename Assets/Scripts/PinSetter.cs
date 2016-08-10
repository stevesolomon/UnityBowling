using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    private const string pinTextFormat = "{0} Pins Remaining";

    public Text pinText;

    public float settleTimeSeconds = 3f;

    public float distanceToRaise = 40f;

    public Ball ball;

    public GameObject pinSet;

    public int LastStandingCount { get; private set; }

    private int LastSettledCount { get; set; }

    private List<Pin> Pins { get; set; } 

    private bool CanUpdatePins { get; set; }

    private float LastChangeTime { get; set; }

    private ActionController ActionController { get; set; }

    private Animator Animator { get; set; }

	// Use this for initialization
	void Start ()
    {
        Pins = GameObject.FindObjectsOfType<Pin>().ToList();
        ActionController = new ActionController();
        Animator = this.GetComponent<Animator>();

        InitializePinCounts();

        if (ball == null)
        {
            this.ball = GameObject.FindObjectOfType<Ball>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (CanUpdatePins)
        {
            UpdatePinsStanding();
        }
	}

    /// <summary>
    /// Raises only the standing pins by distanceToRaise.
    /// </summary>
    public void RaisePins()
    {
        print("Raising Pins");

        foreach (var pin in Pins)
        {
            if (pin.IsStanding())
            {
                pin.Raise(distanceToRaise);
            }
        }
    }

    /// <summary>
    /// Lowers currently raised pins.
    /// </summary>
    public void LowerPins()
    {
        print("Lowering Pins");

        foreach (var pin in Pins)
        {
            if (pin.IsStanding())
            {
                pin.Lower(distanceToRaise);
            }
        }
    }

    /// <summary>
    /// Creates a new set of pins.
    /// </summary>
    public void RenewPins()
    {
        print("Renewing Pins");

        Instantiate(this.pinSet, new Vector3(0f, 40f, 1829f), Quaternion.identity);

        InitializePinCounts();
    }

    private void InitializePinCounts()
    {
        Pins = GameObject.FindObjectsOfType<Pin>().ToList();
        LastStandingCount = -1;
        LastSettledCount = 10;
        CanUpdatePins = false;

        int standingPins = CountStandingPins();
        pinText.color = Color.green;
        pinText.text = string.Format(pinTextFormat, standingPins);
    }

    private void UpdatePinsStanding()
    {
        int currentStanding = CountStandingPins();

        // There has been a change...
        if (currentStanding != LastStandingCount)
        {
            LastChangeTime = Time.time;
            LastStandingCount = currentStanding;

            pinText.text = string.Format(pinTextFormat, currentStanding);

            return;
        }

        // There has not been a change, let's see if we've waited long enough for a change...
        if ((Time.time - LastChangeTime) > settleTimeSeconds)
        {
            PinsHaveSettled();
        }        
    }

    private int CountStandingPins()
    {
        int pinsStanding = 0;

        foreach (var pin in this.Pins)
        {
            if (pin.IsStanding())
            {
                pinsStanding++;
            }
        }

        return pinsStanding;
    }

    private void PinsHaveSettled()
    {
        int standingPins = CountStandingPins();
        int pinsFallen = this.LastSettledCount - standingPins;
        this.LastSettledCount = standingPins;

        print(pinsFallen);

        LastStandingCount = -1;
        pinText.color = Color.green;
        this.CanUpdatePins = false;

        var response = this.ActionController.Bowl(pinsFallen);
        HandleResponse(response);

        this.ball.Reset();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            CanUpdatePins = true;
            pinText.color = Color.red;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Pin pin = collider.gameObject.GetComponentInParent<Pin>();

        if (pin != null)
        {
            this.Pins.Remove(pin);
            Destroy(collider.transform.parent.gameObject);            
        }
    }

    private void HandleResponse(ActionResponse response)
    {
        switch (response)
        {
            case ActionResponse.Tidy:
                this.Animator.SetTrigger("tidyPinsTrigger");
                break;
            case ActionResponse.Reset:
            case ActionResponse.EndTurn:
                this.LastSettledCount = 10;
                this.Animator.SetTrigger("resetPinsTrigger");
                break;
        }
    }
}
