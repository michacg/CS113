using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 2;
    //[SerializeField] float m_turnSpeed = 200;
    [SerializeField] float m_floatHeight = 4.0f;
    [SerializeField] float m_floatMultiplier = 0.1f;
    [SerializeField] float m_viewDistance = 4.0f;

    public GameObject player;

    private Animator m_animator;
    private Rigidbody m_rigidBody;
    private Collider[] objectsAround;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private bool floatingUp = true;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
/*    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;*/

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
        player.transform.parent = transform;
    }

    void Update()
    {
        if (transform.GetChild(0).gameObject.tag == "Player" && transform.childCount == 1)
        {
            if (Input.GetKey(KeyCode.F))
                PossessObject();
        }
        else if (transform.childCount >= 1 && Input.GetKeyDown(KeyCode.F))
            ReleaseObject();                 

        //Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * m_viewDistance, Color.red, 0.1f);

        Move();
        m_wasGrounded = m_isGrounded;
    }

/*    void PossessObject()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * m_viewDistance, Color.red, 0.1f);
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out vision, m_viewDistance))
        {
            if (vision.collider.tag == "Object")
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.position = new Vector3(vision.collider.gameObject.transform.position.x, 0, vision.collider.gameObject.transform.position.z);
                vision.collider.gameObject.transform.parent = transform;
            }
        }
    }*/

    void PossessObject()
    {
        objectsAround = Physics.OverlapSphere(GetComponent<Transform>().position, m_viewDistance);
        for (int i = 0; i < objectsAround.Length; ++i)
        {
            if (objectsAround[i].gameObject.CompareTag("Object"))
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.position = new Vector3(objectsAround[i].gameObject.transform.position.x, 0, objectsAround[i].gameObject.transform.position.z);
                objectsAround[i].gameObject.transform.parent = transform;
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_viewDistance);
    }

    void ReleaseObject()
    {
        Debug.Log("Child released.");
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).transform.parent = null;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("floatControl");
        }

    }

    IEnumerator floatControl()
    {
        float height = transform.position.y;
        
        if (height < m_floatHeight && floatingUp)
        {
            transform.position = new Vector3(transform.position.x, height + m_floatMultiplier, transform.position.z);
        }
        else if (height >= m_floatHeight && floatingUp)
        {
            floatingUp = false;
        }
        
        if (height > 0f && !floatingUp)
        {
            transform.position = new Vector3(transform.position.x, height - m_floatMultiplier, transform.position.z);
        }
        else if (height <= 0f && !floatingUp)
        {
            floatingUp = true;
        }
        yield return null;

    }
}
