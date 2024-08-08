
//using AngleSharp;
//using AngleSharp.Dom;
//using System;
//using System.Threading.Tasks;

//namespace SoulCenterProject.Modules.Ai.Helpers.WebScraper
//{
//    public class DocumentationScraper
//    {
//        public async Task<string> GetDocumentationContent(string keyword, string contain, string linkUrl)
//        {
//            try
//            {
//                // 1. Create a browsing context
//                var config = Configuration.Default.WithDefaultLoader();
//                var context = BrowsingContext.New(config);

//                // 2. Navigate to the search page
//                var document = await context.OpenAsync(linkUrl);

//                // 3. Fill the search box and submit
//                var searchInput = document.QuerySelector("input[name='q']"); // Adjust selector if needed
//                if (searchInput != null)
//                {
//                    searchInput.Value = keyword;

//                    var submitButton = document.QuerySelector("button[name='btnK']"); // Adjust selector if needed
//                    if (submitButton != null)
//                    {
//                        await submitButton.ClickAsync();
//                    }
//                    else
//                    {
//                        Console.WriteLine("Error: Could not find submit button.");
//                        return "";
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Error: Could not find search input field.");
//                    return "";
//                }

//                // 4. Extract search results (Wait for results to load - adjust delay if necessary)
//                await Task.Delay(2000); // Example: Wait for 2 seconds

//                var resultTitles = document.QuerySelectorAll("h3.LC20lb.MBeuO.DKV0Md"); // Adjust selector if needed
//                var resultLinks = document.QuerySelectorAll("a[jsname='UWckNb']"); // Adjust selector if needed

//                // 5. Filter and navigate to the target page
//                string targetUrl = "";
//                for (int i = 0; i < resultTitles.Length; i++)
//                {
//                    if (resultTitles[i].TextContent.Contains(contain))
//                    {
//                        targetUrl = resultLinks[i].GetAttribute("href");
//                        break;
//                    }
//                }

//                if (string.IsNullOrEmpty(targetUrl))
//                {
//                    Console.WriteLine("Error: No matching documentation found.");
//                    return "";
//                }

//                // Navigate to the target documentation page
//                document = await context.OpenAsync(targetUrl);

//                // 6. Extract the documentation content
//                var articleContent = document.QuerySelector("article.document__body"); // Adjust selector if needed
//                if (articleContent != null)
//                {
//                    return articleContent.TextContent; // Or articleContent.InnerHtml for HTML
//                }
//                else
//                {
//                    Console.WriteLine("Error: Could not find documentation content.");
//                    return "";
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error: " + ex.Message);
//                return "";
//            }
//        }
//    }
//// ** Explanation:**

////1. ** Initialization:** The code sets up the AngleSharp browsing context, similar to a web browser, to load and interact with web pages.

////2. ** Navigation and Search:** It navigates to the provided `linkUrl`, finds the search input field, enters the `keyword`, and simulates a click on the submit button.

////3. **Result Extraction and Filtering:** It waits for the search results to load and then uses CSS selectors to extract elements containing the result titles and URLs.It then iterates through the titles to find a match for the provided `contain` string.

////4. **Target Page Navigation:** If a matching result is found, the code retrieves the corresponding URL and navigates to that page.

////5. **Content Extraction:** Finally, it locates the `<article>` element with the class "document__body", which is assumed to contain the actual documentation content.It extracts and returns either the text content or the inner HTML of this element, depending on your needs.

////**Important Considerations:**

////* **Website Structure:** The provided CSS selectors are based on assumptions about the website's HTML structure. You'll need to inspect the target website's source code and adjust the selectors accordingly if the structure differs.

////* **Error Handling:**  While the code includes basic error handling, consider adding more robust checks and specific exception handling (e.g., for network errors, specific HTML element not found errors) to make it more resilient.

////* **Asynchronous Operations:** The `OpenAsync()` and `ClickAsync()` methods are asynchronous. Using `await` ensures that the code waits for these operations to complete before proceeding, which is essential for web scraping tasks.

//}
