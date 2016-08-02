using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    private const string pinTextFormat = "{0} Pins Remaining";

    public Text pinText;

    private Pin[] Pins { get; set; }    

    private bool CanUpdatePins { get; set; }

	// Use this for initialization
	void Start ()
    {
        Pins = GameObject.FindObjectsOfType<Pin>();

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
        int pinsStanding = CountStandingPins();

        pinText.text = string.Format(pinTextFormat, pinsStanding);
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            CanUpdatePins = true;
            pinText.color = Color.red;
        }
    }
}
