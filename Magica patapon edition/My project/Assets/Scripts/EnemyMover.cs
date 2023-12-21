using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("isHiting", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleMode.battleIsOn)
        {
            _animator.SetBool("isHiting", true);
        }
    }
}
