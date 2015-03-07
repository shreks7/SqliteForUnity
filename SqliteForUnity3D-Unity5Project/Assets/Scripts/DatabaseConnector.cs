using UnityEngine;
using System.Collections;
using System.IO;
using SqliteForUnity3D;
using System.Collections.Generic;

public class DatabaseConnector {
	private ISQLiteConnection _connection;
	public bool databaseExist = false;

	[SerializeField]
	private static string[] levelNames = {"Moebius Blood",
	                             "Octagon Sanctum",
	                             "Liquid Punishment",
	                             "Final Discord",
	                             "Concrete Agony",
	                             "Commando Dissolution",
	                             "Freestyle Sanctum",
	                             "Swiss Cheese Shrine",
	                             "Wicked Doom",
	                             "Vengeful Hive",
	                             "Lava Vertigo",
	                             "Mouldy Old Woe",
	                             "Ghastly Devastation",
								 "Catacomb Labyrinth"};
	
	public DatabaseConnector(string DatabaseName){
		
		var factory = new ConnectionFactory();
		
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
		if (!File.Exists(filepath))
		{
			Debug.Log("Database not in Persistent path");
			// if it doesn't ->
			// open StreamingAssets directory and load the db ->
			
			#if UNITY_ANDROID 
			var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
			while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDb.bytes);
			#elif UNITY_IOS
			var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#elif UNITY_WP8
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			
			#elif UNITY_WINRT
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#endif
			
			Debug.Log("Database written");
		}
		
		var dbPath = filepath;
		#endif
		databaseExist = true;
		_connection = factory.Create(dbPath);
		Debug.Log("Final PATH: " + dbPath);     
		
	}

	public void SetKey(string key)
	{
		_connection.SetDbKey(key);
	}
	
	public void Key(string key)
	{
		_connection.Key(key);
	}
	
	public void CreateChapterTables(){
		_connection.DropTable<PChapter> ();
		_connection.CreateTable<PChapter> ();
		
		_connection.InsertAll (new[]{
			new PChapter{
				Id = GetRandomString(),
				Name = "Sunrise",
				Locked = false
			},
			new PChapter{
				Id = GetRandomString(),
				Name = "Dawn",
				Locked = true
			},
			new PChapter{
				Id = GetRandomString(),
				Name = "Dusk",
				Locked = true
			},
		});
		
		CreateLevelDB();
	}

	public static string GetRandomString()
	{
		string path = Path.GetRandomFileName();
		path = path.Replace(".", ""); // Remove period.
		return path;
	}
	
	public static string GetRandomLevelNames(){
		int index = Random.Range(0, levelNames.Length-1);
		return levelNames [index];
	}

	private void CreateLevelDB(){
		_connection.DropTable<PLevel> ();
		_connection.CreateTable<PLevel> ();
		
		IEnumerable<PChapter> chapters = GetAllChapters();
		foreach (PChapter chapter in chapters) {
			for(int i=1;i<=10;i++){
				_connection.Insert(
					new PLevel{
					Id = GetRandomString(),
					Name = levelNames[i],
					ChapterId = chapter.Id,
					Locked = true
				});
			}
		}
	}


	public IEnumerable<PChapter> GetAllChapters(){
		return _connection.Table<PChapter>();
	}
	
	public PChapter GetChapter(string name){
		return _connection.Table<PChapter> ().Where (x => x.Name == name).FirstOrDefault ();
	}
	
	public PLevel GetLevel(string chapterName, string levelName){
		PChapter _pchapter = GetChapter (chapterName);
		List<PLevel> levels = _connection.Query<PLevel>(string.Format("select * from PLevel where ChapterId = {0} AND Name = {1}",_pchapter.Id,levelName));
		return levels [0];
	}
	
	public IEnumerable<PLevel> GetAllLevelsInChapter(string chapterName){
		PChapter _pchapter = GetChapter (chapterName);
		return _connection.Table<PLevel>().Where(x => x.ChapterId == _pchapter.Id);
	}
	
	public IEnumerable<PLevel> GetAllLevelsInChapterById(string chapterId){
		return _connection.Table<PLevel>().Where(x => x.ChapterId == chapterId);
	}
	
	
	public void InsertChapter(PChapter p){
		_connection.Insert (p);
	}

}
