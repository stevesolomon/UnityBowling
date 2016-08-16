using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text[] rollTexts;

    public Text[] frameScoreTexts;

	// Use this for initialization
	void Start ()
    {
	    foreach (var rollText in rollTexts)
        {
            rollText.text = "";
        }

        foreach (var frameScoreText in frameScoreTexts)
        {
            frameScoreText.text = "";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
