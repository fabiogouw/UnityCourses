using UnityEngine;

public class GhostChaser : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private AudioSource _audio;
    private float speed = 1f;
    public Transform target;
    public AudioClip attackSFX;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        { // if Rigidbody is missing
            Debug.LogError("Rigidbody2D component missing from this gameobject");
        }
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }
        // if no target specified, assume the player
        if (target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 0f)
        {
            //move if distance from target is greater than 0
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (player.playerCanMove)
            {
                playSound(attackSFX);
                // apply damage to the player
                player.ApplyDamage(1);
                transform.Translate(new Vector3(0, -10, 0));
            }
        }
    }

    void playSound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
