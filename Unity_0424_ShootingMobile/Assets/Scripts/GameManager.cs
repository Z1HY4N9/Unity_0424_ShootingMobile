using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;   // �ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ API)
using System.Linq;                  // �ޥ� �t�ζ��X�@�� (��Ƶ��c�AList, ArrayList...)
using Photon.Realtime;

namespace Z1HY4N9 
{
	/// <summary>
	/// �C���޲z��
	/// �P�_�p�G�O�s�u�i�J�����a
	/// �N�ͦ����⪫��(�Ԥh)
	/// </summary>

	public class GameManager : MonoBehaviourPunCallbacks
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
			// ���a�w�g�[�J�ж�����...

			// Photon �s�u.��e�ж�.�i���� = �_ (��L���a�ݤ��즹�ж��A����[�J)
			PhotonNetwork.CurrentRoom.IsVisible = false;

			#region �H���ͦ�
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
			#endregion

		}

		// �����a���}�ж��|����@��
		public override void OnPlayerLeftRoom(Player otherPlayer)
		{
			base.OnPlayerLeftRoom(otherPlayer);

			// �p�G ��e�ж����a�H�� �ѤU�@�H �N�Y��
			if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
				Win();
		}

		private void Win() 
		{
			print("�ӧQ");
		}

	}
}

