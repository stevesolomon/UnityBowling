using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public float distanceToRaise = 40f;

    public Ball ball;

    public GameObject pinSet;

    public PinCounter pinCounter;

    private List<Pin> Pins { get; set; }     

    private ActionController ActionController { get; set; }

    private Animator Animator { get; set; }

	// Use this for initialization
	void Start ()
    {
        Pins = GameObject.FindObjectsOfType<Pin>().ToList();
        ActionController = new ActionController();
        Animator = this.GetComponent<Animator>();

        pinCounter = GameObject.FindObjectOfType<PinCounter>();

        if (ball == null)
        {
            this.ball = GameObject.FindObjectOfType<Ball>();
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

        Pins = GameObject.FindObjectsOfType<Pin>().ToList();
        this.pinCounter.RenewedPins(Pins);
    }

    public void HandleResponse(ActionResponse response)
    {
        switch (response)
        {
            case ActionResponse.Tidy:
                this.Animator.SetTrigger("tidyPinsTrigger");
                break;
            case ActionResponse.Reset:
            case ActionResponse.EndTurn:
                this.pinCounter.LastSettledCount = 10;
                this.Animator.SetTrigger("resetPinsTrigger");
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            //pinText.color = Color.red;
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
}
