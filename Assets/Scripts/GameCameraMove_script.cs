using UnityEngine;
using System.Collections;

public class GameCameraMove_script : MonoBehaviour {

    [SerializeField]
    private float cameraDragSpeedMod1 = 0.0785f;
    [SerializeField]
    private float cameraDragSpeedMod2 = 2.5f;
    [SerializeField]
    private float cameraDrag = 0;
    [SerializeField]
    private float cameraZoomSpeed;
    [SerializeField]
    private float starryParallax;

    [SerializeField]
    private int xTest = 0;
    [SerializeField]
    private int xTest2 = 0;

    [SerializeField]
    private GameObject starryBackground;
    [SerializeField]
    private GameObject[] leftStars;
    [SerializeField]
    private GameObject[] centerStars;
    [SerializeField]
    private GameObject[] rightStars;

    [SerializeField]
    private Camera myCamera;

    private float cameraDragSpeed;
    
    private bool screenGliding = false;

    /*******************************************************************************************************************
    * This value will increment whenever the left stars move to the right side and decrement when the opposite occurs  *
    * At 0, the setup will be LCR  = left|center|right for the organization of stars                                   *
    * At 1, the setup will be CRL since it moved to the right thus moving the left stars onto the right                *
    *                                                                                                                  *
    * -3: LCR                                                                                                          *
    * -2: CRL                                                                                                          *
    * -1: RLC                                                                                                          *
    * 0 : LCR                                                                                                          *
    * 1 : CRL                                                                                                          *
    * 2 : RLC                                                                                                          *
    * 3 : LCR                                                                                                          *
    *                                                                                                                  *
    * Taking this number % 3 should reveal which side needs to move next                                               *
    *                                                                                                                  *
    * If starMoveCount % 3 == 0, the L or R moves                                                                      *
    * If == 1,  C or L moves                                                                                           *
    * and if == 2, R or C moves                                                                                        *
    ********************************************************************************************************************/
    //[SerializeField]
    //private int starMoveCount = 0;
    //private int starTriggerFirstPos = 0;
    //private int starTriggerXActual = 0;

    private Transform myCamTrans;
    private Transform starryTrans;

    private Vector3 newCameraPosition;
    private Vector3 newBackgroundPosition;
    private Vector3 mousePos;
    private Vector3 mousePosOnScreen;
    private Vector3 cameraVelocity;
    //private Vector2 lastTriggered = Vector2.zero;

    void Awake ()
	{
        myCamTrans = transform;
        starryTrans = starryBackground.transform;
	}
	
	void Start () 
	{
        cameraDragSpeed = cameraDragSpeedMod1 * Camera.main.orthographicSize / cameraDragSpeedMod2;
	}

    void Update()
    {
        xTest = (int)starryTrans.localPosition.x;
        xTest2 = Mathf.Abs( (int)starryTrans.localPosition.x % 10 );
        // handle click+drag movement of camera
        if( Input.GetMouseButton( 1 ) )
        {
            if( newCameraPosition == Vector3.zero )
            {
                Vector3 dragPanning = mousePos - Camera.main.ScreenToWorldPoint( Input.mousePosition );
                dragPanning.z = 0;
                Vector3 temp = myCamTrans.position;
                temp += dragPanning;
                screenGliding = false;
                if( ( temp.x > -415 ) && ( temp.x < 415 ) && ( temp.y > -255 ) && ( temp.y < 255 ) )
                {
                    Cursor.visible = false;
                    myCamTrans.position = temp;
                    starryTrans.position -= dragPanning / starryParallax;
                }
            }
        }

        if( Input.GetMouseButtonUp( 1 ) )
        {
            Cursor.visible = true;
            if( newCameraPosition == Vector3.zero )
            {
                screenGliding = true;
            }
        }

        if( ( newCameraPosition == Vector3.zero ) && ( screenGliding ) )
        {
            cameraVelocity = myCamera.velocity;
            cameraVelocity.z = 0f;

            //print( "Camera Velocity : " + cameraVelocity );

            if( ( myCamTrans.position.x + cameraVelocity.x < -415f ) || ( myCamTrans.position.x + cameraVelocity.x > 415f ) )
            {
                cameraVelocity.x = 0f;
            }
            if( ( myCamTrans.position.y + cameraVelocity.y > 255f ) || ( myCamTrans.position.y + cameraVelocity.y < -255f ) )
            {
                cameraVelocity.y = 0f;
            }

            myCamTrans.Translate( cameraVelocity * Time.deltaTime * cameraDrag );
            starryTrans.Translate( ( -cameraVelocity * Time.deltaTime * cameraDrag / starryParallax ) );
        }

        if( cameraVelocity == Vector3.zero )
        {
            screenGliding = false;
        }

        float mouseScroll = Input.GetAxis( "Mouse ScrollWheel" );

        float tempsize = myCamera.orthographicSize - mouseScroll * cameraZoomSpeed;
        if( ( tempsize > 3.5 ) && ( tempsize < 5.3 ) )
        {
            myCamera.orthographicSize = myCamera.orthographicSize - mouseScroll * cameraZoomSpeed;
        }
        mousePosOnScreen = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint( mousePosOnScreen );
        mousePos.z = 0;

        // The below region was an attempt to make the starry background infinitely scroll
        #region
        //int one = Mathf.Abs( ( (int)starryTrans.localPosition.x + 1 ) % 10 );
        //int two = Mathf.Abs( ( (int)starryTrans.localPosition.x + 2 ) % 10 );
        //int three = Mathf.Abs( ( (int)starryTrans.localPosition.x - 1 ) % 10 );
        //int four = Mathf.Abs( ( (int)starryTrans.localPosition.x - 2 ) % 10 );

        //print( "Left1: " + one + " Left2: " + two + " Right1: " + three + " Right2: " + four );

        // get triggered if the local x position is around a 10 integer
        //if( ( Mathf.Abs( ( (int)starryTrans.localPosition.x + 1 ) % 10 ) == 0 ) || ( Mathf.Abs( ( (int)starryTrans.localPosition.x - 1 ) % 10 ) == 0 ) || ( Mathf.Abs( ( (int)starryTrans.localPosition.x + 2 ) % 10 ) == 0 ) || ( Mathf.Abs( ( (int)starryTrans.localPosition.x - 2 ) % 10 ) == 0 ) )
        //{
        //    int tempTriggerPos = Mathf.Abs( (int)starryTrans.localPosition.x % 10 );

        //    //int lower = Mathf.Abs( starTriggerFirstPos - tempTriggerPos );
        //    //float upper = Mathf.Abs( starTriggerXActual - starryTrans.localPosition.x );
        //    //Debug.Log( "lower: " + lower + " upper: " + upper );

        //    if( ( Mathf.Abs( starTriggerFirstPos - tempTriggerPos ) < 2 ) || ( Mathf.Abs( starTriggerXActual - starryTrans.localPosition.x ) > 5 ) )
        //    {
        //        starTriggerFirstPos = tempTriggerPos;
        //        starTriggerXActual = (int)starryTrans.localPosition.x;
        //        // we don't trigger a move because this can only happen when we move within or out and then back into the same trigger zone
        //    }
        //    else
        //    {
        //        //print( "made in it" );
        //        // we trigger a move because it crossed the 10 line to reach the other side
        //        // first we record our current location as the next safety check so we don't spam movements back and forth
        //        starTriggerFirstPos = tempTriggerPos;
        //        starTriggerXActual = (int)starryTrans.localPosition.x;
        //        // then check to see which way we need to move, left or right
        //        if( starTriggerXActual < 0 )
        //        {
        //            if( ( tempTriggerPos == 1 ) || ( tempTriggerPos == 2 ) ) // camera is moving to the right
        //            {
        //                if( starMoveCount % 3 == 0 )
        //                {
        //                    MoveStarsRight( leftStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == 1 )
        //                {
        //                    MoveStarsRight( centerStars, leftStars );
        //                }
        //                else if( starMoveCount % 3 == 2 )
        //                {
        //                    MoveStarsRight( rightStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == -1 )
        //                {
        //                    MoveStarsRight( rightStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == -2 )
        //                {
        //                    MoveStarsRight( centerStars, leftStars );
        //                }
        //            }
        //            else if( ( tempTriggerPos == 8 ) || ( tempTriggerPos == 9 ) ) // camera is moving to the left
        //            {
        //                if( starMoveCount % 3 == 0 )
        //                {
        //                    MoveStarsLeft( rightStars, leftStars );
        //                }
        //                else if( starMoveCount % 3 == 1 )
        //                {
        //                    MoveStarsLeft( leftStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == 2 )
        //                {
        //                    MoveStarsLeft( centerStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == -1 )
        //                {
        //                    MoveStarsLeft( centerStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == -2 )
        //                {
        //                    MoveStarsLeft( leftStars, centerStars );
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if( ( tempTriggerPos == 8 ) || ( tempTriggerPos == 9 ) ) // camera is moving to the right
        //            {
        //                if( starMoveCount % 3 == 0 )
        //                {
        //                    MoveStarsRight( leftStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == 1 )
        //                {
        //                    MoveStarsRight( centerStars, leftStars );
        //                }
        //                else if( starMoveCount % 3 == 2 )
        //                {
        //                    MoveStarsRight( rightStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == -1 )
        //                {
        //                    MoveStarsRight( rightStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == -2 )
        //                {
        //                    MoveStarsRight( centerStars, leftStars );
        //                }
        //            }
        //            else if( ( tempTriggerPos == 1 ) || ( tempTriggerPos == 2 ) ) // camera is moving to the left
        //            {
        //                if( starMoveCount % 3 == 0 )
        //                {
        //                    MoveStarsLeft( rightStars, leftStars );
        //                }
        //                else if( starMoveCount % 3 == 1 )
        //                {
        //                    MoveStarsLeft( leftStars, centerStars );
        //                }
        //                else if( starMoveCount % 3 == 2 )
        //                {
        //                    MoveStarsLeft( centerStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == -1 )
        //                {
        //                    MoveStarsLeft( centerStars, rightStars );
        //                }
        //                else if( starMoveCount % 3 == -2 )
        //                {
        //                    MoveStarsLeft( leftStars, centerStars );
        //                }
        //            }
        //        }
        //    }
        //}

        // Functions relocated to here for now for organization however, if you want to go at this again, these should be located outside Update()
        //void MoveStarsRight( GameObject[] starsMoving, GameObject[] starsAnchoring )
        //{
        //starMoveCount++;
        //float newRightX = starsAnchoring[0].transform.localPosition.x + 20f;
        //for( int i = 0; i < starsMoving.Length; i++ )
        //{
        //    starsMoving[i].transform.localPosition = new Vector3( newRightX, starsMoving[i].transform.localPosition.y, starsMoving[i].transform.localPosition.z );
        //}
        //}

        //void MoveStarsLeft( GameObject[] starsMoving, GameObject[] starsAnchoring )
        //{
        //starMoveCount--;
        //float newLeftX = starsAnchoring[0].transform.localPosition.x - 20f;
        //for( int i = 0; i < starsMoving.Length; i++ )
        //{
        //    starsMoving[i].transform.localPosition = new Vector3( newLeftX, starsMoving[i].transform.localPosition.y, starsMoving[i].transform.localPosition.z );
        //}
        #endregion
    }

    void LateUpdate()
    {
        if( Input.GetKey( KeyCode.A ) )
        {
            newCameraPosition.x -= cameraDragSpeed * 0.05f;
            newBackgroundPosition.x += ( cameraDragSpeed * 0.05f ) / starryParallax;
        }

        if( Input.GetKey( KeyCode.D ) )
        {
            newCameraPosition.x += cameraDragSpeed * 0.05f;
            newBackgroundPosition.x -= ( cameraDragSpeed * 0.05f ) / starryParallax;
        }

        if( Input.GetKey( KeyCode.W ) )
        {
            newCameraPosition.y += cameraDragSpeed * 0.05f;
            newBackgroundPosition.y -= ( cameraDragSpeed * 0.05f ) / starryParallax;
        }

        if( Input.GetKey( KeyCode.S ) )
        {
            newCameraPosition.y -= cameraDragSpeed * 0.05f;
            newBackgroundPosition.y += ( cameraDragSpeed * 0.05f ) / starryParallax;
        }

        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            newCameraPosition.x = 0f;
            newCameraPosition.y = 0f;
            newBackgroundPosition.x = 0f;
            newBackgroundPosition.y = 0f;
        }

        if( ( myCamTrans.position.x + newCameraPosition.x < -415f ) || ( myCamTrans.position.x + newCameraPosition.x > 415f ) )
        {
            newCameraPosition.x = 0f;
            newBackgroundPosition.x = 0f;
        }
        if( ( myCamTrans.position.y + newCameraPosition.y > 255f ) || ( myCamTrans.position.y + newCameraPosition.y < -255f ) )
        {
            newCameraPosition.y = 0f;
            newBackgroundPosition.y = 0f;
        }
        transform.Translate( newCameraPosition );
        starryTrans.Translate( newBackgroundPosition );
    }

    
}
