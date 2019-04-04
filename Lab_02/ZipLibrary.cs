using CliWrap;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02
{
    public class ZipLibrary
    {
        public int Create(string outPathname, string folderName)
        {
            var arguments = $@"/c 7z a -tzip {outPathname} {folderName}";

            try
            {
                var stdout = Cli.Wrap(@"C:\Windows\System32\cmd.exe").SetArguments(arguments).Execute();
            }
            catch (Exception ex)
            {
                return 1;
            }

            return 0;
        }
    }
}
