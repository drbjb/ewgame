using UnityEngine;

public class lookAt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target, Vector3.right);


        }
    }
}