using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;
using System.Reflection;
using System;

public class IntegrationTestScript
{
    /// <summary>
    /// Dynamically create digger object for use in integration testing.
    /// </summary>
    /// <returns>The fake player.</returns>
    private static GameObject TestDigger()
    {
        GameObject go = new GameObject();
        go.AddComponent<DiggerControl>();
        FuelBehaviour fb = go.AddComponent<FuelBehaviour>();
        fb.fuelBar = go.AddComponent<Text>();
        fb.fuel = 100;
        fb.dollarBar = new GameObject().AddComponent<Text>();
        fb.dollars = 100;
        return go;
    }

    /// <summary>
    /// Ensures that when the drill moves and digs, the fuel goes down.
    /// </summary>
    [UnityTest]
    public IEnumerator MovementConsumesFuel()
    {
        GameObject digger = IntegrationTestScript.TestDigger();
        DiggerControl control = digger.GetComponent<DiggerControl>();
        FuelBehaviour fb = digger.GetComponent<FuelBehaviour>();
        fb.fuel = 2;
        yield return null;
        var initialFuel = fb.fuel;
        // To do this, move down twice and ensure that fuel has been consumed.
        control.TestMovement("down", digger);
        Assert.AreEqual(fb.fuel, initialFuel - 1);
        // Use the Assert class to test conditions.
        // yield to skip a frame
    }

    /// <summary>
    /// Ensure that the drill cannot dig dirt/gold/diamond above it. It can only move up when there is nothing above it.
    /// </summary>
    [UnityTest]
    public IEnumerator CannotDigUp()
    {
        GameObject digger = IntegrationTestScript.TestDigger();
        DiggerControl control = digger.GetComponent<DiggerControl>();
        yield return null;
        Vector3 initialPos = digger.transform.position;
        control.TestMovement("down", digger);
        control.TestMovement("down", digger);
        control.TestMovement("down", digger);
        Assert.Less(digger.transform.position.y, initialPos.y);
        initialPos.y = digger.transform.position.y;
        Assert.AreEqual(digger.transform.position.x, initialPos.x);
        control.TestMovement("right", digger);
        Assert.AreEqual(digger.transform.position.y, initialPos.y);
        Assert.Greater(digger.transform.position.x, initialPos.x);
        control.TestMovement("up", digger);
        Assert.Greater(digger.transform.position.y, initialPos.y);
    }

    /// <summary>
    /// Ensure that the digger cannot buy extra fuel if they don't actually have the money for it.
    /// </summary>
    [UnityTest]
    public IEnumerator CannotBuyWithoutDollars()
    {
        GameObject digger = IntegrationTestScript.TestDigger();
        FuelBehaviour fb = digger.GetComponent<FuelBehaviour>();
        fb.dollars = 0;
        yield return null;
        fb.UpdateDollars(10);
        int initialDollars = fb.dollars;
        Assert.AreEqual(10, initialDollars);
        fb.Refuel();
        Assert.AreEqual(fb.dollars, initialDollars - 5);
        fb.Refuel();
        Assert.AreEqual(fb.dollars, 0);
        int endingFuel = fb.fuel;
        fb.Refuel();
        Assert.AreEqual(fb.dollars, 0);
        Assert.AreEqual(fb.fuel, endingFuel);
    }
}
