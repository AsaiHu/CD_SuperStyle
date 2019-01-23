using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SwfUtil
    {
        /// <summary>
        /// 把PDF文件转化为SWF文件
        /// </summary>
        /// <param name="toolPah">pdf2swf工具路径</param>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>true=转化成功</returns>
        public static bool PDFToSWF(string toolPath, string sourcePath, string targetPath)
        {
            Process pc = new Process();
            bool returnValue = true;

            string cmd = toolPath;
            string args = " -t " + sourcePath + " -s flashversion=9 -o " + targetPath;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo = psi;
                pc.Start();
                pc.WaitForExit();
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw new Exception(ex.Message);
            }
            finally
            {
                pc.Close();
                pc.Dispose();
            }
            return returnValue;
        }
    }
}
