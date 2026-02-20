using Interface;
using TargetSystem.Notifier;

namespace Extensions
{
    public class OutlineSwitcher : SelectionListener<Outline>
    {
        public OutlineSwitcher(ISelectionNotifier<Outline> notifier)
            : base(notifier)
        {
        }

        protected override void OnSelected(Outline outline)
        {
            outline.Enable();
        }

        protected override void OnUnselected(Outline outline)
        {
            outline.Disable();
        }
    }
}