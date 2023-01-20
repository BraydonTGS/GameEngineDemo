using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameEngine.Tests
{
    [TestClass]
    public class PlayerCharacterShould
    {
        PlayerCharacter sut;

        [TestInitialize]
        public void TestInitialize()
        {
            sut = new PlayerCharacter
            {
                FirstName = "Sarah",
                LastName = "Smith"
            };
        }

        [TestMethod]
        [PlayerDefaults]
        public void BeInexperiencedWhenNew()
        {
            Assert.IsTrue(sut.IsNoob);
        }

        [TestMethod]
        [PlayerDefaults]
        public void NotHaveNickNameByDefault()
        {
            Assert.IsNull(sut.Nickname);            
        }

        [TestMethod]
        [PlayerDefaults]
        public void StartWithDefaultHealth()
        {
            Assert.AreEqual(100, sut.Health);
        }
    

        [DataTestMethod]
        [DynamicData(nameof(ExternalHealthDamageTestData.TestData), 
                     typeof(ExternalHealthDamageTestData))]
        [PlayerHealth]
        public void TakeDamage(int damage, int expectedHealth)
        {
            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }        


        [TestMethod]
        [PlayerHealth]
        public void TakeDamage_NotEqual()
        {
            sut.TakeDamage(1);

            Assert.AreNotEqual(100, sut.Health);
        }

        [TestMethod]
        [PlayerHealth]
        [TestCategory("Another Category")]
        public void IncreaseHealthAfterSleeping()
        {
            sut.Sleep(); // Expect increase between 1 to 100 inclusive

            //Assert.IsTrue(sut.Health >= 101 && sut.Health <= 200);
            Assert.That.IsInRange(sut.Health, 101, 200);
        }


        [TestMethod]
        public void CalculateFullName()
        { 
            Assert.AreEqual("SARAH SMITH", sut.FullName, true);
        }

        [TestMethod]
        public void HaveFullNameStartingWithFirstName()
        {
            StringAssert.StartsWith(sut.FullName, "Sarah");
        }

        [TestMethod]
        public void HaveFullNameEndingWithLastName()
        {
            StringAssert.EndsWith(sut.FullName, "Smith");
        }

        [TestMethod]
        public void CalculateFullName_SubstringAssertExample()
        {
            StringAssert.Contains(sut.FullName, "ah Sm");
        }

        [TestMethod]
        public void CalculateFullNameWithTitleCase()
        {
            StringAssert.Matches(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));            
        }

        [TestMethod]
        public void HaveALongBow()
        {
            CollectionAssert.Contains(sut.Weapons, "Long Bow");
        }

        [TestMethod]
        public void NotHaveAStaffOfWonder()
        {
            CollectionAssert.DoesNotContain(sut.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            CollectionAssert.AreEqual(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveAllExpectedWeapons_AnyOrder()
        {
            var expectedWeapons = new[]
            {
                "Short Bow",
                "Long Bow",
                "Short Sword"
            };

            CollectionAssert.AreEquivalent(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveNoDuplicateWeapons()
        {
            CollectionAssert.AllItemsAreUnique(sut.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneKindOfSword()
        {
            //Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));
            CollectionAssert.That.AtLeastOneItemSatisfies(sut.Weapons,
                                                          weapon => weapon.Contains("Sword"));
        }

        [TestMethod]
        public void HaveNoEmptyDefaultWeapons()
        {
            //Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));

            //CollectionAssert.That.AllItemsNotNullOrWhitespace(sut.Weapons);

            //sut.Weapons.Add(" ");

            //CollectionAssert.That.AllItemsSatisfy(sut.Weapons,
                                            //      weapon => !string.IsNullOrWhiteSpace(weapon));

            CollectionAssert.That.All(sut.Weapons, weapon =>
            {
                StringAssert.That.NotNullOrWhiteSpace(weapon);
                Assert.IsTrue(weapon.Length > 5);
                //etc.
            });
        }

    }
}
