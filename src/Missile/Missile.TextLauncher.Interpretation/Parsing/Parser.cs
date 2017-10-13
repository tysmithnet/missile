using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of IParser
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Parsing.IParser" />
    [Export(typeof(IParser))]
    public class Parser : IParser
    {
        /// <inheritdoc />
        /// <summary>
        ///     Parses the provided tokens asynchronously
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that when complete will hold the root of the AST
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">tokens</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     there can be only 1 provider token
        ///     or
        ///     there can be only 1 destination token
        /// </exception>
        public Task<RootNode> ParseAsync(IEnumerable<Token> tokens, CancellationToken cancellationToken)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            var list = tokens.ToList();

            if (!(list.FirstOrDefault() is ProviderToken))
                list.Insert(0, new ProviderToken("noop", new string[0]));
            if (!(list.LastOrDefault() is DestinationToken))
                list.Add(new DestinationToken("noop", new string[0]));

            if (list.OfType<ProviderToken>().Count() > 1)
                throw new ArgumentException("there can be only 1 provider token");

            if (list.OfType<DestinationToken>().Count() > 1)
                throw new ArgumentException("there can be only 1 destination token");

            var rootNode = new RootNode
            {
                ProviderNode = new ProviderNode(list.First() as ProviderToken),
                FilterNodes = list.OfType<FilterToken>().Select(x => new FilterNode(x)).ToList(),
                DestinationNode = new DestinationNode(list.Last() as DestinationToken)
            };
            return Task.FromResult(rootNode);
        }
    }
}