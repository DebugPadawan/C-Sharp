/**************************************************************************
 * Script:        ChampionSelector.cs
 * Author:        DebugPadawan (https://github.com/DebugPadawan)
 * Created on:    19.01.2025
 * Description:   This script is used to select a champion from a list of champions.
 **************************************************************************/

using System.Globalization;
using HtmlAgilityPack;

class ChampionSelector
{
    // URL of the website that contains the list of champions
    const string URL = "https://www.counterstats.net/";

    // List of champions. Why static? Because we want to keep the list of champions in memory and access it from different methods.
    static List<Champion> champions = new List<Champion>();

    static void Main(string[] args)
    {
        // Set the console output encoding to UTF-8 to display special characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Fetch the champions from the website
        FetchChampions();

        // Display the welcome message
        Console.WriteLine("Welcome to the Champion Selector!");

        // Loop forever
        while (true)
        {
            Console.WriteLine("Please type the name of the champion you want to select: ");

            // Read the champion name from the console
            var championName = Console.ReadLine();

            // Check if the champion name is empty
            if (string.IsNullOrEmpty(championName))
            {
                Console.WriteLine("Please type a valid champion name.");

                // Continue to the next iteration of the loop
                continue;
            }

            // Replace spaces with dashes (e.g. "miss fortune" -> "miss-fortune") and convert to lowercase
            championName = championName.Replace(" ", "-").ToLower();

            // Find the champion with the given name
            var champion = champions.FirstOrDefault(c => c.name.Equals(championName, StringComparison.OrdinalIgnoreCase));

            // If the champion is found
            if (champion != null)
            {
                // Display the selected champion
                Console.WriteLine($"You have selected {champion.nameBeautified}!");

                // Get the best counters for the champion
                var bestCounters = champion.GetBestCounters();

                // Display the best counters
                Console.WriteLine($"The best counters for {champion.nameBeautified} are: ");

                // Loop through the best counters and display them
                foreach (var counter in bestCounters)
                {
                    Console.WriteLine("     - " + counter);
                }

                // Get the worst counters for the champion
                var worstCounters = champion.GetWorstCounters();

                // Display the worst counters
                Console.WriteLine($"The worst counters for {champion.nameBeautified} are: ");

                // Loop through the worst counters and display them
                foreach (var counter in worstCounters)
                {
                    Console.WriteLine("     - " + counter);
                }
            }
            else
            {
                // Display an error message if the champion is not found
                Console.WriteLine("Champion not found. Please try again.");
            }
        }
    }

    // Why static? Because we don't need an instance of the class to call this method. As example, we can call this method directly from the Main method.
    static void FetchChampions()
    {
        // Implementation for fetching champions
        Console.WriteLine("Fetching champions...");

        // Create a new instance of HttpClient
        var httpClient = new HttpClient();                   

        // Get the HTML content of the URL       
        var html = httpClient.GetStringAsync(URL).Result;

        // Create a new instance of HtmlDocument
        var htmlDocument = new HtmlDocument();    

         // Load the HTML content into the HtmlDocument
        htmlDocument.LoadHtml(html);                               

        // Select the div with the id 'champions'
        var championsDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='champions']");        

        // Champion names are the ids of the divs inside the champions div
        var championDivs = championsDiv.SelectNodes(".//div");

        // Loop through all the champion divs
        foreach (var championDiv in championDivs)
        {
            // Get the id attribute of the div
            var championName = championDiv.GetAttributeValue("id", ""); 

            // ───────────────────────────────────────────────
            // BUG: The name of the champion sometimes contains a special character like ' in it. Name will not be displayed correctly in console.
            // ───────────────────────────────────────────────


            // Check if the champion name is not empty
            if (!string.IsNullOrEmpty(championName))
            {
                // Create a new instance of the Champion class
                var champion = new Champion(championName);

                // Add the champion to the list of champions
                champions.Add(champion);            

                //Console.WriteLine($"Fetched champion: {champion.nameBeautified}");
            }
        }

        // Close the HttpClient
        httpClient.Dispose();

    }
}


class Champion
{
    // Name of the champion (e.g. "miss-fortune")
    public string name;

    // Beautified name of the champion (e.g. "Miss Fortune")
    public string nameBeautified;

    private string championUrl;

    // List of counters for the champion. Why private? Because we don't want to expose the counters list directly to the outside world.
    private List<string> counters = new List<string>();

    public Champion(string name)
    {
        this.name = name;

        this.nameBeautified = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace("-", " "));
        
        this.championUrl = $"https://www.counterstats.net/league-of-legends/{name}";
    }

    /// <summary>
    /// Get the best counters for the champion
    /// </summary>
    /// <returns>List of best counters</returns>
    public List<string> GetBestCounters()
    {
        List<string> bestCountersList = new List<string>();

        // Create a new instance of HttpClient
        var httpClient = new HttpClient();

        // Get the HTML content of the URL
        var html = httpClient.GetStringAsync(championUrl).Result;

        // Create a new instance of HtmlDocument
        var htmlDocument = new HtmlDocument();

        // Load the HTML content into the HtmlDocument
        htmlDocument.LoadHtml(html);

        // The first div with the class 'champ-box' contains the best counters
        var bestCountersDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='champ-box']");
    
        // Get the best counters. (A champion per anchor tag with the class 'champ-box__row')
        var bestCounters = bestCountersDiv.SelectNodes(".//a[@class='champ-box__row']");

        // Loop through the first 3 best counters
        for (int i = 0; i < 3; i++)
        {
            // Get the name of the counter
            var counterName = bestCounters[i].SelectSingleNode(".//span[@class='champion']").InnerText;

            // Add the counter to the list of best counters
            bestCountersList.Add(counterName);
        }

        // Close the HttpClient
        httpClient.Dispose();

        return bestCountersList;
    }

    /// <summary>
    /// Get the worst counters for the champion
    /// </summary>
    /// <returns>List of worst counters</returns>
    public List<string> GetWorstCounters()
    {
        List<string> worstCountersList = new List<string>();

        // Create a new instance of HttpClient
        var httpClient = new HttpClient();

        // Get the HTML content of the URL
        var html = httpClient.GetStringAsync(championUrl).Result;

        // Create a new instance of HtmlDocument
        var htmlDocument = new HtmlDocument();

        // Load the HTML content into the HtmlDocument
        htmlDocument.LoadHtml(html);

        // The second div with the class 'champ-box' contains the worst counters
        var worstCountersDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='champ-box'][2]");

        // Get the worst counters. (A champion per anchor tag with the class 'champ-box__row')
        var worstCounters = worstCountersDiv.SelectNodes(".//a[@class='champ-box__row']");

        // Loop through the first 3 worst counters
        for (int i = 0; i < 3; i++)
        {
            // Get the name of the counter
            var counterName = worstCounters[i].SelectSingleNode(".//span[@class='champion']").InnerText;

            // Add the counter to the list of worst counters
            worstCountersList.Add(counterName);
        }

        // Close the HttpClient
        httpClient.Dispose();

        return worstCountersList;
    }
}
