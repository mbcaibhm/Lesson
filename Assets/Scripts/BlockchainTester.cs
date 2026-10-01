using UnityEngine;
using System.Numerics;

public class BloackchainTester : MonoBehaviour
{
    [Header("블록체인 매니져 연결")]
    public BlockchainManager bcManager;

    [Header("테스트 금액")]
    [Tooltip("테스트할 게임 머니 숫자 넣어라")]
    public int testAmount = 100;

    void Start()
    {
        if(bcManager == null)
        {
            LogError("블록체인매니져를 연결해라!!!");
            return;
        }
        Log("블록체인 테스터 준비 완료");
    }

    [ContextMenu("1. 플레이어 잔액 조회")]
    public async void Test_GetBalance()
    {
        Log("플레이어 잔액 조회 시작...");
        try
        {
            BigInteger balance = await bcManager.GetPlayerBalance();
            Log("조회 성공!");
            Log($"플레이어 잔액: {balance} Ruby");
        }
        catch(System.Exception e)
        {
            LogError($"잔액 조회 실패: {e.Message}");
        }
    }

    [ContextMenu("2. 아이템 구매 테스트")]
    public async void Test_BuyItem()
    {
        Log($"아이템 구매 테스트 시작 ({testAmount} Ruby)");
        try
        {
            string txHash = await bcManager.BuyItem(testAmount);
            Log($"구매 성공!");
            Log($"트랙잭션: {txHash}");

            BigInteger balance = await bcManager.GetPlayerBalance();
            Log($"플레이어 잔액: {balance} Ruby");
        }
        catch (System.Exception e)
        {
            LogError($"구매 실패: {e.Message}");
        }
    }


    [ContextMenu("3. 아이템 판매 테스트")]
    public async void Test_SellItem()
    {
        Log($"아이템 판매 테스트 시작 ({testAmount} Ruby)");
        try
        {
            string txHash = await bcManager.SellItem(testAmount);
            Log($"판매 성공!");
            Log($"트랙잭션: {txHash}");

            BigInteger balance = await bcManager.GetPlayerBalance();
            Log($"플레이어 잔액: {balance} Ruby");
        }
        catch (System.Exception e)
        {
            LogError($"판매 실패: {e.Message}");
        }
    }


    [ContextMenu("4. 보상 지급 테스트")]
    public async void Test_GiveReward()
    {
        Log($"보상 지급 테스트 시작 ({testAmount} Ruby)");
        try
        {
            string txHash = await bcManager.GiveReward(testAmount);
            Log($"보상 지급 성공!");
            Log($"트랙잭션: {txHash}");

            BigInteger balance = await bcManager.GetPlayerBalance();
            Log($"플레이어 잔액: {balance} Ruby");
        }
        catch (System.Exception e)
        {
            LogError($"보상 지급 실패: {e.Message}");
        }
    }



    #region 로그 찍는 함수

    void Log(string message) => Debug.Log($"[Tester] => {message}");
    void LogError(string message) => Debug.LogError($"[Tester] => {message}");

    #endregion
}
