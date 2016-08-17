using System.Collections.Generic;
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
	
	public void FillFrames(IEnumerable<int> frames)
    {
        int i = 0;

        foreach (var frame in frames)
        {
            frameScoreTexts[i].text = frame.ToString();
            i++;
        }
    }

    public void FillRolls(IEnumerable<int> rolls)
    {
        int i = 0;

        foreach (var roll in rolls)
        {
            rollTexts[i].text = roll.ToString();
            i++;
        }
    }
}
