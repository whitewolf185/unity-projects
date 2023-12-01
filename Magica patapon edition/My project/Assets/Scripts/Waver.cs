using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Windows;

public class Waver : MonoBehaviour
{
    [SerializeField] private BeatController _beatController;
    [SerializeField] private float _wavingAmplitude;

    private Stopwatch _timeElapsed;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        _timeElapsed = Stopwatch.StartNew();
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var angleFrequancy = (_beatController.bpm / 60f)  * Mathf.PI;
        transform.rotation = initialRotation * Quaternion.Euler(new Vector3(0f, 0f, _wavingAmplitude * Mathf.Sin(angleFrequancy * (float)_timeElapsed.Elapsed.TotalSeconds))); 
    }
}
