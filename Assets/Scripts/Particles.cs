using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Particles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem BulletParticles;

    [SerializeField]
    private ParticleSystem MuzzleParticles;

    private List<ParticleCollisionEvent> EventList;

    public Color paintColor;

    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [Space]
    private ParticleSystem Part;
    private void Awake()
    {
        Part = GetComponent<ParticleSystem>();
        EventList = new List<ParticleCollisionEvent>();
    }
    public void Fire()
    {
        BulletParticles.Play();
        MuzzleParticles.Play();
    }  

    public void StopFire()
    {
        BulletParticles.Stop();
        MuzzleParticles.Stop();
    }
    private void OnParticleCollision(GameObject Other)
    {
        int Events = BulletParticles.GetCollisionEvents(Other, EventList);

        Paintable p = Other.GetComponent<Paintable>();

        if (p != null)
        {
            for (int i = 0; i < Events; i++)
            {
                Vector3 pos = EventList[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
            }
        }
    }
}
