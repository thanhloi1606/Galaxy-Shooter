using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private GameObject _laserPrefabs;
    [SerializeField]
    private float _fireRate = 0.05f;
    private float _canFire = -1;
    // Start is called before the first frame update
    void Start()
    {
        //Take the current position =  new position(0,0,0 )
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();     
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            shootLaser();
        }  
    }

    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticaltalInput = Input.GetAxis("Vertical");
        
        Vector3 inputDirection = new Vector3(horizontalInput, verticaltalInput, 0);
        transform.Translate(inputDirection * _speed *Time.deltaTime);

        if ((transform.position.x > 9))
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        if ((transform.position.x < -9))
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        if ((transform.position.y < -4))
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if ((transform.position.y > 6))
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
    }

    void shootLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefabs, transform.position + new Vector3(0,1,0), Quaternion.identity);
    }
}
