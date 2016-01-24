using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager_script : MonoBehaviour {

    [SerializeField]
    private Text profileNameObj;

    private bool gotName = false;

	void Start () 
	{
        DontDestroyOnLoad( gameObject );
	}
	
	void Update ()
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
	}
}
