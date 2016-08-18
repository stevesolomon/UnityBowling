using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        string formattedScores = FormatRollsForDisplay(rolls.ToList());

        foreach (char roll in formattedScores)
        {
            rollTexts[i].text = roll.ToString();
            i++;
        }
    }

    public static string FormatRollsForDisplay(List<int> rolls)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < rolls.Count; i++)
        {
            int box = sb.Length + 1;

            if (box % 2 == 0 && rolls[i - 1] + rolls[i] == 10) //Spare
            {
                sb.Append("/");
            }
            else if (box >= 19 && rolls[i] == 10) //Strike in 10th Frame
            {
                sb.Append("X");
            }
            else if (rolls[i] == 10) //Strike
            {
                sb.Append("X ");
            }
            else if (rolls[i] == 0) //Gutter ball
            {
                sb.Append("-");
            }
            else
            {
                sb.Append(rolls[i]);               
            }
        }

        return sb.ToString();
    }
}
