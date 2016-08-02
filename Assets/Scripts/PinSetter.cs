using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    private const string pinTextFormat = "{0} Pins Remaining";

    public Text pinText;

    public int LastStandingCount { get; private set; }

    private List<Pin> Pins { get; set; } 

    private bool CanUpdatePins { get; set; }

    private float LastChangeTime { get; set; }

	// Use this for initialization
	void Start ()
    {
        Pins = GameObject.FindObjectsOfType<Pin>().ToList();
        LastStandingCount = -1;

        UpdatePinsStanding();

        pinText.color = Color.green;
        CanUpdatePins = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (CanUpdatePins)
        {
            UpdatePinsStanding();
        }
	}

    private void UpdatePinsStanding()
    {
        LastStandingCount = CountStandingPins();
        pinText.text = string.Format(pinTextFormat, LastStandingCount);

        if (PinsHaveSettled())
        {

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

    private bool PinsHaveSettled()
    {
        bool pinsHaveSettled = false;

        return pinsHaveSettled;
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
        Pin pin = collider.gameObject.GetComponent<Pin>();

        if (pin != null)
        {
            this.Pins.Remove(pin);
            Destroy(collider.gameObject);
            
        }
    }
}
