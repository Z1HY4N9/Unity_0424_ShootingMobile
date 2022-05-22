using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Z1HY4N9
{
	public class IAPManager : MonoBehaviour
	{
		[SerializeField, Header("�ʶR�ֽ����s")]
		private IAPButton iapBuySkingRed;
		[SerializeField, Header("�ʶR���ܰT��")]
		private Text textIAPTip;


		private bool hasSkinRed;


		private void Awake()
		{
			// ����ֽ����ʫ��s �ʶR���\�� �K�[��ť�� (�ʶR���\��k)
			iapBuySkingRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
			// ����ֽ����ʫ��s �ʶR���ѫ� �K�[��ť�� (�ʶR���Ѥ�k)
			iapBuySkingRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);
		}



		// �ʶR���\
		private void PurchaseCompleteSkinRed(Product product)
		{
			textIAPTip.text = " ����ֽ��ʶR���\! " ;

			// �B�z�ʶR���\�᪺�欰
			hasSkinRed = true;
			
			// ����T���I�s���ä��ʴ��ܰT��
			// ����I�s(��k�W��, ����ɶ�)
			Invoke("HiddenIAPTip", 3);
		
		}

		// �ʶR����
		private void PurchaseFailedSkinRed(Product product, PurchaseFailureReason reason) 
		{
			textIAPTip.text = " ����ֽ��ʶR���ѡA��] : " + reason;

			Invoke("HiddenIAPTip", 3);
		}

		// ���ä��ʴ��ܰT��
		private void HiddenIAPTip()
        {
			textIAPTip.text = "";
        }





	}

}



