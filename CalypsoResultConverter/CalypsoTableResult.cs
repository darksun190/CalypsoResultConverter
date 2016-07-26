using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CalypsoResultConverter
{
    /// <summary>
    /// input is a folder which Calypso save table file results
    /// Raise the event ResultAdded when Calypso generate a result;
    /// </summary>
    public class CalypsoTableResult
    {
        public delegate void ResultGeneratedEventHandler(object sender, CalypsoResultEventArgs cre);
        public event ResultGeneratedEventHandler ResultAdded;

        Queue<string> fet_file_queue = new Queue<string>();
        FileSystemWatcher FSW = new FileSystemWatcher();
        public CalypsoTableResult(string directory_path) :
            this(new DirectoryInfo(directory_path))
        {

        }
        public CalypsoTableResult(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                throw new DirectoryNotFoundException(string.Format("Path: {0} didn't exist", di.FullName));
            }
            FSW.Path = di.FullName;
            FSW.Filter = "*_fet.txt";
            FSW.Changed += FSW_Changed;
            FSW.Created += FSW_Created;
            FSW.EnableRaisingEvents = true;
        }

        private void FSW_Created(object sender, FileSystemEventArgs e)
        {
            string file_name = e.FullPath;
            fet_file_queue.Enqueue(file_name);
        }

        private void FSW_Changed(object sender, FileSystemEventArgs e)
        {
            string file_name = e.FullPath;
            if (file_name != fet_file_queue.Dequeue())
            {
                return;
            }
            if (!checkHdrChrFile(file_name))
            {
                return;
            }
            var fetFile = new FileInfo(file_name);
            string pre_file_name = fetFile.FullName;
            pre_file_name = pre_file_name.Substring(0, pre_file_name.Count() - 8);
            string chr_name = pre_file_name + "_chr.txt";
            FileInfo chrFile = new FileInfo(chr_name);
            string hdr_name = pre_file_name + "_hdr.txt";

            FileInfo hdrFile = new FileInfo(hdr_name);
            if (
                chrFile.Exists
                &&
                hdrFile.Exists
                &&
                fetFile.Exists
                )
            {
                System.Threading.Thread.Sleep(1000);
              
                OperationResult OR = getORfromFile(hdrFile, chrFile, fetFile);
               
                ResultAdded(this, new CalypsoResultEventArgs(OR));

            }


        }


        private bool checkHdrChrFile(string file_name)
        {

            FileInfo fi = new FileInfo(file_name);
            string pre_file_name = fi.FullName;
            pre_file_name = pre_file_name.Substring(0, pre_file_name.Count() - 8);
            string chr_name = pre_file_name + "_chr.txt";
            string hdr_name = pre_file_name + "_hdr.txt";
            if (
                System.IO.File.Exists(chr_name)
                &&
                System.IO.File.Exists(hdr_name)
                )
            {
                return true;
            }
            return false;
        }
        public static OperationResult getORfromFile(FileInfo hdrFile, FileInfo chrFile, FileInfo fetFile)
        {
            CharacteristicTable chrContent = new CharacteristicTable(chrFile);
            FeatureTable fetContent = new FeatureTable(fetFile);
            HeaderTable hdrContent = new HeaderTable(hdrFile);
            OperationResult OR = new OperationResult()
            {
                CharData = chrContent,
                FeatureData = fetContent,
                HeaderData = hdrContent
            };
            return OR;
        }
    }
}
