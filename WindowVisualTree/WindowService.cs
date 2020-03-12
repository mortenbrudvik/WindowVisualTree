using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace WindowVisualTree
{
    public class WindowService
    {
        public List<Window> GetWindows()
        {
            var rootElement = AutomationElement.RootElement;
            var winCollection = rootElement.FindAll(TreeScope.Children, Condition.TrueCondition);

            return (from AutomationElement winElement in winCollection select new Window(winElement)).ToList();
        }
    }
}