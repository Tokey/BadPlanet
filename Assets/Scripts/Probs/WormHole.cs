using UnityEngine;
using System.Collections;

public class WormHole : MonoBehaviour {

	public Transform endPoint;

	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		Player _player = _colInfo.collider.GetComponent<Player>();
		Enemy _enemy = _colInfo.collider.GetComponent<Enemy>();
		if (_player != null)
		{

			Debug.Log("Wormhole found");
			_player.transform.position = endPoint.position;


		}

		if (_enemy != null)
		{

			Debug.Log("Wormhole found");
			_enemy.transform.position = endPoint.position;

		}
	}
}
