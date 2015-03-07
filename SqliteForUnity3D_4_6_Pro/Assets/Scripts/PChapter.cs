using UnityEngine;
using System.Collections;
using SqliteForUnity3D;

public class PChapter {
	
	[PrimaryKey]
	public string Id { get;set; }
	public string Name { get;set; }
	public bool Locked { get;set;}
	
	public override string ToString ()
	{
		return string.Format ("[PChapter: Id={0}, Name={1},Locked={2}]", Id, Name,Locked);
	}
	
	
}
