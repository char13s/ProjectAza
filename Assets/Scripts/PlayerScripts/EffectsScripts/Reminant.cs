using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reminant : MonoBehaviour
{
    [SerializeField] protected float dieRate;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Destroy(gameObject, dieRate);
    }
}
