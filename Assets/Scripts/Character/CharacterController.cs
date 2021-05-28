using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float _health = 200f;
    public float _moveSpeed;
    public float _rotateSpeed;
    public float _damage;
    public float _attackRange = 0.8f;

    public float _attackRate = 1f; //seconds
    float _nextAttackTime = 0f;

    private Animator _anim;
    public Transform _attackPoint;
    public LayerMask _enemyLayers;

    public float _x, _y;
    // Start is called before the first frame update
    void Start()
    {
        _damage = 20.0f;
        _moveSpeed = 5.0f;
        _rotateSpeed = 200.0f;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");

        if (Time.time >= _nextAttackTime) {

            if (Input.GetMouseButtonDown(0)) {
                Attack();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }

        }
        

        transform.Rotate(0, _x * Time.deltaTime * _rotateSpeed, 0);
        transform.Translate(0, 0, _y * Time.deltaTime * _moveSpeed);

        _anim.SetFloat("SpdX", _x);
        _anim.SetFloat("SpdY", _y);
    }

    void Attack() {
        _anim.SetTrigger("Attack");

        Collider[] _hitEnemies =  Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider enemy in _hitEnemies) {
            enemy.GetComponent<EnemyController>().TakeDamage(_damage);
        }
    }

    public void takeDamage(float _damage) {
        _health -= _damage;
    }

    private void OnDrawGizmosSelected() {

        if (_attackPoint == null) {
            Debug.Log("No hit");
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
