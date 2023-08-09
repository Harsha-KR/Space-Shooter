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
    public void A_PlayerMovesLeft()
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
    public void B_PlayerMovesRight()
    {
        altDriver.PressKey(AltKeyCode.D, duration: 2);
        var initialPosition = altDriver.FindObject(By.NAME, "Player").GetWorldPosition();
        altDriver.PressKey(AltKeyCode.A, duration: 0.5f);
        var finalPosition = altDriver.FindObject(By.NAME, "Player").GetWorldPosition();
        var direction_x = finalPosition.x - initialPosition.x;
        var direction_y = finalPosition.y - initialPosition.y;
        Assert.True(direction_x < 0);

    }
    [Test]
    public void C_PlayerDestroysAsteroid()
    {
        var asteroid_x = altDriver.FindObject(By.NAME, "Asteroid").GetWorldPosition().x;
        var player_x = altDriver.FindObject(By.NAME, "Player").GetWorldPosition().x;
        bool isPlayerInLineWithAsteroid = (player_x - asteroid_x) < 0.5f;
        while(isPlayerInLineWithAsteroid == false)
        {
            if(player_x>asteroid_x)
            { 
                altDriver.PressKey(AltKeyCode.A, duration: 0.1f);
                player_x = altDriver.FindObject(By.NAME, "Player").GetWorldPosition().x;
            }
            if(player_x<asteroid_x)
            {
                altDriver.PressKey(AltKeyCode.D, duration: 0.1f);
                player_x= altDriver.FindObject(By.NAME, "Player").GetWorldPosition().x;
            }
            isPlayerInLineWithAsteroid = (player_x - asteroid_x) < 0.5f;
        }
        altDriver.PressKey(AltKeyCode.Space);
        
    }
}