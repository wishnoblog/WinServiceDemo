using System;
using NLog;
using System.Threading;

namespace ServiceDemo
{
    /// <summary>
    /// 模擬執行中的程式
    /// </summary>
    public sealed class DemoProgram
    {
        /// <summary>
        /// NLog
        /// </summary>
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 執行時間模擬
        /// </summary>
        private const int RunSec = 7;

        /// <summary>
        /// 模擬程式要執行x秒
        /// </summary>
        public void Run(Guid id)
        {
            _logger.Info($"[{id}]開始執行 停7秒");
            //停止7秒
            Thread.Sleep(RunSec * 1000);
            _logger.Info($"[{id}]執行完成 {RunSec}秒到了");
        }
    }
}
