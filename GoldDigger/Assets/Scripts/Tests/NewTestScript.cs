using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class NewTestScript {

    [Test]
    public void TestFuelConsume() {
        GameObject go = new GameObject();
        go.AddComponent<DiggerControl>();
        go.AddComponent<Text>();
        go.AddComponent<FuelBehaviour>();

        go.GetComponent<FuelBehaviour>().fuelBar = go.GetComponent<Text>();
        int fuel = 2;

        //Consume fuel if player is underground
        go.transform.position = new Vector3(1, 2, 1);
        GameObject fb = go.GetComponent<DiggerControl>().TestFuelConsume(fuel, go);

        Assert.AreEqual(1, fb.GetComponent<FuelBehaviour>().fuel);
        Assert.AreNotEqual(2, fb.GetComponent<FuelBehaviour>().fuel);
        Assert.AreEqual("Fuel: 1", fb.GetComponent<FuelBehaviour>().fuelBar.text);

        /*//If player is at surface (y=1), fuel should not be consumed
        go.transform.position = new Vector3(1, 1, 1);
        fb = go.GetComponent<DiggerControl>().TestFuelConsume(fuel, go.GetComponent<FuelBehaviour>());

        Assert.AreEqual(2, fb.fuel);
        Assert.AreNotEqual(1, fb.fuel);
        Assert.AreEqual("Fuel: 2", fb.fuelBar.text);*/
    }

    [Test]
    public void TestIsOutOfGrid()
    {
        bool result = false;
        GameObject go = new GameObject();
        go.AddComponent<DiggerControl>();


        result = go.GetComponent<DiggerControl>().IsOutOfGrid(1, -2, 1);
        Assert.IsFalse(result);

        result = go.GetComponent<DiggerControl>().IsOutOfGrid(0, 0, 3);
        Assert.IsTrue(result);

        result = go.GetComponent<DiggerControl>().IsOutOfGrid(5, -5, 2);
        Assert.IsFalse(result);

        result = go.GetComponent<DiggerControl>().IsOutOfGrid(5, -5, 5);
        Assert.IsFalse(result);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
