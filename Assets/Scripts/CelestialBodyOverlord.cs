using UnityEngine;
using System.Collections;

public class CelestialBodyOverlord : MonoBehaviour
{
	void Start () 
	{
	
	}

    public void Update()
    {
        //Debug.Log("hi");

        for (int i = 0; i < Overlord.instance.Bodies.Count; i++)
        {
            for (int k = 0; i < Overlord.instance.Bodies.Count; i++)
            {
                if (Overlord.instance.Bodies[i] != Overlord.instance.Bodies[k])
                {
                    //Debug.Log("apply gravity");
                    ApplyGravity(Overlord.instance.Bodies[i].GetComponent<Rigidbody2D>(), Overlord.instance.Bodies[k].GetComponent<Rigidbody2D>());
                }
            }
        }
    }

    public void AddBody(GameObject _gobj)
    {
        Overlord.instance.Bodies.Add(_gobj);
    }

    public void DestroyBody(GameObject _gobj)
    {
        Overlord.instance.Bodies.Remove(_gobj);
        _gobj.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        _gobj.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        _gobj.gameObject.GetComponent<CelestialBody>().deathSound.Play();
        Destroy(_gobj);
    }

    void ApplyGravity(Rigidbody2D A, Rigidbody2D B)
    {
        Vector3 dist = B.transform.position - A.transform.position;
        float r = dist.magnitude;
        dist.Normalize();

        double G = 6.674f * (10 ^ -11);
        float force = ((float)G * A.mass * B.mass) / (r * r);

        A.AddForce(-dist * force);
        B.AddForce(dist * force);
    }

    IEnumerator Destroy(GameObject _gobj)
    {
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(_gobj);
    }

}

