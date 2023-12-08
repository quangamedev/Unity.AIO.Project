using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class JObjectSaveStrategy : ScriptableObject
{
	public abstract void SaveToFile(string saveFile, JObject State);

	public abstract JObject LoadFromFile(string saveFile);
	public abstract string GetExtension();

	public string GetPathFromSaveFile(string saveFile)
	{
		return Path.Combine(Application.persistentDataPath, saveFile + GetExtension());
	}
}