using NUnit.Framework;
using AltTester.AltTesterUnitySDK.Driver;

public class MyFirstTest
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver = new AltDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void A_EnterSinglePlayer()
    {
        altDriver.LoadScene("MainMenu");
        altDriver.FindObject(By.NAME, "Canvas/Single_Player_btn").Tap();
        altDriver.WaitForCurrentSceneToBe("Single_Player", timeout: 10);
        var currentScene = altDriver.GetCurrentScene();
        string currentSceneShouldBe = "Single_Player";
        Assert.That(currentScene == currentSceneShouldBe);
    }
    [Test]
    public void B_PauseTheTitle()
    {
        altDriver.PressKey(AltKeyCode.T);
        var pauseMenu = altDriver.FindObject(By.NAME, "Canvas/PauseMenuPanel");
        Assert.IsTrue(pauseMenu.enabled);
        altDriver.PressKey(AltKeyCode.T);
        Assert.IsFalse(pauseMenu.enabled);
    }
   /*
    [Test]
    public void QuitToMainMenu()
    {
        altDriver.PressKey(AltKeyCode.Escape);
        if(altDriver.FindObject(By.NAME, "Canvas/Pause Menu Panel").enabled)
        {
            altDriver.FindObject(By.NAME, "Canvas/Pause Menu Panel/Quit to Menu").Tap();
        }
        else
        {
            altDriver.PressKey(AltKeyCode.Escape);
            altDriver.FindObject(By.NAME, "Canvas/Pause Menu Panel/Quit to Menu").Tap();
        }
        altDriver.WaitForCurrentSceneToBe("MainMenu",timeout:3);
        var currentScene = altDriver.GetCurrentScene();
        string currentSceneShouldBe = "MainMenu";
        Assert.That(currentScene == currentSceneShouldBe);

    }*/
}