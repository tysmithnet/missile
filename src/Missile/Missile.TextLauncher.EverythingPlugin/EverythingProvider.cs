using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CommandLine;
using Missile.TextLauncher.ListPlugin;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.EverythingPlugin
{
    [Export(typeof(IProvider))]
    public class EverythingProvider : IProvider<object>
    {   
        public string Name { get; set; } = "everything";

        public IObservable<object> Provide(string[] args)
        {                           
            if(args == null || args.Length == 0)
                throw new ArgumentException($"{nameof(args)} should have at least 1 element");

            var commandArgs = args.Take(args.Length - 1).ToArray();
            var options = new EverythingProviderOptions();
            Parser.Default.ParseArgumentsStrict(commandArgs, options); // todo: handle bad args
            Everything_SetRegex(options.IsRegex);                                              
            Everything_SetSearchW(args.Last());
            Everything_SetMax(1000); // todo: add to settings and options, options overrides
            return GetFiles().ToObservable();
        }

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern int Everything_SetSearchW(string lpSearchString);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRegex(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMax(int dwMax);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetOffset(int dwOffset);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetReplyWindow(IntPtr hWnd);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetReplyID(int nId);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchPath();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchCase();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchWholeWord();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetRegex();

        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetMax();

        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetOffset();

        [DllImport("Everything64.dll")]
        public static extern string Everything_GetSearch();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetLastError();

        [DllImport("Everything64.dll")]
        public static extern IntPtr Everything_GetReplyWindow();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetReplyID();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsQueryReply(int message, IntPtr wParam, IntPtr lParam, uint nId);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumFileResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumFolderResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotFileResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotFolderResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotResults();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsVolumeResult(int nIndex);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFolderResult(int nIndex);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFileResult(int nIndex);

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathNameW(int nIndex, StringBuilder lpString, int nMaxCount);

        [DllImport("Everything64.dll")]
        public static extern void Everything_Reset();

        public IEnumerable<FileListDestinationItem> GetFiles()
        {
            const int bufferSize = 1 << 20;
            var stringBuilder = new StringBuilder(bufferSize);
            Everything_QueryW(true);
            var numResults = Everything_GetNumResults();                           
            for (var i = 0; i < numResults; i++)
            {
                Everything_GetResultFullPathNameW(i, stringBuilder, bufferSize);
                var fileInfo = new FileInfo(stringBuilder.ToString());
                FileListDestinationItem info = null;
                try
                {
                    info = new FileListDestinationItem(fileInfo);
                }
                catch (IOException e)
                {
                    //todo: do something with this
                }
                catch (UnauthorizedAccessException e)
                {
                    // todo: do something with this
                }

                if (info != null)
                    yield return info;

                stringBuilder.Clear();
            }
        }
    }


    public class EverythingProviderOptions
    {
        [Option('r', "regex", HelpText = "Indicates the search text is a regular expression")]
        public bool IsRegex { get; set; }
    }
}