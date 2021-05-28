using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DieState : State
{
    bool _isDead;

    private EnemyController _enemy;
    private Animator _animator;
    public NavMeshAgent _agent;
    public FollowState _followState;
    public PatrolState _patrolState;
    public AttackState _attackState;

    private void OnEnable() {

        base.onEnable();
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _followState = GetComponent<FollowState>();
        _patrolState = GetComponent<PatrolState>();
        _attackState = GetComponent<AttackState>();
        _isDead = false;

    }

    void Update() {

        Die();

    }

    public override void CheckExit() {

        

    }

    void Die() {
        if (!_isDead) {
            if (_enemy._currentHealth <= 0) {
                _isDead = true;
                _animator.SetTrigger("isDead");
                _agent.enabled = false;
                _followState.enabled = false;
                _patrolState.enabled = false;
                _attackState.enabled = false;
                Debug.Log("Anim Die");
            }
        } 
    }
}
