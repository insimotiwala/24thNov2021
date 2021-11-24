using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class keypressmove : MonoBehaviour
{
    // public GameObject part;
    //public GameObject[] Roboparts;

    //public List<GameObject> Parts = new List<GameObject>();
    public List<GameObject> Roboparts = new List<GameObject>();

    //private int p = 0;

    // Start is called before the first frame update
    private void Start()

    {
        foreach (GameObject p in Roboparts)
        {
            p.GetComponent<MobileUnit>().enabled = false;
            p.GetComponentInChildren<CollisionDetection>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
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
}