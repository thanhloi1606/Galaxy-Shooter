using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // Update is called once per frame

    //ID for powerups
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int _powerUpID;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            switch (_powerUpID)
            {
                case 0:
                    player.tripleShotActive();
                    break;
                case 1:
                    player.speedBootsActive();
                    break;
                case 2:
                    player.ShieldActive();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
