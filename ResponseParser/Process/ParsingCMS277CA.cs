using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ResponseParser.Model;
using System.IO;

namespace ResponseParser.Process
{
    public partial class ParsingProcess
    {
        public static async Task ParsingCMS277CA(CancellationToken cts)
        {
            string _277CaSourceFolder = ConfigurationManager.AppSettings["277CASourceFolder"];
            string _277CaArchiveFolder = ConfigurationManager.AppSettings["277CAArchiveFolder"];
            string _277CaLogFolder = ConfigurationManager.AppSettings["277CALogFolder"];
            if (!Directory.Exists(_277CaArchiveFolder)) Directory.CreateDirectory(_277CaArchiveFolder);
            if (!Directory.Exists(_277CaLogFolder)) Directory.CreateDirectory(_277CaLogFolder);
            if (_277CaSourceFolder != null && Directory.GetFiles(_277CaSourceFolder, "*", SearchOption.AllDirectories).Length > 0)
            {
                System.Text.StringBuilder sbLog = new StringBuilder();
                sbLog.AppendLine("Start time:" + DateTime.Now.ToString());
                try
                {
                    DirectoryInfo di = new DirectoryInfo(_277CaSourceFolder);
                    FileInfo[] fis = di.GetFiles();
                    Parallel.ForEach(fis, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (fi) => {
                        using (var context = new Cms277CAContext())
                        {
                            if (cts.IsCancellationRequested) return;
                            List<_277CABillProv> billProvs = new List<_277CABillProv>();
                            List<_277CAPatient> patients = new List<_277CAPatient>();
                            List<_277CALine> lines = new List<_277CALine>();
                            List<_277CAStc> stcs = new List<_277CAStc>();
                            _277CAFile processingFile = context.Table277CAFile.FirstOrDefault(x => x.FileName == fi.Name);
                            if (processingFile != null)
                            {
                                Console.WriteLine("File " + fi.Name + " already processed before");
                                sbLog.AppendLine("File " + fi.Name + " already processed before");
                                return;
                            }
                            string s277CA = File.ReadAllText(fi.FullName);
                            string[] s277CALines = s277CA.Split('~');
                            s277CA = null;
                            int encounterCount = s277CALines.Count(x => x.StartsWith("TRN*2*")) - 1;
                            if (encounterCount <= 0)
                            {
                                Console.WriteLine("File " + fi.Name + " not valid");
                                sbLog.AppendLine("File " + fi.Name + " not valid");
                                return;
                            }
                            Console.WriteLine("Processing file " + fi.Name + " total records: " + encounterCount.ToString());
                            sbLog.AppendLine("Processing file " + fi.Name + " total records: " + encounterCount.ToString());

                            processingFile = new _277CAFile();
                            processingFile.FileName = fi.Name;

                            string tempSeg = s277CALines[1];
                            string[] tempArray = tempSeg.Split('*');
                            processingFile.ReceiverId = tempArray[2];
                            processingFile.SenderId = tempArray[3];
                            processingFile.TransactionDate = tempArray[4];
                            processingFile.TransactionTime = tempArray[5];
                            tempSeg = s277CALines[5];
                            tempArray = tempSeg.Split('*');
                            processingFile.ReceiverName = tempArray[3];
                            tempSeg = s277CALines[10];
                            tempArray = tempSeg.Split('*');
                            processingFile.SenderName = tempArray[3];
                            tempSeg = s277CALines[11];
                            tempArray = tempSeg.Split('*');
                            processingFile.BatchId = tempArray[2];
                            tempSeg = s277CALines[0];
                            tempArray = tempSeg.Split('*');
                            processingFile.ICN = tempArray[13];

                            processingFile.CreateUser = Environment.UserName;
                            processingFile.CreateDate = DateTime.Today;
                            context.Table277CAFile.Add(processingFile);
                            context.SaveChanges();

                            string LoopName = "";
                            foreach (string s277CALine in s277CALines)
                            {
                                Parser.Parser.Parser277CALine(s277CALine, ref processingFile, ref billProvs, ref patients,
                                    ref lines, ref stcs, ref LoopName);
                            }

                            context.Table277CABillProv.AddRange(billProvs);
                            context.Table277CAPatient.AddRange(patients);
                            context.Table277CALine.AddRange(lines);
                            context.Table277CAStc.AddRange(stcs);
                            await context.SaveChangesAsync(cts);
                        }
                        if (File.Exists(Path.Combine(_277CaArchiveFolder, fi.Name))) File.Delete(Path.Combine(_277CaArchiveFolder, fi.Name));
                        fi.MoveTo(Path.Combine(_277CaArchiveFolder, fi.Name));
                    });
                }
                catch (Exception ex)
                {
                    sbLog.AppendLine(ex.Message);
                }
                finally
                {
                    sbLog.AppendLine("End time:" + DateTime.Now.ToString());
                    File.AppendAllText(Path.Combine(_277CaLogFolder, "277CALog.txt"), sbLog.ToString());
                }

            }

        }

    }
}
