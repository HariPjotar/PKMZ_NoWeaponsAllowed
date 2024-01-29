using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPOIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pointsOfInterest;
    [SerializeField] private List<GameObject> _tempListOfPoints;

    public GameObject CurrentPOI;

    public GameObject SelectRandomPOI()
    {
        CurrentPOI = _pointsOfInterest[Random.Range(0, _pointsOfInterest.Count)];
        return CurrentPOI;
    }

    public GameObject SelectNewRandomPOI()
    {
        _tempListOfPoints.Clear();

        foreach (GameObject current in _pointsOfInterest)
        {
            if(current != CurrentPOI)
            {
                _tempListOfPoints.Add(current);
            }
        }

        CurrentPOI = _tempListOfPoints[Random.Range(0, _tempListOfPoints.Count)];
        return CurrentPOI;
    }
}
