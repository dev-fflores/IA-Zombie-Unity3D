using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State{

    bool _detectedPlayer;

    public State _nextState;

    private Animator _animator;

    private void OnEnable() {

        base.onEnable();
        _detectedPlayer = false;
        _animator = GetComponent<Animator>();
    }

    void Update(){

        _animator.SetBool("detectedPlayer", false);

    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            _detectedPlayer = true;
            //Debug.Log("Entrando");
        }
    }

    public override void CheckExit() {

        if (_detectedPlayer) {

            _stateMachine.ChangeState(_nextState);

        }

    }
}
