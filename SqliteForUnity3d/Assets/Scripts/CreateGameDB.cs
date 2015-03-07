using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CreateGameDB : MonoBehaviour {

	public string DBKey = "PLEASE ENTER DB KEY";
	public string DatabaseName = "game.db";
	
	private int newDB = 0;
	private DatabaseConnector dConnector;
	
	public Transform contentPanel;
	public GameObject levelButton;
	public GameObject chapterButton;

	public GameObject passwordPanel;
	public InputField password;
	public Button submitButton;
	public Button createTables;

	public bool resetDatabase = false;

	void Start () {

		dConnector = new DatabaseConnector(DatabaseName);

		if (resetDatabase) {
			PlayerPrefs.DeleteAll ();
		}

		newDB = PlayerPrefs.GetInt ("newDB");
		if (newDB == 0) {
			createTables.onClick.AddListener (() => {
				try{
					//Create tables
					dConnector.CreateChapterTables ();
					//Lock DB with key
					dConnector.SetKey (DBKey);
					//Flip NewDB
					newDB = 1;
					PlayerPrefs.SetInt ("newDB", newDB);
					createTables.enabled = false;
					createTables.image.color = new Color(1f,1f,1f,0.3f);

				}catch(Exception){
					//Database already exist
					createTables.enabled = false;
					createTables.image.color = new Color(1f,1f,1f,0.3f);
					newDB = 1;
					PlayerPrefs.SetInt ("newDB", newDB);

				}
			});
		} else {
			createTables.enabled = false;
			createTables.image.color = new Color(1f,1f,1f,0.3f);
		}

		submitButton.onClick.AddListener(()=>{
			string pass = password.text;
			if(pass == DBKey)
			{
				//Unlock Db with KEY
				dConnector.Key (DBKey);
				generateLevels();
				passwordPanel.SetActive(false);
			}
			else{
				password.text = "Invalid Password";
			}
		});

	}

	public void generateLevels(){
		IEnumerable<PChapter> chapters = dConnector.GetAllChapters();
		int i = 1;
		foreach(PChapter chapter in chapters){
			//Generate Chapters
			GameObject newButton = Instantiate (chapterButton) as GameObject;
			ChapterButton cButton = newButton.GetComponent<ChapterButton>();
			cButton.nameLabel.text = chapter.Name;
			cButton.lockedLabel.text = chapter.Locked?"Locked":"Unlocked";
			newButton.transform.SetParent(contentPanel);

			ToConsole (chapter);

			//Generate Levels
			int j=1;
			IEnumerable<PLevel> levels = dConnector.GetAllLevelsInChapterById(chapter.Id);
			foreach(PLevel level in levels){
				GameObject newLevelButton = Instantiate (levelButton) as GameObject;
				LevelButton lButton = newLevelButton.GetComponent<LevelButton>();
				lButton.nameLabel.text = level.Name;
				lButton.lockedLabel.text = level.Locked?"Locked":"Unlocked";
				lButton.levelNumber.text = "#"+j;
				j++;

				newLevelButton.transform.SetParent(contentPanel);
				ToConsole(level);
			}
			i++;
		}

	}
	
	private void ToConsole(object msg){
		Debug.Log (msg.ToString());
	}
	
	
}
