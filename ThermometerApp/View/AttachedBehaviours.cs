using System.Windows;
using System.Windows.Input;

namespace ThermometerApp
{
    /// <summary>
    /// Attatched behavior - dependency properties.
    /// De bruges her til Event to Command binding i tilfælde, hvor WPF ikke har command property
    /// </summary>
    public static class AttachedBehaviours
    {
        #region MousOverCommandProperty & Methods
        public static readonly DependencyProperty MouseOverCommandProperty = DependencyProperty.RegisterAttached(
                                   "MouseOverCommand",
                                   typeof(ICommand),
                                   typeof(AttachedBehaviours),
                                   new FrameworkPropertyMetadata(new PropertyChangedCallback(AttachedBehaviours.MouseOverChanged)));

        public static void SetMouseOverCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(AttachedBehaviours.MouseOverCommandProperty, value);
        }

        public static ICommand GetMouseOverCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(AttachedBehaviours.MouseOverCommandProperty);
        }

        private static void MouseOverChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = target as UIElement;
            element.MouseEnter += (sender, eventArgs) =>
            {
                UIElement uielement = (UIElement)sender;
                ICommand command = (ICommand)uielement.GetValue(AttachedBehaviours.MouseOverCommandProperty);
                command.Execute(null);
            };

        }

        #endregion

        #region MousOutCommandProperty &Methods
        public static readonly DependencyProperty MouseOutCommandProperty = DependencyProperty.RegisterAttached(
                                    "MouseOutCommand",
                                    typeof(ICommand),
                                    typeof(AttachedBehaviours),
                                    new FrameworkPropertyMetadata(new PropertyChangedCallback(AttachedBehaviours.MouseOutChanged)));


        public static void SetMouseOutCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(AttachedBehaviours.MouseOutCommandProperty, value);
        }

        public static ICommand GetMouseOutCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(AttachedBehaviours.MouseOutCommandProperty);
        }

        private static void MouseOutChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = target as UIElement;
            element.MouseLeave += (sender, eventArgs) =>
            {
                UIElement uielement = (UIElement)sender;
                ICommand command = (ICommand)uielement.GetValue(AttachedBehaviours.MouseOutCommandProperty);
                command.Execute(null);
            };

        }
        #endregion
        
        #region ClosedCommandProperty & Methods
        public static readonly DependencyProperty ClosedCommandProperty = DependencyProperty.RegisterAttached(
                            "ClosedCommand",
                            typeof(ICommand),
                            typeof(AttachedBehaviours),
                            new FrameworkPropertyMetadata(new PropertyChangedCallback(AttachedBehaviours.ClosedChanged)));


        public static void SetClosedCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(AttachedBehaviours.ClosedCommandProperty, value);
        }

        public static ICommand GetClosedCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(AttachedBehaviours.ClosedCommandProperty);
        }

        private static void ClosedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Window element = target as Window;
            element.Closed += (sender, eventArgs) =>
            {
                Window window = (Window)sender;
                ICommand command = (ICommand)window.GetValue(AttachedBehaviours.ClosedCommandProperty);
                command.Execute(null);
            };

        }
        #endregion
    }
}
