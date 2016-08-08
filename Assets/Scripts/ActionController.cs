using UnityEngine;
using System.Collections;
using System;

public enum ActionResponse
{
    Tidy,
    Reset,
    EndTurn,
    EndGame
}

public class ActionController
{
    private int[] bowlStats;

    private int bowlIndex;

    public ActionController()
    {
        this.bowlStats = new int[21];
        this.bowlIndex = 0;
    }

	public ActionResponse Bowl(int pinsDropped)
    {
        ActionResponse response = ActionResponse.Tidy;

        bowlStats[bowlIndex] = pinsDropped;

        if (pinsDropped < 0 || pinsDropped > 10)
        {
            throw new ArgumentException("pinsDropped must be between 0 and 10.");
        }

        if (this.bowlIndex == 21 - 1)
        {
            return ActionResponse.EndGame;
        }

        // Test for Strike in the last frame.
        if (this.bowlIndex >= 19 - 1)
        {
            if (Roll21Awarded())
            {
                this.bowlIndex++;
                return ActionResponse.Reset;
            }
            else if (this.bowlIndex == 20 - 1)
            {
                return ActionResponse.EndGame;
            }

            this.bowlIndex++;
            return ActionResponse.Tidy;               
        }

        if (pinsDropped == 10)
        {
            response = ActionResponse.EndTurn;
        }

        if (pinsDropped >= 0 && pinsDropped < 10 )
        {            
            // Tidy if we're mid-frame, EndTurn if we're at the last frame.
            response = (this.bowlIndex % 2) == 0 ? ActionResponse.Tidy : ActionResponse.EndTurn;
            this.bowlIndex++;
        }

        return response;
    }

    private bool Roll21Awarded()
    {
        return bowlStats[19 - 1] + bowlStats[20 - 1] >= 10;
    }
}
