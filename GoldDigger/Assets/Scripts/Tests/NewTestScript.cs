using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class NewTestScript {

    [Test]
    public void TestFuelConsume()
    {
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

        /*//If player is at surface (y=1), fuel should not be consumed.
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

    [Test]
    public void TestMovement()
    {
        GameObject go = new GameObject();
        go.AddComponent<DiggerControl>();
        go.AddComponent<Text>();
        go.AddComponent<FuelBehaviour>();

        go.GetComponent<FuelBehaviour>().fuelBar = go.GetComponent<Text>();

        go.transform.position = new Vector3(2, -5, 1);
        Vector3 res = go.GetComponent<DiggerControl>().TestMovement("up", go);
        Assert.AreEqual(new Vector3(2, -4, 1), res);

        go.transform.position = new Vector3(2, -5, 1);
        res = go.GetComponent<DiggerControl>().TestMovement("down", go);
        Assert.AreEqual(new Vector3(2, -6, 1), res);

        go.transform.position = new Vector3(2, -5, 1);
        res = go.GetComponent<DiggerControl>().TestMovement("left", go);
        Assert.AreEqual(new Vector3(1, -5, 1), res);

        go.transform.position = new Vector3(2, -5, 1);
        res = go.GetComponent<DiggerControl>().TestMovement("right", go);
        Assert.AreEqual(new Vector3(3, -5, 1), res);

    }

    [Test]
    public void TestSpawnMap()
    {
        GameObject go = new GameObject();
        go.AddComponent<SpawnGrid>();

        go.GetComponent<SpawnGrid>().SpawnMap();

        Assert.AreEqual(SpawnGrid.width, go.GetComponent<SpawnGrid>().grid.GetLength(0));
        //Assert.AreEqual(SpawnGrid.height, go.GetComponent<SpawnGrid>().grid.GetLength(1));

    }

    [Test]
    public void TestUpdateFuel()
    {
        GameObject go = new GameObject();
        go.AddComponent<FuelBehaviour>();
        go.AddComponent<Text>();

        go.GetComponent<FuelBehaviour>().fuel = 8;
        go.GetComponent<FuelBehaviour>().fuelBar = go.GetComponent<Text>();

        go.GetComponent<FuelBehaviour>().UpdateFuel();
        Assert.AreEqual("Fuel: 8", go.GetComponent<FuelBehaviour>().fuelBar.text);
    }

    [Test]
    public void TestUpdateDollars()
    {
        GameObject go = new GameObject();
        go.AddComponent<FuelBehaviour>();
        go.AddComponent<Text>();

        go.GetComponent<FuelBehaviour>().dollars = 8;
        go.GetComponent<FuelBehaviour>().dollarBar = go.GetComponent<Text>();

        go.GetComponent<FuelBehaviour>().UpdateDollars(8);
        Assert.AreEqual("$ 8", go.GetComponent<FuelBehaviour>().dollarBar.text);
    }

    [Test]
    public void TestCollect()
    {
        GameObject go = new GameObject();
        go.AddComponent<ColliderLogic>();

        go.AddComponent<FuelBehaviour>();
        go.AddComponent<Text>();

        go.GetComponent<ColliderLogic>().points = 0;
        go.GetComponent<ColliderLogic>().fuelBehaviour = go.GetComponent<FuelBehaviour>();
        go.GetComponent<FuelBehaviour>().dollarBar = go.GetComponent<Text>();

        go.GetComponent<ColliderLogic>().Collect("Diamond");
        Assert.AreEqual(20, go.GetComponent<ColliderLogic>().points);
        go.GetComponent<ColliderLogic>().Collect("Dirt");
        Assert.AreEqual(20, go.GetComponent<ColliderLogic>().points);
        go.GetComponent<ColliderLogic>().Collect("Gold");
        Assert.AreEqual(30, go.GetComponent<ColliderLogic>().points);



    }
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
