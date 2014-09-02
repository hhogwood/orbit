using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputOverlord : MonoBehaviour
{
    public Vector3 AimVector;

    private Vector3 mouseStartVector;
    private GameObject lineObject;
    private LineRenderer line;
    private GameObject planet;
    private bool active = false;
    

    public void Init()
    {
        //StartCoroutine(Delay());

        lineObject = GameObject.Instantiate(Overlord.instance.LinePrefab) as GameObject;
        line = lineObject.GetComponent<LineRenderer>();
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
	}
	
	public void Update () 
	{
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseStartVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                planet = GameObject.Instantiate(Overlord.instance.PlanetPrefab) as GameObject;
                Overlord.instance.CBO.AddBody(planet);
                float scale = Mathf.Clamp((float)Overlord.instance.Random.NextDouble() * .3f, .1f, .3f);
                planet.transform.localScale = new Vector3(scale, scale, 1);
                planet.transform.rigidbody2D.mass *= scale;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                line.SetPosition(0, Vector3.zero);
                line.SetPosition(1, Vector3.zero);
                planet.GetComponent<CelestialBody>().Active = true;
                planet.rigidbody2D.AddForce(AimVector * 10);
                planet.audio.Play();
                planet = null;
            }

            else if (Input.GetMouseButton(0))
            {
                planet.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);

                AimVector = mouseStartVector - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), AimVector, Color.red);

                line.SetPosition(0, new Vector3(mouseStartVector.x, mouseStartVector.y, 1f));
                line.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f));
            }
        }
	}

    public IEnumerator Delay()
    { 
        yield return new WaitForSeconds(2f);
        active = true;
    }
}

