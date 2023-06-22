using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AlertBehavior : MonoBehaviour
{
    public float changeSpeed = 2f;
    public float spawnTime = 3;
    public GameObject fireballPrefab;
    public float shootForce = 500f;
    public GameObject fireballVFX;

    private Material material;
    private float targetTransparency = 0f;

    private void Start()
    {
        // Get the material
        material = GetComponent<Renderer>().material;

        // Start the transparency change
        StartCoroutine(ChangeMaterialTransparency());
        InvokeRepeating("SpawnFireball", spawnTime, 200);

    }

    void SpawnFireball(){
        Instantiate(fireballVFX, transform.position, transform.rotation);
        GameObject fireball = Instantiate(fireballPrefab,  new Vector3(transform.position.x, -3, transform.position.z), 
            transform.rotation) as GameObject;
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * shootForce);
        
        Destroy(gameObject, 0);
        
    }

    private IEnumerator ChangeMaterialTransparency()
    {
        while (true)
        {
            Color color = material.color;
            float startTransparency = color.a;
            targetTransparency = startTransparency > 0.5f ? 0.3f : 0.7f;

            float counter = 0;
            while (counter < changeSpeed)
            {
                counter += Time.deltaTime;
                float alphaValue = Mathf.Lerp(startTransparency, targetTransparency, counter / changeSpeed);

                material.color = new Color(color.r, color.g, color.b, alphaValue);
                yield return null;
            }

            yield return new WaitForSeconds(changeSpeed); // Pause before next cycle
        }
    }

}