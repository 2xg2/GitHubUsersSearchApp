using System.Windows.Input;
using Xamarin.Forms;

namespace GitHubUsersSearchApp.Behaviors
{
    public class EntryCompletedBehavior : BehaviorBase<Entry>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EntryCompletedBehavior), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Completed += EntryCompleted;
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Completed -= EntryCompleted;
        }

        private void EntryCompleted(object sender, System.EventArgs e)
        {
            if (Command != null && sender != null)
            {
                string entryText = ((Entry)sender).Text;
                if (Command.CanExecute(entryText))
                {
                    Command.Execute(entryText);
                }
            }
        }
    }
}
