//-----------------------------------------------------------------------
// <copyright file="CodeProccessingHelper.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using AngleSharp;
using CefSharp;
using CefSharp.OffScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Ai.CodeGeneration
{
    internal class CodeProccessingHelper
    {
        public static async Task<string> ExtractContentCefSharp(string url, string elementName, string className)
        {
            // Initialize CefSharp in off-screen mode
            var settings = new CefSettings();
            Cef.Initialize(settings);

            // Create a hidden browser instance
            var browser = new ChromiumWebBrowser(url);

            // Wait for the page to finish loading
            await browser.WaitForInitialLoadAsync();

            // Inject JavaScript to extract content
            var script = $@"(function() {{
                var elements = document.querySelectorAll('{elementName}.{className}');
                var content = '';
                for (var i = 0; i < elements.length; i++) {{
                    content += elements[i].textContent.trim() + '\n';
                }}
                return content;
            }})()";

            var result = await browser.EvaluateScriptAsync(script);

            // Clean up
            Cef.Shutdown();

            return result.Result?.ToString() ?? string.Empty; // Return extracted content or empty string
        }

        public static List<Tuple<string, string>> ExtractElementsAndClasses(string html)
        {
            var elementsAndClasses = new List<Tuple<string, string>>();

            // Load the HTML into a document
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            // Find all elements with a class attribute
            var nodes = document.DocumentNode.SelectNodes("//*[contains(@class,'')]");

            if(nodes != null)
            {
                foreach(var node in nodes)
                {
                    // Extract the element name and class names
                    string elementName = node.Name;
                    string classNames = node.GetAttributeValue("class", string.Empty);

                    elementsAndClasses.Add(Tuple.Create(elementName, classNames));
                }
            }

            return elementsAndClasses;
        }

        public static List<Tuple<string, string>> ExtractElementsAndClassesAngelcharp(string html)
        {
            var elementsAndClasses = new List<Tuple<string, string>>();

            // Parse the HTML using AngleSharp
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(req => req.Content(html)).Result;

            // Find all elements with a class attribute
            var elements = document.All.Where(m => m.ClassList.Length > 0);

            foreach(var element in elements)
            {
                // Extract the element name and class names
                string elementName = element.LocalName;
                string classNames = string.Join(" ", element.ClassList);

                elementsAndClasses.Add(Tuple.Create(elementName, classNames));
            }

            return elementsAndClasses;
        }
    }
}