using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WizardMove : MonoBehaviour
{
    // movement
    [SerializeField] private float _acceleration;
    [SerializeField] private float _secondsToMove;
    [SerializeField] private Animator _animator;

    private Stopwatch _timeMove;
    private bool isMooving = false;
    private float currSpeed = 0;
    private float currAcceleration = 0;
    private bool isAttacking = false;
    private AnimatorStateInfo prevStateInfo;

    private void Start()
    {
        isMooving = false;
        currSpeed = 0;
        currAcceleration = 0;
        isAttacking = false;
        prevStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMooving)
        {
            _animator.SetBool("isMoving", true);
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
        } else
        {
            _animator.SetBool("isMoving", false);
        }

        if (isAttacking)
        {
            
            _animator.SetBool("isAttacking", true);
            
            if (prevStateInfo.IsName("Attak_Sword") && _animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE")) {
                _animator.SetBool("isAttacking", false);
                isAttacking = false;
            }

            prevStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }
    }

    public void StartMove()
    {
        // если мы уже двигаемся, то не начинаем движение заново
        if (isMooving) { return; }

        currAcceleration = _acceleration;
        isMooving = true;
        _timeMove = Stopwatch.StartNew();
    }

    public void StartAttack()
    {
        // if we are already attacking, than do not attack twice
        if (isAttacking) { return; }
        isAttacking = true;
    }
}
