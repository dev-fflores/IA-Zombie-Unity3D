using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public Vector3 _walkPoint;
    bool _walkPointSet;
    public float _walkPointRange;

    public State _nextState;
    public NavMeshAgent _agent;
    public Transform _player;
    public LayerMask _whatIsGround, _whatIsPlayer;
    private Animator _animator;

    public float _sightRange;
    public bool _playerInSightRange;
    bool _isPatroling;

    private void OnEnable() {

        base.onEnable();

        _isPatroling = true;
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

        _animator.SetBool("detectedPlayer", false);
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);

        if (!_playerInSightRange) {
            patroling();
            _isPatroling = true;
        } else {
            _isPatroling = false;
        }
    }

    private void patroling() {

        if (!_walkPointSet) {
            searchWalkPoint();
        }

        if (_walkPointSet) {
            _agent.SetDestination(_walkPoint);
        }

        Vector3 _distanceToWalkPoint = transform.position - _walkPoint;

        if (_distanceToWalkPoint.magnitude < 1f) {
            _walkPointSet = false;
        }

    }

    private void searchWalkPoint() {

        float _randomX = Random.Range(-_walkPointRange, _walkPointRange);
        float _randomZ = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + _randomX, transform.position.y, transform.position.z + _randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;

    }

    /*private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            _detectedPlayer = true;
            //Debug.Log("Entrando");
        }
    }*/

    public override void CheckExit() {

        if (!_isPatroling) {

            _stateMachine.ChangeState(_nextState);

        }

    }
}
