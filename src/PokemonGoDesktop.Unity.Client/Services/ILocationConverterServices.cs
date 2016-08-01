using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.Client.Services
{
	/// <summary>
	/// Conversion service that map lattitude and longitude to Unity3D coordinates
	/// and Unity3D coordinates to map latitude and longitude.
	/// </summary>
	public interface ILocationConverterServices
	{
		/// <summary>
		/// Converts engine x coordinate to the world latitude.
		/// </summary>
		/// <param name="x">X value.</param>
		/// <returns></returns>
		double ToWorldLatitude(float x);

		/// <summary>
		/// Converts engine y coordinate to the world longitude.
		/// </summary>
		/// <param name="y">X value.</param>
		/// <returns></returns>
		double ToWorldLongitude(float z);

		/// <summary>
		/// Converts real-world latitude to engine x coordinate.
		/// </summary>
		/// <param name="latitude">X value.</param>
		/// <returns></returns>
		float ToEngineXCoord(double latitude);

		/// <summary>
		/// Converts real-world longitude to engine x coordinate.
		/// </summary>
		/// <param name="longitude">X value.</param>
		/// <returns></returns>
		float ToEngineZCoord(float longitude);
	}
}
