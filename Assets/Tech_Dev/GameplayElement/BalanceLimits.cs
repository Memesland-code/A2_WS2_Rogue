using System;
using UnityEngine;

namespace Tech_Dev.GameplayElement
{
    public class BalanceLimits : MonoBehaviour
    {
        [SerializeField] private float _rotationLimit;
        private GameObject _plank;
        private Vector3 _rotation;
        
        void Update()
        {
            _plank = transform.GetChild(0).gameObject;
            _rotation = _plank.transform.rotation.eulerAngles;

            print(_rotation.z);

            if (_rotation.z < 360 - _rotationLimit && _rotation.z > 180)
            {
                _plank.transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 360 - _rotationLimit);
            }
            else if (_rotation.z > _rotationLimit && _rotation.z < 180)
            {
                _plank.transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, _rotationLimit);
            }
        }
    }
}
