using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text pinText;

    private Pin[] pins;

    private string pinTextFormat = "{0} Pins Remaining";

	// Use this for initialization
	void Start ()
    {
        pins = GameObject.FindObjectsOfType<Pin>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int pinsStanding = PinsStanding();

        pinText.text = string.Format(pinTextFormat, pinsStanding);
	}

    private int PinsStanding()
    {
        int pinsStanding = 0;

        foreach (var pin in this.pins)
        {
            if (pin.IsStanding())
            {
                pinsStanding++;
            }
        }

        return pinsStanding;
    }
}
