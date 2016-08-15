using NUnit.Framework;
using System.Linq;

namespace Assets.Editor
{
    public class ScoreControllerTests
    {
        private ScoreController scoreController;

        [SetUp]
        public void Setup()
        {
            this.scoreController = new ScoreController();
        }

        [Test]
        public void FirstFrameTwoRollsScore()
        {
            int[] rolls = { 2, 3 };
            int[] frames = { 5 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void FirstThreeRollsScore()
        {
            int[] rolls = { 2, 3, 4 };
            int[] frames = { 5 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void FirstTwoFramesScore()
        {
            int[] rolls = { 2, 3, 4, 5 };
            int[] frames = { 5, 9 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void FirstFiveRollsScore()
        {
            int[] rolls = { 2, 3, 4, 5, 6 };
            int[] frames = { 5, 9 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void FirstThreeFramesScore()
        {
            int[] rolls = { 2, 3, 4, 5, 6, 1 };
            int[] frames = { 5, 9, 7 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void FirstSevenRollsScore()
        {
            int[] rolls = { 2, 3, 4, 5, 6, 1, 2 };
            int[] frames = { 5, 9, 7 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void BowlStrikeThenOneScore()
        {
            int[] rolls = {10, 1};
            int[] frames = { };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void BowlSpareScore()
        {
            int[] rolls = { 1, 9 };
            int[] frames = { };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void BowlTwoFramesThenStrike()
        {
            int[] rolls = { 1, 2, 3, 4, 5, 5 };
            int[] frames = { 3, 7 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleSpareBonus()
        {
            int[] rolls = { 1, 2, 3, 5, 5, 5, 3, 3};
            int[] frames = { 3, 8, 13, 6 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleTwoSpareBonuses()
        {
            int[] rolls = { 1, 2, 3, 5, 5, 5, 3, 3, 7, 1, 9, 1, 6 };
            int[] frames = { 3, 8, 13, 6, 8, 16 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleStrikeBonus()
        {
            int[] rolls = { 10, 3, 4 };
            int[] frames = { 17, 7 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleStrikeBonusLaterFrames()
        {
            int[] rolls = { 1, 2, 3, 4, 5, 4, 3, 2, 10, 1, 3, 3, 4 };
            int[] frames = { 3, 7, 9, 5, 14, 4, 7 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleMultipleConsecutiveStrikes()
        {
            int[] rolls = { 10, 10, 2, 3 };
            int[] frames = { 22, 15, 5 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleMultipleConsecutiveStrikesWithAFrameSeparationStrike()
        {
            int[] rolls = { 10, 10, 2, 3, 10, 5, 3 };
            int[] frames = { 22, 15, 5, 18, 8 };

            Assert.AreEqual(frames.ToList(), this.scoreController.FrameScores(rolls));
        }

        [Test]
        public void HandleGutterBallGame()
        {
            int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] frames = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleSpareBonusFirstFrame()
        {
            int[] rolls = { 5, 5, 3 };
            int[] frames = { 13 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleSpareInLastFrame()
        {
            int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 7 };
            int[] frames = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 35 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleAllStrikesGame()
        {
            int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[] frames = { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleStrikeInLastFrame()
        {
            int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 2, 3 };
            int[] frames = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 33 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleRealWorldGameScore()
        {
            int[] rolls = { 10, 9, 1, 9, 1, 9, 1, 9, 1, 7, 0, 9, 0, 10, 8, 2, 8, 2, 10 };
            int[] frames = { 20, 39, 58, 77, 94, 101, 110, 130, 148, 168 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }

        [Test]
        public void HandleRealWorldGameScore2()
        {
            int[] rolls = { 8, 2, 8, 1, 9, 1, 7, 1, 8, 2, 9, 1, 9, 1, 10, 10, 7, 1 };
            int[] frames = { 18, 27, 44, 52, 71, 90, 110, 137, 155, 163 };

            Assert.AreEqual(frames.ToList(), this.scoreController.CumulativeScores(rolls));
        }
    }
}