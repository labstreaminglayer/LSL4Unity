using System.Collections;
using LSL4Unity.Scripts;
using UnityEngine;
using UnityEngine.Assertions; // Don't forget the Namespace import

namespace LSL4Unity.Demos
{
	public class RandomMarker : MonoBehaviour {

		public LSLMarkerStream markerStream;
	
		void Start () {

			Assert.IsNotNull(markerStream, "You forgot to assign the reference to a marker stream implementation!");

			if (markerStream != null)
				StartCoroutine(WriteContinouslyMarkerEachSecond());
		}
	
		IEnumerator WriteContinouslyMarkerEachSecond()
		{
			while (true)
			{
				// an example for demonstrating the usage of marker stream
				var currentMarker = GetARandomMarker();
				markerStream.Write(currentMarker);
				yield return new WaitForSecondsRealtime(1f);
			}
		}

		private string GetARandomMarker()
		{
			return UnityEngine.Random.value > 0.5 ? "A" : "B";
		}
	}
}
