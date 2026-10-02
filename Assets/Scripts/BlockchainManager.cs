using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.InteropServices;

public class BlockchainManager1 : MonoBehaviour
{

    [Header("컨트랙트 설정")]
    public string contractAddress = "0x...";        // 배포한 GameToken 컨트랙트 주소
    public string rpcUrl = "https://ethereum-sepolia-rpc.publicnode.com";  //여기서 빠른 RPC선택하면 됨 https://chainlist.org/chain/11155111
    public string shopPrivateKey = "0x...";         // 상점 주인 개인키 (절대 공개 금지!)
    private string contractABI = @"[
        {
            'inputs': [{'internalType': 'address', 'name': '', 'type': 'address'}],
            'name': 'balanceOf',
            'outputs': [{'internalType': 'uint256', 'name': '', 'type': 'uint256'}],
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'inputs': [{'internalType': 'uint256', 'name': '_value', 'type': 'uint256'}],
            'name': 'buyItem',
            'outputs': [{'internalType': 'bool', 'name': 'success', 'type': 'bool'}],
            'stateMutability': 'nonpayable',
            'type': 'function'
        },
        {
            'inputs': [
                {'internalType': 'address', 'name': '_to', 'type': 'address'},
                {'internalType': 'uint256', 'name': '_value', 'type': 'uint256'}
            ],
            'name': 'sellItem',
            'outputs': [{'internalType': 'bool', 'name': 'success', 'type': 'bool'}],
            'stateMutability': 'nonpayable',
            'type': 'function'
        },
        {
            'inputs': [
                {'internalType': 'address', 'name': '_to', 'type': 'address'},
                {'internalType': 'uint256', 'name': '_value', 'type': 'uint256'}
            ],
            'name': 'giveReward',
            'outputs': [{'internalType': 'bool', 'name': 'success', 'type': 'bool'}],
            'stateMutability': 'nonpayable',
            'type': 'function'
        }
    ]";

    [Header("잔액조회 테스트")]
    public TMP_Text balanceText;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void InitWeb3(string rpc,string key,string addr,string abi);

    [DllImport("__Internal")]
    private static extern void ConnetWallet();
    [DllImport("__Internal")]
    private static extern void GetBalance();
    [DllImport("__Internal")]
    private static extern void BuyItem(int amount);
    [DllImport("__Internal")]
    private static extern void SellItem(int amount);
    [DllImport("__Internal")]
    private static extern void GiveReward(int amount);
#endif




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    InitWeb3(rpcUrl, shopPrivateKey, contractAddress, contractABI);
#endif
    }

    public void JS_ConnectWallet()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        ConnectWallet();
    #endif

    }
    public void JS_GetBalance()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        GetBalance();
    #endif

    }
    public void JS_BuyItem(int amount)
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        BuyItem(amount);
    #endif

    }
    public void JS_SellItem(int amount)
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        SellItem(amount);
    #endif
    }
    public void JS_GiveReward(int amount)
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        GiveReward(amount);
    #endif

    }

    public void OnBalanceReceived(string balance)
    {
        balanceText.text = $"Balance: {balance}Ruby";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
