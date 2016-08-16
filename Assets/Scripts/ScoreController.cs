using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreController
{
    /// <summary>
    /// Returns a list of individual frame scores, not cumulative scores.
    /// </summary>
    /// <param name="rolls"></param>
    /// <returns></returns>
    public IList<int> FrameScores (IEnumerable<int> rolls)
    {
        var frameScores = new List<int>();

        for (int i = 1; i < rolls.Count(); i += 2)
        {
            // Prevent an 11th frame score due to strikes on the final frame.
            if (frameScores.Count() == 10)
            {
                break;
            }

            int lastRoll = rolls.ElementAt(i - 1);
            int currRoll = rolls.ElementAt(i);

            // Standard frame
            if (lastRoll + currRoll < 10)
            {
                frameScores.Add(lastRoll + currRoll);
            }

            //We're moving on to test strikes/spares. If we don't have at least one lookahead
            //roll available we can't do any more scoring. We're done!
            if (rolls.Count() - i <= 1)
            {
                break;
            }

            //Strike and bonus
            if (lastRoll == 10)
            {
                i--; //Strike frame has a single bowl.
                frameScores.Add(10 + rolls.ElementAt(i + 1) + rolls.ElementAt(i + 2));
            }
            //Spare and bonus
            else if (lastRoll + currRoll == 10)
            {
                frameScores.Add(10 + rolls.ElementAt(i + 1));
            }
        }

        return frameScores;
    }
    
    /// <summary>
    /// Returns a list of cumulative scores like a traditional bowling score card.
    /// </summary>
    /// <param name="rolls"></param>
    /// <returns></returns>
    public IList<int> CumulativeScores(IEnumerable<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();

        var frameScores = this.FrameScores(rolls);
        int runningTotal = 0;

        foreach (var frameScore in frameScores)
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }
}
