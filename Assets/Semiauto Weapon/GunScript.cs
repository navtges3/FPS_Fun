using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform barrel;
    public float range = 0f;

    public float delay = 0f;
    bool fired;

    bool isAuto;
    public KeyCode switchFireMode;

    private void Update()
    {
        if (Input.GetButton("Fire1") && !fired && isAuto)
        {
            fired = true;
            StartCoroutine("FireAuto");
        }
        if (Input.GetButtonDown("Fire1") && !fired && !isAuto)
        {
            fired = true;
            StartCoroutine("FireSemi");
        }

        if (Input.GetKeyDown(switchFireMode))
        {
            if (!isAuto)
            {
                isAuto = true;
            }
            else
            {
                isAuto = false;
            }
        }
    }

    IEnumerator FireAuto() 
    {
        RaycastHit hit;
        Ray ray = new Ray(barrel.position, transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            if(hit.collider.tag == "Enemy")
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.health -= 1;
            }

           
        }

        Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
        yield return new WaitForSeconds(delay);
        fired = false;
    }

    IEnumerator FireSemi()
    {
        RaycastHit hit;
        Ray ray = new Ray(barrel.position, transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.health -= 1;
            }
        }

        Debug.DrawRay(barrel.position, transform.forward * range, Color.red);
        yield return null;
        fired = false;
    }
}
