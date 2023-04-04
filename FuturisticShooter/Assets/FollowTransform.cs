using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private Transform _parentTransform;

    [SerializeField]
    private Vector3 _offset = Vector3.zero;

    private void Awake() {
        if (!_parentTransform) _parentTransform = _targetTransform;
    }

    private void Update() {
        transform.position = _parentTransform.TransformPoint(_targetTransform.localPosition + _offset);
    }
}
