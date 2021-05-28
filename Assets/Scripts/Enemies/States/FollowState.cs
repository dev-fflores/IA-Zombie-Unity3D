using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State {

    int _findplayer;

    public State _attackState;
    public State _idleState;
    Animator _animator;
    public NavMeshAgent _agent;
    public Transform _player;
    public LayerMask _whatIsGround, _whatIsPlayer;

    public float _sightRange, _attackRange;
    public bool _playerInSightRange, _playerInAttackRange;

    private void OnEnable() {

        base.onEnable();

        _findplayer = 2;
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();

    }

    void Update() {

        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);

        if (!_playerInSightRange && !_playerInAttackRange) {
            _animator.SetBool("isAttacking", false);
            _animator.SetBool("detectedPlayer", false);
            _findplayer = 0;
        }
        if (_playerInSightRange && !_playerInAttackRange) {
            _animator.SetBool("detectedPlayer", true);
            _animator.SetBool("isAttacking", false);
            chasePlayer();
            _findplayer = 2;
        }
        if (_playerInAttackRange && _playerInSightRange) {
            _findplayer = 1;
        }

    }

    private void chasePlayer() {
        _agent.SetDestination(_player.position);
        
    }


    public override void CheckExit() {

        if (_findplayer == 0) {

            _stateMachine.ChangeState(_idleState);

        } else if (_findplayer == 1) {

            _stateMachine.ChangeState(_attackState);

        } 

    }
}