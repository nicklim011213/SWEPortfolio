using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
namespace Stock_Stuff
{
    class Program
    {
        static void Main()
        {
            while (true) // Master While loop
            {
                Console.WriteLine("Enter your indicator, exit to exit, update to update a list of 25, or calculate to see indicators");
                String Indicator = "";
                Indicator = Console.ReadLine().ToUpper();
                if (Indicator == "EXIT")
                {
                    Environment.Exit(0); // If EXIT is typed stop program
                }
                else if (Indicator == "UPDATE")
                {
                    UpdateStock();     // Run update
                }
                else if (Indicator == "CALCULATE")
                {
                    Console.WriteLine("Updating Indicator for all Stocks in list");
                    Recommend();  // Run calculate
                }
                // Type Symbol Convert to uppercase

                String URLPt1 = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";
                String URlPt2 = Indicator + "&apikey=~~~";
                String CompleteURl = URLPt1 + URlPt2;
                Uri queryUri = new Uri(CompleteURl);
                //2 URl Parts where he Symbol is put inside the URl
                // This is just the url for the API call

                using (WebClient client = new WebClient())
                {
                    String JSONDATA = client.DownloadString(queryUri); // This is the reponse

                    Console.WriteLine("Do you want to write to file? Y/N"); // Ask do you want to write
                    String Awnser = "";
                    Awnser = Console.ReadLine().ToUpper(); // Get Awnser to if they want to write to file
                    if (Awnser == "Y") // if YES
                    {
                        if (File.Exists(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt")) // If the file exdists name = Symbol then write
                        { // if the file exists alreadt
                            // Replace Block
                            Console.WriteLine("Writing to file...");


                            System.DateTime Time = new System.DateTime();
                            Time = DateTime.Now;
                            JSONDATA = JSONDATA.Replace("{", "");  // Remove {
                            JSONDATA = JSONDATA.Replace("}", "");  // Remove }
                            JSONDATA = JSONDATA.Replace(",", "");  // Remove ,
                            JSONDATA = JSONDATA.Replace('"', '*'); // Replace " with *
                            JSONDATA = JSONDATA.Replace("*", "");  // Remove Star
                            JSONDATA = JSONDATA.Replace('-', '/'); // Replace - With /
                            JSONDATA = JSONDATA.Replace("Time Series (Daily):", "");
                            JSONDATA = JSONDATA.Replace("Meta Data:", "");
                            JSONDATA = JSONDATA.Replace("1. Information: Daily Prices (open high low close) and Volumes", "");
                            JSONDATA = JSONDATA.Replace("2. Symbol: " + Indicator, "");
                            JSONDATA = JSONDATA.Replace("16:00:01", "");
                            JSONDATA = JSONDATA.Replace("4. Output Size: Compact", "");
                            JSONDATA = JSONDATA.Replace("5. Time Zone: US/Eastern", "");
                            // Removes Extra Info in the front of the response

                            int refreshedline = JSONDATA.IndexOf("3."); // find this particular line
                            JSONDATA = JSONDATA.Remove(refreshedline, 29); // remove it

                            {
                                String Date = Time.Year + "-" + Time.Month.ToString("00") + "-" + Time.Day.ToString("00");
                                Console.WriteLine(Date);
                                // Find first - in the data
                                int line = 0; // holds the line it was found on
                                string lineInFile = ""; 
                                System.IO.StreamReader Search = new System.IO.StreamReader(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt");
                                while ((lineInFile = Search.ReadLine()) != null)
                                {
                                    if (lineInFile.Contains("/"))
                                    {
                                        break;
                                    }
                                    line++;
                                }
                                // Finds first / in line somewhere
                                Search.Close();
                                String Comparision = "";
                                System.IO.StreamReader Search2 = new System.IO.StreamReader(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt");
                                for (int i = 0; i < line + 1; i++)
                                {
                                    Comparision = Search2.ReadLine();
                                }
                                Search2.Close(); // searches response for /
                                System.Console.WriteLine("New Data Found");
                                // Finds the / in the response
                                string[] NewAndOld = JSONDATA.Split(Comparision); // splits into new and old data
                                String OldFile = File.ReadAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt"); // old file is coppied
                                String Prepend = NewAndOld[0]; // prepend is old file
                                Prepend = Prepend.Remove(Prepend.Length - 5); // fixes formatting
                                String Final = NewAndOld[0] + OldFile; // Final is old + new
                                if (String.IsNullOrWhiteSpace(Prepend) == true) // If prepend is blank
                                {
                                    Console.WriteLine("No new Data found"); // Nothing found
                                    Final = OldFile;
                                }

                                File.WriteAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt", Final); // Write to file
                                String[] PreRemvoal = File.ReadAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt"); 
                                File.WriteAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt", PreRemvoal.Skip(8).ToArray()); // Add prepend
                            }
                        }
                        else
                        {
                            // If file doesnt exist already
                            Console.WriteLine("Creating and writing to file...");
                            File.Create(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt").Dispose();  // Create File
                            // Create File

                            JSONDATA = JSONDATA.Replace("{", "");  // Remove {
                            JSONDATA = JSONDATA.Replace("}", "");  // Remove }
                            JSONDATA = JSONDATA.Replace(",", "");  // Remove ,
                            JSONDATA = JSONDATA.Replace('"', '*'); // Replace " with *
                            JSONDATA = JSONDATA.Replace("*", "");  // Remove Star
                            JSONDATA = JSONDATA.Replace('-', '/'); // Replace - With /
                            JSONDATA = JSONDATA.Replace("Time Series (Daily):", "");
                            JSONDATA = JSONDATA.Replace("Meta Data:", "");
                            JSONDATA = JSONDATA.Replace("1. Information: Daily Prices (open high low close) and Volumes", "");
                            JSONDATA = JSONDATA.Replace("2. Symbol: " + Indicator, "");
                            JSONDATA = JSONDATA.Replace("16:00:01", "");
                            JSONDATA = JSONDATA.Replace("4. Output Size: Compact", "");
                            JSONDATA = JSONDATA.Replace("5. Time Zone: US/Eastern", "");
                            // Removes Extra Info 

                            int refreshedline = JSONDATA.IndexOf("3.");
                            JSONDATA = JSONDATA.Remove(refreshedline, 29);
                            // Fixes formatting

                            File.WriteAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt", JSONDATA);
                            String[] PreRemvoal = File.ReadAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt");
                            File.WriteAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt", PreRemvoal.Skip(8).ToArray());
                            // Wrtie to file
                        }
                    }
                    else
                    {
                        Console.Write("Returning to start");
                    }
                }
            }
        }
        static void UpdateStock()
        {
            string[] Inds = {"AAPL","ABNB","ADBE","ADI","ADP","ADSK","AEP","ALGN","AMAT","AMD","AMGN","AMZN","ANSS","ASML","ATVI","AVGO","AZN","BIDU","BIIB","BKNG","CDNS","CEG","CHTR",
            "CMCSA","COST","CPRT","CRWD","CSCO","CSX","CTAS","CTSH","DDOG","DLTR","DOCU","DXCM","EA","EBAY","EXC","FAST","FISV","FTNT","GILD","GOOG","GOOGL","HON","IDXX","ILMN","INTC",
            "INTU","ISRG","JD","KDP","KHC","KLAC","LCID","LRCX","LULU","MAR","MCHP","MDLZ","MELI","META","MNST","MRNA","MRVL","MSFT","MTCH","MU","NFLX","NTES","NVDA","NXPI","ODFL","OKTA",
            "ORLY","PANW","PAYX","PCAR","PDD","PEP","PYPL","QCOM","REGN","ROST","SBUX","SGEN","SIRI","SNPS","SPLK","SWKS","TEAM","TMUS","TSLA","TXN","VRSK","VRSN","VRTX","WBA","WDAY",
            "XEL","ZM","ZS","KO","F","RTX","BA","LMT","NOC" };\
            // Listing ~100 stocks to update when update is called    
                
            foreach (string Ind in Inds)
            {
                using (WebClient client = new WebClient())
                {
                    String URLPt1 = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";
                    String URlPt2 = Ind + "&apikey=~~~";
                    String CompleteURl = URLPt1 + URlPt2;
                    Uri queryUri = new Uri(CompleteURl);
                    String JSONDATA = client.DownloadString(queryUri);
                    if (File.Exists(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt")) // If the file exdists name = Symbol then write
                    {
                        // If the file exists

                        // Replace Block
                        Console.WriteLine("Writing to file...");
                        JSONDATA = JSONDATA.Replace("{", "");  // Remove {
                        JSONDATA = JSONDATA.Replace("}", "");  // Remove }
                        JSONDATA = JSONDATA.Replace(",", "");  // Remove ,
                        JSONDATA = JSONDATA.Replace('"', '*'); // Replace " with *
                        JSONDATA = JSONDATA.Replace("*", "");  // Remove Star
                        JSONDATA = JSONDATA.Replace('-', '/'); // Replace - With /

                        System.DateTime Time = new System.DateTime();
                        System.DateTime Time2 = new System.DateTime();
                        System.DateTime Time3 = new System.DateTime();
                        Time = DateTime.Now;
                        Time2 = DateTime.Now.AddDays(-1);
                        Time3 = DateTime.Now.AddDays(-2);
                        JSONDATA = JSONDATA.Replace("Time Series (Daily):", "");
                        JSONDATA = JSONDATA.Replace("Meta Data:", "");
                        JSONDATA = JSONDATA.Replace("1. Information: Daily Prices (open high low close) and Volumes", "");
                        JSONDATA = JSONDATA.Replace("2. Symbol: " + Ind, "");
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time.Year + "/" + Time.Month.ToString("00") + "/" + Time.Day.ToString("00"), ""); // Fix Date replacement
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time2.Year + "/" + Time2.Month.ToString("00") + "/" + Time2.Day.ToString("00"), ""); // Fix Date replacement
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time3.Year + "/" + Time3.Month.ToString("00") + "/" + Time3.Day.ToString("00"), ""); // Fix Date replacement
                        JSONDATA = JSONDATA.Replace("16:00:01", "");
                        JSONDATA = JSONDATA.Replace("4. Output Size: Compact", "");
                        JSONDATA = JSONDATA.Replace("5. Time Zone: US/Eastern", "");
                        // This cleans the response from the api of any data we dont need

                        int refreshedline = JSONDATA.IndexOf("3.");
                        JSONDATA = JSONDATA.Remove(refreshedline, 29);
                        // Removes Extra Info String 
                        // Fix the double Space when updating
                        {
                            String Date = Time.Year + "-" + Time.Month.ToString("00") + "-" + Time.Day.ToString("00");
                            // Find first -
                            int line = 0;
                            string lineInFile = "";
                            System.IO.StreamReader Search = new System.IO.StreamReader(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt");
                            while ((lineInFile = Search.ReadLine()) != null)
                            {
                                if (lineInFile.Contains("/"))
                                {
                                    break;
                                }
                                line++;
                            } // go to that line and make it a / rather than -
                            Search.Close();
                            String Comparision = "";
                            System.IO.StreamReader Search2 = new System.IO.StreamReader(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt");
                            for (int i = 0; i < line + 1; i++)
                            {
                                Comparision = Search2.ReadLine();
                            }
                            Search2.Close();
                            
                            // If new data is found take the response and split it based on what the newest day is in file
                            System.Console.WriteLine("New Data Found:");
                            System.Console.WriteLine("New Date Onfile is" + Comparision);
                            string[] NewAndOld = JSONDATA.Split(Comparision);
                            // Add First half of split into file
                            String OldFile = File.ReadAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt");
                            String Prepend = NewAndOld[0];
                            System.Console.WriteLine("News Date Online is" + NewAndOld[0]);
                            System.Console.WriteLine("Old File is" + OldFile);

                            // Test to see if it fixes new line bug
                            Prepend = Prepend.Remove(Prepend.Length - 5);


                            String Final = Prepend + OldFile;
                            System.Console.WriteLine(Final);
                            // Write data

                            if (String.IsNullOrWhiteSpace(Prepend) == true)
                            {
                                Console.WriteLine("No new Data found");
                                Final = OldFile;
                                // If blank no new data is found
                            }

                            File.WriteAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt", Final);
                            String[] PreRemvoal = File.ReadAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt");
                            //File.WriteAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt", PreRemvoal.Skip(8).ToArray());
                            Thread.Sleep(15000); // prevent too many API calls in 1 minutes
                        }
                    }
                    else
                    {
                        Console.WriteLine("Creating and writing to file...");
                        File.Create(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt").Dispose();  // Create File

                        JSONDATA = JSONDATA.Replace("{", "");  // Remove {
                        JSONDATA = JSONDATA.Replace("}", "");  // Remove }
                        JSONDATA = JSONDATA.Replace(",", "");  // Remove ,
                        JSONDATA = JSONDATA.Replace('"', '*'); // Replace " with *
                        JSONDATA = JSONDATA.Replace("*", "");  // Remove Star
                        JSONDATA = JSONDATA.Replace('-', '/'); // Replace - With /


                        // Replace Block
                        System.DateTime Time = new System.DateTime();
                        System.DateTime Time2 = new System.DateTime();
                        System.DateTime Time3 = new System.DateTime();
                        Time = DateTime.Now;
                        Time2 = DateTime.Now.AddDays(-1);
                        Time3 = DateTime.Now.AddDays(-2);
                        JSONDATA = JSONDATA.Replace("Time Series (Daily):", "");
                        JSONDATA = JSONDATA.Replace("Meta Data:", "");
                        JSONDATA = JSONDATA.Replace("1. Information: Daily Prices (open high low close) and Volumes", "");
                        JSONDATA = JSONDATA.Replace("2. Symbol: " + Ind, "");
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time.Year + "/" + Time.Month.ToString("00") + "/" + Time.Day.ToString("00"), ""); // Fix Date replacement
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time2.Year + "/" + Time2.Month.ToString("00") + "/" + Time2.Day.ToString("00"), ""); // Fix Date replacement
                        //JSONDATA = JSONDATA.Replace("3. Last Refreshed: " + Time3.Year + "/" + Time3.Month.ToString("00") + "/" + Time3.Day.ToString("00"), ""); // Fix Date replacement
                        JSONDATA = JSONDATA.Replace("16:00:01", "");
                        JSONDATA = JSONDATA.Replace("4. Output Size: Compact", "");
                        JSONDATA = JSONDATA.Replace("5. Time Zone: US/Eastern", "");
                        // Removes Extra Info String 

                        int refreshedline = JSONDATA.IndexOf("3.");
                        JSONDATA = JSONDATA.Remove(refreshedline, 29);


                        File.WriteAllText(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt", JSONDATA);
                        String[] PreRemvoal = File.ReadAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt");
                        File.WriteAllLines(@"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Ind + ".txt", PreRemvoal.Skip(8).ToArray());
                        Thread.Sleep(15000); // Prevent API call spam
                        // Wrtie to file
                    }
                }
            }
            Main();
        }

        static void Recommend()
        {
            int endofcalc = 0;
            string[] Inds = {"AAPL","ABNB","ADBE","ADI","ADP","ADSK","AEP","ALGN","AMAT","AMD","AMGN","AMZN","ANSS","ASML","ATVI","AVGO","AZN","BIDU","BIIB","BKNG","CDNS","CEG","CHTR",
            "CMCSA","COST","CPRT","CRWD","CSCO","CSX","CTAS","CTSH","DDOG","DLTR","DOCU","DXCM","EA","EBAY","EXC","FAST","FISV","FTNT","GILD","GOOG","GOOGL","HON","IDXX","ILMN","INTC",
            "INTU","ISRG","JD","KDP","KHC","KLAC","LCID","LRCX","LULU","MAR","MCHP","MDLZ","MELI","META","MNST","MRNA","MRVL","MSFT","MTCH","MU","NFLX","NTES","NVDA","NXPI","ODFL","OKTA",
            "ORLY","PANW","PAYX","PCAR","PDD","PEP","PYPL","QCOM","REGN","ROST","SBUX","SGEN","SIRI","SNPS","SPLK","SWKS","TEAM","TMUS","TSLA","TXN","VRSK","VRSN","VRTX","WBA","WDAY",
            "XEL","ZM","ZS","KO","F","RTX","BA","LMT","NOC" };
            
            
            int[] ScoresFinal = new int[Inds.Count()]; // Contain a list of scores for each stock
            float[] ClosingFloatAll = new float[Inds.Count()]; ; // Closing price of all stocks in a float
            // Adds basic Indicator List
            int ScoreArrayItter = 0; // Itterator for for loops
            bool NeuTrade = false;  // Determines if buying with 0 score should be done
            Console.WriteLine("Do you want to trade on score 0 (50% of value of score 1) T for true anything else for false");
            if (Console.ReadLine().ToUpper() == "T")
            {
                NeuTrade = true;
            }
            else
            {
                NeuTrade = false;
            }

            foreach (String Indicator in Inds) // Lists through each ticker
            {
                string StockFile = @"C:\Users\Nick\Desktop\Stock Bot\Stock stuff Net 5\" + Indicator + ".txt"; 
                {
                    // Converison of values
                    String[] StringCloseArrayReverased = File.ReadAllLines(StockFile).Where(i => i.Contains("4. close: ")).ToArray(); // Read all closing Prices
                    float[] Closingfloat = new float[StringCloseArrayReverased.Count()]; // Make an array of floats of the Closing
                    int itter = 0; // Controls array subscript
                    foreach (string close in StringCloseArrayReverased)
                    {
                        String cleaned = close.Replace("4. close:", ""); // Remove some text from string so regex doesnt see 4. and get confused
                        cleaned = Regex.Match(cleaned, @"\d+\.\d+").Value; // Get value of just decimal @ means string literally \d+ means number (at least 1) and \. Means 1 decimal
                        Closingfloat[itter] = float.Parse(cleaned); // Turn the string you just regex'ed into a float
                        itter++;
                    }

                    
                    int score = 0;
                    float Simpleaverage = 0; // Keep this outside the modules as other modules depend on it

                    //Modules Start here Each one has a title that explains what it does
                        
                    {
                        Simpleaverage = 0;
                        for (int i = 0; i < Closingfloat.Count(); i++)
                        {
                            Simpleaverage += Closingfloat[i];
                        }
                        Simpleaverage = Simpleaverage / Closingfloat.Count();
                        Console.WriteLine("Stock " + Indicator + " has an simple average value of " + Simpleaverage);
                    } 

                    // Simple AVG Module (No Score)
                    {
                        //Module RSI Start
                        /*
                        float upavg = 0;
                        float dnavg = 0;
                        for (int i = 14; i > 0; i--)
                        {
                            if (Closingfloat[i] - Closingfloat[i + 1] > 0)
                            {
                                upavg += Closingfloat[i] - Closingfloat[i + 1];
                            }
                            else
                            {
                                dnavg += Math.Abs(Closingfloat[i] - Closingfloat[i + 1]);
                            }
                        }
                        float AvgGain = upavg / 14;
                        float AVGLoss = dnavg / 14;
                        float RSFinal = AvgGain / AVGLoss;
                        float RSIFinal = 100 - (100 / (1 + RSFinal));
                        Console.WriteLine("Stock " + Indicator + " has an RSI value of " + RSIFinal);

                        if(RSIFinal < 10)
                        {
                            Console.WriteLine("The stock is expected to be oversold so you might consider buying now");
                            score += 3; 
                        }
                        else if (RSIFinal < 20)
                        {
                            Console.WriteLine("The stock is expected to be oversold so you might consider buying now");
                            score += 2;
                        }
                        else if (RSIFinal < 30)
                        {
                            Console.WriteLine("The stock is expected to be oversold so you might consider buying now");
                            score += 1;
                        }
                        else if(RSIFinal > 90)
                        {
                            Console.WriteLine("The stock is overbought considering selling");
                            score += -3;
                        }
                        else if (RSIFinal > 80)
                        {
                            Console.WriteLine("The stock is overbought considering selling");
                            score += -2;
                        }
                        else if (RSIFinal > 70)
                        {
                            Console.WriteLine("The stock is overbought considering selling");
                            score += -1;
                        }
                        else
                        {
                            Console.WriteLine("The stock is neutral hold");
                        }
                        */
                        //Module RSI End
                    } // RSI Module
                    {
                        //Module 14 / Max historical Start
                        /* float twoweekavg = 0;
                        for (int i = 14; i > 0; i--)
                        {
                            twoweekavg += Closingfloat[i];
                        }
                        twoweekavg = twoweekavg / 14;
                        Console.WriteLine("Stock " + Indicator + " has a 2 week average of " + twoweekavg);

                        if (twoweekavg > Simpleaverage)
                        {
                            Console.WriteLine("based on a higher 2 week average the stock is expected to rise from the simple average");
                            if (twoweekavg / Simpleaverage >= 1.06)
                                score += 3;
                            else if (twoweekavg / Simpleaverage >= 1.04)
                                score += 2;
                            else if (twoweekavg / Simpleaverage >= 1.02)
                                score += 1;
                        }
                        else
                        {
                            if (twoweekavg / Simpleaverage <= 0.94)
                                score -= 3;
                            else if (twoweekavg / Simpleaverage <= 0.96)
                                score -= 2;
                            else if (twoweekavg / Simpleaverage <= 0.98)
                                score -= 1;
                            Console.WriteLine("based on a higher 2 week average the stock is not expected to rise from the simple average");
                        }
                        //Module 14 / Max historical End
                       */
                    } // 2 Week over max historical Module
                    
                    
                    {
                        //StDev EXP Module Start
                        double Stdev = 0;
                        for (int i = 0; i < Closingfloat.Count(); i++)
                        {
                            Stdev += (Closingfloat[i] - Simpleaverage) * (Closingfloat[i] - Simpleaverage);
                        }
                        Stdev = Stdev / Closingfloat.Count() - 1;
                        Stdev = Math.Sqrt(Stdev);
                        Console.WriteLine("Expected values for stockprice of " + Indicator + " are between " + (Simpleaverage + Stdev) + " and " + (Simpleaverage - Stdev));

                        //Scoring
                        if (Closingfloat[Array.IndexOf(Inds, Indicator)] >= (Closingfloat[Array.IndexOf(Inds, Indicator)] + (Stdev * 2)) || Closingfloat[Array.IndexOf(Inds, Indicator)] <= (Closingfloat[Array.IndexOf(Inds, Indicator)] - (Stdev * 2)))
                        {
                            Console.WriteLine(Indicator + " is outside of  2 * stdev heavily reducing score");
                            score = score - 4;

                        }
                        else if (Closingfloat[Array.IndexOf(Inds, Indicator)] >= (Closingfloat[Array.IndexOf(Inds, Indicator)] + Stdev) || Closingfloat[Array.IndexOf(Inds, Indicator)] <= (Closingfloat[Array.IndexOf(Inds, Indicator)] - Stdev))
                        {
                            Console.WriteLine(Indicator + " is outside of 1 * stdev reducing score");
                            score = score - 2;
                        }
                        //StDev EXP Module end
                    } // Stdev Module
                    
                          // Gets a 30 60 90 and 200 Day avg
                    {
                        //Start of 30 60 90 and 200 Day AVG Module
                        /////////////
                        /////////////
                        // 30 Day 60 Day 90 Day 200 Day AVG / Simple Avg
                        /*
                        float tdavg = 0;
                        if (Closingfloat.Count() >= 30)
                        {
                            for (int i = 30; i > 0; i--)
                            {
                                tdavg += Closingfloat[i];
                            }
                            tdavg = tdavg / 30;
                            Console.WriteLine("Stock " + Indicator + " has a 30 Day average of " + tdavg);
                        }
                        float sdavg = 0;
                        if (Closingfloat.Count() >= 60)
                        {
                            for (int i = 60; i > 0; i--)
                            {
                                sdavg += Closingfloat[i];
                            }
                            sdavg = sdavg / 60;
                            Console.WriteLine("Stock " + Indicator + " has a 60 Day average of " + sdavg);
                        }
                        float ndavg = 0;
                        if (Closingfloat.Count() >= 90)
                        {
                            for (int i = 90; i > 0; i--)
                            {
                                ndavg += Closingfloat[i];
                            }
                            ndavg = ndavg / 90;
                            Console.WriteLine("Stock " + Indicator + " has a 90 Day average of " + ndavg);
                        }
                        float thdavg = 0;
                        if (Closingfloat.Count() >= 200)
                        {
                            for (int i = 200; i > 0; i--)
                            {
                                thdavg += Closingfloat[i];
                            }
                            thdavg = thdavg / 200;
                            Console.WriteLine("Stock " + Indicator + " has a 200 Day average of " + thdavg);
                        }


                        //Scoring
                        if (Closingfloat.Count() >= 30 && tdavg != 0)
                        {
                            if (twoweekavg > tdavg)
                            {
                                Console.WriteLine("based on a higher 2 week average the stock is expected to rise from the 30 day");
                                if (twoweekavg / tdavg >= 1.06)
                                    score += 3;
                                else if (twoweekavg / tdavg >= 1.04)
                                    score += 2;
                                else if (twoweekavg / tdavg >= 1.02)
                                    score += 1;
                            }
                            else
                            {
                                if (twoweekavg / tdavg <= 0.94)
                                    score -= 3;
                                else if (twoweekavg / tdavg <= 0.96)
                                    score -= 2;
                                else if (twoweekavg / tdavg <= 0.98)
                                    score -= 1;
                                Console.WriteLine("based on a lower 2 week average the stock is not expected to rise from the 30 day");
                            }
                        }
                        if (Closingfloat.Count() >= 60 && sdavg != 0)
                        {
                            if (twoweekavg > sdavg)
                            {
                                Console.WriteLine("based on a higher 2 week average the stock is expected to rise from the 60 day");
                                if (twoweekavg / sdavg >= 1.06)
                                    score += 3;
                                else if (twoweekavg / sdavg >= 1.04)
                                    score += 2;
                                else if (twoweekavg / sdavg >= 1.02)
                                    score += 1;
                            }
                            else
                            {
                                if (twoweekavg / sdavg <= 0.94)
                                    score -= 3;
                                else if (twoweekavg / sdavg <= 0.96)
                                    score -= 2;
                                else if (twoweekavg / sdavg <= 0.98)
                                    score -= 1;
                                Console.WriteLine("based on a lower 2 week average the stock is not expected to rise from the 60 day");
                            }
                        }
                        if (Closingfloat.Count() >= 90 && ndavg != 0)
                        {
                            if (twoweekavg > ndavg)
                            {
                                Console.WriteLine("based on a higher 2 week average the stock is expected to rise from the 90 day");
                                if (twoweekavg / ndavg >= 1.06)
                                    score += 3;
                                else if (twoweekavg / ndavg >= 1.04)
                                    score += 2;
                                else if (twoweekavg / ndavg >= 1.02)
                                    score += 1;
                            }
                            else
                            {
                                if (twoweekavg / ndavg <= 0.94)
                                    score -= 3;
                                else if (twoweekavg / ndavg <= 0.96)
                                    score -= 2;
                                else if (twoweekavg / ndavg <= 0.98)
                                    score -= 1;
                                Console.WriteLine("based on a lower 2 week average the stock is not expected to rise from the 90 day");
                            }
                        }
                        if (Closingfloat.Count() >= 200 && thdavg != 0)
                        {
                            if (twoweekavg > thdavg)
                            {
                                Console.WriteLine("based on a higher 2 week average the stock is expected to rise from the 200 day");
                                if (twoweekavg / thdavg >= 1.06)
                                    score += 3;
                                else if (twoweekavg / thdavg >= 1.04)
                                    score += 2;
                                else if (twoweekavg / thdavg >= 1.02)
                                    score += 1;
                            }
                            else
                            {
                                if (twoweekavg / thdavg <= 0.94)
                                    score -= 3;
                                else if (twoweekavg / thdavg <= 0.96)
                                    score -= 2;
                                else if (twoweekavg / thdavg <= 0.98)
                                    score -= 1;
                                Console.WriteLine("based on a lower 2 week average the stock is not expected to rise from the 200 day");
                            }
                        }
                        */
                        // 30 Day 60 Day 90 Day 200 Day AVG / Simple Avg END
                        //End of 30 60 90 and 200 Day AVG Module
                    } // 30 60 90 200 Module
                    
                    // Modules for 50 and 200 day AVG
                    {
                        float fdavg = 0;
                        if (Closingfloat.Count() >= 50)
                        {
                            for (int i = 50; i > 0; i--)
                            {
                                fdavg += Closingfloat[i];
                            }
                            fdavg = fdavg / 50;
                            Console.WriteLine("Stock " + Indicator + " has a 30 Day average of " + fdavg);
                        }
                        float tdavg = 0;
                        if (Closingfloat.Count() >= 200)
                        {
                            for (int i = 200; i > 0; i--)
                            {
                                tdavg += Closingfloat[i];
                            }
                            tdavg = tdavg / 200;
                            Console.WriteLine("Stock " + Indicator + " has a 60 Day average of " + tdavg);
                        }

                        //Scoring
                        if (fdavg > tdavg * 1.08)
                        {
                            score += 3;
                        }
                        else if (fdavg > tdavg * 1.04)
                        {
                            score += 3;
                        }
                        else if (fdavg > tdavg * 1.02)
                        {
                            score += 3;
                        }
                        if (fdavg > tdavg * 0.98)
                        {
                            score -= 1;
                        }
                        else if (fdavg > tdavg * 0.96)
                        {
                            score -= 2;
                        }
                        else if (fdavg > tdavg * 0.92)
                        {
                            score -= 3;
                        }
                    } // 50 / 200 Module






                    ClosingFloatAll[endofcalc] = Closingfloat[0];
                    endofcalc++;

                    Console.WriteLine("Score = " + score);
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////");
                    ScoresFinal[ScoreArrayItter] = score;
                    ScoreArrayItter++;
                    // Formatting + incrimenting var that controls what stock ticker we are on as well as the score indicator
                }
                if (endofcalc == Inds.Count())
                {
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("Results");
                    float money = 0;
                    Console.WriteLine("How much are all your assests worth?");
                    money = float.Parse(Console.ReadLine()); // Read how much your assets are worth

                    //Result Display Module Start
                    float total = 0;
                    foreach (int StockScore in ScoresFinal)
                    {
                        if (StockScore > 0)
                        {
                            total += StockScore; // Total score
                        }
                        else if (StockScore == 0 && NeuTrade == true) // Disable this block
                        {
                            total += 0.5f; // This will add or remove stocks if neutral trade is on
                        }
                    }
                    int itter2 = 0;
                    double cashleftover = 0;
                    
                    
                    foreach (int StockScore in ScoresFinal)
                    {
                        if (StockScore > 0) // If a stock has a score > 0
                        {
                            float Percentage = (((float)StockScore) / (float)total) * 100; 
                            // Percentage of total worth it should be

                            if (Math.Floor(((Percentage / 100) * money) / ClosingFloatAll[itter2]) != 0) // If you can buy more than 0 stocks
                            {
                                Console.WriteLine("Stock " + Inds[itter2] + " Should be about " + Percentage + "% of your portfollio or about " + ((Percentage / 100) * money) + " Which is " +
                                  Math.Floor(((Percentage / 100) * money) / ClosingFloatAll[itter2]) + " Stocks");
                                cashleftover += (((Percentage / 100) * money) - (Math.Floor(((Percentage / 100) * money) / ClosingFloatAll[itter2]) * ClosingFloatAll[itter2]));
                                // Write how much you can buy in terms of $ Shares and then add cash left over 
                            }
                        }
                        else if (StockScore == 0 && NeuTrade == true) // Remove the if else to stock trade on neutral
                        {
                            float PercentageNeu = (0.5f / (float)total) * 100;
                             // Percentage of total worth it should be if neutral trade or 0 score trading is on
                            
                            if (Math.Floor(((PercentageNeu / 100) * money) / ClosingFloatAll[itter2]) != 0)
                            {
                                Console.WriteLine("Stock " + Inds[itter2] + " Should be about " + PercentageNeu + "% of your portfollio or about " + ((PercentageNeu / 100) * money) + " Which is " +
                                  Math.Floor(((PercentageNeu / 100) * money) / ClosingFloatAll[itter2]) + " Stocks");
                                cashleftover += (((PercentageNeu / 100) * money) - (Math.Floor(((PercentageNeu / 100) * money) / ClosingFloatAll[itter2]) * ClosingFloatAll[itter2]));
                                // Write how much you can buy in terms of $ Shares and then add cash left over 
                            }
                        }
                        itter2++;
                    }
                    Console.WriteLine("You should have " + cashleftover + " Cash left over"); // Display how much cash left over
                }
            }
            //Result Display Module Start End
            Console.ReadLine();
            Main();
        }
    }
}

/* APi Key: ~~~~~~ */
/* https://www.alphavantage.co/ */
