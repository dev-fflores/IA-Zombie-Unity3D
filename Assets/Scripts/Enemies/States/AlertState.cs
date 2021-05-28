using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State
{
    bool _detectedPlayer;

    public State _nextState;

    private Animator _animator;

    private void OnEnable() {

        base.onEnable();
        _detectedPlayer = true;
        _animator = GetComponent<Animator>();

    }

    void Update() {

        _animator.SetBool("detectedPlayer", true);        

    }
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            _detectedPlayer = false;
            //Debug.Log("Saliendo");
        }
    }

    public override void CheckExit() {

        if (!_detectedPlayer) {

            _stateMachine.ChangeState(_nextState);

        }

    }
}
