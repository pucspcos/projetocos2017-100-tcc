using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
	public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
	public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
	public Image m_FillImage;                           // The image component of the slider.
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
	public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
	public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
	public AudioSource playerAudioDano;
	public TankMovement tankMovement;
	public AudioClip damageClip;
	public float m_CurrentHealth;						// How much health the tank currently has.

	private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
	private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.   
	private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?
	private bool m_Damage;


	private void Awake()
	{

		playerAudioDano = GetComponent<AudioSource>();
		tankMovement = GetComponent<TankMovement>();
		m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
		
		m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
		
		m_ExplosionParticles.gameObject.SetActive(false);
	}

	void Update()
	{
		if(m_Damage)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		m_Damage = false;
	}


	private void OnEnable()
	{
		m_CurrentHealth = m_StartingHealth;
		m_Dead = false;
		
		SetHealthUI();
	}


	public void TakeDamage(float amount)
	{
		m_Damage = true;

		m_CurrentHealth -= amount;
		
		SetHealthUI();
		playerAudioDano.clip = damageClip;
		playerAudioDano.Play();
		
		if (m_CurrentHealth <= 0f && !m_Dead)
		{
			OnDeath();
		}
	}


	private void SetHealthUI()
	{
		m_Slider.value = m_CurrentHealth;
		m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
	}


	private void OnDeath()
	{
		m_Dead = true;
		
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);
		
		m_ExplosionParticles.Play();
		
		m_ExplosionAudio.Play();
		
		gameObject.SetActive(false);
	}
}