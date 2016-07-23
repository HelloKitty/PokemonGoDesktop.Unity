using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// ResponseEnvelope middleware registry.
	/// </summary>
	public interface IResponseEnvelopeMiddlewareRegistry
	{
		/// <summary>
		/// Attempts to register a middleware.
		/// </summary>
		/// <param name="middleware">Middleware to register.</param>
		/// <returns>Indicates if the middleware has been registered sucessfully.</returns>
		bool RegisterMiddleware(IResponseEnvelopeMiddleware middleware);
	}
}
