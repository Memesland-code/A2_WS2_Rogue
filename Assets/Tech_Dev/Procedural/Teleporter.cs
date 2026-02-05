using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private Teleporter _destinationTeleporter;

		private void OnDrawGizmos()
		{
			if (_destinationTeleporter != null)
			{
				Gizmos.DrawLine(transform.position, _destinationTeleporter.transform.position);
			}
			else
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(transform.position, 0.75f);
			}
		}

		public Teleporter GetDestination()
		{
			return _destinationTeleporter;
		}

		public void SetDestinationTeleporter(Teleporter destinationTeleporter)
		{
			_destinationTeleporter = destinationTeleporter;
		}
	}
}