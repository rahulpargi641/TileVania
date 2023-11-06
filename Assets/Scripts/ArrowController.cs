using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 20f;
    private Rigidbody2D rigidbody;
    private float xSpeed;

    // Start is called before the first frame update
    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        xSpeed = transform.localScale.y * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(xSpeed, 0f);
    }
}