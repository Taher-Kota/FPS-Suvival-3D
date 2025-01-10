using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    PlayerController playerControl;
    [SerializeField]
    float MoveSpeed = 4f, SprintSpeed = 8f, CrouchSpeed = 1.5f;
    [SerializeField]
    float StandingHeight = 1.6f, CrouchHeight = 1f;
    [SerializeField]
    float crouchTransitionSpeed = 2f;
    Transform LookRoot;

    private bool isCrouching;

    private FootStepsSound footstep;

    private float CrouchFootVolume = .2f;
    private float SprintFootVolume = 1f;
    private float WalkFootVolumeMin = .3f , WalkFootVolumeMax = 0.7f;

    private float CrouchStepDist = .5f;
    private float WalkStepDist = .4f;
    private float SprintStepDist = .25f;

    PlayerStats playerStats;
    private float SprintValue = 100f;
    private float Sprint_Treshold = 20f;

    private void Awake()
    {
        playerControl = GetComponent<PlayerController>();
        LookRoot = transform.GetChild(0).transform;
        footstep = GetComponentInChildren<FootStepsSound>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        footstep.MinVolume = WalkFootVolumeMin;
        footstep.MaxVolume = WalkFootVolumeMax;
        footstep.StepDistance = WalkStepDist;
    }
    void Update()
    {
        Crouch();
        Sprint();
    }

    void Sprint()
    {
        if (SprintValue > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            footstep.MinVolume = SprintFootVolume;
            footstep.MaxVolume = SprintFootVolume;
            footstep.StepDistance = SprintStepDist;
            playerControl.speed = SprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            footstep.MinVolume = WalkFootVolumeMin;
            footstep.MaxVolume = WalkFootVolumeMax;
            footstep.StepDistance = WalkStepDist;
            playerControl.speed = MoveSpeed;
        }
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            SprintValue -= Sprint_Treshold * Time.deltaTime;
            if(SprintValue <= 0f)
            {
                SprintValue = 0f;
                playerControl.speed = MoveSpeed;
                footstep.MinVolume = WalkFootVolumeMin;
                footstep.MaxVolume = WalkFootVolumeMax;
                footstep.StepDistance = WalkStepDist;
            }
            playerStats.DisplayStamina(SprintValue);
        }
        else
        {
            if(SprintValue <= 100f)
            {
                SprintValue += (Sprint_Treshold / 2f) * Time.deltaTime;
                playerStats.DisplayStamina(SprintValue);
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;            
            footstep.MinVolume = CrouchFootVolume;
            footstep.MaxVolume = CrouchFootVolume;
            footstep.StepDistance = CrouchStepDist;
            StartCoroutine(ToggleCrouch());
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !isCrouching)
        {
            footstep.MinVolume = WalkFootVolumeMin;
            footstep.MaxVolume = WalkFootVolumeMax;
            footstep.StepDistance = WalkStepDist;
        }
    }

    IEnumerator ToggleCrouch()
    {
        float targetHeight = isCrouching ? CrouchHeight : StandingHeight;
        float targetSpeed = isCrouching ? CrouchSpeed : MoveSpeed;

        float currentHeight = LookRoot.localPosition.y;
        float time = 0f;

        while (Mathf.Abs(LookRoot.localPosition.y - targetHeight) > 0.01f)
        {
            time += Time.deltaTime * crouchTransitionSpeed;
            float newHeight = Mathf.Lerp(currentHeight, targetHeight, time);
            LookRoot.localPosition = new Vector3(0f, newHeight, 0f);
            yield return null;
        }

        LookRoot.localPosition = new Vector3(0f, targetHeight, 0f);
        playerControl.speed = targetSpeed;
    }
}
