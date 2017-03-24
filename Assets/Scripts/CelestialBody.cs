using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CelestialBody : MonoBehaviour 
{
    public int Note;
    public bool Active = false;
    public AudioSource deathSound;

    private Vector3 forceSun;
    private Vector3 position;
    private Vector3 velocity;
    private List<Color32> colors = new List<Color32>();

   

	void Start () 
    {
        #region colors
        colors.Add(new Color32(153, 204, 255, 255));
        colors.Add(new Color32(153, 153, 255, 255));
        colors.Add(new Color32(204, 153, 255, 255));
        colors.Add(new Color32(255, 153, 255, 255));
        colors.Add(new Color32(153, 255, 255, 255));
        colors.Add(new Color32(92, 173, 255, 255));
        colors.Add(new Color32(31, 143, 255, 255));
        colors.Add(new Color32(255, 153, 204, 255));
        colors.Add(new Color32(153, 255, 204, 255));
        colors.Add(new Color32(255, 143, 31, 255));
        colors.Add(new Color32(255, 173, 92, 255));
        colors.Add(new Color32(255, 153, 153, 255));
        colors.Add(new Color32(153, 255, 153, 255));
        colors.Add(new Color32(204, 255, 153, 255));
        colors.Add(new Color32(255, 255, 153, 255));
        colors.Add(new Color32(255, 204, 153, 255));
        #endregion

        gameObject.GetComponent<SpriteRenderer>().color = colors[Overlord.instance.Random.Next(0, colors.Count)];

        Note = Overlord.instance.Random.Next(0, 9);
        GetComponent<AudioSource>().clip = Overlord.instance.Notes[Note];
        deathSound.clip = Overlord.instance.Notes[Note+10];
        //audio.Play();
	}
	
	void Update () 
    {
	    
	}

    void FixedUpdate()
    {
        if (Active)
        {
            Vector3 dist = Overlord.instance.Sun.transform.position - this.transform.position;
            float r = dist.magnitude;
            dist.Normalize();

            double G = 6.674f * (10 ^ 11);
            float force = ((float)G * this.GetComponent<Rigidbody2D>().mass * Overlord.instance.Sun.GetComponent<Rigidbody2D>().mass) / (r * r);

            GetComponent<Rigidbody2D>().AddForce(dist * force);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Active)
        {
            if (other.gameObject.tag == "Sun")
            {
                Overlord.instance.CBO.DestroyBody(this.gameObject);
            }

            else if (other.gameObject.tag == "Planet")
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.GetComponent<SpriteRenderer>().color = colors[Overlord.instance.Random.Next(0, colors.Count)];
            }
        }
    }

}
