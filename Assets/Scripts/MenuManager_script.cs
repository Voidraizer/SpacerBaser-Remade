using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager_script : MonoBehaviour {

    private const int MAINMENU = 0;
    private const int STORYMENU = 1;
    private const int CUSTOMMENU = 2;
    private const int PROFILEMENU = 3;
    private const int OPTIONSMENU = 4;

    [SerializeField]
    private Text profileNameObj;
    [SerializeField]
    private Text astCountObj;
    [SerializeField]
    private Slider astCountSlider;
    [SerializeField]
    private Text astRichObj;
    [SerializeField]
    private Slider astRichSlider;
    [SerializeField]
    private Text astBeltObj;
    [SerializeField]
    private Slider astBeltSlider;

    [SerializeField]
    private GameObject[] MenuSlides;

    private float menuTransitionSpeed = Screen.width * 2.5f;

    private bool gotName = false;

    private int ActiveMenu = 0;

    void Awake()
    {
        GameObject temp = GameObject.Find( "MenuManager" );
        if( ( temp != null ) && ( temp != gameObject ) )
        {
            Destroy( gameObject );
        }
    }

    void Start()
    {
        DontDestroyOnLoad( gameObject );

        profileNameObj.text = "Void!";

        if( profileNameObj != null )
        {
            gotName = true;
        }
    }

    void Update()
    {
        if( !gotName )
        {
            if( SceneManager.GetActiveScene().name == "menu" )
            {
                profileNameObj = GameObject.Find( "ProfileNameText" ).GetComponent<Text>();
                profileNameObj.text = "Void!";

                if( profileNameObj != null )
                {
                    gotName = true;
                }
            }
        }

        if( ActiveMenu == CUSTOMMENU )
        {
            astCountObj.text = astCountSlider.value.ToString();
            astRichObj.text = Mathf.Round( astRichSlider.value * 100f ).ToString() + "%";
            switch( astBeltSlider.value.ToString() )
            {
                case "0":
                    astBeltObj.text = "Horizontal Belt";
                    break;
                case "1":
                    astBeltObj.text = "Angled Belt";
                    break;
                case "2":
                    astBeltObj.text = "Vertical Belt";
                    break;
                case "3":
                    astBeltObj.text = "Ring Belt";
                    break;
                default:
                    break;
            }
        }
    }

    private void Menu2MenuTransition( int incomingMenu )
    {
        menuTransitionSpeed = Screen.width * 2.5f;
        StartCoroutine( M2MTransitionIn( incomingMenu ) );
        StartCoroutine( M2MTransitionOut( ActiveMenu ) );
    }

    private IEnumerator M2MTransitionOut( int outboundMenu )
    {
        bool lerping = true;
        //float start = Time.time;
        Vector2 startPos = MenuSlides[outboundMenu].transform.position;
        Vector2 goal = new Vector2( startPos.x - Screen.width, startPos.y );
        float timeDiff = 0f;
        float totalDistance = Vector2.Distance( startPos, goal );
        while( lerping )
        {
            timeDiff += Time.deltaTime;
            float distanceTravelled = timeDiff * menuTransitionSpeed;

            Vector2 pos = Vector2.Lerp( startPos, goal, distanceTravelled / totalDistance);
            MenuSlides[outboundMenu].transform.position = pos;
            //MenuSlides[currentMenu].transform.Translate( -menuTransitionSpeed, 0f, 0f );
            yield return new WaitForEndOfFrame();
            if( ( distanceTravelled / totalDistance ) >= 1f )
            {
                lerping = false;
            }
        }
    }
    private IEnumerator M2MTransitionIn( int inboundMenu )
    {
        bool lerping = true;
        //float start = Time.time;
        Vector2 startPos = new Vector2( MenuSlides[ActiveMenu].transform.position.x + Screen.width, MenuSlides[ActiveMenu].transform.position.y );
        MenuSlides[inboundMenu].transform.position = startPos;
        Vector2 goal = new Vector2( startPos.x - Screen.width, startPos.y );
        float timeDiff = 0f;
        float totalDistance = Vector2.Distance( startPos, goal );
        yield return new WaitForSeconds( 0.05f );
        while( lerping )
        {
            timeDiff += Time.deltaTime;
            float distanceTravelled = timeDiff * menuTransitionSpeed;

            Vector2 pos = Vector2.Lerp( startPos, goal, distanceTravelled / totalDistance );
            MenuSlides[inboundMenu].transform.position = pos;
            //MenuSlides[currentMenu].transform.Translate( -menuTransitionSpeed, 0f, 0f );
            yield return new WaitForEndOfFrame();
            if( ( distanceTravelled / totalDistance ) >= 1f )
            {
                lerping = false;
                ActiveMenu = inboundMenu;
            }
        }
    }


   /*****************************************************************************
    *                             MAIN MENU BUTTONS                             *
    *****************************************************************************/

    public void Main_StoryButton()
    {
        if( ActiveMenu == MAINMENU )
        {
            Menu2MenuTransition( STORYMENU );
        }
    }

    public void Main_CustomButton()
    {
        if( ActiveMenu == MAINMENU )
        {
            Menu2MenuTransition( CUSTOMMENU );
        }
    }

    public void Main_ProfileButton()
    {
        if( ActiveMenu == MAINMENU )
        {

        }
    }

    public void Main_OptionsButton()
    {
        if( ActiveMenu == MAINMENU )
        {

        }
    }

    public void Main_ExitButton()
    {
        Application.Quit();
    }



    /*****************************************************************************
     *                            STORY MENU BUTTONS                             *
     *****************************************************************************/

    public void Story_Mission1()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission2()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission3()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission4()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission5()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission6()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission7()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission8()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Mission9()
    {
        if( ActiveMenu == STORYMENU )
        {

        }
    }

    public void Story_Back()
    {
        if( ActiveMenu == STORYMENU )
        {
            Menu2MenuTransition( MAINMENU );
        }
    }

    /*****************************************************************************
     *                           CUSTOM MENU BUTTONS                             *
     *****************************************************************************/

    public void Custom_Start()
    {
        if( ActiveMenu == CUSTOMMENU )
        {
            
        }
    }

    public void Custom_Back()
    {
        if( ActiveMenu == CUSTOMMENU )
        {
            Menu2MenuTransition( MAINMENU );
        }
    }

    /*****************************************************************************
     *                          PROFILE MENU BUTTONS                             *
     *****************************************************************************/











    /*****************************************************************************
     *                          OPTIONS MENU BUTTONS                             *
     *****************************************************************************/
}
