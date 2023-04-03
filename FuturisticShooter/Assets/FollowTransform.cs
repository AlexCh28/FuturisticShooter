using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private Vector3 _offset = Vector3.zero;

    private void Update() {
        transform.position = _targetTransform.TransformPoint(_targetTransform.localPosition + _offset);
    }
}
