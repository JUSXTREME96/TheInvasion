using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> checkpoints = new List<GameObject>();

    private bool isCheck1 = true;
    private bool isCheck2 = false;
    private bool isCheck3 = false;

    public int currentHealth, maximumHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 0)
        {
            CheckpointCheck();
            currentHealth = maximumHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == checkpoints[0].name)
        {
            isCheck1 = true;
            isCheck2 = false;
            isCheck3 = false;
        }

        if(other.name == checkpoints[1].name)
        {
            isCheck1 = false;
            isCheck2 = true;
            isCheck3 = false;
        }

        if(other.name == checkpoints[2].name)
        {
            isCheck1 = false;
            isCheck2 = false;
            isCheck3 = true;
        }
    }

    void CheckpointCheck()
    {
        if(isCheck1 == true)
        {

            transform.position = checkpoints[0].transform.position;
        }

        if(isCheck2 == true)
        {
            transform.position = checkpoints[1].transform.position;
        }

        if(isCheck3 == true)
        {
            transform.position = checkpoints[2].transform.position;
        }
    }
}
