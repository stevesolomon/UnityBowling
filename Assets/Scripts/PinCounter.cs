using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PinCounter : MonoBehaviour
{
    private const string pinTextFormat = "{0} Pins Remaining";

    public Text pinText;

    private GameManager gameManager;

    public float settleTimeSeconds = 3f;

    public int LastSettledCount { get; set; }

    public int LastStandingCount { get; private set; }    

    private float LastChangeTime { get; set; }

    private bool BallOutOfPlay { get; set; }

    private List<Pin> Pins { get; set; }

    // Use this for initialization
    void Start ()
    {
        Pins = GameObject.FindObjectsOfType<Pin>().ToList();

        this.gameManager = GameObject.FindObjectOfType<GameManager>();

        LastStandingCount = -1;
        LastSettledCount = 10;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.BallOutOfPlay)
        {
            UpdatePinsStanding();
            int standingPins = CountStandingPins();
            pinText.text = string.Format(pinTextFormat, standingPins);
            pinText.color = Color.red;
        }
        else
        {            
            pinText.color = Color.green;
        }
    }

    private int CountStandingPins()
    {
        int pinsStanding = 0;

        List<Pin> pinsToRemove = new List<Pin>();

        foreach (var pin in this.Pins)
        {
            if (pin == null)
            {
                pinsToRemove.Add(pin);
                continue;
            }

            if (pin.IsStanding())
            {
                pinsStanding++;
            }
        }

        foreach (var pin in pinsToRemove)
        {
            this.Pins.Remove(pin);
        }

        return pinsStanding;
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

    private void PinsHaveSettled()
    {
        int standingPins = CountStandingPins();
        int pinsFallen = this.LastSettledCount - standingPins;
        this.LastSettledCount = standingPins;

        print(pinsFallen);

        LastStandingCount = -1;
        pinText.color = Color.green;

        this.gameManager.Bowl(pinsFallen);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Ball>())
        {
            this.BallOutOfPlay = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Ball>())
        {
            this.BallOutOfPlay = true;
        }
    }
}
