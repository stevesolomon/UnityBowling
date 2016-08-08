using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class ActionControllerTests
{
    private ActionController actionController;

    [SetUp]
    public void Setup()
    {
        this.actionController = new ActionController();
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void BowlWithPinsDroppedLessThanZeroThrows()
    {
        this.actionController.Bowl(-1);
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void BowlWithPinsDroppedGreaterThanTenThrows()
    {
        this.actionController.Bowl(11);
    }

    [Test]
    public void OneStrikeReturnsEndTurn()
    {
        ActionResponse response = this.actionController.Bowl(10);

        Assert.AreEqual(ActionResponse.EndTurn, response);
    }

    [Test]
    public void BowlStandardFirstFrameReturnsTidy()
    {
        ActionResponse response = this.actionController.Bowl(7);
        Assert.AreEqual(ActionResponse.Tidy, response);
    }

    [Test]
    public void BowlStandardRegularFrameReturnsEndTurn()
    {
        this.actionController.Bowl(2);
        ActionResponse response = this.actionController.Bowl(3);
        Assert.AreEqual(ActionResponse.EndTurn, response);
    }

    [Test]
    public void BowlSpareRegularFrameReturnsEndTurn()
    {
        this.actionController.Bowl(1);
        this.actionController.Bowl(4);
        this.actionController.Bowl(2);
        ActionResponse response = this.actionController.Bowl(8);
        Assert.AreEqual(ActionResponse.EndTurn, response);
    }

    [Test]
    public void BowlStrikeInFirstRollOfLastFrameReturnsReset()
    {
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        foreach (int bowl in bowls)
        {
            this.actionController.Bowl(bowl);
        }

        ActionResponse response = this.actionController.Bowl(10);
        Assert.AreEqual(ActionResponse.Reset, response);
    }

    [Test]
    public void BowlSpareInFirstRollOfLastFrameReturnsReset()
    {
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        foreach (int bowl in bowls)
        {
            this.actionController.Bowl(bowl);
        }

        this.actionController.Bowl(1);
        ActionResponse response = this.actionController.Bowl(9);
        Assert.AreEqual(ActionResponse.Reset, response);
    }

    [Test]
    public void BowlLastFrameNoStrikeOrSpareReturnsEndGame()
    {
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        foreach (int bowl in bowls)
        {
            this.actionController.Bowl(bowl);
        }

        this.actionController.Bowl(2);
        ActionResponse response = this.actionController.Bowl(1);
        Assert.AreEqual(ActionResponse.EndGame, response);
    }

    [Test]
    public void BowlLastFrameExtraBowl21ReturnsEndGame()
    {
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        foreach (int bowl in bowls)
        {
            this.actionController.Bowl(bowl);
        }

        this.actionController.Bowl(10);
        this.actionController.Bowl(10);
        ActionResponse response = this.actionController.Bowl(1);
        Assert.AreEqual(ActionResponse.EndGame, response);
    }
}
