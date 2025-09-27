using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text customerText;

    private float timer = 0f;
    private bool timerRunning = true;

    private int delivered = 0;
    private int total = 0;

    void Update()
    {
        if (!timerRunning) return;

        //Update timer and display formatted time
        timer += Time.deltaTime;
        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);
        timerText.text = $"Time: {min:00}:{sec:00}";
    }

    //Set total numbers of customers at game start
    public void SetTotalCustomers(int amount)
    {
        total = amount;
        UpdateCustomerText();
    }

    //Increment delivered count and update UI 
    public void AddDelivered()
    {
        delivered++;
        UpdateCustomerText();
    }

    //Stop timer when game ends
    public void StopTimer()
    {
        timerRunning = false;
    }

    //Update customer progress display
    private void UpdateCustomerText()
    {
        customerText.text = $"Customer: {delivered} / {total}";
    }

    //Return elapsed time for external use
    public float GetElapsedTime()
    {
        return timer;
    }
}
