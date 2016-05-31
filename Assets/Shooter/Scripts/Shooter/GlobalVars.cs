using UnityEngine;
using System.Collections;
namespace Shooter{
public class GlobalVars {
	public static string currentMap = "Shooter/Scenes/map_otherMap";
	public enum joinGameTypes {server,client,none};
	public static joinGameTypes joinGameType = joinGameTypes.none;
	
	public static bool isLocalPlayer = true;
	
	public static bool isServer = false;
}
}
