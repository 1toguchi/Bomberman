using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;
using Assert = UnityEngine.Assertions.Assert;

public class BombSpeedUpControllerTest {

    [Test]
    public void BombSpeedUpControllerTestSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator BombSpeedUpControllerTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }

    [TestCase()]
    public void TriggerEnterTest(Collider other)
    {
        Assert.AreEqual(other.tag, "Player");
    }
}
