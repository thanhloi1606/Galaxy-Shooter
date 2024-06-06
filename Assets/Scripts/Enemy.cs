using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    //Handle the animator Component
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The PLayer is NULL");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The Animator is NULL"); 
        }
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
    
    private void OnTriggerEnter2D(Collider2D other)
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
            _speed = 0;
            _animator.SetTrigger("onEnemyDeath");
            Destroy(this.gameObject, 2.7f);
            if(_player != null)
            {
                _player.AddScore(10);
            }

            Destroy(other.gameObject);
        }
    }
}
