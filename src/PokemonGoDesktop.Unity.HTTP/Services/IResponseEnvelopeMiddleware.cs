using Networking.Envelopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGoDesktop.Unity.HTTP
{
	/// <summary>
	/// Contract for middleware that process a <see cref="ResponseEnvelope"/>.
	/// </summary>
	public interface IResponseEnvelopeMiddleware
	{
		/// <summary>
		/// Process logic on the provided <see cref="ResponseEnvelope"/>.
		/// </summary>
		/// <param name="envelope">Envelope to process.</param>
		void ProcessResponseEnvelope(ResponseEnvelope envelope);
	}
}
