using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == gameObject.tag)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
