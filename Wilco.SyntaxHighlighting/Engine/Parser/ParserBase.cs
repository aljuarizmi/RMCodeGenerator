using System;

namespace Wilco.SyntaxHighlighting
{
	/// <summary>
	/// Provides the abstract base class for a parser.
	/// </summary>
	public abstract class ParserBase : IParser
	{
		/// <summary>
		/// Initializes a new instance of a <see cref="Wilco.SyntaxHighlighting.ParserBase"/> class.
		/// </summary>
		public ParserBase()
		{
			//
		}

		/// <summary>
		/// Parses source code.
		/// </summary>
		/// <param name="source">The source code which will be parsed.</param>
		/// <param name="scannerResult">The result returned by the scanners after scanning the source code.</param>
		/// <returns>The highlighted source code.</returns>
		public virtual string Parse(string source, OccurrenceCollection scannerResult)
		{
			string parsedToken;
			for (int i = scannerResult.Count - 1; i >= 0; i--)
			{
				parsedToken = this.ParseToken(source.Substring(scannerResult[i].Start, scannerResult[i].Length), scannerResult[i].Node);
				source = source.Remove(scannerResult[i].Start, scannerResult[i].Length).Insert(scannerResult[i].Start, parsedToken);
			}

			return source;
		}

		/// <summary>
		/// Parses a token.
		/// </summary>
		/// <param name="token">The token which the node matched.</param>
		/// <param name="node">The node which matched the token.</param>
		/// <returns>The highlighted token.</returns>
		protected abstract string ParseToken(string token, INode node);
	}
}