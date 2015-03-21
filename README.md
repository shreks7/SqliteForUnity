# SqliteForUnity3D
Sqlite3 for Unity with AES encryption

After trying out alot of sqlite3 libs/plugins for Unity, I couldn't find any that supported encryption, was free and was easy to use. However there were some good ones out there that I forked and added encryption support to it.

# Can't Wait ?
Download Unity Packages - [Unity 5](https://github.com/shreks7/SqliteForUnity3D/blob/master/UnityPackages/SqliteForUnity3D.unitypackage) | [Unity 4.6 Pro](https://github.com/shreks7/SqliteForUnity3D/blob/master/UnityPackages/Sqlite3ForUnity3D46.unitypackage)

# Quick Facts: 
- SqliteForUnity provides sqlite support for Unity3D (5.0 & 4.6 Pro) with encryption
- Download project folders to try out the sample scenes [Unity 5](https://github.com/shreks7/SqliteForUnity3D/tree/master/SqliteForUnity3D-Unity5Project),[Unity 4.6 Pro](https://github.com/shreks7/SqliteForUnity3D/tree/master/SqliteForUnity3D_4_6_Pro)
- Support for x86/x64/Android
- Plugins folder contains SqliteForUnity3D.dll,libsqlite3.so(for Android),sqlite.dll(both x8enc6/x64) with encryption support


# Sample Scene - [Android]
![alt tag](https://root2games.appspot.com/file/git/sqliteA.png)
![alt tag](https://root2games.appspot.com/file/git/sqliteB.png)
![alt tag](https://root2games.appspot.com/file/git/sqliteC.png)

# Usage
Usage is similiar to sqlite-net & SQLite4Unity3d. Reiterating it here -
```
using UnityEngine;
using System.Collections;
using SqliteForUnity3D;

public class PStudent {
	[PrimaryKey]
	public string Id { get;set; }
	public string Name { get;set; }
}

//Creating Tables & Inserting
var factory = new ConnectionFactory();
.....
_connection = factory.Create(dbPath);
_connection.CreateTable<PStudent> ();
_connection.Insert(new PStudent{
    Id = "XYZ",
    Name = "John"
});

//This uses Linq Reflections , please avoid using on iOS
_connection.Table<PStudent> ().Where (x => x.Name == "John").FirstOrDefault ();
//Instead
string name = "John";
_connection.Query<PStudent>(string.Format("select * from PStudent where Name = {0}",name));
```

### Encryption
```
//To lock db
_connection.SetDbKey(key);
// To unlock db
_connection.Key(key);
```

# SqliteForUnity Source
SqliteForUnity is a fork of https://github.com/codecoding/SQLite4Unity3d by [@CodeCoding](https://github.com/codecoding). A wrapper around the great c# client for sqlite - **[sqlite-net](https://github.com/praeclarum/sqlite-net)** by @praeclarum

**x86/x64 Encryption**
The encryption is based on AES similiar to the one used by https://github.com/rindeal/SQLite3-Encryption (Infact you can use sqlite3.dll by @rindeal too).

**Android Encryption**
For Android I compiled the sqlite3 src with the encryption from @rindeal for both armeabi & x86 platform. I have included the source [here](https://github.com/shreks7/AndroidSqlite3Encrypted).

#TODO
1. To add support for Windows Phone 8.1/8
2. To test it on iOS


