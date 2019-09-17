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
        public static async Task ParsingCMSMAO2(CancellationToken cts)
        {
            string MAO2SourceFolder = ConfigurationManager.AppSettings["MAO2SourceFolder"];
            string MAO2ArchiveFolder = ConfigurationManager.AppSettings["MAO2ArchiveFolder"];
            string MAO2LogFolder = ConfigurationManager.AppSettings["MAO2LogFolder"];
            if (!Directory.Exists(MAO2ArchiveFolder)) Directory.CreateDirectory(MAO2ArchiveFolder);
            if (!Directory.Exists(MAO2LogFolder)) Directory.CreateDirectory(MAO2LogFolder);
            if (MAO2SourceFolder != null && Directory.GetFiles(MAO2SourceFolder, "*", SearchOption.AllDirectories).Length > 0)
            {
                System.Text.StringBuilder sbLog = new StringBuilder();
                sbLog.AppendLine("Start time:" + DateTime.Now.ToString());
                try
                {
                    DirectoryInfo di = new DirectoryInfo(MAO2SourceFolder);
                    FileInfo[] fis = di.GetFiles();
                    Parallel.ForEach(fis, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (fi) => {
                        using (var context = new CmsMao2Context())
                        {
                            if (cts.IsCancellationRequested) return;
                            List<MAO2Detail> details = new List<MAO2Detail>();
                            MAO2File processingFile = context.TableMao2File.FirstOrDefault(x => x.FileName == fi.Name);
                            if (processingFile != null)
                            {
                                Console.WriteLine("File " + fi.Name + " already processed before");
                                sbLog.AppendLine("File " + fi.Name + " already processed before");
                                return;
                            }

                            processingFile = new MAO2File
                            {
                                FileName = fi.Name,
                                CreateUser = Environment.UserName,
                                CreateDate = DateTime.Today
                            };
                            context.TableMao2File.Add(processingFile);
                            context.SaveChanges();
                            using (StreamReader sr = fi.OpenText())
                            {
                                string line = "";
                                while ((line = sr.ReadLine()) != null)
                                {
                                    Parser.Parser.ParserMao2Line(line, ref processingFile, ref details);
                                }
                            }

                            context.TableMao2Detail.AddRange(details);
                            await context.SaveChangesAsync(cts);
                        }
                        if (File.Exists(Path.Combine(MAO2ArchiveFolder, fi.Name))) File.Delete(Path.Combine(MAO2ArchiveFolder, fi.Name));
                        fi.MoveTo(Path.Combine(MAO2ArchiveFolder, fi.Name));
                    });
                }
                catch (Exception ex)
                {
                    sbLog.AppendLine(ex.Message);
                }
                finally
                {
                    sbLog.AppendLine("End time:" + DateTime.Now.ToString());
                    File.AppendAllText(Path.Combine(MAO2LogFolder, "Mao2Log.txt"), sbLog.ToString());
                }
            }
        }
    }
}
