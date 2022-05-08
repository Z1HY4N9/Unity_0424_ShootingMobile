using Photon.Pun;
using UnityEngine;

// namespace �R�W�Ŷ� : �{���϶� �i�H�Ϲj�ۦP�W�r���{��
namespace Z1HY4N9
{
	/// <summary>
	///  ����t�� : ��ð����ʥ\��
	///  �����n�챱��Ⲿ��
	/// </summary>

	public class SystemControl : MonoBehaviour
	{
		[SerializeField, Header("�����n��")]
		private Joystick joystick;
		[SerializeField, Header("���ʳt��"), Range(0, 300)]
		private float speed = 7f;

		private Rigidbody rig;

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();	
		}

		private void Update()
		{
			//GetJoystickValue();	
		}

		private void FixedUpdate()
		{
			Move();
		}

		//���o�����n��ȨæR�X��Console
		private void GetJoystickValue()
		{
			print("<color=yellow>���� : " + joystick.Horizontal + "</color>");
		}

		//���ʥ\��
		private void Move()
		{
			// ����.�[�t�� = �T���V�q(X.Y.Z)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
		}	
	}
}
