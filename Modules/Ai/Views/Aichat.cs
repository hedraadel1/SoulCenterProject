//-----------------------------------------------------------------------
// <copyright file="Aichat.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IO;
using MySql.Data.MySqlClient;
using SoulCenterProject.Modules.Ai.CodeGeneration;
using SoulCenterProject.Modules.Ai.Services;

using Telerik.WinControls.UI;
using Message = SoulCenterProject.Models.Message;

namespace SoulCenterProject.Modules.Ai.Views
{
    public partial class Aichat : DevExpress.XtraEditors.XtraForm
    {
        private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro-001:generateContent?key=AIzaSyAd4D4CHgRU7cMb1lzYncdhK0RAeDg__Nc";

        private readonly GeminiAiService _aiService;
        private readonly DatabaseService _databaseService;
        private readonly SummarizationService _summarizationService;
        private readonly TaggingService _taggingService;
        // 1. User Sends Message
        //string input = "aaa";
    
        private string input;


        //// 2. Retrieve Conversation History
        //List<Message> conversationHistory = RetrieveHistoryFromDatabase(); // Implement this function

        //private static List<Message> RetrieveHistoryFromDatabase()
        //{
        //    throw new NotImplementedException();
        //}

        //// 3. Generate Tags for User Input
        //string tags = GetTagsForMessage(messageText: input);

        //// 4. Prepare Gemini Request (You'll need a function to assemble this)
        //string geminiRequest = PrepareGeminiRequest(conversationHistory, input, tags);

        //private static string PrepareGeminiRequest(List<Message> conversationHistory, string input, string tags)
        //{
        //    throw new NotImplementedException();
        //}

        //// 5. Call Gemini API
        //string chatResponse = CallGeminiAPI(geminiRequest);

        //private static string CallGeminiAPI(string geminiRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //// 6. Summarize (Separate API)
        //string summary = SummarizeText(input + " " + chatResponse);


        public Aichat()
        {
            InitializeComponent();
            
        }

        private void AddMessageProgrammatically()
        {
            RadChatPanel.AutoAddUserMessages = false;
            RadChatPanel.SendMessage += RadChatPanel_SendMessage;
        }

        private void Button_extract_Click(object sender, EventArgs e)
        {
            string exampleString = @"
namespace Soul.Ai.CodeGeneration
{
    public class BasicCodeGenerator : ICodeGenerator
    {
        public string StatusofExcute;

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name=""directoryPath"">The path of the directory to create.</param>

        public void CreateDirectory(string directoryPath)
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                StatusofExcute = $""Error creating directory: {ex.Message}"";
            }
        }

        /// <summary>
        /// Creates a new file at the specified path.
        /// </summary>
        /// <param name=""filePath"">The path of the file to create.</param>

        public void CreateFile(string filePath)
        {
            try
            {
                // Simple creation, overwrites if the file exists
                File.WriteAllText(filePath, """");
            }
            catch (Exception ex)
            {
                StatusofExcute = $""Error creating file: {ex.Message}"";
            }
        }
{startmethod}
        public void UpdateFile(string filePath, string newContent)
        {
            try
            {
                File.WriteAllText(filePath, newContent);
            }
            catch (Exception ex)
            {
                StatusofExcute = $""Error updating file: {ex.Message}"";
            }
        }
{endmethod}
        // Simple Class and Method Manipulation
        public void CreateClass(string className, string classPath, string initialContent = """")
        {
            string fullFilePath = Path.Combine(classPath, className + "".cs"");

            using (StreamWriter writer = File.CreateText(fullFilePath))
            {
                writer.WriteLine($""public class {className}"");
                writer.WriteLine(""{"");
                writer.WriteLine(initialContent);
                writer.WriteLine(""}"");
            }
        }

        public void UpdateClass(string classPath, string className, string updatedContent)
        {
            string fullFilePath = Path.Combine(classPath, className + "".cs"");
            File.WriteAllText(fullFilePath, updatedContent);
        }

        public void AddMethod(string classPath, string className, string methodName, string methodContent)
        {
            string fullFilePath = Path.Combine(classPath, className + "".cs"");
            string fileContent = File.ReadAllText(fullFilePath);
            fileContent += ""\n"" + methodContent; // Simple append
            File.WriteAllText(fullFilePath, fileContent);
        }
}";
            var codeGenerator = new BasicCodeGenerator();
            String extract = codeGenerator.ExtractMethodText(exampleString);
            textBox_extract.Text = extract;
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        private async void Button_Send_Click(object sender, EventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
        }

        private string CombineIntoDartClass(List<FunctionOrWidget> functionsAndWidgets)
        {
            // Implement this function to combine the modified functions/widgets back into a Dart class
            throw new NotImplementedException();
        }

        private int CountWords(string text)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private Task<string> ExplainWithGeminiAI(string text)
        {
            // Implement this function to send the text to Gemini AI and get an explanation
            throw new NotImplementedException();
        }

        private async void GenerateProjectStructure_Click(object sender, EventArgs e)
        {
            var codeGenerator = new BasicCodeGenerator();

            // Get the current solution path
            string solutionPath = @"E:\Soul-20240328T185548Z-001\Soul\Soul.sln";

            // Get the current project name
            string projectName = "Soul";
            if(string.IsNullOrEmpty(solutionPath) || string.IsNullOrEmpty(projectName))
            {
                MessageBox.Show("Error retrieving solution path or project name.");
                return;
            }

            // Create a unique file name using the current time
            string currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string outputFileName = $"{projectName}_structure_{currentTime}.txt";
            string projectDirectory = Path.GetDirectoryName(solutionPath);
            string outputPath = Path.Combine(projectDirectory, outputFileName);

            // Show the loading indicator
            progressBar1.Visible = true;

            // Disable the button while processing
            GenerateProjectStructure.Enabled = false;

            try
            {
                // Perform the operation (writing the project structure to file) asynchronously
                await Task.Run(
                    () =>
                    {
                        codeGenerator.GenerateProjectStructureDetails(solutionPath, projectName, outputPath);
                    });

                // Show success message
                textBox1.Text = "Project structure successfully written to file.";
                textBox1.Visible = true;
            } catch(Exception ex)
            {
                // Show error message if an exception occurs
                textBox1.Text = "Error: " + ex.Message;
                textBox1.Visible = true;
            }

            // Hide the loading indicator
            progressBar1.Visible = false;

            // Re-enable the button
            GenerateProjectStructure.Enabled = true;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        //public async Task AnalyzeAndExplainDartClass(string dartClassContent)
        //{
        //    // Parse the Dart class content and extract functions and widgets
        //    var functionsAndWidgets = ParseDartClass(dartClassContent);

        //    foreach (var item in functionsAndWidgets)
        //    {
        //        // Count words in each function/widget
        //        int wordCount = CountWords(item.Content);

        //        // If word count is more than 7000, split the function/widget
        //        if (wordCount > 7000)
        //        {
        //            var splitItems = SplitFunctionOrWidget(item);
        //            functionsAndWidgets.Remove(item);
        //            functionsAndWidgets.AddRange(splitItems);
        //        }

        //        // Explain each function/widget with Gemini AI
        //        string explanation = await ExplainWithGeminiAI(item.Content);

        //        // Write the explanation in the Dart class as comments
        //        item.Content = $"/*\n{explanation}\n*/\n{item.Content}";
        //    }

        //    // Combine the modified functions/widgets back into a Dart class
        //    string modifiedDartClassContent = CombineIntoDartClass(functionsAndWidgets);

        //    // Return the modified Dart class
        //    return modifiedDartClassContent;
        //}

        private List<FunctionOrWidget> ParseDartClass(string dartClassContent)
        {
            var functionsAndWidgets = new List<FunctionOrWidget>();
            var lines = dartClassContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var currentFunctionOrWidget = new StringBuilder();
            string currentName = null;

            foreach(var line in lines)
            {
                if(line.Contains("class ") || line.Contains("void ") || line.Contains("Widget "))
                {
                    if(currentFunctionOrWidget.Length > 0 && currentName != null)
                    {
                        functionsAndWidgets.Add(
                            new FunctionOrWidget { Name = currentName, Content = currentFunctionOrWidget.ToString() });
                        currentFunctionOrWidget.Clear();
                    }

                    var nameMatch = Regex.Match(line, @"(class|void|Widget)\s+(\w+)");
                    if(nameMatch.Success)
                    {
                        currentName = nameMatch.Groups[2].Value;
                    }
                }

                currentFunctionOrWidget.AppendLine(line);
            }

            if(currentFunctionOrWidget.Length > 0 && currentName != null)
            {
                functionsAndWidgets.Add(
                    new FunctionOrWidget { Name = currentName, Content = currentFunctionOrWidget.ToString() });
            }

            return functionsAndWidgets;
        }

        private void RadChatPanel_Click(object sender, EventArgs e)
        {
        }

        //private async void RadChatPanel_SendMessage(object sender, SendMessageEventArgs e)
        //{
        //    AddMessageProgrammatically();
        //    ChatTextMessage textMessage = e.Message as ChatTextMessage;
        //    textMessage.Message = textMessage.Message;
        //    Author author = new Author(Properties.Resources.user, "Hedra");
        //    this.RadChatPanel.Author = author;

        //    // Debug.WriteLine("RadChatPanel.Author Name: " + this.RadChatPanel.Author.Name);

        //    // User Input
        //    input = textMessage.Message;
        //    ChatTextMessage message1 = new ChatTextMessage(input, author, DateTime.Now.AddHours(1)); // Make sure RadChatPanel.Author is valid
        //    this.RadChatPanel.AddMessage(message1);

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var requestData = new
        //        {
        //            contents = new List<object>
        //            {
        //                new
        //                {
        //                    parts = new List<object>
        //                    {
        //                        new { text = input }
        //                    }
        //                }
        //            },
        //            generationConfig = new
        //            {
        //                temperature = 0.9,
        //                topK = 1,
        //                topP = 1,
        //                maxOutputTokens = 2048,
        //                stopSequences = new List<string>()
        //            },
        //            safetySettings = new List<object>
        //            {
        //                new { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
        //                new { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
        //                new { category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
        //                new { category = "HARM_CATEGORY_DANGEROUS_CONTENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" }
        //            }
        //        };

        //        string json = JsonConvert.SerializeObject(requestData);
        //        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro:streamGenerateContent?key=AIzaSyC06TuD9zECGHsMxMwQ5aBRaUB80pKnbWY", data);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = await response.Content.ReadAsStringAsync();
        //            // Parse the response and add it to the ListView
        //            var parsedResult = JsonConvert.DeserializeObject<dynamic>(result);
        //            if (parsedResult != null && parsedResult.candidates != null && parsedResult.candidates.Count > 0)
        //            {
        //                if (parsedResult.candidates[0].content != null && parsedResult.candidates[0].content.parts != null && parsedResult.candidates[0].content.parts.Count > 0)
        //                {
        //                    string chatResponse = parsedResult.candidates[0].content.parts[0].text;
        //                    //  Author Airauthor = new Author(Properties.Resources.user, "Gemini");


        //                    Author aiAuthor = new Author(Properties.Resources.ai, "Gemini"); // Create AI Author
        //                    ChatTextMessage message2 = new ChatTextMessage(chatResponse, aiAuthor, DateTime.Now.AddHours(1).AddMinutes(10)); // Use aiAuthor here
        //                    this.RadChatPanel.AddMessage(message2);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Error: Could not extract response text. Check JSON structure.");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Error: Could not parse JSON response.");
        //            }
        //        }

        //        // 7. Store Messages & Summaries
        //        //string summary = SummarizeText(input + " " + chatResponse);

        //        //var userMessage = new Message()
        //        //{
        //        //    UserId = 1,
        //        //    Text = input,
        //        //    Type = MessageType.BasicType,
        //        //    Tags = tags,
        //        //    Summary = summary
        //        //};
        //        //StoreUserMessage(userMessage);

        //        //var aiMessage = new Message()
        //        //{
        //        //    UserId = 2, // Or set if applicable, 
        //        //    Text = chatResponse,
        //        //    Type = MessageType.BasicType,
        //        //    Summary = summary
        //        //};
        //        //StoreAIMessage(aiMessage);
        //    }


        //}


         private async void RadChatPanel_SendMessage(object sender, SendMessageEventArgs e)
         {
             AddMessageProgrammatically();

             ChatTextMessage textMessage = e.Message as ChatTextMessage;
             if(textMessage == null)
             {
                 return; // Basic check
             }

             Author author = new Author(Properties.Resources.user, "Hedra");
             RadChatPanel.Author = author;

             // User Input
             string input = textMessage.Message;
             ChatTextMessage userMessage = new ChatTextMessage(input, author, DateTime.Now.AddHours(1));
             RadChatPanel.AddMessage(userMessage);

             // Store user message (before getting AI response)


             //try
             //{
             //    var aiResponse = await _aiService.GetResponseAsync(input);


             //    Author aiAuthor = new Author(Properties.Resources.ai, "Gemini");
             //    ChatTextMessage aiMessage = new ChatTextMessage(
             //        aiResponse,
             //        aiAuthor,
             //        DateTime.Now.AddHours(1).AddMinutes(10));
             //    RadChatPanel.AddMessage(aiMessage);
             //} catch(Exception ex)
             //{
             //    // Handle errors gracefully, e.g. show an error message on the UI
             //    MessageBox.Show($"Error getting AI response: {ex.Message}");
             //}
         }


        private void sele()
        {
            //string input = Textbox_input.Text;
            //string response = "";
            //string name = "";
            //string date = DateTime.Now.ToString("dd/MM/yyyy");
            //string time = DateTime.Now.ToString("HH:mm:ss");
            //string date_time = date + " " + time;
            //string chat = name + " : " + input;
            //string chat_date_time = date_time + " : " + chat;
            //string chat_date = date + " : " + chat;
            //string chat_time = time + " : " + chat;
            //string chat_date_time_name = date_time + " : " + name + " : " + chat;
            //string chat_date_name = date + " : " + name + " : " + chat;

            //// Initialize the firefox Driver instead of chromedriver

            //using (IWebDriver driver = new ChromeDriver())
            //{


            //    //write code to use an exising cookie to login to the website
            //    driver.Navigate().GoToUrl("https://gemini.google.com/app");
            //    driver.Manage().Window.Maximize();
            //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            //    driver.Manage().Cookies.AddCookie(new Cookie("__Secure-1PSID", "g.a000ggjXDOM33xLEMkG7U6tIIpdTum6F9X7SIG6dGDIdErCh00LjEVr9YUSRbNAyTtJAFfh7KQACgYKAUQSAQASFQHGX2MieohjqmIytgsALDgdr5pxVhoVAUF8yKqLzwhpQCes7uEjF1TUJJQF0076"));
            //    driver.Navigate().GoToUrl("https://gemini.google.com/app");
            //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);


            //    // driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
            //    IWebElement signButton = driver.FindElement(By.ClassName("mdc-button__label"));
            //    signButton.Click();
            //    IWebElement emailinput = driver.FindElement(By.Id("identifierId"));
            //    emailinput.SendKeys("hedra.adel@gmail.com");
            //    emailinput.SendKeys(OpenQA.Selenium.Keys.Enter);

            //    IWebElement passinput = driver.FindElement(By.ClassName("text-input-field"));
            //    passinput.SendKeys("We@@Can##@Do$$It");


            //    passinput.SendKeys(OpenQA.Selenium.Keys.Enter);
            //    driver.Navigate().GoToUrl("https://gemini.google.com/app");
            //    // Find the chat input element by its id and send the input text
            //    IWebElement chatInput = driver.FindElement(By.ClassName("text-input-field"));
            //    chatInput.SendKeys(input);
            //            driver.Navigate().GoToUrl("https://gemini.google.com/app");
            //    // Find the send button by its id and click it
            //    IWebElement sendButton = driver.FindElement(By.Id("send-button"));
            //    sendButton.Click();

            //    // Wait for the response and get the text of the chat details
            //    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds for the response to load
            //    IWebElement chatDetails = driver.FindElement(By.ClassName("response-content"));
            //    response = chatDetails.Text;

            //    // Insert the response into the Gemini_Chat_Listview
            //    // Assuming Gemini_Chat_Listview is a ListView control
            //    ListViewItem item = new ListViewItem(new[] { date_time, name, response });
            //    Gemini_chat_listview.Items.Add(item);
            //}
        }

        private List<FunctionOrWidget> SplitFunctionOrWidget(FunctionOrWidget item)
        {
            // Implement this function to split a function/widget into two or more functions/widgets
            throw new NotImplementedException();
        }

        private void StoreAIMessage(Message message)
        {
            using(var connection = new MySqlConnection("your_connection_string"))
            {
                connection.Open();
                InsertMessage(connection, message);
            }
        }


        private void StoreUserMessage(Message message)
        {
            using(var connection = new MySqlConnection("your_connection_string"))
            {
                connection.Open();
                InsertMessage(connection, message);
            }
        }

        // ... (You can add other database interaction functions as needed)

        // Tagging function (assuming you have a tagging API/library in place)
        public static string GetTagsForMessage(string messageText)
        {
            // Implement the logic to call your tagging API/library
            // Example placeholder:
            return "tag1, tag2";
        }

        // Database access functions
        public static bool InsertMessage(MySqlConnection connection, Message message)
        {
            var command = new MySqlCommand(
                "INSERT INTO conversations (user_id, text, type, tags, summary, timestamp) VALUES (@userId, @text, @type, @tags, @summary, @timestamp)",
                connection);
            command.Parameters.AddWithValue("@userId", message.UserId);
            command.Parameters.AddWithValue("@text", message.Text);
            command.Parameters.AddWithValue("@type", message.Type.ToString());
            command.Parameters.AddWithValue("@tags", message.Tags);
            command.Parameters.AddWithValue("@summary", message.Summary);
            command.Parameters.AddWithValue("@timestamp", message.Timestamp);

            return command.ExecuteNonQuery() > 0;
        }

        // Summarization function
        public static string SummarizeText(string textToSummarize)
        {
            // Implement the logic to call your summarization API/library
            // Example placeholder:
            return "This is a summary of the provided text.";
        }
    }

    public class FunctionOrWidget
    {
        public string Content { get; set; }

        public string Name { get; set; }
    }
}