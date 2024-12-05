using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _timerInterval = 1f;
}
