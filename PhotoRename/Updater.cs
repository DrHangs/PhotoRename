using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;

namespace PhotoRename
{
    internal class Updater
    {
        static async Task<string> GetLatestGitHubTagAsync(string user, string repo)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("PhotoRename", "1.0"));

            string url = $"https://api.github.com/repos/{user}/{repo}/releases/latest";
            var response = await http.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);
            string? tag = doc.RootElement.GetProperty("tag_name").GetString();

            return string.IsNullOrEmpty(tag) ? string.Empty : tag; // z. B. "v1.0.1"
        }

        static bool IsNewerVersion(string githubTag, Version current)
        {
            if (githubTag.StartsWith('v'))
                githubTag = githubTag[1..];

            if (!Version.TryParse(githubTag, out Version? githubVersion))
                return false;

            return githubVersion > current;
        }

        public static async Task CheckForUpdatesAsync()
        {
            Version current = Assembly.GetExecutingAssembly().GetName().Version!;
            string latestTag = await GetLatestGitHubTagAsync("DrHangs", "PhotoRename");
            //string latestTag = await GetLatestVersion();

            if (IsNewerVersion(latestTag, current))
            {
                var result = MessageBox.Show(
                    $"Ein neues Update ({latestTag}) ist verfügbar!\nWillst du zur Download-Seite gehen?",
                    "Update verfügbar",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information
                );

                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://github.com/DrHangs/PhotoRename/releases/latest",
                        UseShellExecute = true
                    });
                }
            }
        }

    }
}
