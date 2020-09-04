using NLog;
using System;
using System.ServiceProcess;
using System.Timers;

namespace ServiceDemo
{
    /// <summary>
    /// 服務執行者
    /// </summary>
    public partial class Agent : ServiceBase
    {
        /// <summary>
        /// NLog
        /// </summary>
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 執行計時器
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// 服務啟動時才會更換的執行ID
        /// </summary>
        private Guid id;

        /// <summary>
        /// 預設的執行時間 5秒
        /// </summary>
        private readonly int DefaulltIntervalInSec = 5* 1000;

        /// <summary>
        /// Demo 測試用程式
        /// </summary>
        private readonly DemoProgram _demo;

        /// <summary>
        /// 判斷是否執行中的旗標
        /// </summary>
        private bool _isRunning = false;

        /// <summary>
        /// 建構子
        /// </summary>
        public Agent()
        {
            
            InitializeComponent();
            _logger.Info($"[{id}]服務初始化");
            _demo = new DemoProgram();
        }
        /// <summary>
        /// 服務啟動
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            id = Guid.NewGuid();
            _logger.Info($"服務啟動 id=[{id}]");
            _logger.Info($"[{id}]執行第一次");
            _demo.Run(id);
            _logger.Info($"[{id}]執行第一次完成");
            _timer.Elapsed += TimerElapsed;
            _timer.Interval = DefaulltIntervalInSec;
            _logger.Info($"[{id}]計時器啟動,等待{DefaulltIntervalInSec/1000}");
            _timer.Start();
        }
        /// <summary>
        /// 服務停止
        /// </summary>
        protected override void OnStop()
        {
            _isRunning = false;
            _timer.Stop();
            _logger.Info($"[{id}]服務停止");
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Info($"[{id}]計時器時間到");
            try
            {
                if (_isRunning)
                {
                    _logger.Info($"[{id}] 偵測到程序正在執行，略過");
                    return;
                }
                _isRunning = true;
                _timer.Stop();
                _logger.Info($"[{id}] 暫停計時，新一輪更換新的id");
                id = Guid.NewGuid();
                _logger.Info($"新一輪的id = [{id}] ");
                _demo.Run(id);
                _logger.Info($"[{id}] 執行完成 重新起動計時器，等待{DefaulltIntervalInSec / 1000}秒");
                _isRunning = false;
                _timer.Start();
            }
            catch (Exception ex)
            {
                _logger.Error($"[{id}]  錯誤代碼 {ex}");
                _isRunning = false;
                _timer.Start();
            }
        }
    }
}
