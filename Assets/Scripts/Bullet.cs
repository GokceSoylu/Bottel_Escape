using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        print("Çarpıştı: " + objectWeHit.gameObject.name);

        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit" + objectWeHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Beer_C"))
        {
            print("hit a bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            GameManager.Instance.AddScore(1);
        }
        if (objectWeHit.gameObject.CompareTag("Beer_B"))
        {
            print("hit a bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            GameManager.Instance.AddScore(5);
        }
        if (objectWeHit.gameObject.CompareTag("Beer_R")) // Kırmızı şişe
        {
            print("hit red bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            GameManager.Instance.AddScore(10);
            GameManager.Instance.AdjustTime(-10f);
            //FindObjectOfType<CameraEffects>().TriggerBlur();
            // Süreden 5 saniye azalt
        }

        if (objectWeHit.gameObject.CompareTag("Beer_S")) // Siyah şişe
        {
            print("hit black bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            GameManager.Instance.AdjustTime(+5f); // Süreye 5 saniye ekle
                                                  // Puan eklenmiyor

        }

    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
        GlobalReferances.Instance.bulletImpactEffectPrefab,
        contact.point,
        Quaternion.LookRotation(contact.normal));
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
