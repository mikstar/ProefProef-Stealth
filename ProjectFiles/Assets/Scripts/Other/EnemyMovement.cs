using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
   
    //Public
    public Transform[] Waypoints;
    public float movementSpeed;
    public float turnSpeed;
    public float waypointPauze;

    public float _alarmTimer = 2f;
    public float _cooldownTimer = 3f;
    public float _waypointTimer = 4f;

    //Private
    private int _currentPoint;



    

    private bool _doPatrol = true;
    private bool _alarmed = false;

    private Vector3 _target;
    private Vector3 _movedir;
    private Vector3 _velocity;

    private Rigidbody _rigidbody;

    private EnemySight _enemySight;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _enemySight = GetComponent<EnemySight>();

    }

    void Update() {
        if (_enemySight.playerInSight)
        {
            _doPatrol = false;
            _alarmed = true;
            
            if (_alarmed)
            {
                _cooldownTimer = 3f;
                _velocity = Vector3.zero;
                _movedir = _enemySight.personalLastSighting - transform.position;
                _alarmTimer -= Time.deltaTime;

                if (_alarmTimer <= 0)
                {
                    Debug.Log("Timer klaar");
                    _velocity = _movedir.normalized * (movementSpeed*2);
                }
            }
        }
        else
        {
            _velocity = Vector3.zero;

            if (_alarmed)
            {
                _alarmTimer = 2f;
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer <= 0)
                {
                    _doPatrol = true;
                }
            }
        }
        

        if (_doPatrol){

            if (_currentPoint < Waypoints.Length){
                _target = Waypoints[_currentPoint].position;
                _movedir = _target - transform.position;
                _velocity = _rigidbody.velocity;

                if (_movedir.magnitude < 1){
                    if (_currentPoint == waypointPauze)
                    {
                        _velocity = Vector3.zero;
                        _waypointTimer -= Time.deltaTime;

                        if (_waypointTimer <= 0)
                        {
                            _currentPoint++;
                            _velocity = _movedir.normalized * movementSpeed;
                            _waypointTimer = 4f;
                        }
                    }
                    else {
                        _currentPoint++;
                    }
                }
                else {
                    _velocity = _movedir.normalized * movementSpeed;
                }
            }
            else {
                _currentPoint = 0;     
            }
        }
        else {
           //_velocity = Vector3.zero;
        }

        Quaternion muhrotation = Quaternion.LookRotation(_movedir);
        Quaternion currentRot = transform.localRotation;

        transform.localRotation = Quaternion.Lerp(currentRot, muhrotation, Time.deltaTime * turnSpeed);
     
        _rigidbody.velocity = _velocity;
    }
}
