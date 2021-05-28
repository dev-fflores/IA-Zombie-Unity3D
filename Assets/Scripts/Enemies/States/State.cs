using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class State : MonoBehaviour{

    protected StateMachine _stateMachine;

    public void Awake() {

        _stateMachine = this.GetComponent<StateMachine>();

    }

    public virtual void onEnable() {

    }

    public virtual void CheckExit() {



    }
}
