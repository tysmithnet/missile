namespace Missile.TextLauncher.Conversion
{
    public class ConverterRedinessScore
    {
        public int SourceDistance { get; set; }
        public int DestDistance { get; set; }

        public override bool Equals(object obj)
        {
            var score = obj as ConverterRedinessScore;
            return score != null &&
                   SourceDistance == score.SourceDistance &&
                   DestDistance == score.DestDistance;
        }

        public override int GetHashCode()
        {
            var hashCode = -376618403;
            hashCode = hashCode * -1521134295 + SourceDistance.GetHashCode();
            hashCode = hashCode * -1521134295 + DestDistance.GetHashCode();
            return hashCode;
        }
    }
}