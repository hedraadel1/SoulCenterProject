using AngleSharp;
using AngleSharp.Dom;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Ai.Helpers.WebScraper
{
    public class DocumentationScraperDevexpress
    {
        private const string DevExpressBaseUrl = "https://docs.devexpress.com";
        private const string DevExpressSearchUrl = "https://docs.devexpress.com/search";
        public async Task<Dictionary<string, string>> GetDevExpressControlUrls()
        {
            var controlUrls = new Dictionary<string, string>();

            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync("https://docs.devexpress.com/WindowsForms/7891/controls-and-libraries"); // URL of the page with the control list

                // Select all elements with class 'project-toc__link' 
                var controlLinks = document.QuerySelectorAll("a.project-toc__link");

                foreach (var link in controlLinks)
                {
                    string controlName = link.TextContent.Trim();
                    string relativeUrl = link.GetAttribute("href");

                    // Construct the full URL
                    string fullUrl = $"{DevExpressBaseUrl}{relativeUrl}";

                    // Add to the dictionary (handling potential duplicates)
                    if (!controlUrls.ContainsKey(controlName))
                    {
                        controlUrls.Add(controlName, fullUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching control URLs: {ex.Message}");
            }

            return controlUrls;
        }

        public async Task<string> GetDevExpressDocumentation(string controlType, string controlName)
        {
            try
            {
                // 1. Construct the search query for DevExpress documentation
                string searchQuery = $"{controlType} {controlName}";

                // 2. Create a browsing context
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);

                // 3. Build the DevExpress search URL with the query
                string searchUrl = $"{DevExpressSearchUrl}/?query={Uri.EscapeDataString(searchQuery)}&project=WindowsForms";

                // 4. Navigate to the search results page
                var document = await context.OpenAsync(searchUrl);

                // 5. Extract search result links 
                var resultLinks = document.QuerySelectorAll("a.search-result__link");

                // 6. Find the most relevant documentation link
                string targetUrl = FindMostRelevantLink(resultLinks, controlName);

                if (string.IsNullOrEmpty(targetUrl))
                {
                    return "No relevant documentation found.";
                }

                // 7. Navigate to the target documentation page
                document = await context.OpenAsync("https://docs.devexpress.com" + targetUrl);

                // 8. Extract the documentation content
                var articleContent = document.QuerySelector("article.document__body");
                if (articleContent != null)
                {
                    return articleContent.TextContent;
                }
                else
                {
                    return "Documentation content not found on the target page.";
                }

            }
            catch (Exception ex)
            {
                // Enhanced error handling (log the exception or display a user-friendly message)
                Console.WriteLine($"Error during scraping: {ex.Message}");
                return "An error occurred while fetching documentation.";
            }
        }

        // Helper function to find the most relevant documentation link
        private string FindMostRelevantLink(IHtmlCollection<IElement> resultLinks, string controlName)
        {
            string bestMatch = resultLinks
                .Select(link => link.GetAttribute("href"))
                .FirstOrDefault(url =>
                    !string.IsNullOrEmpty(url) &&
                    url.IndexOf(controlName, StringComparison.OrdinalIgnoreCase) >= 0); // Use IndexOf

            return bestMatch ?? resultLinks.FirstOrDefault()?.GetAttribute("href");
        }
    }
}