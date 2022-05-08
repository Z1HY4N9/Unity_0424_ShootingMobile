using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;		// �ޥ�Photon Pun API
using Photon.Realtime;  // �ޥ�Photon �ή�API

namespace Z1HY4N9
{


	/// <summary>
	/// �j�U�޲z��
	/// ���a���U��ԫ���s�}�l�ǰt�ж�
	/// </summary>
	// MonoBehaviourPunCallbacks �s�u�\��^�I���O
	// �Ҧp : �n�J�j�U��^�G�A���w���{��

	public class LobbyManager : MonoBehaviourPunCallbacks
	{
		// GameObject �C������:�s��Unity�����Ҧ�����
		// SerializeField �N�p�H�����ܦb�ݩʭ��O�W
		// Header ���D�A�b�ݩʭ��O�W��ܲ���r���D
		[SerializeField, Header("�s�u���e��")]
		private GameObject goConnectView;
		[SerializeField, Header("��ԫ��s")]
		private Button btnBattle;
		[SerializeField, Header("�s�u�H��")]
		private Text textCountPlayer;

		// ����ƥ� : ����C���ɰ���@���A��l�Ƴ]�w
		private void Awake()
		{
			// Photon �s�u���s�u�ϥγ]�w
			PhotonNetwork.ConnectUsingSettings();
		}

		// override ���\�мg�~�Ӫ������O����
		// �s�u�ܱ���x�A�bConnectUsingSettings�����|�۰ʳs�u
		public override void OnConnectedToMaster()
		{
			base.OnConnectedToMaster();
			print("<color=yellow>1.�w�i�J����x</color>");

			// Photon �s�u.�[�J�j�U
			PhotonNetwork.JoinLobby();
		}

		// �m����j�U���\��|���榹��k
		public override void OnJoinedLobby()
		{
			base.OnJoinedLobby();
			print("<color=yellow>2.�w�i�J�j�U</color>");

			//��ԫ��s.���� = �Ұ�
			btnBattle.interactable = true;
		}



		// ����
		// �����s��{�����q���y�{
		// 1. ���Ѥ��}����k Public Method
		// 2. ���s�b�I�� On Click ��]�w�I�s����k

		//�}�l�s�u���
		public void StartConnect()
		{
			print("3.�}�l�s�u...");

			//�C������,�Ұʳ]�w(���L��) - true ��� false ����
			goConnectView.SetActive(true);

			//Photon �s�u �� �[�J�H���ж�
			PhotonNetwork.JoinRandomRoom();
		}

		// �[�J�H���ж�����
		// 1. �s�u�~��t�ɭP����
		// 2. �٨S���ж�
		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			base.OnJoinRandomFailed(returnCode, message);
			print("<color=red>4.�[�J�H���ж�����</color>");

			RoomOptions ro = new RoomOptions();  // �s�W�ж��]�w����
			ro.MaxPlayers = 5;                   // ���w�ж��̤j�H��
			PhotonNetwork.CreateRoom("", ro);     // �إߩж��õ����ж����� ""�����ж��W

		}

		//�[�J�ж�
		public override void OnJoinedRoom()
		{
			base.OnJoinedRoom();
			print("<color=yellow>5.�}�Ъ̶i�J�ж�</color>");
			int currentCount = PhotonNetwork.CurrentRoom.PlayerCount; // ��e�ж��H��
			int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;      // ��e�ж��̤j�H��

			textCountPlayer.text = "�s�u�H��" + currentCount + " / " + maxCount;
		}

		//��L���a�i�J�ж�
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			base.OnPlayerEnteredRoom(newPlayer);
			print("<color=yellow>6.���a�i�J�ж�</color>");
			int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
			int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

			textCountPlayer.text = "�s�u�H��" + currentCount + " / " + maxCount;

		}

	}
}
