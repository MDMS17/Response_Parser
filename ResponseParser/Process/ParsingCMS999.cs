using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseParser.Model;
using System.Threading;
using System.Configuration;
using System.IO;

namespace ResponseParser.Process
{
    public partial class ParsingProcess
    {
        public static async Task ParsingCMS999(CancellationToken cts)
        {
            string _999SourceFolder = ConfigurationManager.AppSettings["999SourceFolder"];
            string _999ArchiveFolder = ConfigurationManager.AppSettings["999ArchiveFolder"];
            string _999LogFolder = ConfigurationManager.AppSettings["999LogFolder"];
            if (!Directory.Exists(_999ArchiveFolder)) Directory.CreateDirectory(_999ArchiveFolder);
            if (!Directory.Exists(_999LogFolder)) Directory.CreateDirectory(_999LogFolder);
            if (_999SourceFolder != null && Directory.GetFiles(_999SourceFolder, "*", SearchOption.AllDirectories).Length > 0)
            {
                System.Text.StringBuilder sbLog = new StringBuilder();
                sbLog.AppendLine("Start time:" + DateTime.Now.ToString());
                try
                {
                    DirectoryInfo di = new DirectoryInfo(_999SourceFolder);
                    FileInfo[] fis = di.GetFiles();
                    Parallel.ForEach(fis, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (fi) => {
                        using (var context = new Cms999Context())
                        {
                            if (cts.IsCancellationRequested) return;
                            List<_999Transaction> transactions = new List<_999Transaction>();
                            List<_999Error> errors = new List<_999Error>();
                            List<_999Element> elements = new List<_999Element>();
                            _999File processingFile = context.Table999File.FirstOrDefault(x => x.FileName == fi.Name);
                            if (processingFile != null)
                            {
                                Console.WriteLine("File " + fi.Name + " already processed before");
                                sbLog.AppendLine("File " + fi.Name + " already processed before");
                                return;
                            }
                            string s999 = File.ReadAllText(fi.FullName);
                            string[] s999Lines = s999.Split('~');
                            s999 = null;

                            processingFile = new _999File();
                            processingFile.FileName = fi.Name;

                            string tempSeg = s999Lines[1];
                            string[] tempArray = tempSeg.Split('*');
                            processingFile.ReceiverId = tempArray[2];
                            processingFile.SenderId = tempArray[3];
                            processingFile.TransactionDate = tempArray[4];
                            processingFile.TransactionTime = tempArray[5];
                            tempSeg = s999Lines[0];
                            tempArray = tempSeg.Split('*');
                            processingFile.ICN = tempArray[13];
                            processingFile.ProductionFlag = tempArray[15];
                            processingFile.CreateUser = Environment.UserName;
                            processingFile.CreateDate = DateTime.Today;
                            context.Table999File.Add(processingFile);
                            context.SaveChanges();
                            string loopName = "";
                            foreach (string s999Line in s999Lines)
                            {
                                Parser.Parser.Parser999Line(s999Line, ref processingFile, ref transactions, ref errors, ref elements, ref loopName);
                            }

                            context.Table999Transaction.AddRange(transactions);
                            context.Table999Error.AddRange(errors); context.Table999Element.AddRange(elements);
                            await context.SaveChangesAsync(cts);
                        }
                        if (File.Exists(Path.Combine(_999ArchiveFolder, fi.Name))) File.Delete(Path.Combine(_999ArchiveFolder, fi.Name));
                        fi.MoveTo(Path.Combine(_999ArchiveFolder, fi.Name));
                    });
                }
                catch (Exception ex)
                {
                    sbLog.AppendLine(ex.Message);
                }
                finally
                {
                    sbLog.AppendLine("End time:" + DateTime.Now.ToString());
                    File.AppendAllText(Path.Combine(_999LogFolder, "999Log.txt"), sbLog.ToString());
                }
            }

        }
    }
}
