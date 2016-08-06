﻿using UnityEngine;
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
    
}
