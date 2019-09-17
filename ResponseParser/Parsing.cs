using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ResponseParser.Process;
using System.Threading;
using Timer = System.Timers.Timer;

namespace ResponseParser
{
    public partial class Parsing : ServiceBase
    {
        public Parsing()
        {
            InitializeComponent();
        }

        private Timer _timer;
        protected override void OnStart(string[] args)
        {
            _timer = new Timer();
            _timer.Interval = 60000;
            _timer.Elapsed += timerElapsed;
            _timer.AutoReset = false;
            _timer.Start();
            GC.KeepAlive(_timer);
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
        void timerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            CancellationTokenSource cts = new CancellationTokenSource();
            Task TaskParsingCMS999 = ParsingProcess.ParsingCMS999(cts.Token);
            Task TaskParsingCMS277CA = ParsingProcess.ParsingCMS277CA(cts.Token);
            Task TaskParsingCMSMAO2 = ParsingProcess.ParsingCMSMAO2(cts.Token);
            Task TaskParsingDHCS = ParsingProcess.ParsingDHCS(cts.Token);
            Task.WhenAll(TaskParsingCMS999, TaskParsingCMS277CA, TaskParsingCMSMAO2, TaskParsingDHCS);
            cts = null;
            _timer.Enabled = true;
        }
    }
}
