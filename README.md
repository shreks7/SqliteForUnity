# SqliteForUnity3D
Sqlite3 for Unity3d with AES encryption

After trying out alot of sqlite3 libs/plugins for Unity3D, I couldn't find any that supported encryption, was free and was easy to use. However there were some good ones out there that I forked and added encrption support to it.

Quick Facts: 
- SqliteForUnity3D provides sqlite support for Unity3D (5.0 & 4.6 Pro) with encryption
- Download project folders to try out the [sample scenes]()
- Support for x86/x64/Android
- Plugins folder contains SqliteForUnity3D.dll,libsqlite3.so(for Android),sqlite.dll(both x86/x64) with encrpytion support


# Sample Scene - [Android]
![alt tag](https://root2games.appspot.com/file/git/sqliteA.png)
![alt tag](https://root2games.appspot.com/file/git/sqliteB.png)
![alt tag](https://root2games.appspot.com/file/git/sqliteC.png)

# SqliteForUnity3D Plugins
SqliteForUnity3D is a fork of https://github.com/codecoding/SQLite4Unity3d by [@CodeCoding](https://github.com/codecoding). Its a small wrapper around the great c# library **[sqlite-net](https://github.com/praeclarum/sqlite-net)** by @praeclarum

**x86/x64 Encryption**
The encryption is based on AES similiar to the one used by https://github.com/rindeal/SQLite3-Encryption (Infact you can use sqlite3.dll by @rindeal too).

*Android Encryption**
For Android I compiled the sqlite3 src with the encrypton from @rindeal for both armeabi & x886 platform. I have included the source for them [here]().

#TODO
1. To add support for Windows Phone 8.1/8
2. To test it on iOS


