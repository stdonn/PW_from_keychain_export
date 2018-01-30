using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PW_from_keychain_export
{
    public class ProcessKeychain
    {
        protected void onFileNotFound(string path)
        {
            FileNotFoundException ex = new FileNotFoundException("Specified file doesn't exist!");
            ex.Data.Add("Time", DateTime.Now);
            ex.Data.Add("File", path);

            throw ex;
        }

        public bool processKeychainFile(string inFile, string outFile)
        {
            if (!File.Exists(inFile))
            {
                onFileNotFound(inFile);
                return false;
            }

            List<KeychainEntry> entries = new List<KeychainEntry>();
            SortedSet<String> types = new SortedSet<string>();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Console.WriteLine("Start reading the Keychain file...");
            KeychainEntry entry = new KeychainEntry();
            entry.group = "Keychain Import";
            entry.notes = "";
            entry.password = "";
            entry.title = "";
            entry.url = "";
            entry.username = "";
            bool first = true;
            bool nextpassword = false;

            string[] lines = File.ReadAllLines(inFile);
            foreach (string line in lines)
            {
                line.Trim();
                // first -> new keychain!?
                if (line.Contains("keychain: "))
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    else
                    {
                        if (entry.notes == "inet")
                            entries.Add(entry);
                        entry = new KeychainEntry();
                        entry.group = "Keychain Import";
                        entry.notes = "";
                        entry.password = "";
                        entry.title = "";
                        entry.url = "";
                        entry.username = "";
                    }
                }

                if (line.Contains("srvr") || line.Contains("svce"))
                {
                    entry.title = line.Replace("\"srvr\"<blob>=", "").Replace("\"svce\"<blob>=\"", "").Trim().Trim('"').Replace("\"", "\\\"");
                    entry.url = entry.title;
                }
                else if (line.Contains("acct"))
                    entry.username = line.Replace("\"acct\"<blob>=", "").Trim().Trim('"').Replace("\"", "\\\"");
                else if (line.Contains("class: "))
                {
                    entry.notes = line.Replace("class: ", "").Trim().Trim('"').Replace("\"", "\\\"");
                    types.Add(entry.notes);
                }
                else if (line.Contains("data:"))
                    nextpassword = true;
                else if (nextpassword) {
                    entry.password = line.Trim().Trim('"').Replace("\"", "\\\"");;
                }
            }
            if (entry.notes == "inet")
                entries.Add(entry);
            Console.WriteLine("Found {0} entries for inet...", entries.Count);

            StringBuilder csvData = new StringBuilder(4096);
            csvData.AppendLine("\"Group\", \"Title\", \"Username\", \"Password\", \"URL\", \"Notes\"");

            foreach (KeychainEntry ent in entries)
            {
                csvData.Append("\"" + ent.group + "\",");
                csvData.Append("\"" + ent.title + "\",");
                csvData.Append("\"" + ent.username + "\",");
                csvData.Append("\"" + ent.password + "\",");
                csvData.Append("\"" + ent.url + "\",");
                csvData.AppendLine("\"" + ent.notes + "\"");
            }

            Console.WriteLine("\nFound following additional classes:");
            foreach (string t in types)
                Console.WriteLine(t);

            Console.WriteLine("Start writing output CSV file...");
            File.WriteAllText(outFile, csvData.ToString());

            watch.Stop();
            Console.WriteLine("Finished conversion...");
            Console.WriteLine("Time: {0} ms", watch.ElapsedMilliseconds);

            return true;
        }
    }
}
