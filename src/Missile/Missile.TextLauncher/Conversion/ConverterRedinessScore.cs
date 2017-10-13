namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    ///     A scalar type representing a converters ability to handle a conversion
    /// </summary>
    public class ConverterRedinessScore
    {
        /// <summary>
        ///     How "far" the source type is from the requested type
        /// </summary>
        /// <value>
        ///     The source distance.
        /// </value>
        public int SourceDistance { get; set; }

        /// <summary>
        ///     How "far" the dest type is from the request type
        /// </summary>
        /// <value>
        ///     The dest distance.
        /// </value>
        public int DestDistance { get; set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ConverterRedinessScore score &&
                   SourceDistance == score.SourceDistance &&
                   DestDistance == score.DestDistance;
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = -376618403;
            hashCode = hashCode * -1521134295 + SourceDistance.GetHashCode();
            hashCode = hashCode * -1521134295 + DestDistance.GetHashCode();
            return hashCode;
        }
    }
}