namespace Missile.TextLauncher.Interpretation.Compilation
{
    public sealed class NullRootNode : RootNode
    {
        public override bool Equals(object obj)
        {
            return obj is NullRootNode;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}