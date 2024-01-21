using Html_Serializer;

 class Selector
{
    public string TagName { get; set; }
    public string Id { get; set; }
    public List<string> Classes { get; set; } = new List<string>();
    public Selector Parent { get; set; }
    public Selector Child { get; set; }

    public Selector()
    {
        Classes = new List<string>();
    }

    public static Selector FromQueryString(string queryString)
    {
        string[] selectors = queryString.Split();
        Selector root = new Selector();
        Selector currentSelector = root;

        foreach (string selectorString in selectors)
        {
            string[] parts = selectorString.Split('#');
            if (parts.Length > 1)
            {
                currentSelector.Id = parts[1];
                parts = parts[0].Split('.');
            }
            else
            {
                parts = selectorString.Split('.');
            }

            if (!string.IsNullOrEmpty(parts[0]))
            {
                currentSelector.TagName = parts[0];
            }

            for (int i = 1; i < parts.Length; i++)
            {
                currentSelector.Classes.Add(parts[i]);
            }

            Selector newSelector = new Selector();
            currentSelector.Child = newSelector;
            newSelector.Parent = currentSelector;
            currentSelector = newSelector;
        }

        return root;
    }


    private static bool IsValidHtmlTagName(string tagName)
    {
        return HtmlHelper.Instance.HtmlTags.Contains(tagName) && HtmlHelper.Instance.HtmlVoidTags.Contains(tagName);
    }
    public override string ToString()
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

}
