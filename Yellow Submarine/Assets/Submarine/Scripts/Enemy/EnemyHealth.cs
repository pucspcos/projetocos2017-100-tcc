using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	public GameObject EnemyExplosionPrefab;   

    public AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	private ParticleSystem m_ExplosionParticles;
	private AudioSource m_ExplosionAudio;

	void Awake ()
    {
        enemyAudio = GetComponent <AudioSource> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
		m_ExplosionParticles = Instantiate(EnemyExplosionPrefab).GetComponent<ParticleSystem>();

		m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

		m_ExplosionParticles.gameObject.SetActive(false);
	}


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (float amount)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
		
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);

		m_ExplosionParticles.Play();

		m_ExplosionAudio.Play();

		gameObject.SetActive(false);
		Destroy(gameObject, 1.5f);
		Destroy(m_ExplosionParticles.gameObject, 1.5f);
	}


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
