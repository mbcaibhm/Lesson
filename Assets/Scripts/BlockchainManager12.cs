using UnityEngine;
using System.Numerics;              //BigInteger(아주 큰 숫자)를 쓰기 위한 네임스페이스
using System.Threading.Tasks;       //비동기 작업(async/await)을 쓰기 위한 네임스페이스
using Nethereum.Web3;               //이더리움 블록체인과 통신하기 위한 네임스페이스
using Nethereum.Web3.Accounts;      //이더리움 계정(지갑)을 관리하기 위한 네임스페이스
using Nethereum.Hex.HexTypes;       //16진수(0x....) 형식의 데이터를 다루기 위한 네임스페이스


public class BlockchainManager : MonoBehaviour
{
    [Header("컨트랙트 설정")]
    public string contractAddress = "0x...";        // 배포한 GameToken 컨트랙트 주소
    public string rpcUrl = "https://ethereum-sepolia-rpc.publicnode.com";  //여기서 빠른 RPC선택하면 됨 https://chainlist.org/chain/11155111

    [Header("개인키 설정")]
    public string shopPrivateKey = "0x...";         // 상점 주인 개인키 (절대 공개 금지!)
    public string playerPrivateKey = "0x...";       // 플레이어 개인키 (절대 공개 금지!) => WEBGL + 메타마스크 연동하면 필요 없음
    
    private Web3 shopWeb3;                          // 상점 Web3 객체
    private Web3 playerWeb3;                        // 플레이어 Web3 객체
    private Account shopAccount;                    // 상점 계정
    private Account playerAccount;                  // 플레이어 계정

    // GameToken 컨트랙트 ABI (필요한 함수만)
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

    void Start()
    {
        InitializeBlockchain();
    }

    // 블록체인 초기화
    private void InitializeBlockchain()
    {
        // 상점 계정 생성
        shopAccount = new Account(shopPrivateKey);
        shopWeb3 = new Web3(shopAccount, rpcUrl);

        // 플레이어 계정 생성
        playerAccount = new Account(playerPrivateKey);
        playerWeb3 = new Web3(playerAccount, rpcUrl);
    }

    // 플레이어 잔액 조회
    public async Task<BigInteger> GetPlayerBalance()
    {
        var contract = playerWeb3.Eth.GetContract(contractABI, contractAddress);
        var balanceFunction = contract.GetFunction("balanceOf");

        var balance = await balanceFunction.CallAsync<int>(playerAccount.Address);

        return balance;
    }

    // 아이템 구매 - 테스트용 (Nethereum으로 직접 구매, 메타마스크 없이)
    // 실제로는 플레이어 개인키가 필요하므로 데모/테스트 목적으로만 사용
    public async Task<string> BuyItem(int amount)
    {
        var contract = playerWeb3.Eth.GetContract(contractABI, contractAddress);
        var buyItemFunction = contract.GetFunction("buyItem");
        var gas = new HexBigInteger(100000);

        var receipt = await buyItemFunction.SendTransactionAndWaitForReceiptAsync(
            playerAccount.Address,
            gas,
            null,
            null,
            amount
        );

        return receipt.TransactionHash;
    }

    // 아이템 판매 - 상점이 플레이어에게 토큰 전송 (자동, 컨펌 없음)
    public async Task<string> SellItem(int amount)
    {
        var contract = shopWeb3.Eth.GetContract(contractABI, contractAddress);
        var sellItemFunction = contract.GetFunction("sellItem");
        var gas = new HexBigInteger(100000);

        var receipt = await sellItemFunction.SendTransactionAndWaitForReceiptAsync(
            shopAccount.Address,
            gas,
            null,
            null,
            playerAccount.Address,
            amount
        );

        return receipt.TransactionHash;
    }

    // 보상 지급 - 상점이 플레이어에게 보상 전송 (자동, 컨펌 없음)
    public async Task<string> GiveReward(int amount)
    {
        var contract = shopWeb3.Eth.GetContract(contractABI, contractAddress);
        var sellItemFunction = contract.GetFunction("giveReward");
        var gas = new HexBigInteger(100000);

        var receipt = await sellItemFunction.SendTransactionAndWaitForReceiptAsync(
            shopAccount.Address,
            gas,
            null,
            null,
            playerAccount.Address,
            amount
        );

        return receipt.TransactionHash;
    }


    /// <summary>
    /// 플레이어 주소 가져오기
    /// </summary>
    public string GetPlayerAddress()
    {
        return playerAccount.Address;
    }

    /// <summary>
    /// 상점 주소 가져오기
    /// </summary>
    public string GetShopAddress()
    {
        return shopAccount.Address;
    }
}