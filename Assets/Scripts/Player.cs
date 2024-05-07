using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefabs;
    [SerializeField]
    private GameObject _tripleShotPrefabs;
    [SerializeField]
    private float _fireRate = 10f;
    private float _canFire = -1;

    private int _lives = 3;

    private Spawn _spawnManager;
    // Start is called before the first frame update
    private bool _isTrippleShotActive = false;
    private bool _isSpeedBootsActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    void Start()
    {
        //Take the current position =  new position(0,0,0 )
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();     
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
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
        if (_isTrippleShotActive == false)
        {
            Instantiate(_laserPrefabs, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPrefabs, transform.position + new Vector3(-0.25f,0,0), Quaternion.identity);
        }

    }
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives -= 1;

        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void tripleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }
    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTrippleShotActive = false;
    }

    public void speedBootsActive()
    {
        _isSpeedBootsActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBootsDownRoutine());

    }
    IEnumerator SpeedBootsDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBootsActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
}
