using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleTypes
{
    Explosion,
}

public class ParticleManager : Singleton<ParticleManager>
{
    private List<GameObject> _particleEffectList = new List<GameObject>();

    void Awake()
    {
        /*
         * Initialize Particle Effects, probably with preloaded assets
         * 
        _particleEffectList = PrefabRepository.Instance.ParticleEffects;
        */
    }


    public void SpawnParticleEffect(Vector2 where
                                    , ParticleTypes effect
                                    , Vector2? direction = null
                                    , Transform parent = null
                                    , float lifetime = 0
                                    , bool keepAlive = false)
    {
        Vector2 dir = Vector2.zero;

        if (direction.HasValue)
            dir = direction.Value;

        GameObject particleEffect = _particleEffectList[(int)effect];
        var particle = Instantiate(particleEffect, where, Quaternion.Euler(dir));

        if (parent != null)
            particle.transform.parent = parent;

        if (lifetime > 0)
            Destroy(particle, lifetime);
    }
}
