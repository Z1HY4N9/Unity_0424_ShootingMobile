using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;
using System.Collections;
using System.Runtime.ExceptionServices;

namespace Z1HY4N9
{
    /// <summary>
    /// ���˺޲z
    /// </summary>
    
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("��q"), Range(0, 1000)]
        private float hp = 200;
        [SerializeField, Header("�����S��")]
        private GameObject goVFXHit;
        [SerializeField, Header("���ѵۦ⾹")]
        private Shader shaderDissolve;

        private float hpMax;

        private string nameBullet = "�l�u";

        // �ҫ��Ҧ������V����A�̭��]�t����y
        private SkinnedMeshRenderer[] smr;

        [HideInInspector]
        public Image imgHp;
        [HideInInspector]
        public TextMeshProUGUI textHp;

        private Material materialDissolve;
        private SystemControl systemControl;
        private SystemAttack systemAttack;


        private void Awake()
        {
            systemControl = GetComponent<SystemControl>();
            systemAttack = GetComponent<SystemAttack>();

            hpMax = hp;
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();  // ���o�l����̪�����
            materialDissolve = new Material(shaderDissolve);       // �s�W ���ѵۦ⾹ ����y
            for (int i = 0; i < smr.Length; i++)                   // �Q�ΰj�׽ᤩ�Ҧ��l���� ���ѧ���y
            {
                smr[i].material = materialDissolve;
            }

            if (photonView.IsMine) textHp.text = hp.ToString();
            
            
        }

        // �i�J (�i�Ѧ����q)
        private void OnCollisionEnter(Collision collision)
        {
            if (!photonView.IsMine) return;
            
            // �p�G �I������W��
            if (collision.gameObject.name.Contains(nameBullet))
            {
                // collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                // point �I�쪫�󪺮y��
                Damage(collision.contacts[0].point);
            }
        }

        // ����
        private void OnCollisionStay(Collision collision)
        {
            
        }

        // ���}
        private void OnCollisionExit(Collision collision)
        {
            
        }

        /// <summary>
        /// ���˥\��
        /// </summary>
        /// <param name="posHit">���˸I�����y��</param>
        private void Damage(Vector3 posHit)
        {
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();

            // �s�u.�ͦ�(�S�ġA�����y�СA����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);

            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
        }

        // �ݭn�P�B����k�����K�[ PunRPC �ݩ� Remote Procedure Call ���ݵ{���I�s
        [PunRPC]
        private void Dead() 
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            systemControl.enabled = false;
            systemAttack.enabled = false;
            systemControl.traDirectionIcon.gameObject.SetActive(false);
            
            float valueDidsolve = 10;                                   // ���ѼƭȰ_�l��
             
            for (int i = 0; i < 20; i++)                               // �j����滼��           
            {
                valueDidsolve -= 0.3f;                                 // ���ѭȻ��� 0.3
                materialDissolve.SetFloat("dissolve", valueDidsolve);  // ��s�ۦ⾹�ݩ�.�`�N�n���� Reference
                yield return new WaitForSeconds(0.08f);                // ����

                ReturnToLobby();
            }
        }
        
        /// <summary>
        /// �^��j�U
        /// </summary>
        private void ReturnToLobby()
        {
            if (photonView.IsMine) 
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("�C���j�U");

            }
        }



    }
}


