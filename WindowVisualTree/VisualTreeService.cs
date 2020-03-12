using System.Windows.Automation;

namespace WindowVisualTree
{
    public class VisualTreeService
    {
        
        public TreeNode Convert(AutomationElement rootElement)
        {
            Condition condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
            Condition condition2 = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            var walker = new TreeWalker(new AndCondition(condition1, condition2));
            var elementNode = walker.GetFirstChild(rootElement);
            var node = new TreeNode(rootElement);
            while (elementNode != null)
            {
                var childNode = Convert(elementNode);
                node.Children.Add(childNode);
                elementNode = walker.GetNextSibling(elementNode);
            }

            return node;
        }
    }
}