using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TankHealth playerHealth;


    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.m_CurrentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}
