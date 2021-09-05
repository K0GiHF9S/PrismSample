using Microsoft.Xaml.Behaviors;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PrismSample.Behaviors
{
    public class WindowClosingBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty ClosingCallbackCommandProperty = DependencyProperty.Register(nameof(ClosingCallbackCommand), typeof(ICommand), typeof(WindowClosingBehavior), new PropertyMetadata(null));

        private bool canClose;

        public ICommand ClosingCallbackCommand
        {
            get => (ICommand)GetValue(ClosingCallbackCommandProperty);
            set => SetValue(ClosingCallbackCommandProperty, value);
        }

        protected override void OnAttached()
        {
            AssociatedObject.Closing += AssociatedObject_Closing;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= AssociatedObject_Closing;
        }

        private void AssociatedObject_Closing(object sender, CancelEventArgs e)
        {
            if (canClose || ClosingCallbackCommand is null)
            {
                return;
            }
            e.Cancel = true;
            Action<bool> confirm_callback = ConfirmCallback;
            if (ClosingCallbackCommand.CanExecute(confirm_callback))
            {
                ClosingCallbackCommand.Execute(confirm_callback);
            }
        }

        private void ConfirmCallback(bool canClose)
        {
            this.canClose = canClose;
            if (canClose)
            {
                AssociatedObject.Close();
            }
        }
    }
}
