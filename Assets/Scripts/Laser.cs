using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5f;
    void Update()
    {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > 8)
            {
                Destroy(this.gameObject);
            }
    }
}
