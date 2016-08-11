using UnityEngine;
using System.Collections;

public class LaneBoundingBox : MonoBehaviour
{
    private PinSetter pinSetter;

    void Start()
    {
        this.pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Ball>())
        {
            this.pinSetter.BallOutOfPlay = true;
        }
    }
}
