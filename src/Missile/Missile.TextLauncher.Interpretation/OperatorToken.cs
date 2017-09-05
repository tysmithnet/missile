namespace Missile.TextLauncher.Interpretation
{
    public class OperatorToken : Token
    {
        private string part;

        public OperatorToken(string part)
        {
            this.part = part;
        }

        public string Identifier { get; set; }
    }
}