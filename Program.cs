using System;

namespace HuaiweiPhoneExportFileDateReapir {
    class Program {
        static void Main(string[] args) {
            string path = @"D:\test";
            ProcessDirectory(path);
            Console.WriteLine("done.");
        }


        /// <summary>
        /// 处理目录
        /// </summary>
        /// <param name="path">目录路径</param>
        static void ProcessDirectory(string path) {
            if (!System.IO.Directory.Exists(path))
                return;

            var root = new System.IO.DirectoryInfo(path);
            Console.WriteLine($"【目录】{ root.Name }, {root.FullName}");

            foreach (var fileInfo in root.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly)) {
                Console.WriteLine($"    {fileInfo.Name}");
                ProcessFile(fileInfo);
            }
            foreach (var children in root.GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly)) {
                ProcessDirectory(children.FullName);
            }
        }

        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        static void ProcessFile(System.IO.FileInfo fileInfo) {
            const string regex = "_(?<year>[0-9]{4})(?<month>[0-9]{2})(?<day>[0-9]{2})_(?<hour>[0-9]{2})(?<minute>[0-9]{2})(?<second>[0-9]{2})";
            string prefix = fileInfo.Name.Split('_')[0];
            switch (prefix) {
                //手机相册
                case "IMG":
                //截屏录屏
                case "Screenshot":
                //运动健康
                case "Health":
                //手机视频
                case "VID": {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(fileInfo.Name, regex)) {
                            Console.WriteLine("        【忽略】");
                            break;
                        }
                        var match = System.Text.RegularExpressions.Regex.Match(fileInfo.Name, regex);
                        int year = int.Parse(match.Groups["year"].Value);
                        int month = int.Parse(match.Groups["month"].Value);
                        int day = int.Parse(match.Groups["day"].Value);
                        int hour = int.Parse(match.Groups["hour"].Value);
                        int minute = int.Parse(match.Groups["minute"].Value);
                        int second = int.Parse(match.Groups["second"].Value);
                        var time = new DateTime(year, month, day, hour, minute, second);
                        Console.WriteLine($"        {time:yyyy-MM-dd HH:mm:ss}");
                        fileInfo.CreationTime = time;
                        fileInfo.LastWriteTime = time;
                        fileInfo.LastAccessTime = time;

                    }
                    break;
                default: {
                        Console.WriteLine("        【忽略】");
                    }
                    break;
            }

        }
        /// <summary>
        /// 处理文件：视频
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        static void ProcessFile_Video(System.IO.FileInfo fileInfo) {
        }
        /// <summary>
        /// 处理文件：照片
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        static void ProcessFile_Photo(System.IO.FileInfo fileInfo) {
        }

        /// <summary>
        /// 处理文件：截图
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        static void ProcessFile_Screenshot(System.IO.FileInfo fileInfo) {
        }


    }
}
