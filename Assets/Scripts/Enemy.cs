using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *_speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-6f,6f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            Destroy(this.gameObject);
            if (player!= null)
            {
                player.Damage();
            }

        }
        else if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
        }
    }
}
