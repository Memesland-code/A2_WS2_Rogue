using System;
using UnityEngine;

namespace Tech_Dev.GameplayElement
{
    public class BalanceLimits : MonoBehaviour
    {
        [SerializeField] private float _rotationLimit;
        
        void Update()
        {
            if (transform.rotation.eulerAngles.z < 360 - _rotationLimit && transform.rotation.eulerAngles.z > 180)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 360 - _rotationLimit);
            }
            else if (transform.rotation.eulerAngles.z > _rotationLimit && transform.rotation.eulerAngles.z < 180)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _rotationLimit);
            }
        }
    }
}
