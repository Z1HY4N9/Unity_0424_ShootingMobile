using Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// namespace �R�W�Ŷ� : �{���϶� �i�H�Ϲj�ۦP�W�r���{��
namespace Z1HY4N9
{
	/// <summary>
	///  ����t�� : ��ð����ʥ\��
	///  �����n�챱��Ⲿ��
	/// </summary>

	public class SystemControl : MonoBehaviourPun
	{
		[SerializeField, Header("���ʳt��"), Range(0, 300)]
		private float speed = 7f;
		[SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
		private float rangeDirectionIcon = 2.5f;
		[SerializeField, Header("�������t��"), Range(0, 100)]
		private float speedTurn = 1.5f;
		[SerializeField, Header("�ʵe�Ѽƶ]�B")]
		private string parameterwalk = "�}���]�B";
		[SerializeField, Header("�e��")]
		private GameObject goCanvas;
		[SerializeField, Header("�e�����a��T")]
		private GameObject goCanvasPlayerInfo;
		[SerializeField, Header("�����V�ϥ�")]
		private GameObject goDirection;


		private Rigidbody rig;
		private Animator ani;
		private Joystick joystick;
		private Transform traDirectionIcon;
		private CinemachineVirtualCamera cvc;
		private SystemAttack systemAttack;
		private DamageManager damageManager;
		

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();
			ani = GetComponent<Animator>();
			systemAttack = GetComponent<SystemAttack>();
			damageManager = GetComponent<DamageManager>();

			

			// �p�G�O�s�u�i�J�����a �N�ͦ����a�ݭn������
			if (photonView.IsMine)
			{
				PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
				follow.traPlayer = transform;
				// ���o�����V�ϥ�
				traDirectionIcon = Instantiate(goDirection).transform;
				// ���o�e�����������n�� transform.Find(�l����W��) - �z�L�W�ٷj�M�l����
				GameObject tempCanvas = Instantiate(goCanvas);
				joystick = tempCanvas.transform.Find("Floating Joystick").GetComponent<Joystick>();
				systemAttack.btnFire = tempCanvas.transform.Find("�o�g").GetComponent<Button>();

				// ���o��v�� CM�޲z��
				cvc = GameObject.Find("CM�޲z��").GetComponent<CinemachineVirtualCamera>();
				// ���w�l�ܪ���
				cvc.Follow = transform;

				damageManager.imgHp = GameObject.Find("�Ϥ���q").GetComponent<Image>();
				damageManager.textHp = GameObject.Find("��r��q").GetComponent<TextMeshProUGUI>();
			}
			// �_�h ���O�s�u�i�J�����a �N��������t�ΡA�קK�����h�Ӫ���
			else
			{
				enabled = false;
			}

		}

		private void Update()
		{
			//GetJoystickValue();	
			UpdateDirectionIconPos();
			LookDirecrionIcon();
			UpdateAnimation();
		}

		private void FixedUpdate()
		{
			Move();
		}

		// ���o�����n��ȨæR�X��Console
		private void GetJoystickValue()
		{
			print("<color=yellow>���� : " + joystick.Horizontal + "</color>");
		}

		/// <summary>
		/// ���ʥ\��
		/// </summary>
		private void Move()
		{
			// ����.�[�t�� = �T���V�q(X.Y.Z)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
		}


		/// <summary>
		/// ��s�����V�ϥܪ��y��
		/// </summary>
		private void UpdateDirectionIconPos()
        {
			// �s�y�� = ���⪺�y�� + �T���V�q(�����n�쪺�����P����) * ��V�ϥܪ��d��
			Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.5f, joystick.Vertical) * rangeDirectionIcon;
			// ��s��V�ϥܪ��y�� = �s�y��
			traDirectionIcon.position = pos;
        }

		/// <summary>
		/// ���⭱�V��V
		/// </summary>
		private void LookDirecrionIcon()
        {
			// ���o���V���� = �|�줸.���V����(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
			Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
			// ���⪺���� = �|�줸.����(���⪺���� , ���V����.����t�� * �@�V���ɶ�)
			transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
			// ���⪺�کԨ��� = �T���V�q(0, �쥻��Y�کԨ���, 0)
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		}

		/// <summary>
		/// ��s�ʵe
		/// </summary>
		private void UpdateAnimation()
        {
			// �O�_�]�B = �����n��.���� �����s �� ���� �����s
			bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
			ani.SetBool(parameterwalk, run);
        }
	}
}
