using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        #region 1
        public  static Selector FromQueryString(string queryString)
        {
            var selectors = queryString.Split(' ');
            var rootSelector = new Selector();
            var currentSelector = rootSelector;

            foreach (var selectorStr in selectors)
            {
                var parts = selectorStr.Split('#');
                if (parts.Length > 1)
                {
                    currentSelector.Id = parts[1];
                    parts = parts[0].Split('.');
                }
                else
                {
                    parts = selectorStr.Split('.');
                }

                if (!string.IsNullOrEmpty(parts[0]))
                {
                    currentSelector.TagName = parts[0];
                }

                currentSelector.Classes = new List<string>();

                if (parts.Length > 1)
                {
                    currentSelector.Classes.AddRange(parts[1..]);
                }

                var newSelector = new Selector();
                currentSelector.Child = newSelector;
                newSelector.Parent = currentSelector;
                currentSelector = newSelector;
            }

            return rootSelector;
        }
        public  override string ToString()
        {
            var result = TagName ?? string.Empty;

            if (!string.IsNullOrEmpty(Id))
            {
                result += $"#{Id}";
            }

            if (Classes.Any())
            {
                result += $".{string.Join(".", Classes)}";
            }

            return result;
        }
        #endregion
        public Selector()
        {
            Classes = new List<string>();
        }



    }

}