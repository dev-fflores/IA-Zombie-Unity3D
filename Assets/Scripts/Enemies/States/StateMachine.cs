using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour{

    public State _initialState;
    public float _checkExitRatio = 0.25f;

    public State _currentState;

    private void Awake() {

        _currentState = _initialState;
        _currentState.enabled = true;
        InvokeRepeating("Check", 0, _checkExitRatio);

    }
    private void OnDestroy() {

        CancelInvoke("Check");

    }
    void Check() {

        _currentState.CheckExit();

    }
    public void ChangeState(State nextState) {

        _currentState.enabled = false;
        _currentState = nextState;
        _currentState.enabled = true;

    }
}
