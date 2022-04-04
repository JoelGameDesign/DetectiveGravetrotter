using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sightline : MonoBehaviour
{

    [SerializeField] public float maxLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDistance(float length)
    {
        if (length >= maxLength)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, maxLength);
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, length);
        }
    }
}
