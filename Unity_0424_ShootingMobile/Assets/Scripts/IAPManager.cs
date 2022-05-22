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
			textIAPTip.text = product.ToString() + " �ʶR���\ " ;
		}

		// �ʶR����
		private void PurchaseFailedSkinRed(Product product, PurchaseFailureReason reason) 
		{
			textIAPTip.text = product + " �ʶR���� " + reason;
		}







	}

}



