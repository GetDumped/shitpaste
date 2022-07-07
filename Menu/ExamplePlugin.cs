using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using GorillaLocomotion;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Utilla;

namespace ExamplePlugin;

[BepInPlugin("ghbh.poggers", "Poger mod", "1.0.0")]
[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
[ModdedGamemode]
[ModdedGamemode(/*Could not decode attribute arguments.*/)]
public class ExamplePlugin : BaseUnityPlugin
{
	public class HueShifter : MonoBehaviour
	{
		public float Speed = 0.03f;

		private Renderer rend;

		private void Start()
		{
			rend = ((Component)this).GetComponent<Renderer>();
		}

		private void Update()
		{
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			rend.get_material().SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.get_time() * Speed, 1f), 1f, 1f)));
		}
	}

	private class BtnCollider : MonoBehaviour
	{
		public string relatedText;

		private void OnTriggerEnter(Collider collider)
		{
			if (Time.get_frameCount() >= framePressCooldown + 30)
			{
				Toggle(relatedText);
				framePressCooldown = Time.get_frameCount();
			}
		}
	}

	private static string[] buttons = new string[25]
	{
		"Mod Stick Right [CS]", "Mod Stick Left [CS]", "Vibrator Right", "Vibrator Left", "Super Monke", "Beacons", "Teleport Gun", "Slip Control", "First Person Camera", "Fast Time",
		"Tag All [NEW]", "Claws [CS]", "Princess Wand [CS]", "Sparkler [CS]", "Candy Cane [CS]", "Administrator Badge [CS]", "Turkey Leg [CS]", ":clown: [CS]", "Server Lagger", "Turn Off Tag Freeze",
		"/EDP @EVERYONE", "Rock Game Spam", "Break Gamemode", "Ghost [D?]", "Invisible [D?]"
	};

	private static bool?[] buttonsActive = new bool?[26]
	{
		false, false, false, false, false, false, false, false, false, false,
		false, false, false, false, false, false, false, false, false, false,
		false, false, false, false, false, false
	};

	private static bool gripDown;

	private static Transform oldparent = null;

	private static int troll = 0;

	private static GameObject menu = null;

	private static GameObject canvasObj = null;

	private static bool gravityToggled = false;

	private static bool flying = false;

	private static GameObject reference = null;

	public static int framePressCooldown = 0;

	public static int currentPage = 0;

	private static bool verified = false;

	private static int funstuff = 60;

	private static GameObject pointer = null;

	private static int btnCooldown = 0;

	private static int pageSize = 4;

	private static int pageNumber = 0;

	private bool inAllowedRoom = true;

	private static void ProcessTeleportGun()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		bool flag2 = false;
		List<InputDevice> list = new List<InputDevice>();
		InputDevices.GetDevices(list);
		InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)324, list);
		InputDevice val = list[0];
		((InputDevice)(ref val)).TryGetFeatureValue(CommonUsages.triggerButton, ref flag);
		val = list[0];
		((InputDevice)(ref val)).TryGetFeatureValue(CommonUsages.gripButton, ref flag2);
		if (flag2)
		{
			RaycastHit val2 = default(RaycastHit);
			Physics.Raycast(Player.get_Instance().rightHandTransform.get_position() - Player.get_Instance().rightHandTransform.get_up(), -Player.get_Instance().rightHandTransform.get_up(), ref val2);
			if ((Object)(object)pointer == (Object)null)
			{
				pointer = GameObject.CreatePrimitive((PrimitiveType)0);
				Object.Destroy((Object)(object)pointer.GetComponent<Rigidbody>());
				Object.Destroy((Object)(object)pointer.GetComponent<SphereCollider>());
				pointer.get_transform().set_localScale(new Vector3(0.2f, 0.2f, 0.2f));
			}
			pointer.get_transform().set_position(((RaycastHit)(ref val2)).get_point());
			if (flag)
			{
				((Component)Player.get_Instance()).get_transform().set_position(((RaycastHit)(ref val2)).get_point());
				((Component)Player.get_Instance()).GetComponent<Rigidbody>().set_velocity(new Vector3(0f, 0f, 0f));
				if ((Object)(object)pointer != (Object)null)
				{
					Object.Destroy((Object)(object)pointer);
				}
			}
		}
		else
		{
			Object.Destroy((Object)(object)pointer);
			pointer = null;
		}
	}

	private void Update()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df8: Unknown result type (might be due to invalid IL or missing references)
		if (!inAllowedRoom)
		{
			return;
		}
		GameObject val = GameObject.Find("Shoulder Camera");
		Camera val2 = (((Object)(object)val != (Object)null) ? val.GetComponent<Camera>() : null);
		List<InputDevice> list = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, list);
		InputDevice val3 = list[0];
		((InputDevice)(ref val3)).TryGetFeatureValue(CommonUsages.secondaryButton, ref gripDown);
		if (gripDown && (Object)(object)menu == (Object)null)
		{
			Draw();
			if ((Object)(object)reference == (Object)null)
			{
				reference = GameObject.CreatePrimitive((PrimitiveType)0);
				Object.Destroy((Object)(object)reference.GetComponent<MeshRenderer>());
				reference.get_transform().set_parent(Player.get_Instance().rightHandTransform);
				reference.get_transform().set_localPosition(new Vector3(0f, -0.1f, 0f));
				reference.get_transform().set_localScale(new Vector3(0.01f, 0.01f, 0.01f));
			}
		}
		else if (!gripDown && (Object)(object)menu != (Object)null)
		{
			Object.Destroy((Object)(object)menu);
			menu = null;
			Object.Destroy((Object)(object)reference);
			reference = null;
		}
		if (gripDown && (Object)(object)menu != (Object)null)
		{
			menu.get_transform().set_position(Player.get_Instance().leftHandTransform.get_position());
			menu.get_transform().set_rotation(Player.get_Instance().leftHandTransform.get_rotation());
		}
		if (btnCooldown > 0 && Time.get_frameCount() > btnCooldown)
		{
			btnCooldown = 0;
			buttonsActive[20] = false;
			Object.Destroy((Object)(object)menu);
			menu = null;
			Draw();
		}
		if (buttonsActive[0] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/MOD STICKRIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/MOD STICKRIGHT.").SetActive(false);
		}
		if (buttonsActive[1] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/MOD STICKLEFT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/MOD STICKLEFT.").SetActive(false);
		}
		if (buttonsActive[2] == true)
		{
			GorillaTagger.get_Instance().StartVibration(false, GorillaTagger.get_Instance().tagHapticStrength * 100f, GorillaTagger.get_Instance().tagHapticDuration);
		}
		if (buttonsActive[3] == true)
		{
			GorillaTagger.get_Instance().StartVibration(true, GorillaTagger.get_Instance().tagHapticStrength * 100f, GorillaTagger.get_Instance().tagHapticDuration);
		}
		if (buttonsActive[4] == true)
		{
			bool flag = false;
			bool flag2 = false;
			list = new List<InputDevice>();
			InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, list);
			val3 = list[0];
			((InputDevice)(ref val3)).TryGetFeatureValue(CommonUsages.primaryButton, ref flag);
			val3 = list[0];
			((InputDevice)(ref val3)).TryGetFeatureValue(CommonUsages.secondaryButton, ref flag2);
			if (flag)
			{
				Transform transform = ((Component)Player.get_Instance()).get_transform();
				transform.set_position(transform.get_position() + ((Component)Player.get_Instance().headCollider).get_transform().get_forward() * Time.get_deltaTime() * 16f);
				((Component)Player.get_Instance()).GetComponent<Rigidbody>().set_velocity(Vector3.get_zero());
				if (!flying)
				{
					flying = true;
				}
			}
			else if (flying)
			{
				((Component)Player.get_Instance()).GetComponent<Rigidbody>().set_velocity(((Component)Player.get_Instance().headCollider).get_transform().get_forward() * Time.get_deltaTime() * 36f);
				flying = false;
			}
			if (flag2)
			{
				if (!gravityToggled && ((Collider)Player.get_Instance().bodyCollider).get_attachedRigidbody().get_useGravity())
				{
					((Collider)Player.get_Instance().bodyCollider).get_attachedRigidbody().set_useGravity(false);
					gravityToggled = true;
				}
				else if (!gravityToggled && !((Collider)Player.get_Instance().bodyCollider).get_attachedRigidbody().get_useGravity())
				{
					((Collider)Player.get_Instance().bodyCollider).get_attachedRigidbody().set_useGravity(true);
					gravityToggled = true;
				}
			}
			else
			{
				gravityToggled = false;
			}
		}
		if (buttonsActive[5] == true)
		{
			VRRig[] array = (VRRig[])(object)Object.FindObjectsOfType(typeof(VRRig));
			VRRig[] array2 = array;
			foreach (VRRig val4 in array2)
			{
				if (!val4.isOfflineVRRig && !val4.isMyPlayer && !((MonoBehaviourPun)val4).get_photonView().get_IsMine())
				{
					GameObject val5 = GameObject.CreatePrimitive((PrimitiveType)2);
					Object.Destroy((Object)(object)val5.GetComponent<BoxCollider>());
					Object.Destroy((Object)(object)val5.GetComponent<Rigidbody>());
					Object.Destroy((Object)(object)val5.GetComponent<Collider>());
					val5.get_transform().set_rotation(Quaternion.get_identity());
					val5.get_transform().set_localScale(new Vector3(0.04f, 200f, 0.04f));
					val5.get_transform().set_position(((Component)val4).get_transform().get_position());
					((Renderer)val5.GetComponent<MeshRenderer>()).set_material(((Renderer)val4.mainSkin).get_material());
					Object.Destroy((Object)(object)val5, Time.get_deltaTime());
				}
			}
		}
		if (buttonsActive[6] == true)
		{
			ProcessTeleportGun();
		}
		if (buttonsActive[7] == true)
		{
			Player.get_Instance().unStickDistance = 5f;
			Player.get_Instance().slideControl = 1f;
		}
		else
		{
			Player.get_Instance().unStickDistance = 1f;
			Player.get_Instance().slideControl = 0.00425f;
		}
		if (buttonsActive[8] == true)
		{
			((Behaviour)val2).set_enabled(false);
		}
		else
		{
			((Behaviour)val2).set_enabled(true);
		}
		if (buttonsActive[9] == true)
		{
			Time.set_timeScale(2f);
		}
		else
		{
			Time.set_timeScale(1f);
		}
		if (buttonsActive[10] == true)
		{
			Vector3 position = ((Component)Player.get_Instance()).get_transform().get_position();
			VRRig[] array3 = (VRRig[])(object)Object.FindObjectsOfType(typeof(VRRig));
			VRRig[] array4 = array3;
			foreach (VRRig val6 in array4)
			{
				if (!val6.isOfflineVRRig && !val6.isMyPlayer && !((MonoBehaviourPun)val6).get_photonView().get_IsMine())
				{
					Player myPlayer = val6.myPlayer;
					((Component)Player.get_Instance()).get_transform().set_position(((Component)val6).get_transform().get_position());
					((Component)Player.get_Instance()).GetComponent<Rigidbody>().set_velocity(new Vector3(0f, 0f, 0f));
					PhotonView.Get((Component)(object)((Component)GorillaGameManager.instance).GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", (RpcTarget)2, new object[1] { myPlayer });
				}
			}
		}
		if (buttonsActive[11] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/f_index.01.R/f_index.02.R/f_index.03.R/f_index.03.R_end/WEREWOLF CLAWSLEFT.").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.02.R/f_middle.01.R/f_middle.02.R/f_middle.03.R/f_middle.03.R_end/WEREWOLF CLAWSRIGHT.").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/thumb.01.R/thumb.02.R/thumb.03.R/thumb.03.R_end/WEREWOLF CLAWSLEFT.").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/f_index.01.L/f_index.02.L/f_index.03.L/f_index.03.L_end/WEREWOLF CLAWSLEFT.").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/thumb.01.L/thumb.02.L/thumb.03.L/thumb.03.L_end/WEREWOLF CLAWSRIGHT.").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.02.L/f_middle.01.L/f_middle.02.L/f_middle.03.L/f_middle.03.L_end/WEREWOLF CLAWSRIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/f_index.01.R/f_index.02.R/f_index.03.R/f_index.03.R_end/WEREWOLF CLAWSLEFT.").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.02.R/f_middle.01.R/f_middle.02.R/f_middle.03.R/f_middle.03.R_end/WEREWOLF CLAWSRIGHT.").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/thumb.01.R/thumb.02.R/thumb.03.R/thumb.03.R_end/WEREWOLF CLAWSLEFT.").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/f_index.01.L/f_index.02.L/f_index.03.L/f_index.03.L_end/WEREWOLF CLAWSLEFT.").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/thumb.01.L/thumb.02.L/thumb.03.L/thumb.03.L_end/WEREWOLF CLAWSRIGHT.").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.02.L/f_middle.01.L/f_middle.02.L/f_middle.03.L/f_middle.03.L_end/WEREWOLF CLAWSRIGHT.").SetActive(false);
		}
		if (buttonsActive[12] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/STAR PRINCESS WANDRIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/STAR PRINCESS WANDRIGHT.").SetActive(false);
		}
		if (buttonsActive[13] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/SPARKLERRIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/SPARKLERRIGHT.").SetActive(false);
		}
		if (buttonsActive[14] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/CANDY CANERIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/CANDY CANERIGHT.").SetActive(false);
		}
		if (buttonsActive[15] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/ADMINISTRATOR BADGE").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/ADMINISTRATOR BADGE").SetActive(false);
		}
		if (buttonsActive[16] == true)
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TURKEY LEGRIGHT.").SetActive(true);
		}
		else
		{
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TURKEY LEGRIGHT.").SetActive(false);
		}
		if (buttonsActive[17] == true)
		{
			GameObject.Find("Player/GorillaPlayer/TurnParent/Main Camera/Cosmetics/CLOWN WIG").SetActive(true);
			GameObject.Find("Player/GorillaPlayer/TurnParent/Main Camera/Cosmetics/CLOWN NOSE").SetActive(true);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/CLOWN FRILL").SetActive(true);
		}
		else
		{
			GameObject.Find("Player/GorillaPlayer/TurnParent/Main Camera/Cosmetics/CLOWN WIG").SetActive(false);
			GameObject.Find("Player/GorillaPlayer/TurnParent/Main Camera/Cosmetics/CLOWN NOSE").SetActive(false);
			GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/CLOWN FRILL").SetActive(false);
		}
		if (buttonsActive[18] == true)
		{
			PhotonNetwork.Instantiate(Path.Combine("gorillaprefabs", "Gorilla Player Actual"), Player.get_Instance().leftHandTransform.get_position(), Player.get_Instance().leftHandTransform.get_rotation(), (byte)0, (object[])null);
		}
		if (buttonsActive[19] == true)
		{
			Player.get_Instance().disableMovement = false;
		}
		if (buttonsActive[20] == true)
		{
		}
		if (buttonsActive[21] == true)
		{
			GorillaGameManager[] array5 = Object.FindObjectsOfType<GorillaGameManager>();
			foreach (GorillaGameManager val7 in array5)
			{
				((MonoBehaviourPun)val7).get_photonView().RequestOwnership();
				PhotonNetwork.Destroy(((MonoBehaviourPun)val7).get_photonView());
			}
		}
		if (buttonsActive[22] == true)
		{
			GorillaGameManager[] array6 = Object.FindObjectsOfType<GorillaGameManager>();
			foreach (GorillaGameManager val8 in array6)
			{
				PhotonNetwork.Destroy(((MonoBehaviourPun)val8).get_photonView());
			}
		}
		if (buttonsActive[23] == true)
		{
			((Behaviour)GorillaTagger.get_Instance().myVRRig).set_enabled(false);
		}
		else
		{
			((Behaviour)GorillaTagger.get_Instance().myVRRig).set_enabled(true);
		}
		if (buttonsActive[24] == true)
		{
			((Component)GorillaTagger.get_Instance().myVRRig).get_transform().set_position(new Vector3(100f, 100f, 100f));
			((Behaviour)GorillaTagger.get_Instance().myVRRig).set_enabled(false);
		}
		else
		{
			((Behaviour)GorillaTagger.get_Instance().myVRRig).set_enabled(true);
		}
	}

	public static VRRig FindVRRigForPlayer(Player player)
	{
		foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
		{
			if (!vrrig.isOfflineVRRig && ((Component)vrrig).GetComponent<PhotonView>().get_Owner() == player)
			{
				return vrrig;
			}
		}
		return null;
	}

	private static void AddPageButtons(float n)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		int num = (buttons.Length + pageSize - 1) / pageSize;
		int num2 = pageNumber + 1;
		int num3 = pageNumber - 1;
		if (num2 > num - 1)
		{
			num2 = 0;
		}
		if (num3 < 0)
		{
			num3 = num - 1;
		}
		float num4 = 0f;
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
		((Collider)val.GetComponent<BoxCollider>()).set_isTrigger(true);
		val.get_transform().set_parent(menu.get_transform());
		val.get_transform().set_rotation(Quaternion.get_identity());
		val.get_transform().set_localScale(new Vector3(0.09f, 0.8f, 0.08f));
		val.AddComponent<HueShifter>();
		val.get_transform().set_localPosition(new Vector3(0.56f, 0f, -0.227499992f));
		val.AddComponent<BtnCollider>().relatedText = "NextPage";
		GameObject val2 = new GameObject();
		val2.get_transform().set_parent(canvasObj.get_transform());
		Text val3 = val2.AddComponent<Text>();
		_003F val4 = val3;
		Object builtinResource = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		((Text)val4).set_font((Font)(object)((builtinResource is Font) ? builtinResource : null));
		val3.set_text("Next Page");
		val3.set_fontSize(1);
		val3.set_alignment((TextAnchor)4);
		val3.set_resizeTextForBestFit(true);
		val3.set_resizeTextMinSize(0);
		RectTransform component = ((Component)val3).GetComponent<RectTransform>();
		((Transform)component).set_localPosition(Vector3.get_zero());
		component.set_sizeDelta(new Vector2(0.2f, 0.03f));
		float num5 = 0.52f;
		((Transform)component).set_localPosition(new Vector3(0.064f, 0f, 0.111f - num5 / 2.55f));
		((Transform)component).set_rotation(Quaternion.Euler(new Vector3(180f, 90f, 90f)));
		num4 = 0.13f;
		GameObject val5 = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)val5.GetComponent<Rigidbody>());
		((Collider)val5.GetComponent<BoxCollider>()).set_isTrigger(true);
		val5.get_transform().set_parent(menu.get_transform());
		val5.get_transform().set_rotation(Quaternion.get_identity());
		val5.get_transform().set_localScale(new Vector3(0.09f, 0.8f, 0.08f));
		val5.AddComponent<HueShifter>();
		val5.get_transform().set_localPosition(new Vector3(0.56f, 0f, -0.3575f));
		val5.AddComponent<BtnCollider>().relatedText = "PreviousPage";
		GameObject val6 = new GameObject();
		val6.get_transform().set_parent(canvasObj.get_transform());
		Text val7 = val6.AddComponent<Text>();
		_003F val8 = val7;
		Object builtinResource2 = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		((Text)val8).set_font((Font)(object)((builtinResource2 is Font) ? builtinResource2 : null));
		val7.set_text("Previous Page");
		val7.set_fontSize(1);
		val7.set_alignment((TextAnchor)4);
		val7.set_resizeTextForBestFit(true);
		val7.set_resizeTextMinSize(0);
		RectTransform component2 = ((Component)val7).GetComponent<RectTransform>();
		((Transform)component2).set_localPosition(Vector3.get_zero());
		component2.set_sizeDelta(new Vector2(0.2f, 0.03f));
		num5 = 0.65f;
		((Transform)component2).set_localPosition(new Vector3(0.064f, 0f, 0.111f - num5 / 2.55f));
		((Transform)component2).set_rotation(Quaternion.Euler(new Vector3(180f, 90f, 90f)));
	}

	private static void AddButton(float offset, string text)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
		((Collider)val.GetComponent<BoxCollider>()).set_isTrigger(true);
		val.get_transform().set_parent(menu.get_transform());
		val.get_transform().set_rotation(Quaternion.get_identity());
		val.get_transform().set_localScale(new Vector3(0.09f, 0.8f, 0.08f));
		val.get_transform().set_localPosition(new Vector3(0.56f, 0f, 0.28f - offset));
		val.AddComponent<BtnCollider>().relatedText = text;
		int num = -1;
		for (int i = 0; i < buttons.Length; i++)
		{
			if (text == buttons[i])
			{
				num = i;
				break;
			}
		}
		GameObject val2 = new GameObject();
		val2.get_transform().set_parent(canvasObj.get_transform());
		Text val3 = val2.AddComponent<Text>();
		_003F val4 = val3;
		Object builtinResource = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		((Text)val4).set_font((Font)(object)((builtinResource is Font) ? builtinResource : null));
		val3.set_text(text);
		val3.set_fontSize(1);
		val3.set_alignment((TextAnchor)4);
		val3.set_resizeTextForBestFit(true);
		val3.set_resizeTextMinSize(0);
		RectTransform component = ((Component)val3).GetComponent<RectTransform>();
		((Transform)component).set_localPosition(Vector3.get_zero());
		component.set_sizeDelta(new Vector2(0.2f, 0.03f));
		((Transform)component).set_localPosition(new Vector3(0.064f, 0f, 0.111f - offset / 2.55f));
		((Transform)component).set_rotation(Quaternion.Euler(new Vector3(180f, 90f, 90f)));
		if (buttonsActive[num] == false)
		{
			val.GetComponent<Renderer>().get_material().SetColor("_Color", Color.get_white());
			((Graphic)val3).set_color(Color.get_black());
		}
		else if (buttonsActive[num] == true)
		{
			val.GetComponent<Renderer>().get_material().SetColor("_Color", Color.get_black());
			((Graphic)val3).set_color(Color.get_white());
		}
		else
		{
			val.GetComponent<Renderer>().get_material().SetColor("_Color", Color.get_grey());
		}
	}

	public static void Toggle(string relatedText)
	{
		int num = (buttons.Length + pageSize - 1) / pageSize;
		if (relatedText == "NextPage")
		{
			if (pageNumber < num - 1)
			{
				pageNumber++;
			}
			else
			{
				pageNumber = 0;
			}
			Object.Destroy((Object)(object)menu);
			menu = null;
			Draw();
			return;
		}
		if (relatedText == "PreviousPage")
		{
			if (pageNumber > 0)
			{
				pageNumber--;
			}
			else
			{
				pageNumber = num - 1;
			}
			Object.Destroy((Object)(object)menu);
			menu = null;
			Draw();
			return;
		}
		int num2 = -1;
		for (int i = 0; i < buttons.Length; i++)
		{
			if (relatedText == buttons[i])
			{
				num2 = i;
				break;
			}
		}
		if (buttonsActive[num2].HasValue)
		{
			buttonsActive[num2] = !buttonsActive[num2];
			Object.Destroy((Object)(object)menu);
			menu = null;
			Draw();
		}
	}

	public static void Draw()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		menu = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)menu.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)menu.GetComponent<BoxCollider>());
		Object.Destroy((Object)(object)menu.GetComponent<Renderer>());
		menu.get_transform().set_localScale(new Vector3(0.1f, 0.3f, 0.4f));
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)val.GetComponent<BoxCollider>());
		val.get_transform().set_parent(menu.get_transform());
		val.get_transform().set_rotation(Quaternion.get_identity());
		val.get_transform().set_localScale(new Vector3(0.1f, 1f, 1f));
		val.AddComponent<HueShifter>();
		val.get_transform().set_position(new Vector3(0.05f, 0f, -0.04f));
		canvasObj = new GameObject();
		canvasObj.get_transform().set_parent(menu.get_transform());
		Canvas val2 = canvasObj.AddComponent<Canvas>();
		CanvasScaler val3 = canvasObj.AddComponent<CanvasScaler>();
		canvasObj.AddComponent<GraphicRaycaster>();
		val2.set_renderMode((RenderMode)2);
		val3.set_dynamicPixelsPerUnit(1000f);
		GameObject val4 = new GameObject();
		val4.get_transform().set_parent(canvasObj.get_transform());
		Text val5 = val4.AddComponent<Text>();
		_003F val6 = val5;
		Object builtinResource = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		((Text)val6).set_font((Font)(object)((builtinResource is Font) ? builtinResource : null));
		val5.set_text("Shibex Premium Menu");
		val5.set_fontSize(1);
		((Graphic)val5).set_color(Color.get_white());
		val5.set_alignment((TextAnchor)4);
		val5.set_resizeTextForBestFit(true);
		val5.set_resizeTextMinSize(0);
		RectTransform component = ((Component)val5).GetComponent<RectTransform>();
		((Transform)component).set_localPosition(Vector3.get_zero());
		component.set_sizeDelta(new Vector2(0.28f, 0.05f));
		((Transform)component).set_position(new Vector3(0.06f, 0f, 0.175f));
		((Transform)component).set_rotation(Quaternion.Euler(new Vector3(180f, 90f, 90f)));
		string[] array = buttons.Skip(pageNumber * pageSize).Take(pageSize).ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			AddButton((float)i * 0.13f, array[i]);
		}
		AddPageButtons((float)array.Length * 0.13f);
	}

	[ModdedGamemodeJoin]
	private void RoomJoined(string gamemode)
	{
		inAllowedRoom = true;
	}

	[ModdedGamemodeLeave]
	private void RoomLeft(string gamemode)
	{
		inAllowedRoom = true;
	}
}
