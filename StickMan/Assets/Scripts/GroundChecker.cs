using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private LayerMask _layerMask;
    //Start is called before the first frame update
    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
