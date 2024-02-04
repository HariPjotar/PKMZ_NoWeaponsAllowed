using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class GuardWanderingManager : MonoBehaviour
{
    public event Action OnKnockOut;

    [SerializeField] private GuardPOIManager _poiManager;

    [SerializeField] public GuardStates CurrentGuardState;

    [SerializeField, Tooltip("The least amount of time that a guard spends standing in a spot.")] private float _minimumWaitTime;
    [SerializeField, Tooltip("The most amount of time that a guard spends standing in a spot.")] private float _maximumWaitTime;
    [SerializeField] private float _nextWanderTime;

    [SerializeField] private float _investigationTime;
    private float _stopInvestigatingTime;

    [SerializeField] private float _stopDistance;

    [SerializeField] private bool _hasAggro;

    private Vector3 _currentPOIPoistion;

    private NavMeshAgent _agent;

    private bool _setNewWanderTime;

    private Transform _playerTransform;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        SetNewDestination(_poiManager.SelectRandomPOI());

        _playerTransform = CustomGameManager.Instance.Player.transform;
    }

    void Update()
    {
        if (CurrentGuardState == GuardStates.KNOCKED_OUT)
            return;

        if (_hasAggro)
        {
            SetNewDestination(_playerTransform.gameObject, false);
        }

        if(CurrentGuardState == GuardStates.INVESTIGATING)
        {
            if(Time.time > _stopInvestigatingTime)
            {
                SetNewDestination(_poiManager.SelectNewRandomPOI());
                _setNewWanderTime = false;

                return;
            }
        }

        if(Vector3.Distance(transform.position, _currentPOIPoistion) < _stopDistance)
        {
            ManagerWanderTimer();
        }
    }

    private void ManagerWanderTimer()
    {
        if (!_setNewWanderTime)
        {
            _nextWanderTime = Time.time + UnityEngine.Random.Range(_minimumWaitTime, _maximumWaitTime);
            CurrentGuardState = GuardStates.WAITING;
            _setNewWanderTime = true;
        }

        if (Time.time > _nextWanderTime)
        {
            SetNewDestination(_poiManager.SelectNewRandomPOI());
            _setNewWanderTime = false;
        }
    }

    public void SetNewDestination(GameObject point, bool lostAggro = false)
    {
        if (_agent != null) 
        {
            _agent.SetDestination(point.transform.position);
            _currentPOIPoistion = point.transform.position;

            if (point.tag.Equals("Player"))
            {
                CurrentGuardState = GuardStates.AGGROED;
                _hasAggro = true;

                if (lostAggro)
                {
                    CurrentGuardState = GuardStates.INVESTIGATING;
                    _stopInvestigatingTime = Time.time + _investigationTime;
                    _hasAggro = false;

                    return;
                }

                return;
            }

            CurrentGuardState = GuardStates.WANDERING;
        }
    }

    public void OnKnockOutTrigger()
    {
        CurrentGuardState = GuardStates.KNOCKED_OUT;

        _agent.enabled = false;

        OnKnockOut?.Invoke();

        GetComponent<GuardPOIManager>().enabled = false;
        GetComponent<GuardLookAroundScript>().enabled = false;
        GetComponent<GuardEmotions>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        this.enabled = false;
    }
}
