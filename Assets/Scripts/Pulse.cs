using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] float pulseSpeed;
    [SerializeField] float pulseAmplitude;

    private Vector3 originalScale;
    private float t;
    private float t0 = Mathf.PI / 2.0f;
    private float delta = Mathf.PI / 16.0f;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        t = t0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float baseValue = 1.0f - pulseAmplitude;
        float divisor = 1.0f / pulseAmplitude;

        float factor = baseValue + Mathf.Sin(t) / divisor;
        transform.localScale = originalScale * factor;
        t += delta * Time.fixedDeltaTime * pulseSpeed;
    }
}
