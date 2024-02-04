using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Classes = new List<string>();
            Attributes = new List<string>();
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                yield return currentElement;

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var currentElement = this.Parent;

            while (currentElement != null)
            {
                yield return currentElement;
                currentElement = currentElement.Parent;
            }
        }

        //Wrapping function:
        public static IEnumerable<HtmlElement> FindElementsBySelector(HtmlElement htmlTree, Selector selector)
        {
            var results = new HashSet<HtmlElement>();
            FindElementsBySelectorRecursive(htmlTree, selector, results);
            return results;
        }

        //Recursive function:
        //private static void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        //{
        //    if (!results.Contains(element) && element.MatchesSelector(selector))
        //    {
        //        results.Add(element);
        //    }

        //    foreach (var child in element.Children)
        //    {
        //        FindElementsBySelectorRecursive(child, selector, results);
        //    }
        //}

      //  private static void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        //{
        //    if (!results.Contains(element) && element.MatchesSelector(selector))
        //    {
        //        results.Add(element);
        //    }

        //    // Check for descendants
        //    foreach (var descendant in element.Descendants())
        //    {
        //        if (!results.Contains(descendant) && descendant.MatchesSelector(selector))
        //        {
        //            results.Add(descendant);
        //        }
        //    }

        //    // Check for ancestors
        //    foreach (var ancestor in element.Ancestors())
        //    {
        //        if (!results.Contains(ancestor) && ancestor.MatchesSelector(selector))
        //        {
        //            results.Add(ancestor);
        //        }
        //    }

        //    foreach (var child in element.Children)
        //    {
        //        FindElementsBySelectorRecursive(child, selector, results);
        //    }
        //}
        //private bool MatchesSelector(Selector selector)
        //{
        //    return
        //        (string.IsNullOrEmpty(selector.TagName) || this.Name == selector.TagName) &&
        //        (string.IsNullOrEmpty(selector.Id) || this.Id == selector.Id) &&
        //        (selector.Classes.All(cls => this.Classes.Contains(cls)));

        //}


        private static bool MatchSelector(HtmlElement element, Selector selector)
        {
            // Check if the current element matches the selector
            return (selector.TagName == null || element.Name == selector.TagName) &&
                   (selector.Id == null || element.Id == selector.Id) &&
                   (selector.Classes == null || selector.Classes.All(c => element.Classes.Contains(c)));
        }

        private static void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        {
            // Base case: if the selector is null, return
            if (selector == null)
            {
                return;
            }

            // Check if the current element matches the selector
            if (MatchSelector(element, selector))
            {
                results.Add(element);
            }

            // Recursively search children elements
            foreach (var child in element.Children)
            {
                FindElementsBySelectorRecursive(child, selector, results);
            }

            // Recursively search descendants elements
            foreach (var descendant in element.Descendants())
            {
                if (MatchSelector(descendant, selector))
                {
                    results.Add(descendant);
                }
            }

            // Recursively search ancestors elements
            foreach (var ancestor in element.Ancestors())
            {
                if (MatchSelector(ancestor, selector))
                {
                    results.Add(ancestor);
                }
            }
        }
    }


}