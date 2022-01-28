using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float slowTime;

    private float timePassed =0;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > slowTime) Destroy(gameObject);
    }
}
