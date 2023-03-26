using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public bool canShoot; //so that the player can't shoot in the menu

    public Transform myCameraHead;
    private UICanvasController myUICanvas;

    public Transform firePosition;
    public GameObject muzzleFlash, bulletHole, waterLeak, bloodEffect;
    
    public GameObject bullet;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;
    
    public float timeBetweenShots;

    public int bulletsAvailable, totalBullets, magazineSize;

    public float reloadTime;
    private bool reloading;

    //aiming
    public Transform aimPosition;
    private float aimSpeed = 2f;
    private Vector3 gunStartPosition;
    public float zoomAmount;

    public int damageAmount;
    public string gunName;

    // Start is called before the first frame update
    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvailable = magazineSize;

        gunStartPosition = transform.localPosition;

        myUICanvas = FindObjectOfType<UICanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) //so that the player can't shoot in the menu
        {
            Shoot();
            GunManager();
            UpdateAmmoText();
        }
    }

    private void GunManager()
    {
        if(Input.GetKeyDown(KeyCode.R) && bulletsAvailable < magazineSize && !reloading)
        {
            Reload();
        }

        if(Input.GetMouseButton(1))
        {
            transform.position = Vector3.MoveTowards(transform.position, aimPosition.position, aimSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, gunStartPosition, aimSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<CameraMove>().ZoomIn(zoomAmount);
        }
        if(Input.GetMouseButtonUp(1))
        {
            FindObjectOfType<CameraMove>().ZoomOut();
        }
    }

    private void Shoot()
    {
        if (canAutoFire)
        {
            shooting = Input.GetMouseButton(0);
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
        }
        
        if(shooting && readyToShoot && bulletsAvailable > 0 && !reloading) //fires bullets/attacks. Still need proper bullets to replace placeholder.
        {

            readyToShoot = false;

            RaycastHit hit;

            if(Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if(Vector3.Distance(myCameraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point); //sends bullets to center of screen on crosshair. Still need new crosshair sprite.
                    
                    //if(hit.collider.tag == "Shootable")
                    //{
                    //    Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal)); //creates bullet holes. Still need the bullet hole sprite to make bullet holes instantiate.
                    //}
                    //if(hit.collider.CompareTag("WaterLeaker"))
                    //{
                    //    Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal)); //particle effect for hitting environment such as the ground.
                    //}
                }

                if(hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damageAmount);
                    //Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal)); //need bloodEffect prefab from Harrison
                }
            }
            
            else
            {
                firePosition.LookAt(myCameraHead.position + (myCameraHead.forward * 50f)); //adjust aim for close up targets
            }
            
            bulletsAvailable--;

            //Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition); //creates MuzzleFlash for guns. Still need muzzleflash particle effect asset.
            Instantiate(bullet, firePosition.position, firePosition.rotation); //creates bullets from bullet Prefab
        
            StartCoroutine(ResetShot());

         
        }
    }

    private void Reload()
    {
        int bulletsToAdd = magazineSize - bulletsAvailable;

        if(totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvailable = magazineSize;
        }
        else
        {
            bulletsAvailable += totalBullets;
            totalBullets = 0;
        }

        reloading = true;
        
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);

        reloading = false;
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        readyToShoot = true;
    }

    private void UpdateAmmoText()
    {
        myUICanvas.ammoText.SetText(bulletsAvailable + "/" + totalBullets);
    }

    public void ExtraDamage(int DmgAdd)
    {
        damageAmount += DmgAdd;
    }
}
