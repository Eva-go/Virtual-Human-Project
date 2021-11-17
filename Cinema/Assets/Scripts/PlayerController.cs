using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    //public static bool   
    //스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;


    //점프 조정 변수
    [SerializeField]
    private float jumpForce;

    //상태 변수
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;
    public static bool isKoskPause = false;
    //앉았을 때 얼마나 앉을지 결정하는 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;
    //채팅 변수
    public static  bool isChatting = false;

    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;

    //민감도
    [SerializeField]
    private float lookSensitivity;

    //카메라 최소,최대 각도
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    //필요 컴포넌트
    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    //정지상태 마우스 움직임
    [SerializeField]
    private Transform tf_Crosshair;

    private TextInput textInput;


    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        Cursor.visible = false;
    }
    void Update()
    {
        if (!isKoskPause)
        {
            //Time.timeScale = 1;
            IsGround();
            TryJump();
            TryRun();
            TryCrouch();
            Move();
            CameraRotation();
            CharacterRotation();
            CrosshairReturn();
            Chatting();
        }
        else
        {
            Debug.Log("크로스헤어무빙");
            Chatting();
            CrosshairMoving();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Chatting()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isChatting = !isChatting;
        }
    }
    private void CrosshairReturn()
    {
        tf_Crosshair.localPosition = new Vector2(0, 0);
    }

    private void CrosshairMoving()
    {
        tf_Crosshair.localPosition = new Vector2(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2));
        float t_cursorPosX = tf_Crosshair.localPosition.x;
        float t_cursorPosY = tf_Crosshair.localPosition.y;

        t_cursorPosX = Mathf.Clamp(t_cursorPosX, (-Screen.width / 2 + 50), (Screen.width / 2 - 50));
        t_cursorPosY = Mathf.Clamp(t_cursorPosY, (-Screen.height / 2 + 50), (Screen.height / 2 - 50));

        tf_Crosshair.localPosition = new Vector2(t_cursorPosX, t_cursorPosY);
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }
    private void Crouch()
    {
        isCrouch = !isCrouch;
        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine("CrouchCoroutine");
    }

    IEnumerable CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY,applyCrouchPosY,0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }
    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }
    private void CameraRotation()
    {

        //상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
   
    }

    private void CharacterRotation()
    {
        //좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
