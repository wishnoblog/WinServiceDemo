using NLog;
using System.Threading;

namespace ServiceDemo
{
    /// <summary>
    /// 模擬執行中的程式
    /// </summary>
    public sealed class DemoProgram
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 執行時間模擬
        /// </summary>
        
        private const int RunSec = 7;
        /// <summary>
        /// 模擬程式要執行x秒
        /// </summary>
        public void Run()
        {
            _logger.Info("開始執行 停7秒");
            //停止7秒
            Thread.Sleep(RunSec * 1000);
            _logger.Info("執行完成 7秒到了");
        }
    }
}
