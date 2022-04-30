using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{

    public enum ItemType
    {
        Weapon, Upgrade,Items,Usables,Key
    }

   

    public enum WeaponSize
    {
        HandGun,Rifle,Rocket
    }

    public enum UseType
    {
        Once,Auto,None
    }



    public WeaponSize WSize = WeaponSize.HandGun;
    public int InventX, InventY;
    public int InventW=1, InventH=1;
    public ItemType IType;

    public UseType UType;
    // Start is called before the first frame update
    public string ItemName = "";
    public float Stength = 1.0f;
    public float MaxRange = 15.0f;
    public Texture2D ItemView;
    public string ItemInfo = "";
    public int MaxUses = 7;
    public int CurUses = 4;
    public int CurRefills = 2;
    public GameObject ExitPoint;
    public UnityEngine.VFX.VisualEffect UseFX;
    public float UseFXLength = 1.0f;
    bool UsedOnce = false;
    public AudioSource UseSound;
    public AudioSource RefillSound;
    public GameObject HitDecal;
    public Camera Cam;
    public GameObject FlashPoint;
    public string KeyID = "";
    public int ItemSize = 1;
    public GameObject Item3D;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fp1 != null)
        {
            if (Time.time > flashStart + 0.1f)
            {
                fp1.SetActive(false);
                fp1 = null;
                
            }
        }
        if (FXPlaying)
        {
            if (Time.time > (UseStart + UseFXLength))
            {
                FXPlaying = false;
           
                foreach(var fx in InPlay)
                {
                    fx.Stop();

                }
                InPlay.Clear();
             //   UseFX.Stop();
            }
          //  Debug.Log("Time:" + Time.time + " UeStart:" + UseStart + " Len:" + UseStart + UseFXLength);
        }
        else
        {
            //UseFX.Stop();
            //UseFX.SendEvent("OnStop");
            //UseFX.Stop();
            //UseFX.playRate

        }


    }
    float UseStart = 0.0f;
    bool FXPlaying = false;
    //List<GameObject> Flashes = new List<GameObject>();
    GameObject fp1;
    float flashStart = 0;

    bool ProjectHit(out RaycastHit hit)
    {


        //RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);



        //  Debug.Break();

        if (Physics.Raycast(ray, out hit))
        {

            //   return hit;
            //var pos = hit.point;
            return true;

        }
        return false;
    }
    List<UnityEngine.VFX.VisualEffect> InPlay = new List<UnityEngine.VFX.VisualEffect>();
    void Use()
        {
            if (CurUses > 0)
            {
                CurUses--;
                if (UseSound != null)
                {
                    UseSound.Play();


                    RaycastHit hit;

                    if (ProjectHit(out hit))
                    {


                    var norm = hit.normal;

                        var newHit = GameObject.Instantiate(HitDecal);
                    //    Debug.Break();
                    newHit.transform.position = hit.point;
                    newHit.transform.LookAt(hit.point - norm);
                    newHit.transform.position = newHit.transform.position + norm*0.01f;


                    //newHit.transform.LookAt(Cam.transform.position);
                    }



                }

            if (FlashPoint != null)
            {

                if (fp1 == null)
                {
                    var nfp = GameObject.Instantiate(FlashPoint);
                    nfp.transform.position = ExitPoint.transform.position;
                    nfp.transform.rotation = ExitPoint.transform.rotation;
                    nfp.SetActive(true);
                    nfp.transform.Rotate(0, 0, Random.RandomRange(0, 360));
                    fp1 = nfp;
                    flashStart = Time.time;

                }



            }

                if (UseFX != null)
                {

                    var newFX = GameObject.Instantiate(UseFX);

                    newFX.transform.position = ExitPoint.transform.position;
                     newFX.transform.rotation = ExitPoint.transform.rotation;
                    

                   newFX.Play();
                InPlay.Add(newFX);




                    UseStart = Time.time;
                    FXPlaying = true;
                }

            }

        }
    
    public void ResetUse()
    {
        UsedOnce = false;
    }
    public void UseItem()
    {
        
        if (UType == UseType.Once)
        {
            if (!UsedOnce)
            {
                Use();
                UsedOnce = true; 
            }

        }
    }

}
