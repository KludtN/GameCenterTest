using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

public class test : MonoBehaviour {

    static ILeaderboard m_Leaderboard;

    public string leaderboardID = "dk.gearworks.gamecentertest";

    public string leaderboardName = "02";
    public string achievement1Name = "A01";

	List <IScore> listscores = new List<IScore> ();
	string result = "hans";
	int point;
	IScore testSCore;


	// Use this for initialization
	void Start () {



        // AUTHENTICATE AND REGISTER A ProcessAuthentication CALLBACK
        // THIS CALL NEEDS OT BE MADE BEFORE WE CAN PROCEED TO OTHER CALLS IN THE Social API
        //Social.localUser.Authenticate(ProcessAuthentication);

		Debug.Log ("start");

        // GET INSTANCE OF LEADERBOARD
        //DoLeaderboard();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    ///////////////////////////////////////////////////
    // INITAL AUTHENTICATION (MUST BE DONE FIRST)
    ///////////////////////////////////////////////////

    // THIS FUNCTION GETS CALLED WHEN AUTHENTICATION COMPLETES
    // NOTE THAT IF THE OPERATION IS SUCCESSFUL Social.localUser WILL CONTAIN DATA FROM THE GAME CENTER SERVER
    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("Authenticated, checking achievements");
        

            // MAKE REQUEST TO GET LOADED ACHIEVEMENTS AND REGISTER A CALLBACK FOR PROCESSING THEM
            //Social.LoadAchievements(ProcessLoadedAchievements); // ProcessLoadedAchievements FUNCTION CAN BE FOUND BELOW


			DoLeaderboard();

			Debug.Log (leaderboardName);

            Social.LoadScores(leaderboardName, scores =>
            {
                if (scores.Length > 0)
                {

                    // SHOW THE SCORES RECEIVED
                    Debug.Log("Received " + scores.Length + " scores");

					testSCore = scores[0];
                    string myScores = "Leaderboard: \n";
                    foreach (IScore score in scores)
					{
						result += score.formattedValue+ System.Environment.NewLine;
						listscores.Add(score);
                        myScores += "\t" + "id: " + score.userID + " score: " + score.value + " date: " + score.date + "\n";

						point = (int)scores[0].value;

                        Debug.Log(myScores);
					}

                }
                else
                    Debug.Log("No scores have been loaded.");
            });
        }
        else
            Debug.Log("Failed to authenticate with Game Center.");
    }


    void OnGUI()
    {


		// COLUMN 2
		// ENABLE ACHIEVEMENT 1
		if(testSCore != null)
		{
				//string tex = GUI.TextField (new Rect (225, 300, 200, 75), );

			string tex = GUI.TextField (new Rect (225, 300, 200, 75), result);

		}	
			// COLUMN 1
        // SHOW LEADERBOARDS WITHIN GAME CENTER
        if (GUI.Button(new Rect(20, 20, 200, 75), "View Leaderboard"))
        {
            Social.ShowLeaderboardUI();


        }


        // COLUMN 2
        // ENABLE ACHIEVEMENT 1
        if (GUI.Button(new Rect(225, 20, 200, 75), "Report Achievement 1"))
	        {
            ReportAchievement(achievement1Name, 100.00);
		
        }
		if(GUI.Button(new Rect(20,100,200,75),"add score"))
	        {
		
			Debug.Log(point);
			point++;
			}
	
		if(GUI.Button(new Rect(20,200,200,75),"save score"))
		{
			Debug.Log("send to " + leaderboardID);

			Social.ReportScore(point,leaderboardName,sucess =>{Debug.Log(sucess?"reported score":"Failed score");});

				                   
		}


		if (GUI.Button (new Rect (20, 300, 200, 75), "Connect")) {
			//Social.localUser.Authenticate(success => {Debug.Log("Success");});

        	Social.localUser.Authenticate(ProcessAuthentication);
				
		}
		
		if (GUI.Button (new Rect (20, 380, 200, 75), "Do leaderboards")) 
        	DoLeaderboard();
						

		
	}
	
	
	void ReportAchievement(string achievementId, double progress)
	{
        Social.ReportProgress(achievementId, progress, (result) =>
        {
            Debug.Log(result ? string.Format("Successfully reported achievement {0}", achievementId)
                      : string.Format("Failed to report achievement {0}", achievementId));
        });
    }

    // THIS FUNCTION GETS CALLED WHEN THE LoadAchievements CALL COMPLETES
    void ProcessLoadedAchievements(IAchievement[] achievements)
    {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");

        // You can also call into the functions like this
        Social.ReportProgress("Achievement01", 100.0, result =>
        {
            if (result)
                Debug.Log("Successfully reported achievement progress");
            else
                Debug.Log("Failed to report achievement");
        });
        //Social.ShowAchievementsUI();
    }


    void DoLeaderboard()
    {
		m_Leaderboard = Social.CreateLeaderboard ();
		Debug.Log (m_Leaderboard);
		Debug.Log (m_Leaderboard.id);
        //m_Leaderboard = Social.CreateLeaderboard();
		m_Leaderboard.id = leaderboardName;//leaderboardID;  // YOUR CUSTOM LEADERBOARD NAME
	
        m_Leaderboard.LoadScores(result => DidLoadLeaderboard(result));
		Debug.Log (m_Leaderboard.id);
    }

    void DidLoadLeaderboard(bool result)
    {
        Debug.Log("Received " + m_Leaderboard.scores.Length + " scores");
        foreach (IScore score in m_Leaderboard.scores)
        {
			Debug.Log (score);
		}
		//Social.ShowLeaderboardUI();
    }



}
