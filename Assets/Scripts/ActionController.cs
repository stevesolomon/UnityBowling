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
	public ActionResponse Bowl(int pinsDropped)
    {
        if (pinsDropped < 0 || pinsDropped > 10)
        {
            throw new ArgumentException("pinsDropped must be between 0 and 10.");
        }

        if (pinsDropped == 10)
        {
            return ActionResponse.EndTurn;
        }

        return ActionResponse.Tidy;
    }
}
