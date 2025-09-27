using UnityEngine;
using TMPro;

public class TaxiPickup : MonoBehaviour
{
    public TMP_Text debugText;
    public GameObject gameOverScreen;

    private GameObject currentCustomer;
    private Transform customerDestination;
    private bool hasCustomer = false;

    private int totalCustomers;
    private int customersDelivered = 0;

    private GameUI gameUI;

    private void Start()
    {
        // Count total customers in the scene at game start
        totalCustomers = GameObject.FindGameObjectsWithTag("Customer").Length;

        //Initialize game UI reference and set customer count
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI != null)
        {
            gameUI.SetTotalCustomers(totalCustomers);
        }

        //Ensure game over screen is hidden initally
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Handle customer pickup
        if (other.CompareTag("Customer") && !hasCustomer)
        {
            Customer customer = other.GetComponent<Customer>();
            if (customer != null && !customer.hasBeenDelivered)
            {
                currentCustomer = other.gameObject;
                customerDestination = customer.destination;

                //Hide the customer object after pickup
                currentCustomer.SetActive(false);
                hasCustomer = true;

                string message = "Destination: " + customerDestination.name;
                Debug.Log(message);
                if (debugText != null)
                    debugText.text = message;
            }
        }
    }

    private void Update()
    {
        //Handle drop-off when close to destination
        if (hasCustomer && Vector3.Distance(transform.position, customerDestination.position) < 3f)
        {
            hasCustomer = false;

            currentCustomer.transform.position = customerDestination.position;
            currentCustomer.SetActive(true);

            Customer customer = currentCustomer.GetComponent<Customer>();
            if (customer != null)
                customer.hasBeenDelivered = true;

            customersDelivered++;

            if (gameUI != null)
            {
                gameUI.AddDelivered();

                //End game if all customers have been delivered
                if (customersDelivered >= totalCustomers)
                {
                    gameUI.StopTimer();
                    if (gameOverScreen != null)
                        gameOverScreen.SetActive(true);

                    UIManager uiManager = FindObjectOfType<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.ShowScore(gameUI.GetElapsedTime());
                    }

                    Debug.Log("All Customers delivered – Game Over!");
                    if (debugText != null)
                        debugText.text = "All Customers delivered – Game Over!";
                }
            }

            Debug.Log("Brought to destination!");
            if (debugText != null)
                debugText.text = "Brought to destination!";
        }
    }
}