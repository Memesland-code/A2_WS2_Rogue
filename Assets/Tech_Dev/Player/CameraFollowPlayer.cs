using UnityEngine;

namespace Tech_Dev.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _smoothSpeed;

        private void LateUpdate()
        {
            //? Check if possible to implement smoothed camera follow without lag effect
            //Vector3 desiredPosition = _playerTransform.position + _cameraOffset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            //transform.position = smoothedPosition;
            transform.position = _playerTransform.position + _cameraOffset;
        }
    }
}
