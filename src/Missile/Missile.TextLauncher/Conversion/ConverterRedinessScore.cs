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
        public int SourceDistance { get; set; }

        /// <summary>
        ///     How "far" the dest type is from the request type
        /// </summary>
        public int DestDistance { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var score = obj as ConverterRedinessScore;
            return score != null &&
                   SourceDistance == score.SourceDistance &&
                   DestDistance == score.DestDistance;
        }

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