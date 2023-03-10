using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Tests
{
    [TestClass]
    [TestCategory("Enemy Creation")]
    public class EnemyFactoryShould
    {
        [TestMethod]
        public void NotAllowNullName()
        {
            Console.WriteLine("Creating EnemyFactory");
            EnemyFactory sut = new EnemyFactory();

            Console.WriteLine("Calling Create method");
            Assert.ThrowsException<ArgumentNullException>(
                () => sut.Create(null));
        }

        [TestMethod]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            EnemyFactory sut = new EnemyFactory();

            EnemyCreationException ex = Assert.ThrowsException<EnemyCreationException>(
                () => sut.Create("Zombie", true));

            Assert.AreEqual("Zombie", ex.RequestedEnemyName);
        }

        [TestMethod]
        public void CreateNormalEnemyByDefault()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsInstanceOfType(enemy, typeof(NormalEnemy));
        }

        //[TestMethod]
        //public void CreateNormalEnemyByDefault_NotTypeExample()
        //{
        //    EnemyFactory sut = new EnemyFactory();

        //    Enemy enemy = sut.Create("Zombie");

        //    Assert.IsNotInstanceOfType(enemy, typeof(NormalEnemy));
        //}

        [TestMethod]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsInstanceOfType(enemy, typeof(BossEnemy));
        }

        [TestMethod]
        public void CreateSeparateInstances()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = sut.Create("Zombie");

            Assert.AreNotSame(enemy1, enemy2);
        }
    }
}
