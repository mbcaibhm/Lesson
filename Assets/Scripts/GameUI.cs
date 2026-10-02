using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button ConnectButton;
    public Button balanceButton;
    public Button buyButton;
    public Button sellButton;
    public Button rewardButton;

    private BlockchainManager1 bm;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bm = FindAnyObjectByType<BlockchainManager1>();
        ConnectButton.onClick.AddListener(() => bm.JS_ConnectWallet());
        balanceButton.onClick.AddListener(() => bm.JS_GetBalance());
        buyButton.onClick.AddListener(() => bm.JS_BuyItem(100));
        sellButton.onClick.AddListener(() => bm.JS_SellItem(50));
        rewardButton.onClick.AddListener(() => bm.JS_GiveReward(500));
        
    }


    public void OnClickTest()
    {
        //print("버튼 테스트");
        //bm.JS_ConnectWallet();
    }

    void BuyItem(int value)
    {
        bm.JS_BuyItem(value);
    }
}
