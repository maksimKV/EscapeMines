using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameSimulation;
using Utilities;

namespace EscapeMinesTests
{
    /// <summary>
    /// This is a test class for GameSimualtaionTest and is intended to contain all GameSimulation Unit Tests
    ///</summary>
    [TestClass]
    public class GameSimulationTests
    {

        /// <summary>
        /// Tests the constructor of the class
        ///</summary>
        [TestMethod]
        public void GameSimulationConstructorTest()
        {
            Simulation game = new Simulation(10, 10);
            Assert.IsNotNull(game);
            Assert.IsInstanceOfType(game, typeof(Simulation));
        }

        /// <summary>
        /// Tests the movement of the turtle.
        ///</summary>
        [TestMethod]
        public void GameSimulationMoveTurtleTest()
        {
            Simulation game = new Simulation(10, 10);
            Enums.Result target = game.MoveTurtle();
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(Enums.Result));
        }

        /// <summary>
        /// Tests turtle rotation. Note: It is tricky to test void methods, this is why I have used the movement of the turtle to test the rotation.
        ///</summary>
        [TestMethod]
        public void GameSimulationRotateTurtleTest()
        {
            Simulation game = new Simulation(10, 10);
            game.RotateTurtle(false);
            Enums.Result target = game.MoveTurtle();
            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(Enums.Result));
        }
    }
}
