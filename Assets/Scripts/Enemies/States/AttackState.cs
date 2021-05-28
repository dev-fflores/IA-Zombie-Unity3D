using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    Animator _animator;
    public NavMeshAgent _agent;
    public Transform _player;
    public LayerMask _whatIsGround, _whatIsPlayer;
    public CharacterController _playerController;

    public State _followState;
    
    public float _timeBetweenAttacks;
    bool _alreadyAttacked;
    bool _isAttacking;

    public float _sightRange, _attackRange;
    public bool _playerInSightRange, _playerInAttackRange;

    private void OnEnable() {

        base.onEnable();

        _timeBetweenAttacks = 2f;
        _isAttacking = true;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        //_playerController = GetComponent<CharacterController>();

    }

    void Update() {

        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);

        if (_playerInSightRange && !_playerInAttackRange) {
            _isAttacking = false;
        }

        if (_playerInAttackRange && _playerInSightRange) {
            attackPlayer();
            _animator.SetBool("detectedPlayer", true);
            _animator.SetBool("isAttacking", true);
            _isAttacking = true;
        }

    }

    private void attackPlayer() {

        _agent.SetDestination(transform.position);
        transform.LookAt(_player);

        if (!_alreadyAttacked) {

            ///Attack code here
            _playerController.takeDamage(10f);
            ///End of attack code

            _alreadyAttacked = true;
            Invoke(nameof(resetAttack), _timeBetweenAttacks);
        }

    }

    private void resetAttack() {
        _alreadyAttacked = false;
    }

    public override void CheckExit() {

        if (!_isAttacking) {
            _stateMachine.ChangeState(_followState);
        }

    }
}
