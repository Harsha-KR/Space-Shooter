using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;

public class MyFirstTest
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void Test1()
    {
        altDriver.FindObject(By.NAME, "Canvas/Single_Player_btn").Tap();
        altDriver.WaitForCurrentSceneToBe(sceneName: "Single_Player2",timeout:3);
        var astroid = altDriver.FindObject(By.NAME, "Asteroid");
        Assert.IsTrue(astroid.enabled);
    }
    [Test]
    public void Test2()
    {
        altDriver.PressKey(AltKeyCode.Space);
    }
    [Test]
    public void test3()
    {
        altDriver.PressKey(AltKeyCode.A, duration: 2);
    }
    [Test]
    public void test4()
    {
        altDriver.PressKey(AltKeyCode.D, duration: 2);
    }
}