using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class keypressmove : MonoBehaviour
{
    // public GameObject part;
    //public GameObject[] Roboparts;
    public float speed = 10f;

    private RectTransform rect;

    //public List<GameObject> Parts = new List<GameObject>();
    public List<GameObject> Roboparts = new List<GameObject>();

    //private int p = 0;

    // Start is called before the first frame update
    private void Start()

    {
        {
            rect = GetComponent<RectTransform>();
        }

        foreach (GameObject p in Roboparts)
        {
            p.GetComponent<MobileUnit>().enabled = false;
            p.GetComponentInChildren<CollisionDetection>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        JoystickMovement();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(0.5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(-0.5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0.0f, 0f, -0.5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0.0f, 0f, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);

        CollisionDetection mu = col.GetComponent<CollisionDetection>();
        if (mu == null) { return; }

        // foreach (GameObject p in Roboparts)
        //{
        col.enabled = false;
        col.GetComponentInParent<MobileUnit>().enabled = true;
        col.GetComponent<CollisionDetection>().enabled = true;
        Roboparts.Add(col.gameObject);
        Debug.Log("hit");
    }

    private void JoystickMovement()
    {
        //get input
        Vector3 joy = new Vector3(Input.GetAxis("RightJoyY"), 0, Input.GetAxis("RightJoyX"));

        //camera vectors
        Vector3 forward = Camera.main.transform.forward;
        // Debug.DrawRay(transform.position, forward * 10, Color.green);
        Vector3 project = Vector3.ProjectOnPlane(forward, Vector3.up);
        // Debug.DrawRay(transform.position, project * 10, Color.blue);
        Vector3 right = Camera.main.transform.right;
        // Debug.DrawRay(transform.position, right * 10, Color.black);

        //only continue if joystick pressed more than 0.3f
        if (joy.magnitude < 0.3f) { return; }
        Debug.Log("Player Move");

        //move camera
        Vector3 move = right * -joy.x + project * -joy.z;
        transform.Translate(move.normalized * Time.deltaTime * speed);
        //Debug.DrawRay(transform.position, move, Color.red);
    }
}