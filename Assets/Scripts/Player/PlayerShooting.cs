using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 5f;
    public float range = 100f;
    public int maxBullets = 100;

    public Slider bulletSlider;
    
    private int _currentBullets;
    private readonly float _effectsDisplayTime = 0.2f;
    private AudioSource _gunAudio;
    private Light _gunLight;
    private LineRenderer _gunLine;
    private ParticleSystem _gunParticles;
    private int _shootableMask;
    private RaycastHit _shootHit;
    private Ray _shootRay;

    private float _timer;

    private void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();

        _currentBullets = maxBullets;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    public void Shoot()
    {
        if (_currentBullets <= 0 || gameObject.GetComponentInParent<PlayerHealth>().currentHealth <= 0 || _timer < timeBetweenBullets)
        {
            return;
        }
        
        UpdateBullet(-1);

        _timer = 0f;

        _gunAudio.Play();

        _gunLight.enabled = true;

        _gunParticles.Stop();
        _gunParticles.Play();

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
            }

            _gunLine.SetPosition(1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    }

    public void UpdateBullet(int bulletValue)
    {
        int newBullet = _currentBullets += bulletValue;
        _currentBullets = Mathf.Clamp(newBullet, 0, maxBullets);
        bulletSlider.value = _currentBullets;
    }
}