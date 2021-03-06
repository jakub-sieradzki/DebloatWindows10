using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace DebloatWindows10
{
    public class AppItem
    {
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string PackageFullName { get; set; }
        public string Logo { get; set; }
        public string DisplayName { get; set; }
    }

    public static class AppsInfo
    {
        private static List<AppItem> InstalledAppsInfo;
        private static List<AppItem> UninstalledAppsInfo;
        private static bool showAllApps = false;

        public static void SetOptions(bool allApps)
        {
            showAllApps = allApps;
        }

        public static List<AppItem> GetInstalledAppsInfo(bool refresh)
        {
            if (InstalledAppsInfo == null || refresh)
            {
                GenerateNewInstalledAppsInfo();
            }
            return InstalledAppsInfo;
        }

        public static List<AppItem> GetUninstalledAppsInfo(bool refresh)
        {
            if (UninstalledAppsInfo == null || refresh)
            {
                GenerateNewUninstalledAppsInfo();
            }
            return UninstalledAppsInfo;
        }

        private static void GenerateNewInstalledAppsInfo()
        {
            var pm = new PackageManager();
            var packages = pm.FindPackagesForUser("");

            InstalledAppsInfo = new List<AppItem>();

            if (showAllApps)
            {
                foreach (var package in packages)
                {
                    if (package.InstalledPath.IndexOf(@"\Program Files\") > 0)
                    {
                        List<string> info = PackageInfo(package);
                        InstalledAppsInfo.Add(
                           new AppItem()
                           {
                               Checked = false,
                               Name = info[0],
                               PackageFullName = info[1],
                               Logo = info[2],
                               DisplayName = info[3]
                           });
                    }
                }
            }
            else
            {
                foreach (var package in packages)
                {
                    if (SuggestedAppsList.appsNameArray.Any(package.Id.Name.ToLower().Contains))
                    {
                        List<string> info = PackageInfo(package);
                        InstalledAppsInfo.Add(
                           new AppItem()
                           {
                               Checked = false,
                               Name = info[0],
                               PackageFullName = info[1],
                               Logo = info[2],
                               DisplayName = info[3]
                           });
                    }
                }
            }


        }

        private static List<AppItem> GenerateNewAllAppsInfo()
        {
            List<AppItem> list = new List<AppItem>();

            var pm = new PackageManager();
            var packages = pm.FindPackages();

            if (showAllApps)
            {
                foreach (var package in packages)
                {
                    if (package.InstalledPath.IndexOf(@"\Program Files\") > 0)
                    {
                        List<string> info = PackageInfo(package);
                        list.Add(
                           new AppItem()
                           {
                               Checked = false,
                               Name = info[0],
                               PackageFullName = info[1],
                               Logo = info[2],
                               DisplayName = info[3]
                           });
                    }
                }
            }
            else
            {
                foreach (var package in packages)
                {
                    if (SuggestedAppsList.appsNameArray.Any(package.Id.Name.ToLower().Contains))
                    {
                        List<string> info = PackageInfo(package);
                        list.Add(
                           new AppItem()
                           {
                               Checked = false,
                               Name = info[0],
                               PackageFullName = info[1],
                               Logo = info[2],
                               DisplayName = info[3]
                           });
                    }
                }
            }


            return list;
        }

        private static void GenerateNewUninstalledAppsInfo()
        {
            List<AppItem> allApps = GenerateNewAllAppsInfo();
            List<AppItem> installedApps = GetInstalledAppsInfo(true);

            UninstalledAppsInfo = new List<AppItem>();

            for (int i = 0; i < allApps.Count; i++)
            {
                if (installedApps.Find(x => x.Name == allApps[i].Name) == null)
                {
                    UninstalledAppsInfo.Add(allApps[i]);
                }
            }
        }

        private static List<string> PackageInfo(Package package)
        {
            List<string> info = new List<string>();

            try
            {
                info.Add(package.Id.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                info.Add("");
            }

            try
            {
                info.Add(package.Id.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                info.Add("");
            }

            try
            {
                info.Add(package.Logo.AbsoluteUri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                info.Add("");
            }

            try
            {
                info.Add(package.DisplayName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                info.Add("");
            }

            return info;
        }
    }
}