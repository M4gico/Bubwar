using UnityEngine;

public class ChildWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBullet;
    [SerializeField] private Transform forwardWeapon;
    [SerializeField] private float forceOfTheShot;
    [SerializeField] [Range (0.5f,2f)]private float maxCooldownShoot;
    [SerializeField] [Range(0.2f, 1f)] private float minCooldownShoot;


    private float gunHeat;
    private ChildFollow childFollow;
    private Transform childTransform;
    private Vector3 playerPosition;

    private void Awake()
    {
        childFollow = GetComponentInParent<ChildFollow>();
        childTransform = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        //Get the difference between the player and the child
        //differenceToTarget = childFollow.differenceToTarget.magnitude;
        playerPosition = childFollow.playerTransform.position;

        gunHeat += Time.deltaTime;
        
        if(gunHeat > Random.Range(minCooldownShoot, maxCooldownShoot))
        {
            GameObject bubbleBulletGO = Instantiate(bubbleBullet, forwardWeapon.position, childTransform.rotation);
            gunHeat = 0;
            Rigidbody2D bubbleBulletrb = bubbleBulletGO.GetComponent<Rigidbody2D>();

            //Get the orientation of the weapon compared to the mouse
            transform.right = new Vector2(playerPosition.x,playerPosition.y) - new Vector2(transform.position.x, transform.position.y);
            bubbleBulletrb.AddForce(transform.right * forceOfTheShot);
        }
    }
}
