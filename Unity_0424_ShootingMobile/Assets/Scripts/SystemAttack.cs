using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Z1HY4N9
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviourPun
    {
        [HideInInspector]
        public Button btnFire;

        [SerializeField, Header("�l�u")]
        private GameObject goBullet;
        [SerializeField, Header("�l�u�̤j�ƶq")]
        private int bulletCountMax = 3;
        [SerializeField, Header("�l�u�ͦ���m")]
        private Transform traFire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0, 3000)]
        private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            // �p�G�O ���������a���� �N����o�g
            if (photonView.IsMine)
            {
                // �o�g���s.�I��.�K�[��ť��(�}�j��k) - ���U�o�g���s����}�j��k
                btnFire.onClick.AddListener(Fire);

            }
            
            
           
        }


        //�}�j
        private void Fire()
        {
            // �Ȧs�l�u = �s�u.�ͦ�(����,�y��,����)
            GameObject tempBullet =  PhotonNetwork.Instantiate(goBullet.name, traFire.position, Quaternion.identity);
            //
            tempBullet.GetComponent<Rigidbody>().AddForce(transform.forward * speedFire);
        
        }


    }
}