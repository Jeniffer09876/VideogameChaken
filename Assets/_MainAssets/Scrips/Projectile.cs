using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    public QuadraticCurve curve;
    public float speed;
    private float sampleTime;
    public bool magic;

    // Start is called before the first frame update
    void Start()
    {
        sampleTime = 0f;
        
    }

private void OnEnable()
    {
        transform.position = curve.A.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (magic)
        {
            shootMagic();
        }
    }

    private void shootMagic() 
    {
            sampleTime += Time.deltaTime * speed;
            transform.position = curve.evaluate(sampleTime);

        if (sampleTime > 1f)
        {
            Debug.Log("boom");
            gameObject.SetActive(false);
            magic = false;
            sampleTime = 0f;
          
        }
    }
}
