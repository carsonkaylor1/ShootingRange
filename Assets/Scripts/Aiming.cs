using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Aiming : MonoBehaviour
{

    public GameObject cameraObject;
    private float targetXRotation;
    private float targetYRotation;
    private float targetXRotationV;
    private float targetYRotationV;
    public float rotationSpeed = 0.3f;
    public float holdHeight = -0.5f;
    public float holdSide = 0.5f;
    Vector3 holdVector;
    private MouseLook mouseLook;
    public float ratioHipHold = 1;
    public float hipToAimSpeed = 0.1f;
    private float ratioHipHoldV;

    public float aimRacio = 0.4f;
    
    float zoomAngle = 30;

    public float fireSpeed = 15;
    private float waitTillNextFire = 0;
    public GameObject bullet;
    public GameObject bulletSpawn;

    public float shootingAngleRandomizationAiming = 5f;
    public float shootingAngleRandomizationNotAiming = 15f;
    
    public float recoilAmount = 0.5f;
    public float recoilRecoverTime = 0.2f;
    
    private float currentRecoilZPos;
    private float currentRecoilZPosV;
    Vector3 currentRecoilZPosVector;
    
    public GameObject muzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;

    public GameObject gunSound;
    //public GameObject reloadSound;

    public int clip = 2;
    public int maxClipSize = 2;
    public int reserveAmmo = 30;
    public bool ammoEmpty = false;
    public float reloadTimer = 2;
    private float reloadTimerStart;
    public Text ammoText;
    
    public float gunBobAmountX = 0.5f;
    public float gunBobAmountY = 0.5f;
    private float currentGunBobX;
    private float currentGunBobY;
    Vector3 bob;
    Vector3 recoilBob;
    
    private float gameTimer = 0;
    private bool timerEnabled = false;
    public Text gameTimerText;
    public Text startText;

    //public GameObject fence;
    

    void Start()
    {
        muzzleFlashTimerStart = muzzleFlashTimer;
        //reloadTimerStart = reloadTimer;
        mouseLook = GetComponent<MouseLook>();
        startText.text = ("Press F to Start");
    }

    void LateUpdate()
    {
        bob = new Vector3(holdSide * ratioHipHold + currentGunBobX, holdHeight * ratioHipHold + currentGunBobY, 0);
        recoilBob = new Vector3(0, 0, currentRecoilZPos);

        currentGunBobX = Mathf.Sin(cameraObject.GetComponent<MouseLook>().headBobStepCounter) * gunBobAmountX * ratioHipHold;
        currentGunBobY = Mathf.Cos(cameraObject.GetComponent<MouseLook>().headBobStepCounter * 2) * gunBobAmountY * ratioHipHold;

        transform.position = cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * bob + Quaternion.Euler(targetXRotation, targetYRotation, 0) * recoilBob);

        
        ammoText.text = ("Ammo: " + clip + "/" + reserveAmmo);

        gameTimerText.text = ("Timer: " + gameTimer);

        if (Input.GetKeyDown(KeyCode.F))
        {
            timerEnabled = true;
            startText.text = ("");
        }
        if (timerEnabled)
        {
            gameTimer += Time.deltaTime;
        }

        if(GameObject.FindGameObjectsWithTag("cannon ball").Length <= 0)
        {
            //fence.SetActive(false);
        }
        
        /*
        if (bulletScript.getUserBallCount() >= totalBallCount)
        {
            fence.SetActive(false);
        }
        */
        
        if (Input.GetButton("Fire1") && ammoEmpty == false)
        {
            if(waitTillNextFire <= 0)
            {
                if (bullet)
                {
                    Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                }
                if (gunSound)
                {
                   Instantiate(gunSound, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                }
                
                muzzleFlashEnabled = true;
                targetXRotation += (Random.value - 0.5f) * Mathf.Lerp(shootingAngleRandomizationAiming, shootingAngleRandomizationNotAiming, ratioHipHold);
                targetYRotation += (Random.value - 0.5f) * Mathf.Lerp(shootingAngleRandomizationAiming, shootingAngleRandomizationNotAiming, ratioHipHold);
                currentRecoilZPos -= recoilAmount;
                waitTillNextFire = 1;
                clip -= 1;
                
            }
        }
        
        if(muzzleFlashEnabled == true)
        {
            muzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if(muzzleFlashTimer <= 0)
        {
            muzzleFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
        }
        if(Input.GetKeyDown(KeyCode.R) && clip != maxClipSize && reserveAmmo != 0)
        {
            int totalAmmo = clip + reserveAmmo;
            if(totalAmmo < maxClipSize)
            {
                clip = totalAmmo;
                reserveAmmo = 0;
            }
            else
            {
                int shots = maxClipSize - clip;
                clip = 2;
                reserveAmmo -= shots;
            }
        }


        if(clip <= 0)
        {
            ammoEmpty = true;
        }
        else
        {
            ammoEmpty = false;
        }
        
        
        /*
        if(ammoEmpty == true)
        {
            reloadTimer -= Time.deltaTime;  
        }
        if(reloadTimer <= 0)
        {
            clip = 2;
            reloadTimer = reloadTimerStart;
            ammoEmpty = false;
        }
        */
        
        waitTillNextFire -= Time.deltaTime * fireSpeed;

        currentRecoilZPos = Mathf.SmoothDamp(currentRecoilZPos, 0, ref currentRecoilZPosV, recoilRecoverTime);

        cameraObject.GetComponent<MouseLook>().currentTargetCameraAngle = zoomAngle;


        holdVector = new Vector3(holdSide * ratioHipHold + currentGunBobX, holdHeight * ratioHipHold + currentGunBobY, 0);
        currentRecoilZPosVector = new Vector3(0, 0, currentRecoilZPos);
        
        if (Input.GetButton("Fire2"))
        {
            cameraObject.GetComponent<MouseLook>().currentAimRacio = aimRacio;
            ratioHipHold = Mathf.SmoothDamp(ratioHipHold, 0, ref ratioHipHoldV, hipToAimSpeed);
        }
        if (Input.GetButton("Fire2") == false)
        {
            cameraObject.GetComponent<MouseLook>().currentAimRacio = 1;
            ratioHipHold = Mathf.SmoothDamp(ratioHipHold, 1, ref ratioHipHoldV, hipToAimSpeed);
        }
        

        transform.position = cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * holdVector + Quaternion.Euler(targetXRotation, targetYRotation, 0) * currentRecoilZPosVector);

        targetXRotation = Mathf.SmoothDamp(targetXRotation, cameraObject.GetComponent<MouseLook>().xRotation, ref targetXRotationV, rotationSpeed);
        targetYRotation = Mathf.SmoothDamp(targetYRotation, cameraObject.GetComponent<MouseLook>().yRotation, ref targetYRotationV, rotationSpeed);

        transform.rotation = Quaternion.Euler(targetXRotation, targetYRotation, 0);
    }
}
