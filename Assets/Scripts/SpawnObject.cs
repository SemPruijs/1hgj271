using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject @object;
    public float inTime;
    
    //audio
    public AudioClip withSound;
    public AudioSource _audioSource;
    private bool _useSound;
    
    
    
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    public IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            if (GameManager.Instance.state == GameManager.State.InGame)
            {
                SpawnTheObject();
            }
            yield return new WaitForSeconds(inTime);
        }
    }
    
    private void SpawnTheObject()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Instantiate(@object, position, rotation);
        if (_useSound)
        {
            _audioSource.PlayOneShot(withSound);
        }
    }

}
