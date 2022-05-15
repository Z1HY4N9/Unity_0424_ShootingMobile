using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;   // �ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ API)
using System.Linq;                  // �ޥ� �t�ζ��X�@�� (��Ƶ��c�AList, ArrayList...)

namespace Z1HY4N9 
{
	/// <summary>
	/// �C���޲z��
	/// �P�_�p�G�O�s�u�i�J�����a
	/// �N�ͦ����⪫��(�Ԥh)
	/// </summary>

	public class GameManager : MonoBehaviourPun
	{
		[SerializeField, Header("���⪫��")]
		private GameObject goCharacter;
		[SerializeField, Header("�ͦ��y�Ъ���")]
		private Transform[] traSpawnPoint;

		/// <summary>
		/// �x�s�ͦs�y�вM��
		/// </summary>
		[SerializeField]
		private List<Transform> traSpawnPointList;

		private void Awake()
		{
			traSpawnPointList = new List<Transform>();   // �s�W �M�檫��
			traSpawnPointList = traSpawnPoint.ToList();  // �}�C�ର�M���Ƶ��c
			
			
			// �p�G�O�s�u�i�J�����a�N�b���A���ͦ����⪫��
			//if (photonView.IsMine)
			//{
				int indexRadom = Random.Range(0, traSpawnPointList.Count);   // ���o�H���M��(0,�M�檺����)
				Transform tra = traSpawnPointList[indexRadom];               // ���o�H���y��
				// �z�LPhoton���A��.�ͦ�(����, �y��, ����)
				PhotonNetwork.Instantiate(goCharacter.name, tra.position, tra.rotation);

				traSpawnPointList.RemoveAt(indexRadom);  // �R���w�g���o�L���ͦ��y�и��
			//}

		}


	}
}

