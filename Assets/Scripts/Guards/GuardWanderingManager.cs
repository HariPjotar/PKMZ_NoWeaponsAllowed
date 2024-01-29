using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardWanderingManager : MonoBehaviour
{
    [SerializeField] private GuardPOIManager _poiManager;

    [SerializeField] public GuardStates CurrentGuardState;

    [SerializeField, Tooltip("The least amount of time that a guard spends standing in a spot.")] private float _minimumWaitTime;
    [SerializeField, Tooltip("The most amount of time that a guard spends standing in a spot.")] private float _maximumWaitTime;
    [SerializeField] private float _nextWanderTime;

    private Vector3 _currentPOIPoistion;

    private NavMeshAgent _agent;

    [SerializeField] private float _stopDistance;

    private bool _setNewWanderTime;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        SetNewDestination(_poiManager.SelectRandomPOI());
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, _currentPOIPoistion) < _stopDistance)
        {
            ManagerWanderTimer();
        }
    }

    private void ManagerWanderTimer()
    {
        if (!_setNewWanderTime)
        {
            _nextWanderTime = Time.time + Random.Range(_minimumWaitTime, _maximumWaitTime);
            CurrentGuardState = GuardStates.WAITING;
            _setNewWanderTime = true;
        }

        if (Time.time > _nextWanderTime)
        {
            SetNewDestination(_poiManager.SelectNewRandomPOI());
            _setNewWanderTime = false;
        }
    }

    private void SetNewDestination(GameObject point)
    {
        if (_agent != null) 
        {
            _agent.SetDestination(point.transform.position);
            _currentPOIPoistion = point.transform.position;

            CurrentGuardState = GuardStates.WANDERING;
        }
    }
}
