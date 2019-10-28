using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 2;
    [SerializeField] float m_turnSpeed = 200;
    [SerializeField] float m_jumpForce = 4;
    [SerializeField] float m_viewDistance = 4.0f;

    public GameObject player;

    private Animator m_animator;
    private Rigidbody m_rigidBody;

    private Vector3 prevRotation;
    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private RaycastHit vision;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.childCount == 0)
            player.transform.parent = transform;

        if (transform.GetChild(0).gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
                PossessObject();            
        }
        else if (transform.childCount != 0 && transform.GetChild(0).gameObject.tag != "Player" && Input.GetButtonUp("Jump"))
        {
            transform.GetChild(0).transform.parent = null;
            transform.position = player.transform.position;
        }

        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * m_viewDistance, Color.red, 0.1f);

        Move();
        m_wasGrounded = m_isGrounded;
    }

    void PossessObject()
    {      
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out vision, m_viewDistance))
        {
            if (vision.collider.tag == "Object")
            {
                player.transform.parent = null;
                transform.position = vision.collider.gameObject.transform.position;
                vision.collider.gameObject.transform.parent = transform;
            }
        }
    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;
        }

    }
}
