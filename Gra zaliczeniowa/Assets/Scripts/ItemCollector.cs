using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;

    [SerializeField] private Text CoinsCount;

    [SerializeField] private AudioSource coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinSound.Play();
            Destroy(collision.gameObject);
            coins++;
            CoinsCount.text = "" + coins + "/15";
        }
    }
}
