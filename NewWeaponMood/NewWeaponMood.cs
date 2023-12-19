using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;

public class NewWeaponMood : MonoBehaviour
{
    public static NewWeaponMood instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    [Header("Mood Properties")]
    [SerializeField] Image[] moodImg;
    public int currentCrystal;
    public float greenMoodDuration = 30f , redMoodDuration = 60f;

    [Header("Lantern Properties")]
    public Image lanternImage;

    bool isMoodActivated;
    public bool isGoodMood = true;
    bool isFull;

    [Header("Post-Processing Propertirs")]
    [SerializeField] GameObject RedMoodEffect;
    [SerializeField] GameObject GreenMoodEffect;


    [Header("References")]
    public HealthManager playerHealth;
    public PlayerScript playerScript;
    public PlayerMovement playerMovement;
    private AudioManager audiomanager;
    public float bonusAttack;


    float lerpSpeed;
    
    void Start() {
        lerpSpeed = 3f * Time.deltaTime;
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (PlayerPrefs.HasKey("CurrentCrystal")) {
            currentCrystal = PlayerPrefs.GetInt("CurrentCrystal");
        } else currentCrystal = 0;

        if (PlayerPrefs.HasKey("CurrentCrystalState")) {
            isGoodMood = PlayerPrefs.GetInt("CurrentCrystalState") == 1 ? true : false;
            Debug.Log("Your mood is: " + isGoodMood);
        } else Debug.Log("No mood bool");
    }
    
    void Update() { 
        if (currentCrystal >= 99) {
            isFull = true;
        } else isFull = false;

        EmotionFiller();
    }

    public void CollectGreenEmotion() {
        if (!isFull && isGoodMood == false) {
            isGoodMood = true;
            currentCrystal = 0;
            AddMood(/* Color.green */);
        } else if (isFull && isGoodMood == false) {
            isGoodMood = true;
            currentCrystal = 0;
            AddMood(/* Color.green */);
        } else if (!isFull && isGoodMood == true) {
            AddMood(/* Color.green */);
        } else if (isFull && isGoodMood == true) {
            Debug.Log("You can't collect more crystal.");
            return;
        }
        
        if (isGoodMood == true && currentCrystal >= 99) {
            Debug.Log("Good Buff is Ready to use!");
        }

    }

        public void CollectRedEmotion() {
        if (!isFull && isGoodMood == true) {
            isGoodMood = false;
            currentCrystal = 0;
            AddMood(/* Color.red */);
        } else if (isFull && isGoodMood == true) {
            isGoodMood = false;
            currentCrystal = 0;
            AddMood(/* Color.red */);
        } else if (!isFull && isGoodMood == false) {
            AddMood(/* Color.red */);
        } else if (isFull && isGoodMood == false) {
            Debug.Log("You can't collect more crystal.");
            return;
        }

        if (!isGoodMood && currentCrystal >= 99) {
            Debug.Log("Red Buff is Ready to use!");
        }

    }

    void AddMood(/* Color moodColor */) {
        currentCrystal += 100 / 3;
        // lanternImage.color = moodColor;
    }

    public GameObject RedVFX;
    public GameObject GreenVFX;
    public void ActivateMoodSkill() {
        if (isFull && isGoodMood) 
        {
            StartCoroutine(ActivatedGreenMood());

            ResetLantern();

            IEnumerator ActivatedGreenMood() {
                StartCoroutine(OpenGreenVFX());
                Debug.Log("Defense UP!");
                int defTemp = playerScript._def;
                playerScript._def *= 2;

                GreenMoodEffect.SetActive(true);

                yield return new WaitForSeconds(greenMoodDuration);

                GreenMoodEffect.SetActive(false);

                playerScript._def = defTemp;
                GreenVFX.SetActive(false);
                Debug.Log("Defense Down!");
            }



            IEnumerator OpenGreenVFX()
            {
                audiomanager.PlaySFX(audiomanager.GreenSFX);
                yield return new WaitForSeconds(0.3f);
                GreenVFX.SetActive(true);
            }
        } 
        else if (isFull && !isGoodMood) {
            
            StartCoroutine(ActivatedRedMood());
            
            ResetLantern();
                        
            IEnumerator ActivatedRedMood()
            {
                StartCoroutine(OpenRedVFX());
                Debug.Log("Speed UP!");
                float speedTemp = playerMovement.speed;
                playerMovement.speed *= 1.75f;

                Debug.Log("ATK UP!");
                bonusAttack += 20;

                playerHealth.healthAmount -= playerHealth.healthAmount / 3;

                RedMoodEffect.SetActive(true);

                yield return new WaitForSeconds(redMoodDuration);
                
                RedMoodEffect.SetActive(false);

                Debug.Log("Speed Down!");
                playerMovement.speed = speedTemp;

                Debug.Log("ATK Down!");
                bonusAttack = 0;
                RedVFX.SetActive(false);

            }

            IEnumerator OpenRedVFX()
            {
                audiomanager.PlaySFX(audiomanager.RedSFX);
                yield return new WaitForSeconds(0.3f);
                RedVFX.SetActive(true);
            }
            
        }
    }

    public void ResetLantern() {
        currentCrystal = 0;
    }

    void EmotionFiller() {
        lanternImage.fillAmount = Mathf.Lerp(lanternImage.fillAmount, currentCrystal / 100f, lerpSpeed);
        if (isGoodMood) { 
            lanternImage.color = Color.green;
        } else {
            lanternImage.color = Color.red;
        }
    }
}
    
