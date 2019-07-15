using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public enum interactType
{
    Key,
    Notes,
    Paper,
    Book,
    Jornal,
    Examinable,
    Door,
    Lamp,

}
namespace UnityStandardAssets.Characters.FirstPerson
{

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Raycast")]
        [SerializeField] Image crosshair;
        [SerializeField] public float distance = 2.5f;
        [SerializeField] public LayerMask layerMaskInteract;
        [SerializeField] public Camera playerCam;
        [SerializeField] public bool AmIUsingRaycast;
        [SerializeField] private Vector3 RayOrigin;
        RaycastHit hit;





        [Header("Movimentacao Basica")]
        [SerializeField] private bool isWalking2;
        [SerializeField] private float m_CrounchForce = 0.65f; // verifica a força de agaixar
        [SerializeField] public bool m_IsWalking; // verifica se está andando
        [SerializeField] public bool isRunning;
        [SerializeField] public float m_WalkSpeed; // velocidade do walk
        [SerializeField] public float m_RunSpeed; // velocidade do run
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten; // tempo dos passos
        [SerializeField] private float m_StickToGroundForce; // 
        [SerializeField] private float m_GravityMultiplier; // gravidade
        [SerializeField] private MouseLook m_MouseLook; // mouselook
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob; // usa o headbob
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.



        [Header("Camera")]
        public Animator camAnim;
        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private AudioSource m_AudioSource;
        public Camera camera;
        private float defaultFov = 60.0f;
        private float minimumFov = 45.0f;
        bool isZoomed;
         float lerpTime = 1f;
        float currentLerpTime;
        



        [Header("Stamina")]
        [SerializeField] public float readyToRun = 45.0f;
        [SerializeField] public float speedDecrease = 2.5f;
        [SerializeField] public float speedIncrease = 0.5f;
        [SerializeField] public float stamina;
        [SerializeField] private bool restoringStamina;
        [SerializeField] public Animator animstamina;

        [Header("Audios")]
        public AudioClip BreathSound;
        public AudioSource Breath_AudioSource;



        private void Start()
        {
            isWalking2 = false;
            m_IsWalking = false;
            Breath_AudioSource.clip = BreathSound;
            animstamina.SetBool("isOut", false);
            stamina = 100.0f;
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main; 
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);
        }


        // Update is called once per frame
        
        private void Update()
        {


            // Aqui começa o Raycast

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward , out hit, distance))
            {
                AmIUsingRaycast = true;
                if (hit.transform.gameObject.tag == "examinable")
                {
                    crosshair.GetComponent<Image>().color = Color.red;
                    if (Input.GetButtonDown("interact"))
                    {
                        examin();
                    }
                }
                else
                {
                    crosshair.GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                crosshair.GetComponent<Image>().color = Color.white;
                AmIUsingRaycast = false;
            }








            // end Raycast
                if (!isWalking2)
            {
                camAnim.SetBool("walk", true);
            }
            else
            {
                camAnim.SetBool("walk", false);
            }
            if(Input.GetKey(KeyCode.W))
            {
                isWalking2 = true;
            }
            else
            {
                isWalking2 = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                isWalking2 = true;

            }
            else
            {
                isWalking2 = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                isWalking2 = true;

            }
            else
            {
                isWalking2 = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                isWalking2 = true;

            }
            else
            {
                isWalking2 = false;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isWalking2 = true;

            }
            else
            {
                isWalking2 = false;
            }
            float difference = stamina - 100.0f;
            if (stamina <= readyToRun)
            {
                m_RunSpeed = 4;
                Breath_AudioSource.Play();
                Breath_AudioSource.volume = difference / 100f;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animstamina.SetBool("isOut", true);
                }
                else
                {
                    animstamina.SetBool("isOut", false);
                }
            }
            else if (stamina >= readyToRun)
            {
                m_RunSpeed = 7;
                animstamina.SetBool("isOut", false);
                Breath_AudioSource.volume -= Time.deltaTime;
            }

            if (!m_IsWalking && m_RunSpeed >= 0.1)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            if (isRunning)
            {
                stamina -= speedDecrease * Time.deltaTime;
            }
            else
            {
                stamina += Time.deltaTime * speedIncrease;
            }

            if(stamina >= 100)
            {
                stamina = 100;
            }
            if(stamina <= 0)
            {
                stamina = 0;
            }

            

            Crounch();
            RotateView();
            // the jump state needs to read here to make sure it is not missed

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
            }
            if (!m_CharacterController.isGrounded && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float perc = currentLerpTime / lerpTime;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
  
            var d = Input.GetAxis("EyeZoom");


            if (d > 0f)
            {
                isZoomed = true;
            }
            else if (d < 0f)
            {
                isZoomed = false;
            }
            else if (Input.GetButtonDown("EyeZoom"))
            {
                isZoomed = !isZoomed;
            }

            if (isZoomed)
            {
                m_Camera.fieldOfView = Mathf.Lerp(defaultFov, minimumFov,  perc);
            }
            else
            {
                m_Camera.fieldOfView = Mathf.Lerp(minimumFov, defaultFov,  perc);

            }

            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }
        private void Crounch()
        {
            if (Input.GetButton("Crounch"))
            {
                m_CharacterController.height = m_CrounchForce;
                m_WalkSpeed = 2.5f;

            }
            else
            {
                m_CharacterController.height = 1.8f;
                m_WalkSpeed = 5;
            }
        }
        public void examin()
        {

        }


    }
}
