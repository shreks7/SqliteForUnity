using UnityEngine;
using System.Collections;
using SqliteForUnity3D;

public class PLevel {
	
	[PrimaryKey]
	public string Id { get;set; }
	public string Name { get;set; }
	public string ChapterId { get; set;}
	public bool Locked { get;set;}

	
	public override string ToString ()
	{
		return string.Format ("[PLevel: Id={0}, Name={1}, ChapterId={2}, Locked={3}]", Id, Name,ChapterId,Locked);
	}
	
	
}
