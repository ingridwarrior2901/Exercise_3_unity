using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject bullet;
    public int shootBulletInSeconds = 2;
    public Transform bulletStartPosition;

    private float currentSecond;
    private float deltaTime;
    private const int bulletForce = 2000;

    void Update()
    {
        deltaTime += Time.deltaTime;
        currentSecond = deltaTime % 60;

        if(currentSecond > shootBulletInSeconds)
        {
            currentSecond = 0;
            deltaTime = 0;

            Vector3 canonPosition = bulletStartPosition.position;
            GameObject bulletObject = Instantiate(bullet, canonPosition, Quaternion.identity);
            Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();

            bulletRigidbody.AddForce(transform.right * bulletForce);
        }
    }
}
