using NUnit.Framework;
using System.Linq;

public class ScoreDisplayTests
{
    [Test]
    public void BowlSingle0()
    {
        int[] rolls = { 0 };
        string expected = "-";
        string actual = ScoreDisplay.FormatRollsForDisplay(rolls.ToList());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BowlSingle1()
    {
        int[] rolls = { 1 };
        string expected = "1";
        string actual = ScoreDisplay.FormatRollsForDisplay(rolls.ToList());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BowlSingleStrike()
    {
        int[] rolls = { 10 };
        string expected = "X ";
        string actual = ScoreDisplay.FormatRollsForDisplay(rolls.ToList());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BowlStrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 };
        string expected = "111111111111111111X11";
        string actual = ScoreDisplay.FormatRollsForDisplay(rolls.ToList());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BowlComplicatedScenario1()
    {
        int[] rolls = { 8, 2, 8, 1, 9, 1, 7, 1, 8, 2, 9, 1, 9, 1, 10, 10, 7, 1 };
        string expected = "8/819/718/9/9/X X 71";
        string actual = ScoreDisplay.FormatRollsForDisplay(rolls.ToList());

        Assert.AreEqual(expected, actual);
    }
}
