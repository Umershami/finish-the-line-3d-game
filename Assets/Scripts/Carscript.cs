using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Carscript : MonoBehaviour
{
    public Gameplayscript gameplayscript;

    public Text speedometer;
    public Transform resetcar;
    public float invertcar = -0.5f;
    public MeshRenderer flwheelmesh, frmesh, rlmesh, rrmesh;
    public WheelCollider flwheelcollider, frwheelcollider, rlwheelCollider, rrwheelcollider;

    public GameObject racebtn;
    public GameObject backbtn;
    public GameObject leftbtn;
    public GameObject rightbtn;
    public GameObject FinishLine;
    public AudioSource idleAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource gameplaysound;

  

    public float acceleration = 20f;
    public float maxSpeed = 160f;
    public float turnSpeed = 120f;
    public float deceleration = 5f;
    public Rigidbody carRigidbody;

    public float currentSpeed = 0f;
    public bool isAccelerating = false;
    public bool isReversing = false;
    public bool isTurningLeft = false;
    public bool isTurningRight = false;
    public bool hasCrossedFinishLine = false;
    public bool wincheck = false;

    private float minpitch = 1f;
    private float maxpitch = 2f;
    private float maxSteerAngle = 30f;


    void Start()
    {

        Application.targetFrameRate = 60;

        gameplaysound.Play();
        idleAudioSource.loop = true;
        engineAudioSource.loop = true;

        idleAudioSource.Play();

        SetupButtonEvent(racebtn, OnAccelerateButtonDown, OnAccelerateButtonUp);
        SetupButtonEvent(backbtn, OnReverseButtonDown, OnReverseButtonUp);
        SetupButtonEvent(leftbtn, OnTurnLeftButtonDown, OnTurnLeftButtonUp);
        SetupButtonEvent(rightbtn, OnTurnRightButtonDown, OnTurnRightButtonUp);
    }
   
 
    void Update()
    {
        if (!gameplayscript.resumepanelactivecheck)
        {
            HandleMovement();
        }

        UpdateAudio();
        UpdateWheelMeshes();
    }

    void FixedUpdate()
    {
        UpdateWheelPhysics();
     //   if (transform.up.y < invertcar && !resetcar)
     //   {
      //      StartCoroutine(resetcarr());
      //  }
    }
  //  IEnumerator resetcarr()
  //  {
   //     yield return new WaitForSeconds()
   // }
    private void UpdateAudio()
    {
        if (currentSpeed < 0.1f)
        {
            if (!idleAudioSource.isPlaying)
            {
                idleAudioSource.Play();
            }
            if (engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }
        else
        {
            if (idleAudioSource.isPlaying)
            {
                idleAudioSource.Stop();
            }
            if (!engineAudioSource.isPlaying)
            {
                engineAudioSource.Play();
            }
            engineAudioSource.pitch = Mathf.Lerp(minpitch, maxpitch, currentSpeed / maxSpeed);
        }
    }

    private void UpdateWheelPhysics()
    {
        float motorTorque = 0f;

        if (isAccelerating)
        {
            motorTorque = acceleration;
        }
        else if (isReversing)
        {
            motorTorque = -acceleration;
        }

        flwheelcollider.motorTorque = motorTorque;
        frwheelcollider.motorTorque = motorTorque;
        rlwheelCollider.motorTorque = motorTorque;
        rrwheelcollider.motorTorque = motorTorque;

        float steerAngle = 0f;

        if (isTurningLeft)
        {
            steerAngle = -maxSteerAngle;
        }
        else if (isTurningRight)
        {
            steerAngle = maxSteerAngle;
        }

        flwheelcollider.steerAngle = steerAngle;
        frwheelcollider.steerAngle = steerAngle;
    }

    private void UpdateWheelMeshes()
    {
        if (currentSpeed != 0f || isTurningLeft || isTurningRight)
        {
            UpdateWheelPosition(flwheelcollider, flwheelmesh);
            UpdateWheelPosition(frwheelcollider, frmesh);
            UpdateWheelPosition(rlwheelCollider, rlmesh);
            UpdateWheelPosition(rrwheelcollider, rrmesh);
        }
    }

    private void UpdateWheelPosition(WheelCollider col, MeshRenderer renderer)
    {
        col.GetWorldPose(out Vector3 pos, out Quaternion quat);

        renderer.transform.position = pos;

        if (col == flwheelcollider || col == frwheelcollider)
        {
            renderer.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, quat.eulerAngles.y + col.steerAngle, quat.eulerAngles.z);
        }
        else
        {
            renderer.transform.rotation = quat;
        }
    }

    public void HandleMovement()
    {
        if (!gameplayscript.hascountdownfinished) return;

        float deltaSpeed = acceleration * Time.deltaTime;

        if (isAccelerating)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + deltaSpeed, -maxSpeed, maxSpeed);
        }
        else if (isReversing)
        {
            currentSpeed = Mathf.Clamp(currentSpeed - deltaSpeed, -maxSpeed, 0);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        speedometer.text = Mathf.FloorToInt(currentSpeed).ToString();
        float turnSpeedMultiplier = 1.5f; // Adjust this multiplier to increase turn speed

        if (isTurningLeft)
        {
            transform.Rotate(0, -turnSpeed * turnSpeedMultiplier * Time.deltaTime * (currentSpeed / maxSpeed), 0);
        }
        else if (isTurningRight)
        {
            transform.Rotate(0, turnSpeed * turnSpeedMultiplier * Time.deltaTime * (currentSpeed / maxSpeed), 0);
        }
        Vector3 forwardMovement = transform.forward * currentSpeed * Time.deltaTime;
        carRigidbody.MovePosition(carRigidbody.position + forwardMovement);
    }

    public void OnAccelerateButtonDown() { isAccelerating = true; }
    public void OnAccelerateButtonUp() { isAccelerating = false; }
    public void OnReverseButtonDown() { isReversing = true; }
    public void OnReverseButtonUp() { isReversing = false; }
    public void OnTurnLeftButtonDown() { isTurningLeft = true; }
    public void OnTurnLeftButtonUp() { isTurningLeft = false; }
    public void OnTurnRightButtonDown() { isTurningRight = true; }
    public void OnTurnRightButtonUp() { isTurningRight = false; }

    private void SetupButtonEvent(GameObject button, UnityEngine.Events.UnityAction pointerDownAction, UnityEngine.Events.UnityAction pointerUpAction)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>() ?? button.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((eventData) => { pointerDownAction(); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((eventData) => { pointerUpAction(); });
        trigger.triggers.Add(pointerUpEntry);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            gameplayscript.lostpanel.SetActive(false);
            return;
        }

        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("Car hit the bubble");
            gameplayscript.timerr += 5;
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Bubblebonus"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("instantspeed"))
        {
            currentSpeed = Mathf.Min(currentSpeed + 30f, maxSpeed);
            speedometer.text = Mathf.FloorToInt(currentSpeed).ToString();
            Destroy(collision.gameObject);
            return;
        }
        

        Debug.Log("Car collided with: " + collision.gameObject.name);
        float decreasespeed = 5f;
        currentSpeed -= decreasespeed;
        currentSpeed=Mathf.Max(currentSpeed, 0f);
        carRigidbody.velocity = Vector3.zero;
       
    }

   

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            Debug.Log("Finish line crossed!");
            hasCrossedFinishLine = true;
            wincheck = true;
            gameplayscript.winpanelactivated();
            gameplayscript.lostpanel.SetActive(false);
        }
    }
}
