using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    // Bird needs 0 gravity
    public float Fly_Height;
    public float Rate = 5; // Units/Second
    public int direction; // Going negative or positive x distances?
    public float Height_Diff = 0f;
    // STATS
    public uint give_HP = 4;
    public uint give_Stam = 8;
    public float give_Flame = 3f;

    private Transform trans;
    // Use this for initialization
    void Start () {

        if (Fly_Height == 0f)
        {
            // Establish fly height from where
            // we were spawned in
            this.Fly_Height = getHeight()+5;
            this.direction = getDirection();
        }
		this.give_Flame = Mathf.Max (give_Flame, 0f);

        if (Height_Diff == 0f)
        {
            // Adjust for difficulty
            this.Height_Diff = 10;
        }

        this.trans = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    // THINK
    void FixedUpdate()
    {
        // Avoid dragons?

        float a = (getHeight());
        float cake = (this.Height_Diff * Mathf.Sin(Time.realtimeSinceStartup));
        float diff = a + cake;//Fly_Height - a;
        //Debug.Log("A & cake & diff: " + a + ", " + cake + ", " + diff + ", "+ this.Fly_Height);

        // use the difference to assess direction to go
        adjsustHeight(diff);
        // @@@ Forward Motion

        this.Fly_Height = a;
       // Debug.Log("FIxedupdate time: " + Time.realtimeSinceStartup);
    }

    public int getDirection()
    {
        // Returns only the x-direct as -1,0,1
        
        float cur = trans.position.x;
        int ans = (int)(Mathf.Abs(cur) / cur);

        return ans;
    }


    public float getHeight()
    {
        float ans = trans.position.y;

        return ans;
    }
    public void adjsustHeight(float dist)
    {
        // @@@ Sync wing flap animation!
        //if (dist == 0f) { return; }; // Get out.
        int vert_dir = 1;
        dist = Mathf.Clamp(dist, -1*DistPerTick(), DistPerTick()); // Make sure were not moving too far too fast


        if (trans.position.y + dist > this.Height_Diff)
        {
            // Make sure the bird cant just fly away
            vert_dir = -1;
        }

         //Debug.Log("Old pos: " + temp.position);
        trans.position = new Vector3(   trans.position.x,
                                        trans.position.y + (dist * vert_dir),
                                        trans.position.z
                                    );

        

        //Debug.Log("New pos: " + trans.position);
    }
    public float DistPerTick(){
        int TicksPerSecond = 60;
        return this.Rate/TicksPerSecond;
    }
    // Check for deaths
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == Globals.TAGS.Player)
        {
            GameObject dragon = coll.gameObject;

            this.PreDeath(dragon); // Use this for when birds hit walls
        }
        Destroy(this.gameObject);
    }

    void PreDeath(GameObject attacker)
    {
        // The bird is ABOUT TO DIE
        // Award your bonus to the Dragon
        HealthMonitor dragonSM = attacker.GetComponent<HealthMonitor>();


		dragonSM.StatInput( this.give_HP,
                            this.give_Stam,
                            this.give_Flame );
        // Maybe do a little animation?

    }
}
