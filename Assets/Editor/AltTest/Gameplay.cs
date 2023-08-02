using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;

public class Gameplay
{   
    public AltDriver altDriver;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver();
    }

    
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void PlayerMovesLeft()
    {
        altDriver.LoadScene("Single_Player");
        var initialPosition = altDriver.FindObject(By.NAME, "Player").GetWorldPosition();
        altDriver.PressKey(AltKeyCode.A, duration: 0.5f);
        var finalPosition = altDriver.FindObject(By.NAME, "Player").GetWorldPosition();
        var direction_x = finalPosition.x - initialPosition.x;
        var direction_y = finalPosition.y - initialPosition.y;
        Assert.True(direction_x < 0);
    }
    [Test]
    public void Test2()
    {
        altDriver.PressKey(AltKeyCode.Space);

    }
    [Test]
    public void test4()
    {
        altDriver.PressKey(AltKeyCode.D, duration: 2);
    }
}