using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace Html_Serializer
{
    class HtmlHelper
    {
        private static readonly HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }

        private HtmlHelper()
        {
            
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Files\\HtmlTags.json"));
            HtmlVoidTags =  JsonSerializer.Deserialize<string[]>(File.ReadAllText("Files\\HtmlVoidTags.json"));

        }
    }
}
