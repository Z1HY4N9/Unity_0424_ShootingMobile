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
		[SerializeField, Header("�����V�ϥ�")]
		private Transform traDirectionIcon;
		[SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
		private float rangeDirectionIcon = 2.5f;
		[SerializeField, Header("�������t��"), Range(0, 100)]
		private float speedTurn = 1.5f;


		private Rigidbody rig;

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();	
		}

		private void Update()
		{
			//GetJoystickValue();	
			UpdateDirectionIconPos();
			LookDirecrionIcon();
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


		//��s�����V�ϥܪ��y��
		private void UpdateDirectionIconPos()
        {
			// �s�y�� = ���⪺�y�� + �T���V�q(�����n�쪺�����P����) * ��V�ϥܪ��d��
			Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.5f, joystick.Vertical) * rangeDirectionIcon;
			// ��s��V�ϥܪ��y�� = �s�y��
			traDirectionIcon.position = pos;
        }

		private void LookDirecrionIcon()
        {
			// ���o���V���� = �|�줸.���V����(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
			Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
			// ���⪺���� = �|�줸.����(���⪺���� , ���V����.����t�� * �@�V���ɶ�)
			transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
			// ���⪺�کԨ��� = �T���V�q(0, �쥻��Y�کԨ���, 0)
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		}

	}
}
