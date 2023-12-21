using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BumboxMove : MonoBehaviour
{
    // movement
    [SerializeField] private float _acceleration;
    [SerializeField] private float _secondsToMove;

    private Stopwatch _timeMove;
    private bool isMooving = false;
    private float currSpeed = 0;
    private float currAcceleration = 0;

    private void Start()
    {
        currSpeed = 0;
        currAcceleration = 0;
        isMooving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMooving)
        {
            var secondsIsMoving = _timeMove.ElapsedMilliseconds / 1000;
            // move acceleration to negative -- stoppng
            if (secondsIsMoving >= _secondsToMove / 2 && currAcceleration > 0)
            {
                currAcceleration *= -1;
            }
            currSpeed = Mathf.Max(currSpeed + currAcceleration * Time.deltaTime, 0);
            transform.localPosition += new Vector3(currSpeed * Time.deltaTime, 0f, 0f);
            if (currSpeed <= 0 || secondsIsMoving >= _secondsToMove)
            {
                isMooving = false;
                _timeMove.Reset();
            }
        }
    }

    public void StartMove()
    {
        // если мы уже двигаемся, то не начинаем движение заново
        if (isMooving) {return;}

        currAcceleration = _acceleration;
        isMooving = true;
        _timeMove = Stopwatch.StartNew();
    }
}
