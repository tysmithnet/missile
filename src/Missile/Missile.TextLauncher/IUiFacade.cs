using System.Windows;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents an object capable of manipulating the UI
    /// </summary>
    public interface IUiFacade
    {
        /// <summary>
        ///     Sets the output control
        /// </summary>
        /// <param name="outputControl">The output control</param>
        void SetOutputControl(FrameworkElement outputControl);
    }
}