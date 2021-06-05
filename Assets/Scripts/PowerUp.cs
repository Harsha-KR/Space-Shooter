using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType {   TrippleShot, 
                                Speed, 
                                Shield 
                         };

public class PowerUp : MonoBehaviour
{
    float speed = 3f;

    [SerializeField]
    private PowerUpType pType;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -5f )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                switch(pType)
                {
                    case PowerUpType.TrippleShot: 
                        player.TrippelShotActive();
                        break;
                    case PowerUpType.Speed: 
                        player.SpeedPowerupActive();
                        break;
                    case PowerUpType.Shield:
                        player.ShieldActive();
                        break;
                }                
            }
            Destroy(this.gameObject);
        }
    }
}
