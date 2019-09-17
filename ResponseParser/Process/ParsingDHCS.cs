using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ResponseParser.Model;
using System.IO;
using System.Xml.Linq;

namespace ResponseParser.Process
{
    public partial class ParsingProcess
    {
        public static async Task ParsingDHCS(CancellationToken cts)
        {
            string DHCSSourceFolder = ConfigurationManager.AppSettings["DHCSSourceFolder"];
            string DHCSArchiveFolder = ConfigurationManager.AppSettings["DHCSArchiveFolder"];
            string DHCSLogFolder = ConfigurationManager.AppSettings["DHCSLogFolder"];
            if (!Directory.Exists(DHCSArchiveFolder)) Directory.CreateDirectory(DHCSArchiveFolder);
            if (!Directory.Exists(DHCSLogFolder)) Directory.CreateDirectory(DHCSLogFolder);
            //processing dhcs response files
            if (DHCSSourceFolder != null && Directory.GetFiles(DHCSSourceFolder, "*", SearchOption.AllDirectories).Length > 0)
            {
                System.Text.StringBuilder sbLog = new StringBuilder();
                sbLog.AppendLine("Start time:" + DateTime.Now.ToString());
                try
                {
                    DirectoryInfo di = new DirectoryInfo(DHCSSourceFolder);
                    FileInfo[] fis = di.GetFiles();
                    Parallel.ForEach(fis, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (fi) => {
                        using (var context = new DHCSContext())
                        {
                            if (cts.IsCancellationRequested) return;
                            List<DHCSTransaction> transactions = new List<DHCSTransaction>();
                            List<DHCSEncounter> encounters = new List<DHCSEncounter>();
                            List<DHCSEncounterResponse> responses = new List<DHCSEncounterResponse>();
                            DHCSFile processingFile = context.TableDHCSFile.FirstOrDefault(x => x.FileName == fi.Name);
                            if (processingFile != null)
                            {
                                Console.WriteLine("File " + fi.Name + " already processed before");
                                sbLog.AppendLine("File " + fi.Name + " already processed before");
                                return;
                            }
                            Console.WriteLine("File " + fi.Name + " is processing now...");
                            sbLog.AppendLine("File " + fi.Name + " is processing now...");

                            XDocument xDoc = XDocument.Load(fi.FullName);
                            XNamespace ns = "http://www.dhcs.ca.gov/EDS/DHCSResponse";
                            processingFile = new DHCSFile
                            {
                                FileName = fi.Name,
                                EncounterFileName = xDoc.Descendants(ns + "EncounterFileName").FirstOrDefault()?.Value,
                                SubmitterName =
                                xDoc.Descendants(ns + "EncounterSubmitterName").FirstOrDefault()?.Value,
                                SubmissionDate =
                                xDoc.Descendants(ns + "EncounterSubmissionDate").FirstOrDefault()?.Value,
                                ValidationStatus = xDoc.Descendants(ns + "ValidationStatus").FirstOrDefault()?.Value,
                                CreateUser = Environment.UserName,
                                CreateDate = DateTime.Today
                            };
                            context.TableDHCSFile.Add(processingFile);
                            context.SaveChanges();
                            foreach (XElement eleTransaction in xDoc.Descendants(ns + "Transaction"))
                            {
                                DHCSTransaction transaction = new DHCSTransaction();
                                transaction.FileId = processingFile.FileId;
                                transaction.TransactionStatus = eleTransaction.Attributes("Status").FirstOrDefault()?.Value;
                                transaction.TransactionNumber =
                                    eleTransaction.Descendants(ns + "TransactionNumber").FirstOrDefault()?.Value;
                                foreach (XElement ele in eleTransaction.Descendants(ns + "Identifiers").Descendants(ns + "Envelope"))
                                {
                                    switch (ele.Attributes("IdentifierName").FirstOrDefault()?.Value)
                                    {
                                        case "ISAControlNumber":
                                            transaction.ISAControlNumber =
                                                ele.Attributes("IdentifierValue").FirstOrDefault()?.Value;
                                            break;
                                        case "GroupControlNumber":
                                            transaction.GroupControlNumber =
                                                ele.Attributes("IdentifierValue").FirstOrDefault()?.Value;
                                            break;
                                        case "OriginatorTransactionId":
                                            transaction.OriginatorTransactionId =
                                                ele.Attributes("IdentifierValue").FirstOrDefault()?.Value;
                                            break;
                                    }
                                }
                                transactions.Add(transaction);
                                foreach (XElement eleEncounter in eleTransaction.Descendants(ns + "Encounter"))
                                {
                                    DHCSEncounter encounter = new DHCSEncounter
                                    {
                                        FileId = processingFile.FileId,
                                        TransactionNumber = transactions.Last().TransactionNumber,
                                        EncounterStatus = eleEncounter.Attributes("Status").FirstOrDefault()?.Value,
                                        EncounterReferenceNumber =
                                        eleEncounter.Descendants(ns + "EncounterReferenceNumber").FirstOrDefault()?.Value,
                                        DHCSEncounterId = eleEncounter.Descendants(ns + "EncounterId").FirstOrDefault()?.Value
                                    };
                                    encounters.Add(encounter);
                                    foreach (XElement eleResponse in eleEncounter.Descendants(ns + "Response"))
                                    {
                                        DHCSEncounterResponse response = new DHCSEncounterResponse
                                        {
                                            FileId = processingFile.FileId,
                                            TransactionNumber = transactions.Last().TransactionNumber,
                                            EncounterReferenceNumber = encounters.Last().EncounterReferenceNumber,
                                            Severity = eleResponse.Attributes("Severity").FirstOrDefault()?.Value,
                                            IssueId = eleResponse.Descendants(ns + "Id").FirstOrDefault()?.Value,
                                            IsSNIP = eleResponse.Descendants(ns + "IsSNIP").FirstOrDefault()?.Value,
                                            IssueDescription =
                                            eleResponse.Descendants(ns + "Description").FirstOrDefault()?.Value
                                        };
                                        responses.Add(response);
                                    }
                                }
                            }

                            context.TableDHCSTransaction.AddRange(transactions);
                            context.TableDHCSEncounter.AddRange(encounters);
                            context.TableDHCSEncounterResponse.AddRange(responses);
                            await context.SaveChangesAsync(cts);
                        }
                        if (File.Exists(Path.Combine(DHCSArchiveFolder, fi.Name))) File.Delete(Path.Combine(DHCSArchiveFolder, fi.Name));
                        fi.MoveTo(Path.Combine(DHCSArchiveFolder, fi.Name));
                    });
                }
                catch (Exception ex)
                {
                    sbLog.AppendLine(ex.Message);
                }
                finally
                {
                    sbLog.AppendLine("End time:" + DateTime.Now.ToString());
                    File.AppendAllText(Path.Combine(DHCSLogFolder, "DHCSLog.txt"), sbLog.ToString());
                }
            }
        }
    }
}
