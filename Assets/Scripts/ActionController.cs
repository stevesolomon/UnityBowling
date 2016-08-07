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
        this.bowlStats = new int[20];
        this.bowlIndex = 0;
    }

	public ActionResponse Bowl(int pinsDropped)
    {
        ActionResponse response = ActionResponse.Tidy;

        if (pinsDropped < 0 || pinsDropped > 10)
        {
            throw new ArgumentException("pinsDropped must be between 0 and 10.");
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
}
