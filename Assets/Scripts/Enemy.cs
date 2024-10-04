using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int healtPoints = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
        {
            healtPoints--;
            

            if(healtPoints <= 0)
            {
                Destroy(gameObject);
            }
            
        }
}
